using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Face.CameraCoulm
{
    class CameraProvider
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public int cameraIndex { get; set; }

        /// <summary>
        /// 获取摄像头设备列表
        /// </summary>
        /// <returns>摄像头名字列表</returns>
        public List<string> GetCameraEquipment()
        {
            List<string> cameraList = new List<string>();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cameraList.Add(device.Name);
            }
            return cameraList;
        }

        /// <summary>
        /// 获取实时图像
        /// </summary>
        /// <returns></returns>
        public VideoCaptureDevice CamRunning()
        {
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("未找到摄像头设备");
            }
            videoSource = new VideoCaptureDevice(videoDevices[cameraIndex].MonikerString);
            //设置下像素，这句话不写也可以正常运行：
            videoSource.VideoResolution = videoSource.VideoCapabilities[cameraIndex];
            return videoSource;
        }

        /// <summary>
        /// 获取拍照图像
        /// </summary>
        /// <returns></returns>
        //public Bitmap GetPicture()
        //{

        //}


    }
}
