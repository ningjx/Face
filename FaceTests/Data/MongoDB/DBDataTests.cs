using Microsoft.VisualStudio.TestTools.UnitTesting;
using Face.Data.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Face.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Face.Data.MongoDB.Tests
{
    [TestClass()]
    public class DBDataTests
    {
        [TestMethod()]
        public void GetFaceTest()
        {
            string userName = "测试姓名";
            DBData dBData = new DBData();
            var a =dBData.GetFace(userName);
        }
    }
}