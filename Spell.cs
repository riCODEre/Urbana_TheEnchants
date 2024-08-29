using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Urbana_TheEnchants
{
    class Spell
    {
        public string direction;
        public int SpellLeft;
        public int SpellTop;

        private int speed = 20;
        private PictureBox spell = new PictureBox();
        private System.Windows.Forms.Timer SpellTimer = new System.Windows.Forms.Timer();


        public void MakeSpell(Form form)
        {

            spell.BackColor = Color.DarkBlue;
            spell.Size = new Size(10, 10);
            spell.Tag = "spell";
            spell.Left = SpellLeft;
            spell.Top = SpellTop;
            spell.BringToFront();

            form.Controls.Add(spell);


            SpellTimer.Interval = speed;
            SpellTimer.Tick += new EventHandler(SpellTimerEvent);
            SpellTimer.Start();

        }

        private void SpellTimerEvent(object sender, EventArgs e)
        {

            if (direction == "left")
            {
                spell.Left -= speed;
            }

            if (direction == "right")
            {
                spell.Left += speed;
            }

            if (direction == "up")
            {
                spell.Top -= speed;
            }

            if (direction == "down")
            {
                spell.Top += speed;
            }


            if (spell.Left < 10 || spell.Left > 860 || spell.Top < 10 || spell.Top > 600)
            {
                SpellTimer.Stop();
                SpellTimer.Dispose();
                spell.Dispose();
                SpellTimer = null;
                spell = null;
            }



        }
    }
}
