using System;
using System.Media;

namespace Urbana_TheEnchants
{
    public partial class Urbana : Form
    {

        bool goUp,goLeft,goRight,goDown, GameOver;
        string facing = "right";
        int playerHealth = 100;
        int playerSpeed = 10;
        int playerMana = 100;
        int playerPowerup = 0;
        int enemySpeed = 2;
        int score = 0;
        int enemycount = 3;
        int level1 = 50;
        int level2 = 100;
        int level3 = 150;
        int level4 = 200;
        string curlevel = "Level 0";
        string gamecond = "pause";

        Random random = new Random();
        List<PictureBox> enemyList = new List<PictureBox>();



        public Urbana()
        {
            InitializeComponent();
            
            RestartGame();
            SoundPlayer bgsound = new SoundPlayer();
            bgsound.SoundLocation = "bg_music.wav";
            bgsound.PlayLooping();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            // for no error when health go negative
            if (playerHealth > 1)
            {
                healthbar.Value = playerHealth;
            }
            else
            {
                //die
                enemySpeed = 2;
                GameOver = true;
                gamecond = "stop";
                GameTimer.Stop();
                player.Image = Properties.Resources.ubana1;
                player.Width = 64;
                player.Height = 42;
                player.BackColor = Color.Transparent;
                player.SizeMode = PictureBoxSizeMode.StretchImage;
                MessageBox.Show("Congratulations for reaching " + curlevel
                    + ". Click Enter key to try again.\n \n" +
                    "Sources: \n" +
                    "Music: https://www.youtube.com/watch?v=7_cwKd81z7Q \n" +
                    "Code Template: https://www.youtube.com/watch?v=TxmhaSTRav4 \n" +
                    "Character: https://markvelyx.itch.io/random-npcs \n" +
                    "Potion: https://www.pinterest.ph/pin/527624912593956883/ \n" +
                    "Grass Floor: https://www.slynyrd.com/blog/2019/8/27/pixelblog-20-top-down-tiles \n \n" +
                    "All others (codes, and artistic elements are owned by Eric C. Delos Reyes)", 
                    "Thank you for playing 'Urbana, The Enchants'", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            // setting value
            ScoreAmt.Text = "Score: " + score;
            manabar.Value = playerMana;
            powerupbar.Value = playerPowerup;

            //position change
            if (goLeft == true && player.Left > 7)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + player.Width + 5 < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }
            if (goUp == true && player.Top > 120)
            {
                player.Top -= playerSpeed;
            }
            if (goDown == true && player.Top + player.Height + 10 < this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }

            //difficulty level
            if (score == level1)
            {
                enemySpeed = 3;
                curlevel = "Level 1";
            }
            else if (score == level2)
            {
                enemySpeed = 4;
                curlevel = "Level 2";
            }
            else if (score == level3)
            {
                enemySpeed = 5;
                curlevel = "Level 3";
            }
            else if (score == level4)
            {
                enemySpeed = 6;
                curlevel = "Level 4";
            }

            //mana potion removal
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "mana")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        playerMana = 100;

                    }
                }

                //enemy to player damage
                if (x is PictureBox && (string)x.Tag == "enemy")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }

                    //enemy follow player
                    if (x.Left > player.Left)
                    {
                        x.Left -= enemySpeed;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += enemySpeed;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= enemySpeed;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += enemySpeed;
                    }

                }


                //enemy kill and removal
                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "spell" && x is PictureBox && 
                        (string)x.Tag == "enemy")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;
                            if (playerPowerup == 100) //health powerup
                            {
                                if (playerHealth <= 50)
                                {
                                    playerHealth += 50;
                                }
                                else 
                                {
                                    playerHealth = 100;
                                }
                                playerPowerup = 0;
                            }
                            else 
                            {
                                playerPowerup+= 4;
                            }

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            enemyList.Remove(((PictureBox)x));
                            MakeEnemy();
                        }
                    }
                }


            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gamecond == "stop")
            {
                goDown = false;
                goUp = false;
                goRight = false;
                goLeft = false;
            }
            else
            {
                if (e.KeyCode == Keys.A)
                {
                    goLeft = true;
                    facing = "left";
                    player.Image = Properties.Resources.ubana1_1_left;
                }

                if (e.KeyCode == Keys.D)
                {
                    goRight = true;
                    facing = "right";
                    player.Image = Properties.Resources.ubana1_1;
                }

                if (e.KeyCode == Keys.W)
                {
                    goUp = true;
                    facing = "up";
                    player.Image = Properties.Resources.ubana1_1;
                }

                if (e.KeyCode == Keys.S)
                {
                    goDown = true;
                    facing = "down";
                    player.Image = Properties.Resources.ubana1_1;
                }
            }
            
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.W)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                goDown = false;

            }

            if (e.KeyCode == Keys.Space && playerMana > 0 && GameOver == false)
            {
                playerMana -= 10;
                ShootSpell(facing);


                if (playerMana < 1)
                {
                    DropMana();
                }
            }
            if (e.KeyCode == Keys.Enter && GameOver == true)
            {
                RestartGame();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DropMana()
        {

            PictureBox spell = new PictureBox();
            spell.Image = Properties.Resources.mana1;
            spell.Width = 26;
            spell.Height = 42;
            spell.BackColor = Color.Transparent;
            spell.SizeMode = PictureBoxSizeMode.StretchImage;
            spell.Left = random.Next(10, this.ClientSize.Width - spell.Width);
            spell.Top = random.Next(120, this.ClientSize.Height - spell.Height);
            spell.Tag = "mana";
            this.Controls.Add(spell);

            spell.BringToFront();
            player.BringToFront();



        }

        private void ShootSpell(string direction)
        {
            Spell ShootSpell = new Spell();
            ShootSpell.direction = direction;
            ShootSpell.SpellLeft = player.Left + (player.Width / 2);
            ShootSpell.SpellTop = player.Top + (player.Height / 2);
            ShootSpell.MakeSpell(this);
        }

        private void MakeEnemy()
        {
            PictureBox enemy = new PictureBox();
            enemy.Tag = "enemy";
            enemy.Image = Properties.Resources.Enemy1;
            enemy.Left = random.Next(0, 900);
            enemy.Top = random.Next(0, 800);
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.Height = 84;
            enemy.Width = 52;
            enemy.BackColor = Color.Transparent;
            enemyList.Add(enemy);
            this.Controls.Add(enemy);
            player.BringToFront();
        }


        private void RestartGame()
        {
            player.Image = Properties.Resources.ubana1_1;
            player.Width = 42;
            player.Height = 64;
            player.BackColor = Color.Transparent;
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "mana")
                {
                    this.Controls.Remove(x);
                    ((PictureBox)x).Dispose();
                }
            }
                foreach (PictureBox i in enemyList)
            {
                this.Controls.Remove(i);
            }

            enemyList.Clear();


            for (int i = 0; i < enemycount; i++)
            {
                    MakeEnemy();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            GameOver = false;
            gamecond = "play";
            curlevel = "Level 0";
            playerHealth = 100;
            score = 0;
            playerMana = 100;
            enemycount = 3;
            playerPowerup = 0;
            enemySpeed = 2;

            GameTimer.Start();
        }
    }
}