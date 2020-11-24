using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using IronPython.Runtime.Operations;
using Face.Data.MongoDB;

namespace Face.Recognition
{
    public class RecognitionProvider
    {
        private readonly string APP_ID = "去百度申请";
        private readonly string API_KEY = "去百度申请";
        private readonly string SECRET_KEY = "去百度申请";

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="image">人脸图片</param>
        /// <returns></returns>
        public JObject NetRecognition(Image image)
        {

            try
            {
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
                {
                    Timeout = 60000  // 修改超时时间
                };

                //图片转为Base64
                Bitmap bmp = new Bitmap(image);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length]; ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length); ms.Close();
                var base64 = Convert.ToBase64String(arr);

                string imageType = "BASE64";
                //可选参数
                var options = new Dictionary<string, object>{
                                {"face_field", "age,beauty,gender"},
                                {"max_face_num", 1},
                                {"face_type", "LIVE"}
                };
                JObject result = client.Detect(base64, imageType, options);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接人脸识别API出错：" + ex);
                return new JObject();
            }
        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="image"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public JObject NetFaceRegister(Image image, string groupId, string userId, string info)
        {
            try
            {
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
                {
                    Timeout = 60000  // 修改超时时间
                };

                //图片转为Base64
                string base64 = ImageToBase64(image);
                string imageType = "BASE64";

                var options = new Dictionary<string, object>
                {
                        {"user_info", info},
                        {"quality_control", "NORMAL"},
                        {"liveness_control", "NONE"}
                };
                JObject result = client.UserAdd(base64, imageType, groupId, userId, options);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("人脸录入出错：" + ex);
                return new JObject();
            }

        }

        /// <summary>
        /// 人脸匹配
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public JObject NetFaceMatch(Image image)
        {
            try
            {
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
                {
                    Timeout = 60000  // 修改超时时间
                };

                //图片转为Base64
                string base64 = ImageToBase64(image);

                string imageType = "BASE64";
                List<string> groupList = new List<string>();

                groupList.Add("UsualUser");
                groupList.Add("StarUser");

                string group = string.Join(",", groupList.ToArray());
                JObject result = client.Search(base64, imageType, group);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接人脸识别API出错：" + ex);
                return new JObject();
            }
        }

        public JObject NetGetUserInfo(string userName)
        {
            return new JObject();
        }

        /// <summary>
        /// 匹配
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="matchImage"></param>
        /// <returns></returns>
        public JObject NetTwoFaceMatch(Image sourceImage, Image matchImage)
        {
            try
            {
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
                {
                    Timeout = 60000  // 修改超时时间
                };

                string sourceData = ImageToBase64(sourceImage);
                string matchData = ImageToBase64(matchImage);

                JArray faces = new JArray{
                    new JObject
                    {
                        {"image", sourceData},
                        {"image_type", "BASE64"},
                        {"face_type", "LIVE"},
                        {"quality_control", "LOW"},
                        {"liveness_control", "NONE"},
                    },
                    new JObject
                    {
                        {"image", matchData},
                        {"image_type", "BASE64"},
                        {"face_type", "LIVE"},
                        {"quality_control", "LOW"},
                        {"liveness_control", "NONE"},
                    }
                };

                JObject result = client.Match(faces);
                return result;
            }
            catch (Exception e)
            {
                return new JObject {
                    {"error_code",1 },
                    {"error_msg",e.ToString() }
                };
            }
        }

        /// <summary>
        /// 图片转码
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取数据库用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JObject GetInfo(string userId)
        {
            try
            {
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY)
                {
                    Timeout = 60000  // 修改超时时间
                };
                var groupId = "UsualUser";

                // 调用用户信息查询，可能会抛出网络等异常，请使用try/catch捕获
                var result = client.UserGet(userId, groupId);
                return result;
            }
            catch (Exception e)
            {
                return new JObject() { };
            }  
        }
    }
}
