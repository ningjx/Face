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
using Face.Data;

namespace Face
{
    public partial class File : Skin_Mac
    {
        OpenFileDialog dialog;
        bool ifFiel = false;
        public File()
        {
            InitializeComponent();
            skinButton1.Enabled = ifFiel;
            skinButton2.Enabled = ifFiel;
            skinButton3.Enabled = ifFiel;
        }

        private void File_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinButton3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Image = pictureBox1.Image;
            register.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.Hide();
                string path = System.IO.Path.GetFullPath(dialog.FileName);
                pictureBox1.Image = Image.FromFile(path);
                ifFiel = true;
                skinButton1.Enabled = ifFiel;
                skinButton2.Enabled = ifFiel;
                skinButton3.Enabled = ifFiel;
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
        private void SkinTextBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinButton1_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(dialog.FileName);
            Image image = pictureBox1.Image;
            label2.Text = "正在检测";
            Task<Tuple<Image, string>> taskK = new Task<Tuple<Image, string>>(() =>
            {
                FaceDataProvider baiduDataProvider = new FaceDataProvider();
                Tuple<Image, string> data = new Tuple<Image, string>(baiduDataProvider.DrawSquar(image, path), baiduDataProvider.NetRecognitionDataStr(image)) { };

                return data;
            });
            taskK.Start();
            taskK.Wait();
            pictureBox1.Image = taskK.Result.Item1;
            skinTextBox1.Text = taskK.Result.Item2;
            label2.Text = "";
        }

        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinButton2_Click(object sender, EventArgs e)
        {
            Image image = pictureBox1.Image;
            label2.Text = "正在识别";
            FaceDataProvider baiduDataProvider = new FaceDataProvider();
            skinTextBox1.Text = baiduDataProvider.NetFaceMatchData(image);
            pictureBox1.Image = baiduDataProvider.DrawSquar(image);
            label2.Text = "";
        }

        private void SkinButton4_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
    }
}
