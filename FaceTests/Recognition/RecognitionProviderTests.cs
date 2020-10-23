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
            Image image = null;
            string id = "id";
            string name = "姓名";
            string group = "UsualUser";
            string info = "信息";
            string faceInfo = name + "`" + info;
            Task<JObject> task = new Task<JObject>(
                        () =>
                        {
                            RecognitionProvider baiduRecognitionProvider = new RecognitionProvider();
                            return baiduRecognitionProvider.NetFaceRegister(image, group, id, faceInfo);
                        });
            task.Start();
            task.Wait();
            var aaa = task.Result;
        }

        [TestMethod()]
        public void NetRecognitionTest()
        {
            Image image = null;
            RecognitionProvider baiduRecognitionProvider = new RecognitionProvider();
            var data = baiduRecognitionProvider.NetRecognition(image);
        }

        [TestMethod()]
        public void NetTwoFaceMatchTest()
        {
            Image image1 = null;
            Image image2 = null;
            RecognitionProvider baiduRecognitionProvider = new RecognitionProvider();
            var data = baiduRecognitionProvider.NetTwoFaceMatch(image1, image2);
        }
    }
}