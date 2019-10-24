using IpcLibrary.Properties;

namespace IpcLibrary.Method
{

    public static class Service
    {

        /// <summary>アドレス取得</summary>
        public static string GetAddress()
        {
            return Resource.Address + "/" + Resource.EndPoint;
        }

        /// <summary>ベースアドレス取得</summary>
        public static string GetBaseAddress()
        {
            return Resource.Address;
        }

        /// <summary>エンドポイント取得</summary>
        public static string GetEndpoint()
        {
            return Resource.EndPoint;
        }

    }

}
