using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamWork_galaga_
{
    public partial class Form1 : Form
    {
        //Create variables
        int score = 0;
        int shot = -1;
        int speed = 8;
        bool moveRight, moveLeft;
        bool left = false;
        bool right = true;
        bool shooting = false;
        int j = 0;

        PictureBox PictureBox1 = new PictureBox();

        
        
        //Create Lists
        List<PictureBox> aliens = new List<PictureBox>();
        List<PictureBox> shootyBois = new List<PictureBox>(); 
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }


        //Adding aliens and lazer shots into the form
        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            for (int i = 1; i < 21; i++)
            {
                aliens.Add((PictureBox)Controls.Find("alien" + i, true) [0] );
            }
            //aliens.Add(alien3);
            //aliens.Add(alien7);
            //aliens.Add(alien1);
            //aliens.Add(alien9);
            //aliens.Add(alien5);
            //aliens.Add(alien4);
            //aliens.Add(alien2);
            //aliens.Add(alien8);
            //aliens.Add(alien6);
            //aliens.Add(alien10);
            shootyBois.Add(picLazer);
            shootyBois.Add(picLazer1);
            shootyBois.Add(picLazer2);
            shootyBois.Add(picLazer3);
            shootyBois.Add(picLazer4);
            shootyBois.Add(picLazer5);
            shootyBois.Add(picLazer6);
            shootyBois.Add(picLazer7);
            shootyBois.Add(picLazer8);
            shootyBois.Add(picLazer9);
            shootyBois.Add(picLazer10);
            shootyBois.Add(picLazer11);
            shootyBois.Add(picLazer12);
            shootyBois.Add(picLazer13);
            shootyBois.Add(picLazer14);

            //move projectiles out of the way
            for (int i = 0; i < shootyBois.Count; i++)
            {
                shootyBois[i].Left = 20000;
            }
        }

        //Timer to allow for ship movement
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //move if keydown = true
            if (moveLeft)
            {
                player.Left -= speed;
            }
            if (moveRight)
            {
                player.Left += speed;
            }
        
            // detect boundaries
            if (player.Left < 0)
            {
                player.Left = 0;
                
            }
            if (player.Top < 0)
            {
                player.Top = 0;
                
            }
            if (player.Bottom > this.ClientRectangle.Bottom)
            {
                player.Top = this.ClientRectangle.Bottom - player.Height;
                
            }
            if (player.Right > this.ClientRectangle.Right)
            {
                player.Left = this.ClientRectangle.Right - player.Width;
                
            }

            
            
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //detect keydown
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                moveLeft = true;

            }

            //detect keydown
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }

            //detect keydown
            if (e.KeyCode  == Keys.Space)
            {
                
                shoot();
                //createshooty();

            }

            //detect keydown
            if (e.KeyCode == Keys.Escape)
            {

                Application.Exit();

            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //stop movement 
            if (e.KeyCode == Keys.A)
            {
                moveLeft = false;

            //Stop movement
            }
            if (e.KeyCode == Keys.D)
            {
                moveRight = false;
            }
          
        }

        private void alienMovement(int j)
        {
            //Cycle between left and right movement for aliens
            if (left)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int i = 0; i < aliens.Count; i++)
                    {
                        aliens[i].Left = aliens[i].Left - 5;
                    }
                    if(x > 6)
                    {
                        right = true;
                        left = false;
                    }
                }
                    
            }

            else if (right)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int i = 0; i < aliens.Count; i++)
                    {
                        aliens[i].Left = aliens[i].Left + 5;
                    }
                if (x > 6)
                    {
                        left = true;
                        right = false;
                    }
                }
            }
        }

        //Call alien movement
        private void Timer2_Tick(object sender, EventArgs e)
        {
            
            alienMovement(j);
            j++;
        }


        private void ShootTime_Tick(object sender, EventArgs e)
        {
            alienHit();


            if (shooting)
            {

                shootyBois[0].Top = shootyBois[0].Top - 10;
                shootyBois[1].Top = shootyBois[1].Top - 10;
                shootyBois[2].Top = shootyBois[2].Top - 10;
                shootyBois[3].Top = shootyBois[3].Top - 10;
                shootyBois[4].Top = shootyBois[4].Top - 10;
                shootyBois[5].Top = shootyBois[5].Top - 10;
                shootyBois[6].Top = shootyBois[6].Top - 10;
                shootyBois[7].Top = shootyBois[7].Top - 10;
                shootyBois[8].Top = shootyBois[8].Top - 10;
                shootyBois[9].Top = shootyBois[9].Top - 10;
                shootyBois[10].Top = shootyBois[10].Top - 10;
                shootyBois[11].Top = shootyBois[11].Top - 10;
                shootyBois[12].Top = shootyBois[12].Top - 10;
                shootyBois[13].Top = shootyBois[13].Top - 10;
                shootyBois[14].Top = shootyBois[14].Top - 10;
            }

        }

        
        //SHOOT
        private void shoot()
        {
            shot++;

            shootyBois[shot].Left = player.Left + (player.Width / 2);

            shootyBois[shot].Top = player.Top;

            shootyBois[shot].Visible = true;

            shooting = true;


            if (shot >= 14)
            {
                shot = 0;
            }

            
        }

        //Alien hit detection
        private void alienHit()
        {
            for (int i = 0; i < shootyBois.Count; i++)
            {
                for (int j = 0; j < aliens.Count; j++)
                    if (shootyBois[i].Bounds.IntersectsWith(aliens[j].Bounds))
                    {
                        aliens[j].Visible = false;
                        aliens.Remove(aliens[j]);
                        score += 200;
                        lbScore.Text = "Score:" + score;
                        if(aliens.Count == 0)
                        {
                            MessageBox.Show("You Win!");
                        }
                    }
            }
        }

        private void MoveDown_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Top = aliens[i].Top + 40;
            }
        }

     

        private void alienShooting()
        {

        }

        //private void createshooty()
        //{
        //    PictureBox PictureBox1 = new PictureBox();

        //    // Set the location and size of the PictureBox control.
        //    //PictureBox1.Location = new System.Drawing.Point(70, 120);
        //    PictureBox1.Left = player.Left + (player.Width / 2);
        //    PictureBox1.Top = player.Top;
        //    PictureBox1.Size = new System.Drawing.Size(5, 30);
        //    PictureBox1.TabStop = false;

        //    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        //    // Set the border style to a three-dimensional border.
        //    PictureBox1.BorderStyle = BorderStyle.None;
        //    PictureBox1.Image = Image.FromFile("C:\\Users\\lukel\\Desktop\\TeamWork(galaga)\\TeamWork(galaga)\\Resources\\gern.png");

        //    // Add the PictureBox to the form.
        //    Controls.Add(PictureBox1);

        //    Moving(PictureBox1);

        //}

        //private void Moving(PictureBox pic)
        //{
        //    for(int i = 0; i < 20; i++)
        //    {
        //        pic.Top -= 10;
        //    }
        //}
    }

    //public class lazer
    //{
    //    public lazer(String name, bool alien)
    //    {

    //    }
    //}
}
