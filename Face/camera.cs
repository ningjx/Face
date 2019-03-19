﻿using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Face.CameraCoulm;
using Face.Recognition;
using Newtonsoft.Json.Linq;
using Face.Data;
namespace Face
{
    public partial class Camera : Form
    {
        CameraProvider cameraProvider = new CameraProvider();
        BaiduDataProvider baiduDataProvider;
        public Camera()
        {
            InitializeComponent();
            List<string> camList = cameraProvider.GetCameraEquipment();
            foreach (string name in camList)
            {
                Cameralist.Items.Add(name);
            }
            Cameralist.Text = camList[0];

        }

        /// <summary>
        /// 拍照图片显示
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 拍照按钮
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "拍照")
            {
                Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
                pictureBox1.Image = bitmap;
                this.videoSourcePlayer1.Visible = false;
                this.pictureBox1.Visible = true;
                videoSourcePlayer1.Stop();
                button1.Text = "重新拍照";
            }
            else
            {
                videoSourcePlayer1.Start();
                pictureBox1.Image = null;
                this.videoSourcePlayer1.Visible = true;
                this.pictureBox1.Visible = false;
                button1.Text = "拍照";
            }
        }

        /// <summary>
        /// 选择设备，打开画面
        /// </summary>
        private void Cameralist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cameraProvider.cameraIndex = Cameralist.SelectedIndex;
            videoSourcePlayer1.VideoSource = cameraProvider.CamRunning();//调用摄像头实时画面
            videoSourcePlayer1.Start();
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 进行人脸识别匹配
        /// </summary>
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                textBox1.Text = "先拍一张照片啊";
            }
            else
            {
                //需要异步
                Task.Run(() => textBox1.Text = baiduDataProvider.NetFaceMatchData(pictureBox1.Image));
                Task<Dictionary<string, string>> task = new Task<Dictionary<string, string>>(() => baiduDataProvider.NetRecognitionData(pictureBox1.Image));
                pictureBox1.Image = baiduDataProvider.DrawSquar(pictureBox1.Image, task.Result);

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 返回到主菜单，关闭摄像头
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                textBox1.Text = "先拍一张照片啊";
            }
            else
            {
                Register register = new Register();
                register.Image = pictureBox1.Image;
                register.Show();

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Task<Dictionary<string, string>> task = new Task<Dictionary<string, string>>(() => baiduDataProvider.NetRecognitionData(pictureBox1.Image));
            pictureBox1.Image = baiduDataProvider.DrawSquar(pictureBox1.Image, task.Result);

        }

        private void Camera_Load(object sender, EventArgs e)
        {
            baiduDataProvider = new BaiduDataProvider();
        }
    }
}
