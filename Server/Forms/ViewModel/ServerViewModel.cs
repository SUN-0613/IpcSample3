using AYam.Common.MVVM;

namespace Server.Forms.ViewModel
{

    /// <summary>IPC通信.ViewModel</summary>
    public class ServerViewModel : ViewModelBase
    {

        #region Model

        /// <summary>IPC通信.Model</summary>
        private Model.ServerModel _Model;

        #endregion

        #region Property

        /// <summary>現在値</summary>
        public int NowValue { get; set; }

        #endregion

        /// <summary>IPC通信.ViewModel</summary>
        public ServerViewModel()
        {

            _Model = new Model.ServerModel(UpdateProperty);

        }

        /// <summary>プロパティ更新</summary>
        /// <param name="value">現在値</param>
        private void UpdateProperty(int value)
        {
            NowValue = value;
            CallPropertyChanged(nameof(NowValue));
        }

    }

}
