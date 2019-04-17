using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTests;
using Newtonsoft.Json.Linq;
using FaceTests.Resources;

namespace Face.Data.Tests
{
    [TestClass()]
    public class LocalDataProviderTests
    {
        [TestMethod()]
        public void NetFaceRegisterDataTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            LocalDataProvider localDataProvider = new LocalDataProvider();
            Dictionary<string, string> info = new Dictionary<string, string> { { "性别", "女" }, { "年龄", "19" } };
            var a = localDataProvider.LocalFaceRegisterData(image, "测试姓名2", info);
        }
    }
}