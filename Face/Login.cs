using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using Face.CameraCoulm;
using Face.Data;
using static Face.MainForm;

namespace Face
{
    public partial class Login : Skin_Mac
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CameraProvider cameraProvider = new CameraProvider();
            List<string> camList = cameraProvider.GetCameraEquipment();
            cameraProvider.cameraIndex = 0;
            videoSourcePlayer1.VideoSource = cameraProvider.CamRunning();//调用摄像头实时画面
            videoSourcePlayer1.Start();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Password pa = new Password();
            pa.Show();
            this.Close();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            label3.Text = "正在验证";
            Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = bitmap;
            this.videoSourcePlayer1.Visible = false;
            this.pictureBox1.Visible = true;
            videoSourcePlayer1.Stop();
            FaceDataProvider baiduDataProvider = new FaceDataProvider();
            bool text = baiduDataProvider.Login(pictureBox1.Image);
            if (text)
            {
                label3.Text = "";
                MainForm mainForm = new MainForm();
                PublicValue.Locker = true;
                mainForm.Show();
                this.Hide();
            }
            else
            {
                label3.Text = "";
                MessageBox.Show("登陆失败");
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
        }

        private void VideoSourcePlayer1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Close();
            mainForm.Show();
        }
    }
}
