using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baidu;
namespace Face.Recognition
{
    class Key
    {
        public object GetConnectToBaidu()
        {
            string APP_ID = "15757485";
            string API_KEY = "DbVT6z1gdKUk0NhyBZWIBd99";
            string SECRET_KEY = "6EA1pNWcxlj3qUmy8uZ2DhQ1jO8OdC0G";

            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
            {
                Timeout = 60000  // 修改超时时间
            };
            return client;
        }

    }
}
