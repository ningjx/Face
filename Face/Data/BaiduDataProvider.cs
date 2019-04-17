using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Face.Recognition;
using Newtonsoft.Json;
using Face.Data.MongoDB;
using Microsoft.International.Converters.PinYinConverter;

namespace Face.Data
{
    public class BaiduDataProvider
    {
        ///<summry>
        ///根据百度数据库查找人脸
        ///</summry>
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
                string id = infoArry[0]["user_id"].ToString();
                string group = infoArry[0]["group_id"].ToString();
                string info = infoArry[0]["user_info"].ToString();
                //info解析
                string[] faceInfo = info.Split('`');



                string score = infoArry[0]["score"].ToString();
                string result = $"姓名：{faceInfo[0]}\r\nID：{id}\r\n组名：{group}\r\n信息：{faceInfo[1] }\r\n匹配度：{score}\r\n人脸标识：{faceToken}\r\n";
                return result;
            }
            catch (Exception ex)
            {
                string result = "识别出错：" + ex.ToString();
                return result;
            }
        }

        /// <summary>
        /// 人脸识别
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
                result.Add("faceNum", value["face_num"].ToString());


                //人脸标识
                result.Add("faceToken", infoArry[0]["face_token"].ToString());
                //人脸位置
                JToken location = infoArry[0]["location"];
                result.Add("left", location["left"].ToString());
                result.Add("top", location["top"].ToString());
                result.Add("width", location["width"].ToString());
                result.Add("height", location["height"].ToString());
                //年龄
                result.Add("age", infoArry[0]["age"].ToString());
                //美丑
                result.Add("beauty", infoArry[0]["beauty"].ToString());
                //性别
                //JToken gender = infoArry[0]["gender"];
                result.Add("gender", infoArry[0]["gender"]["type"].ToString());
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
        /// <summary>
        /// 图片上圈人脸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="faceInfo"></param>
        /// <returns></returns>
        public Image DrawSquar(Image image)
        {
            Dictionary<string, string> faceInfo = NetRecognitionData(image);
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

        /// <summary>
        /// 网络保存人脸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public Tuple<bool, string> NetFaceRegisterData(Image image, string groupId, string userId, string info)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                JObject jsonData = baiduRecognitionProvider.NetFaceRegister(image, groupId, userId, info);
                jsonData.TryGetValue("error_code", out JToken errorCodeToken);
                jsonData.TryGetValue("error_msg", out JToken errorMessageToken);
                bool mark = true;
                string message = errorMessageToken.ToString();
                //int errorCode = int.Parse(errorCodeToken.ToString());
                string errorCode = errorCodeToken.ToString();
                switch (errorCode)
                {
                    case "223105":
                        mark = false;
                        break;
                    default:
                        mark = true;
                        break;
                }
                return new Tuple<bool, string>(mark, message);
            }
            catch (Exception ex) { return new Tuple<bool, string>(false, ex.ToString()); }
        }

        public Tuple<bool, string> NetFaceRegisterData(Image image, string userName, JObject info)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                string groupId = "UsualUser";
                string userId = ChineseToPinyin(userName);
                info.Add("UserName", userName);
                JObject jsonData = baiduRecognitionProvider.NetFaceRegister(image, groupId, userId, info.ToString());
                jsonData.TryGetValue("error_code", out JToken errorCodeToken);
                jsonData.TryGetValue("error_msg", out JToken errorMessageToken);
                bool mark = true;
                string message = errorMessageToken.ToString();
                //int errorCode = int.Parse(errorCodeToken.ToString());
                string errorCode = errorCodeToken.ToString();
                switch (errorCode)
                {
                    case "223105":
                        mark = false;
                        break;
                    default:
                        mark = true;
                        break;
                }
                return new Tuple<bool, string>(mark, message);
            }
            catch (Exception ex) { return new Tuple<bool, string>(false, ex.ToString()); }

        }

        public string NetRecognitionDataStr(Image image)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = baiduRecognitionProvider.NetRecognition(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["face_list"];

                string age = infoArry[0]["age"].ToString();
                string beauty = infoArry[0]["age"].ToString();
                string gender = infoArry[0]["gender"]["type"].ToString();
                string result = $"年龄：{age}\r\n颜值：{beauty}\r\n性别：{gender}\r\n";
                return result;
            }
            catch (Exception ex)
            {
                string result = "检测出错{" + ex + "}";
                return result;
            }
        }

        /// <summary>
        /// 两张人脸对比
        /// </summary>
        /// <param name="sourceImage">原来的脸</param>
        /// <param name="matchImage">需要对比的脸</param>
        /// <returns></returns>
        public Tuple<bool, bool, string> NetTowFaceMatchData(Image sourceImage, Image matchImage)
        {
            try
            {
                BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                JObject jsonData = baiduRecognitionProvider.NetTwoFaceMatch(sourceImage, matchImage);
                jsonData.TryGetValue("error_code", out JToken errorCodeToken);
                jsonData.TryGetValue("error_msg", out JToken errorMessageToken);
                jsonData.TryGetValue("result", out JToken value);
                float score = (float)value["score"];
                bool sucess = false;
                bool match = false;
                if (80 < score)
                    match = true;
                if (errorCodeToken.ToString() == "0")
                    sucess = true;

                return new Tuple<bool, bool, string>(match, sucess, errorMessageToken.ToString());
            }
            catch (Exception e)
            {
                return new Tuple<bool, bool, string>(false, false, e.ToString());
            }
        }

        private string ChineseToPinyin(string chinese)
        {
            string result = string.Empty;
            foreach (char item in chinese)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(item);
                    string t = chineseChar.Pinyins[0].ToString();
                    result += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    result += item.ToString();
                }
            }
            return result;
        }
    }
}
