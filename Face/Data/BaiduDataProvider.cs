using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Face.Recognition;
using Newtonsoft.Json;

namespace Face.Data
{
    public class BaiduDataProvider
    {
        public string NetFaceMatchData(Image image)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                JObject jsonData = baiduRecognitionProvider.NetFaceMatch(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["user_list"];

                string faceToken = value["face_token"].ToString();
                string name = infoArry[0]["user_id"].ToString();
                string group = infoArry[0]["group_id"].ToString();
                string info = infoArry[0]["user_info"].ToString();
                string score = infoArry[0]["score"].ToString();
                string result = "姓名：" + name + "\r\n" + "组名：" + group + "\r\n" + "信息：" + info + "\r\n" + "匹配度：" + score + "\r\n"+"人脸标识："+faceToken+"\r\n";
                return result;
            }
            catch(Exception ex)
            {
                string result = "识别出错："+ex.ToString();
                return result;
            }
        }
    }
}
