using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FaceTest;
using Xunit;
using ConsoleApp1.ziyuan;
using Face.Data;

namespace FaceTest
{
    public class BaiduDataProviderTest
    {
        [Fact]
        public void NetFaceMatchDataTest()
        {
            Image image = Resource1.qq_pic_merged_1552621462658;
            BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
            var text = baiduDataProvider.NetFaceMatchData(image);
        }

    }
}
