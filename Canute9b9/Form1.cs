using System;
using System.Drawing;
using System.Windows.Forms;

namespace Canute9b9
{
    public partial class Form1 : Form
    {
        private Random rand = new Random();
        private readonly PictureBox[] linearPbs;
        private int lastModPb = 0;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            linearPbs = new PictureBox[] {
                pb02, pb12, pb22, // Based on number key locations
                pb01, pb11, pb21,
                pb00, pb10, pb20
            };
            resetTiles();
            lblScore.Text = "Score: " + score;
            timer.Interval = 1000;
        }

        private void resetTiles()
        {
            foreach (var item in linearPbs)
            {
                item.Visible = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!linearPbs[lastModPb].Visible)
            {
                linearPbs[lastModPb = rand.Next(0, linearPbs.Length)].Visible = true;
                lblScore.Text = "Score: " + score;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("You were too slooowwwww...");
                resetTiles();
                btnStart.Visible = true;
                timer.Interval = 1000;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            score = 0;
            lblScore.Text = "Score: " + score;
            timer.Start();
            btnStart.Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int k = -1;
            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                case Keys.Z:
                    k = 0;
                    break;
                case Keys.NumPad2:
                case Keys.X:
                    k = 1;
                    break;
                case Keys.NumPad3:
                case Keys.C:
                    k = 2;
                    break;
                case Keys.NumPad4:
                case Keys.A:
                    k = 3;
                    break;
                case Keys.NumPad5:
                case Keys.S:
                    k = 4;
                    break;
                case Keys.NumPad6:
                case Keys.D:
                    k = 5;
                    break;
                case Keys.NumPad7:
                case Keys.Q:
                    k = 6;
                    break;
                case Keys.NumPad8:
                case Keys.W:
                    k = 7;
                    break;
                case Keys.NumPad9:
                case Keys.E:
                    k = 8;
                    break;
            }
            if (k > -1 && k < linearPbs.Length)
            {
                validateTile(linearPbs[k]);
            }
            else score--;
        }

        private void validateTile(PictureBox pb)
        {
            if (pb.Visible)
            {
                pb.Visible = false;
                if (timer.Interval > 520)
                    timer.Interval -= 10;
                score++;
            }
            else score--;
        }

        private void pb00_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb00);
        }

        private void pb10_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb10);
        }

        private void pb20_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb20);
        }

        private void pb01_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb01);
        }

        private void pb11_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb11);
        }

        private void pb21_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb21);
        }

        private void pb02_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb02);
        }

        private void pb12_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb12);
        }

        private void pb22_MouseClick(object sender, MouseEventArgs e)
        {
            validateTile(pb22);
        }
    }
}
