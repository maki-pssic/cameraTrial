using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;

namespace cameraTrial
{
    public partial class frmCameraTrial: Form
    {
        bool _streaming;
        Capture _capture;
        public frmCameraTrial()
        {
            InitializeComponent();
        }

        private void frmCameraTrial_Load(object sender, EventArgs e)
        {
            _streaming = false;
            _capture = new Capture();
            Application.Idle += vid;
        }

        private void vid(object sender, EventArgs e)
        {
            var img = _capture.QueryFrame().ToImage<Bgr, byte>();

            
            img = img.Flip(flipType:Emgu.CV.CvEnum.FlipType.Horizontal); 


            var bmp = img.Bitmap;
            pictureBox1.Image = bmp;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            var pic = pictureBox1.Image;
            if (pic != null)
            {
                pictureBox2.Image = new Bitmap(pic);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title =@"Saving your Photo";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox2.Image != null)
                {
                    try
                    {
                        pictureBox2.Image.Save(saveFileDialog.FileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No image to save.");
                }

                MessageBox.Show(@"Picture saved Successfully!");
                
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }
    }
}
