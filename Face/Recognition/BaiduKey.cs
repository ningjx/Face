using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baidu;
namespace Face.Recognition
{
    class BaiduKey
    {
        public object GetConnectToBaidu()
        {
            string APP_ID = "去百度申请";
            string API_KEY = "去百度申请";
            string SECRET_KEY = "去百度申请";

            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
            {
                Timeout = 60000  // 修改超时时间
            };
            return client;
        }

    }
}
