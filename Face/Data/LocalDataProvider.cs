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
using FaceRecognitionDotNet;
using Image = System.Drawing.Image;

namespace Face.Data
{
    public class LocalDataProvider
    {
        /// <summary>
        /// 本地保存人脸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="userName"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public Tuple<bool, string> LocalFaceRegisterData(Image image, string userName)
        {
            try
            {
                FaceDataProvider baiduDataProvider = new FaceDataProvider();
                Dictionary<string, string> data = baiduDataProvider.NetRecognitionData(image);
                data.TryGetValue("faceToken", out string faceToken);

                //Bitmap bmp = new Bitmap(image);
                //image.Dispose();
                //string name = baiduDataProvider.ChineseToPinyin(userName).ToLower();
                //string fielName = @"D:\FaceProject\Face\Face\Pictures\" + name + ".jpg";
                //bmp.Save("fielName");

                DBData dBData = new DBData();
                dBData.SetFace(image, faceToken, userName);
                bool mark = true;
                string message = "保存完成";
                return new Tuple<bool, string>(mark, message);
            }
            catch (Exception ex) { return new Tuple<bool, string>(false, ex.ToString()); }
        }

        /// <summary>
        /// 本地通过名字获取图片
        /// </summary>
        /// <param name="name">人名</param>
        /// <returns></returns>
        public Image LocalFaceGetData(string name)
        {
            try
            {
                DBData dBData = new DBData();
                BsonDocument data = dBData.GetFace(name).First();
                data.TryGetValue("Image", out BsonValue base64);
                bool sucess = true;

                byte[] bytes = Convert.FromBase64String(base64.ToString());
                MemoryStream memStream = new MemoryStream(bytes);
                Image image = Image.FromStream(memStream);
                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void GetFaceEncoding()
        {
            var faceRecognition = FaceRecognition.Create(@"D:\FaceProject\Face\Face\Recognition\FaceMoudel");
            var img = FaceRecognition.LoadImageFile("");
            faceRecognition.FaceEncodings(img);
        }
    }
}
