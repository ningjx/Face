using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Face.Data.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace Face.Data.MongoDB
{
    public class DBData
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="image"></param>
        /// <param name="faceToken"></param>
        /// <param name="userName"></param>
        /// <param name="info"></param>
        public void SetFace(Image image, string faceToken, string userName)
        {
            DBHelper dBHelper = new DBHelper();
            //图片转码
            string base64 = ImageToBase64(image);

            Dictionary<string, object> data = new Dictionary<string, object> { { "Image", base64 }, { "FaceToken", faceToken }, { "UserName", userName }};
            BsonDocument bsonElements = new BsonDocument();
            bsonElements.AddRange(data);
            dBHelper.SetData(bsonElements);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IFindFluent<BsonDocument, BsonDocument> GetFace(string userName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("UserName", userName);
            DBHelper dBHelper = new DBHelper();
            var data = dBHelper.GetData(filter);
            return data;
        }



        private string ImageToBase64(Image image)
        {
            //图片转为Base64
            Bitmap bmp = new Bitmap(image);
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] arr = new byte[ms.Length]; ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length); ms.Close();
            string base64 = Convert.ToBase64String(arr);
            return base64;
        }
    }
}
