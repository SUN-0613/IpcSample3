using IpcLibrary.Interface;
using IpcLibrary.Method;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Server.Forms.Model
{

    /// <summary>IPC通信.Model</summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServerModel : IExecute, IDisposable
    {

        #region Delegate

        /// <summary>プロパティ更新デリゲート</summary>
        /// <param name="serverTime">表示データ</param>
        public delegate void UpdateProperty(int value);

        #endregion

        /// <summary>プロパティ更新</summary>
        private UpdateProperty _UpdateProperty;

        /// <summary>IPC通信サーバ</summary>
        private ServiceHost _Host;

        /// <summary>IPC通信.Model</summary>
        public ServerModel(UpdateProperty updateProperty)
        {

            _UpdateProperty = updateProperty;

            // サーバ設定
            _Host = new ServiceHost(this, new System.Uri(Service.GetBaseAddress()));
            _Host.AddServiceEndpoint(typeof(IExecute), new NetNamedPipeBinding(), Service.GetEndpoint());
            _Host.Open();

        }

        /// <summary>解放処理</summary>
        public void Dispose()
        {

            _Host.Close();
            _UpdateProperty = null;

        }

        /// <summary>読み込み実行</summary>
        /// <returns>サーバ処理時間</returns>
        async Task<int> IExecute.Execute()
        {

            int value = 0;
            var random = new Random(DateTime.Now.Second * 1000 + DateTime.Now.Millisecond);
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await Task.Run(() => 
            {

                for (var iLoop = 0; iLoop < 10000; iLoop++)
                {

                    value = random.Next();

                    if (iLoop % 100 == 0)
                    {
                        _UpdateProperty((int)stopWatch.ElapsedMilliseconds);
                    }

                }

            });

            stopWatch.Stop();
            value = (int)stopWatch.ElapsedMilliseconds;

            _UpdateProperty(value);

            return value;

        }

    }

}
