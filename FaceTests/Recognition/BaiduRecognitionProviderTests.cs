using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;
using Newtonsoft.Json.Linq;
using FaceTests.Resources;

namespace Face.Recognition.Tests
{
    [TestClass()]
    public class BaiduRecognitionProviderTests
    {
        [TestMethod()]
        public void NetFaceRegisterTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            string id = "gongjiaxin";
            string name = "龚家新";
            string group = "UsualUser";
            string info = "老婆大人";
            string faceInfo = name + "`" + info;
            Task<JObject> task = new Task<JObject>(
                        () =>
                        {
                            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                            return baiduRecognitionProvider.NetFaceRegister(image, group, id, faceInfo);
                        });
            task.Start();
            task.Wait();
            var aaa = task.Result;
        }

        [TestMethod()]
        public void NetRecognitionTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
            var data = baiduRecognitionProvider.NetRecognition(image);
        }

        [TestMethod()]
        public void NetTwoFaceMatchTest()
        {
            Image image1 = Resource1.qq_pic_merged_1552621462658;
            Image image2 = Resource1.Screenshot_2018_10_25_14_00_18_951_com_tencent_mo;
            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
            var data = baiduRecognitionProvider.NetTwoFaceMatch(image1, image2);
        }
    }
}