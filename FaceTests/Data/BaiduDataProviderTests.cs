using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;
using Face.Data;

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

            //构造矩形框的参数
            List<int> location = new List<int>();
            faceInfo.TryGetValue("left", out string left);
            faceInfo.TryGetValue("top", out string top);
            faceInfo.TryGetValue("width", out string width);
            faceInfo.TryGetValue("heigth", out string heigth);
            try { double a = double.Parse(left); int c = (int)a; }catch(Exception ex) { }
            int b = 0;


            location.Add(int.Parse(left));
            location.Add(int.Parse(top.Trim('"')));
            location.Add(int.Parse(width.Trim('"')));
            location.Add(int.Parse(heigth.Trim('"')));

        }
    }
}