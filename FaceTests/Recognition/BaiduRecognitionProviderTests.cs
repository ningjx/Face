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

namespace Face.Recognition.Tests
{
    [TestClass()]
    public class BaiduRecognitionProviderTests
    {
        [TestMethod()]
        public void NetFaceRegisterTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            string id = "id";
            string name = "xx";
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
    }
}