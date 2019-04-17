using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Face.Data;
using Face.Data.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Face.Data
{
    public class LocalDataProvider
    {
        /// <summary>
        /// 本地保存人脸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="info"></param>
        /// <param name="local"></param>
        /// <returns></returns>
        public Tuple<bool, string> LocalFaceRegisterData(Image image, string userName, JObject info)
        {
            try
            {
                BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
                Dictionary<string, string> data = baiduDataProvider.NetRecognitionData(image);
                data.TryGetValue("faceToken", out string faceToken);

                DBData dBData = new DBData();
                dBData.SetFace(image, faceToken, userName, info);
                bool mark = true;
                string message = "保存完成";
                return new Tuple<bool, string>(mark, message);
            }
            catch (Exception ex) { return new Tuple<bool, string>(false, ex.ToString()); }
        }

        /// <summary>
        /// 本地通过名字获取数据
        /// </summary>
        /// <param name="name">人名</param>
        /// <returns></returns>
        public Tuple<bool, Image, string, JObject> LocalFaceGetData(string name)
        {
            try
            {
                DBData dBData = new DBData();
                BsonDocument data = dBData.GetFace(name).First();
                data.TryGetValue("Image", out BsonValue base64);
                data.TryGetValue("FaceToken", out BsonValue faceToken);
                data.TryGetValue("UserName", out BsonValue userName);
                data.TryGetValue("Info", out BsonValue infoBV);
                bool sucess = true;

                byte[] bytes = Convert.FromBase64String(base64.ToString());
                MemoryStream memStream = new MemoryStream(bytes);
                Image image = Image.FromStream(memStream);
                JObject info = JsonConvert.DeserializeObject<JObject>(infoBV.ToJson());
                return new Tuple<bool, Image, string, JObject>(sucess, image, faceToken.ToString(), info);
            }
            catch (Exception e)
            {
                return new Tuple<bool, Image, string, JObject>(false, null, null, new JObject { "error_msg", e.ToString() });
            }
        }
    }
}
