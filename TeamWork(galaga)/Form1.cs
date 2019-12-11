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
        Double shootSpeed = 10;
        Double alienSpeed = 40;
        int round = 1;
        int shotAlien = 0;
        int score = 0;
        int shot = -1;
        int playerSpeed = 8;
        bool moveRight, moveLeft;
        bool left = false;
        bool right = true;
        bool shooting = false;
        int j = 0;
        
        //Create Lists
        List<PictureBox> aliens = new List<PictureBox>();
        List<PictureBox> shootyBois = new List<PictureBox>();
        List<PictureBox> badBois = new List<PictureBox>();
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

            for (int i = 1; i < 12; i++)
            {
                badBois.Add((PictureBox)Controls.Find("badPew" + i, true)[0]);
            }

            

            for (int i = 0; i < 15; i++)
            {
                shootyBois.Add((PictureBox)Controls.Find("picLazer" + i, true)[0]);
            }

            

            //move projectiles out of the way
            for (int i = 0; i < shootyBois.Count; i++)
            {
                shootyBois[i].Left = 20000;
            }
            for (int i = 1; i < badBois.Count; i++)
            {
                badBois[i].Left = 30000;
            }
        }

        //Timer to allow for ship movement
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //move if keydown = true
            if (moveLeft)
            {
                player.Left -= playerSpeed;
            }
            if (moveRight)
            {
                player.Left += playerSpeed;
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
            alienShoot();
            alienMovement(j);
            j++;
            
        }


        private void ShootTime_Tick(object sender, EventArgs e)
        {

            alienHit();
            playerHit();

            if (shooting)
            {

                shootyBois[0].Top -= 10;
                shootyBois[1].Top -= 10;
                shootyBois[2].Top -= 10;
                shootyBois[3].Top -= 10;
                shootyBois[4].Top -= 10;
                shootyBois[5].Top -= 10;
                shootyBois[6].Top -= 10;
                shootyBois[7].Top -= 10;
                shootyBois[8].Top -= 10;
                shootyBois[9].Top -= 10;
                shootyBois[10].Top -= 10;
                shootyBois[11].Top -= 10;
                shootyBois[12].Top -= 10;
                shootyBois[13].Top -= 10;
                shootyBois[14].Top -= 10;
            }

            badBois[0].Top += (int)shootSpeed;
            badBois[1].Top += (int)shootSpeed;
            badBois[2].Top += (int)shootSpeed;
            badBois[3].Top += (int)shootSpeed;
            badBois[4].Top += (int)shootSpeed;
            badBois[5].Top += (int)shootSpeed;
            badBois[6].Top += (int)shootSpeed;
            badBois[7].Top += (int)shootSpeed;
            badBois[8].Top += (int)shootSpeed;
            badBois[9].Top += (int)shootSpeed;
            badBois[10].Top +=(int)shootSpeed;
            
            

        }

        
        //SHOOT
        private void shoot()
        {
            

            shot++;

            shootyBois[shot].Left = player.Left + (player.Width / 2);

            shootyBois[shot].Top = player.Top;

            shootyBois[shot].Visible = true;

            if (shot >= 14)
            {
                shot = 0;
            }

            shooting = true;

            
        }

        //SHOOT
        private void alienShoot()
        {
            int randAlien = 0;
            if (aliens.Count >= 1)
            {
                Random rand = new Random();

                randAlien = rand.Next(0, aliens.Count);


                shotAlien++;


                badBois[shotAlien].Left = aliens[randAlien].Left + (aliens[randAlien].Width / 2);

                badBois[shotAlien].Top = aliens[randAlien].Top;

                badBois[shotAlien].Visible = true;


                if (shotAlien >= 10)
                {
                    shotAlien = 0;
                }

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
                            round++;
                            lbRound.Text = "Round: " + round;
                            MessageBox.Show(lbRound.Text);
                            restart();
                            
                        }
                    }
            }
        }

        //Alien hit detection
        private void playerHit()
        {
            
            bool hit = false;
            
            for (int i = 0; i < badBois.Count; i++)
            {

                if (badBois[i].Bounds.IntersectsWith(player.Bounds))
                {
                    hit = true;
                    badBois[i].SetBounds(0, 0, 0, 0);
                    
                }
                
            }
            if (hit)
            {
                player.Visible = false;
                playerExplosion.Left = player.Left;
                playerExplosion.Top = player.Top;
                playerExplosion.Visible = true;
                
                if (picLife3.Visible)
                {
                    picLife3.Visible = false;
                    player.Visible = true;
                }
                else if (picLife2.Visible)
                {
                    picLife2.Visible = false;
                    player.Visible = true;
                }
                else if (picLife1.Visible)
                {
                    picLife1.Visible = false;
                    player.Visible = true;

                }
                else
                {
                    MessageBox.Show("You Lose!");
                    Application.Exit();
                }
                
                score -= 100;
                lbScore.Text = "Score:" + score;
                hit = false;
           
            }
            
            playerExplosion.Visible = false;
            player.Visible = true;

        }

        private void MoveDown_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Top = aliens[i].Top + (int)alienSpeed;

                if(aliens[i].Top > ClientRectangle.Bottom)
                {
                    aliens.Remove(aliens[i]);
                    playerHit();
                    score -= 400;
                }
            }
        }

        private void restart()
        {
            for (int i = 1; i < 21; i++)
            {
                aliens.Add((PictureBox)Controls.Find("alien" + i, true)[0]);
            }
            alienSpeed = alienSpeed * 1.25;
            shootSpeed = shootSpeed * 1.25;

            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Visible = true;
            }

            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                {
                    aliens[i].Top = 50;
                    
                }

                else{
                    aliens[i].Top = 100;
                }
            }
            for(int i = 0; i < 20; i++)
            {
                aliens[i].Left = ((i - 1) * 50) + 500;
            }
        }
    }
}
