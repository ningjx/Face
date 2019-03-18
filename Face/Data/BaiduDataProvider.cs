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
        ///<summry>人脸识别</summry>
        public string NetFaceMatchData(Image image)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = baiduRecognitionProvider.NetFaceMatch(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["user_list"];

                string faceToken = value["face_token"].ToString();
                string name = infoArry[0]["user_id"].ToString();
                string group = infoArry[0]["group_id"].ToString();
                string info = infoArry[0]["user_info"].ToString();
                string score = infoArry[0]["score"].ToString();
                string result = "姓名：" + name + "\r\n" + "组名：" + group + "\r\n" + "信息：" + info + "\r\n" + "匹配度：" + score + "\r\n" + "人脸标识：" + faceToken + "\r\n";
                return result;
            }
            catch (Exception ex)
            {
                string result = "识别出错：" + ex.ToString();
                return result;
            }
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Dictionary<string, string> NetRecognitionData(Image image)
        {
            try
            {

                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = baiduRecognitionProvider.NetRecognition(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["face_list"];

                Dictionary<string, string> result = new Dictionary<string, string>();
                //人脸数量，现在只用1
                result.Add("faceNum",value["face_num"].ToString());
                

                //人脸标识
                result.Add("faceToken", infoArry[0]["face_token"].ToString());
                //人脸位置
                JToken location = infoArry[0]["location"];
                result.Add("left", location["left"].ToString());
                result.Add("top", location["top"].ToString());
                result.Add("width", location["width"].ToString());
                result.Add("height", location["height"].ToString());
                //年龄
                //result.Add("age", infoArry[0]["age"].ToString());
                //美丑
                //result.Add("beauty", infoArry[0]["beauty"].ToString());
                //性别
                //result.Add("gender", infoArry[0]["beauty"]["gender"].ToString());
                return result;
            }
            catch (Exception ex)
            {
                string info = "检测出错{" + ex + "}";
                Dictionary<string, string> result = new Dictionary<string, string>();
                result.Add("错误消息：", info);
                return result;
            }
        }

        public Image DrawSquar(Image image, Dictionary<string, string> faceInfo)
        {
            //构造矩形框的参数
            List<int> location = new List<int>();
            faceInfo.TryGetValue("left", out string left);
            faceInfo.TryGetValue("top", out string top);
            faceInfo.TryGetValue("width", out string width);
            faceInfo.TryGetValue("height", out string height);

            location.Add((int)double.Parse(left));
            location.Add((int)double.Parse(top));
            location.Add((int)double.Parse(width));
            location.Add((int)double.Parse(height));

            Color color = Color.Green;//线框颜色
            int lineWidth = 3;        //线框宽度

            Graphics graphics = Graphics.FromImage(image);
            Brush brush = new SolidBrush(color);

            Pen pen = new Pen(brush, lineWidth);

            Rectangle rectangle = new Rectangle();
            rectangle.X = location[0];
            rectangle.Y = location[1];
            rectangle.Width = location[2];
            rectangle.Height = location[3];

            graphics.DrawRectangle(pen, rectangle);
            graphics.Dispose();
            return image;
        }
    }
}
