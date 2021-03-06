﻿using Newtonsoft.Json.Linq;
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
using System.Threading;
using FaceRecognitionDotNet;
using Image = System.Drawing.Image;

namespace Face.Data
{
    public class FaceDataProvider
    {

        public string GetData(string userName)
        {
            RecognitionProvider recognitionProvider = new RecognitionProvider();
            string userId = ChineseToPinyin(userName).ToLower();
            JObject jsonData = recognitionProvider.GetInfo(userId);
            if (jsonData == null)
            {
                return "查询失败";
            }
            jsonData.TryGetValue("result", out JToken value);
            JToken infoArry = value["user_list"];
            string info = infoArry[0]["user_info"].ToString();
            JObject faceInfo = (JObject)JsonConvert.DeserializeObject(info);
            string name = faceInfo["UserName"].ToString();
            string text = faceInfo["value"].ToString();
            return $"姓名:{name}\r\n信息:{text}";
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public bool Login(Image image)
        {
            RecognitionProvider recognitionProvider = new RecognitionProvider();
            bool loc = false;
            //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
            JObject jsonData = recognitionProvider.NetFaceMatch(image);
            jsonData.TryGetValue("result", out JToken value);
            JToken infoArry = value["user_list"];
            string info = infoArry[0]["user_info"].ToString();
            //info解析
            JObject faceInfo = (JObject)JsonConvert.DeserializeObject(info);
            string name = faceInfo["UserName"].ToString();
            string score = infoArry[0]["score"].ToString().Substring(0, 5);
            double ss = double.Parse(score);
            if (name == "王宁" && ss >= 70)
            {
                loc = true;
            }
            return loc;
        }

        /// <summary>
        /// 根据数据库Match人脸
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public string NetFaceMatchData(Image image)
        {
            try
            {
                RecognitionProvider recognitionProvider = new RecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = recognitionProvider.NetFaceMatch(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["user_list"];
                string faceToken = value["face_token"].ToString();
                string id = infoArry[0]["user_id"].ToString();
                string group = infoArry[0]["group_id"].ToString();
                string info = infoArry[0]["user_info"].ToString();
                //info解析
                JObject faceInfo = (JObject)JsonConvert.DeserializeObject(info);
                //string[] faceInfo = info.Split('`');

                string name = faceInfo["UserName"].ToString();
                string text = faceInfo["value"].ToString();
                string score = infoArry[0]["score"].ToString();
                int so = (int)double.Parse(score);
                string result = $"姓名：{name}\r\nID：{id}\r\n信息：{text}\r\n匹配度：{so}\r\n人脸标识：{faceToken}\r\n";
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

                RecognitionProvider recognitionProvider = new RecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = recognitionProvider.NetRecognition(image);
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
            Thread.Sleep(1000);
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

        public Image DrawSquar(Image image, string path)
        {
            Dictionary<string, string> faceInfo = NetRecognitionData(image);
            var faceRecognition = FaceRecognition.Create(@"D:\FaceProject\Face\Face\Recognition\FaceMoudel");
            var img = FaceRecognition.LoadImageFile(path);
            var faceLandmarks = faceRecognition.FaceLandmark(img, null, PredictorModel.Large).ToArray();
            List<Tuple<float, float>> points = new List<Tuple<float, float>>();
            foreach (var facePoint in faceLandmarks[0].Values)
            {
                var data = facePoint.ToList().First();
                points.Add(new Tuple<float, float>(data.X, data.Y));
            }

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

            Color color = Color.Purple;//线框颜色
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
            Font font = new Font("sans-serif", 20, FontStyle.Bold);

            //画点
            int i = 0;
            foreach (var point in points)
            {
                graphics.DrawString(i.ToString(), font, brush, point.Item1+15, point.Item2-15);
                i++;
            }
            //graphics.DrawString();
            graphics.Dispose();
            return image;
        }

        /// <summary>
        /// 保存人脸
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
                RecognitionProvider recognitionProvider = new RecognitionProvider();
                JObject jsonData = recognitionProvider.NetFaceRegister(image, groupId, userId, info);
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
                RecognitionProvider recognitionProvider = new RecognitionProvider();
                string groupId = "UsualUser";
                string userId = ChineseToPinyin(userName).ToLower();
                info.Add("UserName", userName);
                JObject jsonData = recognitionProvider.NetFaceRegister(image, groupId, userId, info.ToString());
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
                RecognitionProvider recognitionProvider = new RecognitionProvider();

                //Task<JObject> @object = new Task<JObject>(baiduRecognitionProvider.NetFaceMatch(image) );
                JObject jsonData = recognitionProvider.NetRecognition(image);
                jsonData.TryGetValue("result", out JToken value);
                JToken infoArry = value["face_list"];

                string left = infoArry[0]["location"]["left"].ToString();
                string top = infoArry[0]["location"]["top"].ToString();
                string width = infoArry[0]["location"]["width"].ToString();
                string height = infoArry[0]["location"]["height"].ToString();

                string yaw = infoArry[0]["angle"]["yaw"].ToString();
                string pitch = infoArry[0]["angle"]["pitch"].ToString();
                string roll = infoArry[0]["angle"]["roll"].ToString();

                string result = $"人脸位置：\r\n    距离左边线:{left}\r\n    距离上边线:{top}\r\n    宽度:{width}\r\n    高度:{height}\r\n人脸翻转:\r\n    俯仰角:{yaw}\r\n    平移角:{pitch}\r\n    翻滚角:{roll}";
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
                RecognitionProvider recognitionProvider = new RecognitionProvider();
                JObject jsonData = recognitionProvider.NetTwoFaceMatch(sourceImage, matchImage);
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

        public string ChineseToPinyin(string chinese)
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
