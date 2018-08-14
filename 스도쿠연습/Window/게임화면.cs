using System;
using System.Drawing;
using System.Windows.Forms;
using sudoku;
using sudoku.Generator;

namespace 스도쿠연습.game
{
  
    public partial class 게임화면 : Form
    {
        Board board = new Board();

        protected const int buttonstopmargin = 12;
        protected const int buttonsTopthirdMargin = 8;
        protected const int buttonsleftmargin = 12;
        protected const int buttonsLeftthirdMargin = 8;
        protected const int buttonsheight = 38;
        protected const int buttonswidth = 38;

        /// <summary>
        /// button 상속받아서 좌표값 저장할수 있게 변수 추가한 클래스
        /// </summary>
        public class Buttons : System.Windows.Forms.Button
        {
            public int index_innerrow;
            public int index_innercol;
            public int index_boxrow;
            public int index_boxcol;
        }
        /// <summary>
        /// 클릭한 버튼의 고유 번호 불러옴 ( 나중에 3x3 3x3기반으로 바꿀 예정.)
        /// </summary>
        public int[] clickedbuttonnum
        {
            get;
            private set;
        }

        /// <summary>
        /// 81개 버튼 컨트롤 배열
        /// </summary>

        public Buttons[,][,] gamebuttons;
        private static 게임화면 thisform = null;

        /// <summary>
        /// 게임화면 창을 불러옵니다(싱글턴 패턴 구현으로 중복 실행방지)
        /// 단, 멀티스레드 환경 아니니 주의
        /// </summary>
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
            gamebuttons = new Buttons[3, 3][,];
            int p = 0;

            for (int boxrow = 0; boxrow < 3; boxrow++)
            {
                for (int boxcol = 0; boxcol < 3; boxcol++)
                {
                    gamebuttons[boxrow, boxcol] = new Buttons[3, 3];
                    for (int innerrow = 0; innerrow < 3; innerrow++)
                    {

                        for (int innercol = 0; innercol < 3; innercol++)
                        {
                            makeButton(p, boxrow, boxcol, innerrow, innercol);

                            p++;
                        }
                    }

                }
            }
        }

        private void makeButton(int buttonIndex, int boxrow, int boxcol, int innerrow, int innercol)
        {
            gamebuttons[boxrow, boxcol][innerrow, innercol] = new Buttons();
            Buttons A = gamebuttons[boxrow, boxcol][innerrow, innercol];

            int xPos = buttonsleftmargin + innerrow * buttonswidth + boxrow * (buttonsLeftthirdMargin * buttonsleftmargin + buttonswidth);
            int yPos = buttonstopmargin + innercol * buttonsheight + boxcol * (buttonsTopthirdMargin * buttonstopmargin + buttonsheight);

            A.Text = buttonIndex.ToString();
            A.Location = new System.Drawing.Point(xPos,yPos);
            A.Name = "gamebuttons[" + buttonIndex + "]";
            A.Size = new Size(32, 32);
            A.TabIndex = buttonIndex;
            A.index_boxcol = boxcol;
            A.index_boxrow = boxrow;
            A.index_innercol = innercol;
            A.index_innerrow = innerrow;


            this.Controls.Add(A);
            A.Click += (sender, e) => // 버튼패드 창 띄우는 이벤트 추가(람다 식)
            {
                Buttons button = (Buttons)sender;
                clickedbuttonnum = new int[4] { A.index_boxrow, A.index_boxcol, A.index_innerrow, A.index_innercol };
                NumPadForm 숫자폼 = NumPadForm.GetInstance;

                숫자폼.Show();
                숫자폼.Location = new System.Drawing.Point(this.Location.X + button.Location.X, this.Location.Y + button.Location.Y);
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void 새_게임()
        {
            board = BoardGenarator.genarateboard(board);
            배치하기(this);
            button1.Enabled = true;
        }
        private void 배치하기(게임화면 form)
        {           
            for (int i = 0; i<3; i++) { for(int j = 0; j < 3; j++) { for(int k = 0; k < 3; k++) { for(int l = 0; l < 3; l++)
                        {
                            form.gamebuttons[i, j][k, l].Text = board[i, j].box[k, l].value.ToString();
                        } } } }
            //button1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            배치하기(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            새_게임();
        }

        private void 게임화면_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void 게임화면_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NumPadForm.GetInstance != null)
            {
                NumPadForm.GetInstance.Close();
            }
        }
    }
}
