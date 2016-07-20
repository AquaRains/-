using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 스도쿠연습.game
{
    public partial class 게임화면 : Form
    {
        private const int txtboxtopmargin = 12;
        private const int txtboxleftmargin = 12;
        Button[] gamebuttons;
        private static 게임화면 thisform = null;
        public static 게임화면 GetInstance
        {
            get
            {
                {
                    if (thisform == null || thisform.IsDisposed)
                    {
                        thisform = new 게임화면();

                        return thisform;
                    }
                    else
                    {
                        
                        return thisform;
                    }

                }
            }
        }
        private 게임화면()
        {

            InitializeComponent();
        }

        private void makegamebuttons()
        {
            for(int i = 0; i < 81; i++)
            {
                gamebuttons[i] = new Button();
                gamebuttons[i].Location = new Point(txtboxleftmargin + i % 9 * 38,txtboxtopmargin + i / 9 % 9 * 27);
                gamebuttons[i].Name = "gamebuttons[" + i + "]";
                gamebuttons[i].Size = new Size(32, 21);
                gamebuttons[i].TabIndex = i;

                this.Controls.Add(gamebuttons[i]);
            }
        }

        
    
    }
}
