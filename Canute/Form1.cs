using System;
using System.Windows.Forms;
using CardsLib;

namespace Canute
{
    public partial class Form1 : Form
    {
        private readonly PictureBox[] pbs;
        private readonly int?[] pid;
        private int score;
        private int scoreTicks;
        private const int deltaSpeed = 20;

        public Form1()
        {
            InitializeComponent();
            pbs = new PictureBox[] { pbFirst, pbSecond, pbThird, pbFourth, pbFifth, pbSixth, pbSeventh, pbEnd };
            pid = new int?[pbs.Length];
            this.KeyPreview = true;
            dealTimer.Tick += scoreTimer_Tick;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            score = scoreTicks = 0;
            lblScore.Text = "Score: " + score;
            MessageBox.Show("Press the key on the card. If the card says 10, press 't'. 'r' is restart.");
            btnStart.Visible = false;
            dealCycle();
            dealTimer.Start();
        }

        private void dealCycle()
        {
            // Shifts the card images and the corresponding id
            for (int i = pbs.Length; i > 1; i--)
            {
                pbs[i - 1].Image = pbs[i - 2].Image;
                pid[i - 1] = pid[i - 2];
            }
            // Checks if the image reached the last one
            if (pbs[pbs.Length - 1].Image != null)
            {
                dealTimer.Stop();
                MessageBox.Show("Too sloooowwwwww......");
                return;
            }
            var crd = Dealer.Deal(1)[0];
            pbs[0].Image = numberList.Images[crd.intCardValue];
            pid[0] = crd.intCardValue;
        }

        private void dealTimer_Tick(object sender, EventArgs e)
        {
            dealCycle();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int imgId = -1;
            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                case Keys.D1:
                case Keys.A:
                    imgId = 0;
                    break;
                case Keys.NumPad2:
                case Keys.D2:
                    imgId = 1;
                    break;
                case Keys.NumPad3:
                case Keys.D3:
                    imgId = 2;
                    break;
                case Keys.NumPad4:
                case Keys.D4:
                    imgId = 3;
                    break;
                case Keys.NumPad5:
                case Keys.D5:
                    imgId = 4;
                    break;
                case Keys.NumPad6:
                case Keys.D6:
                    imgId = 5;
                    break;
                case Keys.NumPad7:
                case Keys.D7:
                    imgId = 6;
                    break;
                case Keys.NumPad8:
                case Keys.D8:
                    imgId = 7;
                    break;
                case Keys.NumPad9:
                case Keys.D9:
                    imgId = 8;
                    break;
                case Keys.T:
                case Keys.Space:
                    imgId = 9;
                    break;
                case Keys.J:
                    imgId = 10;
                    break;
                case Keys.Q:
                    imgId = 11;
                    break;
                case Keys.K:
                    imgId = 12;
                    break;
                case Keys.R:
                    dealTimer.Stop();
                    MessageBox.Show("Restart... (you hit 'R')");
                    ResetState();
                    return;
                default:
                    break;
            }
            score -= 10; // Assume the user gets it wrong
            if (imgId == -1) return;

            // Finds the last picturebox that has a valid image
            int counter = pbs.Length - 1;
        checkLatestImg:
            if (counter < 0) return; // That means all the pictureboxes are empty
            if (pbs[counter].Image == null)
            {
                counter--;           // That means the picturebox does
                goto checkLatestImg; // not have an image. Search again!
            }

            if (pid[counter] == imgId)
            {
                score += 20; // Previous -10 and 10 pts as correct response
                pbs[counter].Image = null;
            }
        }

        private void scoreTimer_Tick(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            if (++scoreTicks % 3 == 0)
            {
                scoreTicks = 0;
                if (dealTimer.Interval > deltaSpeed)
                    dealTimer.Interval -= deltaSpeed;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetState();
        }

        private void ResetState()
        {
            dealTimer.Stop();
            for (int i = 0; i < pbs.Length; i++)
            {
                pbs[i].Image = null;
                pid[i] = null;
            }
            btnStart.Visible = true;
        }
    }
}
