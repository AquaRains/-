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
        public int clickedbuttonnum
        {
            get;
            private set;
        }
        private const int txtboxtopmargin = 12;
        private const int txtboxleftmargin = 12;
        public Button[] gamebuttons { get; private set; }
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
            makegamebuttons();
        }

        private void makegamebuttons()
        {
            gamebuttons = new Button[81];
           
            for(int i = 0; i < 81; i++)
            {
                gamebuttons[i] = new Button();
                gamebuttons[i].Text = i.ToString();
                gamebuttons[i].Location = new Point(txtboxleftmargin + i % 9 * 38,txtboxtopmargin + i / 9 % 9 * 38);
                gamebuttons[i].Name = "gamebuttons[" + i + "]";
                gamebuttons[i].Size = new Size(32, 32);
                gamebuttons[i].TabIndex = i;

                this.Controls.Add(gamebuttons[i]);
                gamebuttons[i].Click += (object sender, EventArgs e) =>
                {
                    Button button = (Button)sender;
                    clickedbuttonnum = button.TabIndex;
                    NumPadForm 숫자폼 = NumPadForm.GetInstance;
                    숫자폼.Show();
                  
                };

            }
        }

        

    }
}
