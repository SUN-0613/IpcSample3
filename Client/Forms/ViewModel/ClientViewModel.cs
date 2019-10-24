using AYam.Common.MVVM;
using Client.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Client.Forms.ViewModel
{

    /// <summary>IPC通信.ViewModel</summary>
    public class ClientViewModel : ViewModelBase
    {

        #region Model

        /// <summary>IPC通信.Model</summary>
        private Model.ClientModel _Model;

        #endregion

        #region Property

        /// <summary>実行コマンド</summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }

        /// <summary>計測結果</summary>
        public ObservableCollection<TimerInfo> Results { get; set; }

        /// <summary>結果総数</summary>
        public int ResultsCount { get; set; }

        /// <summary>結果総時間</summary>
        public int ResultTime { get; set; }

        #endregion

        /// <summary>計測結果</summary>
        private List<TimerInfo> _Results;

        /// <summary>IPC通信.ViewModel</summary>
        public ClientViewModel()
        {

            _Model = new Model.ClientModel(UpdateProperties, UpdateResult);

            Results = new ObservableCollection<TimerInfo>();
            _Results = new List<TimerInfo>();

            ExecuteCommand = new DelegateCommand<string>(
                (parameter) => 
                {

                    switch (parameter)
                    {

                        case "start":

                            if (!_Model.IsExecuting)
                            {

                                Results.Clear();
                                CallPropertyChanged(nameof(Results));

                                ResultsCount = 0;
                                CallPropertyChanged(nameof(ResultsCount));

                                ResultTime = 0;
                                CallPropertyChanged(nameof(ResultTime));

                                _Model.IsExecuting = true;
                                _Model.Execute();
                            }

                            break;

                        case "stop":

                            if (_Model.IsExecuting)
                            {
                                _Model.IsExecuting = false;
                            }

                            break;

                    }

                }, 
                () => true);

        }

        /// <summary>プロパティ更新</summary>
        /// <param name="allTime">全体処理時間</param>
        /// <param name="serverTime">サーバ処理時間</param>
        private void UpdateProperties(int allTime, int serverTime)
        {

            _Results.Add(new TimerInfo() { AllTime = allTime, ServerTime = serverTime });

            ResultsCount++;
            CallPropertyChanged(nameof(ResultsCount));

            ResultTime += allTime;
            CallPropertyChanged(nameof(ResultTime));

        }

        /// <summary>結果表示</summary>
        private void UpdateResult()
        {

            _Results.ForEach((result) => 
            {

                Results.Add(new TimerInfo()
                {
                    AllTime = result.AllTime,
                    ServerTime = result.ServerTime
                });

            });

        }

    }

}
