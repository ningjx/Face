using Microsoft.VisualStudio.TestTools.UnitTesting;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceRecognitionDotNet;
using DlibDotNet;
using System.IO;

namespace FaceRecognitionDotNet.Tests
{
    [TestClass()]
    public class FaceRecognitionTests
    {
        string moudelPath = @"D:\FaceProject\Face\Face\Recognition\FaceMoudel";
        private FaceRecognition _FaceRecognition;

        [TestMethod()]
        public void FaceLandmarkTest()
        {
            try
            {
                var faceRecognition = FaceRecognition.Create(moudelPath);
                string path = @"C:\Users\vipni\Desktop\Screenshot_2019-06-09-15-59-59-842_com.tencent.mo.png";
                var img = FaceRecognition.LoadImageFile(path);
                var faceLandmarks = faceRecognition.FaceLandmark(img, null, PredictorModel.Small).ToArray();

                var parts = new[]
                {
                    FacePart.NoseTip,
                    FacePart.LeftEye,
                    FacePart.RightEye
                };

                foreach (var facePart in faceLandmarks[0].Values)
                {

                }

                var points = new[]
                {
                    new Point(496, 295)
            };

                var facePartPoints = faceLandmarks[0][FacePart.NoseTip].ToArray();
                for (var index = 0; index < facePartPoints.Length; index++)
                    Assert.IsTrue(facePartPoints[index] == points[index]);
            }
            //var img = FaceRecognition.LoadImageFile(Path.Combine("TestImages", "wangning.png"));
            catch (Exception e)
            {

            }

        }
    }
}