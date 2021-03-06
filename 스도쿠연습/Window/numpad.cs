﻿using System;
using System.Windows.Forms;

namespace 스도쿠연습.game
{
    public partial class NumPadForm : Form
    {
        public static NumPadForm thisform = null;
        public int clickednum
        {
            get;
            private set;
        }
        Button[] btns = new Button[9];
        public static NumPadForm GetInstance
        {
            get
            {
                {
                    if (thisform == null || thisform.IsDisposed)
                    {
                        thisform = new NumPadForm();

                        return thisform;
                    }
                    else
                    {

                        return thisform;
                    }

                }
            }
        }

        

        private NumPadForm()
        {
            
           InitializeComponent();
            
            for (int i = 0; i < 9; i++)
            {
                btns[i] = (Button)Controls.Find(string.Format("button{0}", i + 1), true)[0];
                btns[i].Click += (object sender, EventArgs e) =>
                {
                    Button button = (Button)sender;
                    clickednum = int.Parse(button.Text);
                    
                    게임화면.GetInstance.gamebuttons[게임화면.GetInstance.clickedbuttonnum[0],게임화면.GetInstance.clickedbuttonnum[1]][게임화면.GetInstance.clickedbuttonnum[2],게임화면.GetInstance.clickedbuttonnum[3]].Text = clickednum.ToString();
                    this.Close();
                };
            }

        }

        private void NumPadForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
