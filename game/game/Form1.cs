using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;
        public Form1()
        {
            InitializeComponent();
            GameReset();
        }
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }
            if (trex.Top > 366 && jumping == false)
            {
                force = 12;
                trex.Top = 367;
                jumpSpeed = 0;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }
                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtScore.Visible = false;
                        label1.Visible = true;
                        isGameOver = true;
                    }
                }

            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "sky")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 0);
                        //score++;
                    }
                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        txtScore.Visible = false;
                        label1.Visible = true;
                        isGameOver = true;
                    }
                }

            }
            if (score > 5)
            {
                obstacleSpeed = 15;
            }
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Space && isGameOver == true)
            {
                GameReset();
            }
        }
        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            trex.Image = Properties.Resources.running;
            isGameOver = false;
            trex.Top = 367;
            txtScore.Visible = true;
            label1.Visible = false;
            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);
                    x.Left = position;
                }
            }
            gameTimer.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
   
