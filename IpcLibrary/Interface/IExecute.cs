using System.ServiceModel;
using System.Threading.Tasks;

namespace IpcLibrary.Interface
{

    /// <summary>プロセス間通信インターフェース</summary>
    [ServiceContract]
    public interface IExecute
    {

        /// <summary>読み込み実行</summary>
        /// <returns>サーバ処理時間</returns>
        [OperationContract]
        Task<int> Execute();

    }

}
