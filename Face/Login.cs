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
            Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = bitmap;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Password pa = new Password();
            pa.Show();
            this.Close();
        }
    }
}
