using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;

namespace Face.Data.Tests
{
    [TestClass()]
    public class BaiduDataProviderTests
    {
        [TestMethod()]
        public void NetRecognitionDataTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
            Dictionary<string, string> faceInfo = baiduDataProvider.NetRecognitionData(image);
        }

        [TestMethod()]
        public void NetFaceMatchDataTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
            string a = baiduDataProvider.NetFaceMatchData(image);
        }
    }
}