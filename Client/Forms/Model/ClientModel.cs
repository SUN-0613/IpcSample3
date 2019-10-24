using IpcLibrary.Interface;
using IpcLibrary.Method;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Client.Forms.Model
{

    /// <summary>IPC通信.Model</summary>
    public class ClientModel
    {

        #region Delegate

        /// <summary>ViewModelプロパティ更新デリゲート</summary>
        /// <param name="allTime">全体処理時間</param>
        /// <param name="serverTime">サーバ処理時間</param>
        public delegate void UpdatePropertyDelegate(int allTime, int serverTime);

        /// <summary>結果表示デリゲート</summary>
        public delegate void UpdateResultsDelegate();

        #endregion

        /// <summary>実行処理FLG</summary>
        public bool IsExecuting = false;

        /// <summary>ViewModelプロパティ更新メソッド</summary>
        private readonly UpdatePropertyDelegate _Update;

        /// <summary>結果表示</summary>
        private readonly UpdateResultsDelegate _UpdateResults;

        /// <summary>サーバ処理</summary>
        private IExecute _Server;

        /// <summary>IPC通信.Model</summary>
        /// <param name="update">ViewModelプロパティ更新</param>
        /// <param name="updateResults">結果表示</param>
        public ClientModel(UpdatePropertyDelegate update, UpdateResultsDelegate updateResults)
        {

            _Update = update;
            _UpdateResults = updateResults;

            _Server = new ChannelFactory<IExecute>(
                new NetNamedPipeBinding(),
                new EndpointAddress(Service.GetAddress())
                ).CreateChannel();

        }

        /// <summary>サーバ処理実行</summary>
        public async void Execute()
        {

            await Task.Run(async () => 
            {

                var stopWatch = new Stopwatch();
                int value;

                while (IsExecuting)
                {

                    stopWatch.Restart();

                    value = await _Server.Execute();

                    _Update((int)stopWatch.ElapsedMilliseconds, value);

                }

            });

            _UpdateResults();

        }

    }

}
