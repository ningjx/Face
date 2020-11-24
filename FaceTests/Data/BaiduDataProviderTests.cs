using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;
using FaceTests.Resources;

namespace Face.Data.Tests
{
    [TestClass()]
    public class BaiduDataProviderTests
    {
        [TestMethod()]
        public void NetRecognitionDataTest()
        {
            Image image = null;
            FaceDataProvider baiduDataProvider = new FaceDataProvider();
            Dictionary<string, string> faceInfo = baiduDataProvider.NetRecognitionData(image);
        }
        //+		[1]	{[faceToken, 7d694c3b5db9360050081b77bd015dcd]}	System.Collections.Generic.KeyValuePair<string, string>

        [TestMethod()]
        public void NetFaceMatchDataTest()
        {
            Image image = null;
            FaceDataProvider baiduDataProvider = new FaceDataProvider();
            string a = baiduDataProvider.NetFaceMatchData(image);
        }

    }
}