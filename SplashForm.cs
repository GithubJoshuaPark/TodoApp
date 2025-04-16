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
using TodoApp.Properties;

namespace TodoApp
{
    public partial class SplashForm : Form
    {
        private Timer fadeTimer;
        private Timer closeTimer;

        public SplashForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.BackColor = Color.White;
            this.Size = new Size(500, 500);
            this.Opacity = 0.0; // Start fully transparent

            // Load image
            string imagePath = Path.Combine(Application.StartupPath, "todo_thumbnail.png");
            if (File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Dock = DockStyle.Fill;
            }
            //pictureBox1.Image = Properties.Resources.todo_thumbnail; // Fallback to embedded resource if file not found
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox1.Dock = DockStyle.Fill;

            // Configure loading label
            lblLoading.Text = "Loading...";
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            lblLoading.Dock = DockStyle.Bottom;
            lblLoading.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // Fade-in timer
            fadeTimer = new Timer();
            fadeTimer.Interval = 50; // every 50 ms
            fadeTimer.Tick += FadeInEffect;

            // Close after 3 seconds
            closeTimer = new Timer();
            closeTimer.Interval = 3000; // 3 seconds
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                this.Close();
            };

            this.Load += (s, e) =>
            {
                fadeTimer.Start();
                closeTimer.Start();
            };
        }

        private void FadeInEffect(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
            else
                fadeTimer.Stop();
        }
    }
}
