using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using DlibDotNet;

namespace Face
{
    public partial class FormTest : Skin_Mac
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

        }

        private void skinTextBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = System.IO.Path.GetFullPath(dialog.FileName);
                var detector = Dlib.GetFrontalFaceDetector();
                var img = Dlib.LoadImage<byte>(path);
                Dlib.PyramidUp(img);
                var dets = detector.Operator(img);


                byte[] bytes = Convert.FromBase64String(img.ToString());
                MemoryStream memStream = new MemoryStream(bytes);
                Image image = Image.FromStream(memStream);
                skinPictureBox1.Image = image;
            }
        }
    }
}
