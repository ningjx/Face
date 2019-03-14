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

namespace Face.Recognition
{
    class BaiduRecognitionProvider
    {
        private readonly string APP_ID = "15757485";
        private readonly string API_KEY = "DbVT6z1gdKUk0NhyBZWIBd99";
        private readonly string SECRET_KEY = "6EA1pNWcxlj3qUmy8uZ2DhQ1jO8OdC0G";

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
                var base64= Convert.ToBase64String(arr);

                string imageType = "BASE64";
                // 可选参数
                //var options = new Dictionary<string, object>{
                //                {"face_field", "age"},
                //                {"max_face_num", 1},
                //                {"face_type", "LIVE"}
                // };
                JObject result = client.Detect(base64, imageType);
                return result;
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接人脸识别API出错："+ex);
                return new JObject();
            }
        }

        public JObject NetFaceRegister(Image image,string groupId,string userId)
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
                JObject result = client.UserAdd(base64, imageType, groupId, userId);
                return result;
            }
            catch(Exception ex)
            {
                MessageBox.Show("人脸录入出错："+ex);
                return new JObject();
            }
            
        }

        public JObject NetFaceMatch(Image image)
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
                List<string> groupList = new List<string>();

                groupList.Add("UsualUser");
                groupList.Add("StarUser");

                string group = string.Join(",", groupList.ToArray());
                JObject result = client.Search(base64, imageType,group);
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接人脸识别API出错：" + ex);
                return new JObject();
            }
        }

    }
}
