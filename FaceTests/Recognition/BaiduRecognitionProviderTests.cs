using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;
using Newtonsoft.Json;

namespace Face.Recognition.Tests
{
    [TestClass()]
    public class BaiduRecognitionProviderTests
    {
        [TestMethod()]
        public void NetFaceRegisterTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
            string id = "gongjiaxin";
            string name = "龚家新";
            string group = "UsualUser";
            string info = "老婆大人";
            string faceInfo = name + "`" + info;
            var aaa = baiduRecognitionProvider.NetFaceRegister(image, group, id, faceInfo);
        }
    }
}