using AForge.Video.DirectShow;
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
using CCWin;

namespace Face
{
    public partial class Camera : Skin_Mac
    {
        CameraProvider cameraProvider = new CameraProvider();
        
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
                Image image = pictureBox1.Image;
                label2.Text = "正识别呢";
                //需要异步
                Task<Tuple<Image,string>> task = new Task<Tuple<Image, string>>
                (() =>
                {
                    BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
                    string text = baiduDataProvider.NetFaceMatchData(image);
                    Image imageDeal = baiduDataProvider.DrawSquar(image, baiduDataProvider.NetRecognitionData(image));
                    Tuple<Image, string> tuple = new Tuple<Image, string>(imageDeal, text);
                    return tuple;
                });
                task.Start();
                task.Wait();
                pictureBox1.Image = task.Result.Item1;
                textBox1.Text = task.Result.Item2;
                label2.Text = "";

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
            Image image = pictureBox1.Image;
            label2.Text = "检测中";
            Task<Image> task= new Task<Image>(() =>
            {
                BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
                return baiduDataProvider.DrawSquar(image, baiduDataProvider.NetRecognitionData(image));
            });
            task.Start();
            task.Wait();
            pictureBox1.Image = task.Result;
            textBox1.Text = "";//以后加

            label2.Text = "";
        }

        private void Camera_Load(object sender, EventArgs e)
        {
            Task.Run(()=> {
                BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
                baiduDataProvider.NetRecognitionData(Resource1.Image1);
            });            
        }
    }
}
