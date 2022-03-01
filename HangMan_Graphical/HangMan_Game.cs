using System.Text;

namespace HangMan_Graphical
{
    public partial class HangMan_Game : Form
    {
        private WordStorage storage = new();

        private int misses = 0;
        private string keyWord = "";
        private string playerWord = "";

        private bool Won 
        { 
            get => !keyWord.Equals("") && keyWord.Equals(playerWord.Replace(" ", "")) ? true : false;
        }
        private bool Lose 
        { 
            get => misses >= 6 ? true : false;
        }

        #region Draw Pointers
        private Pen myPen = new(Color.White, 3);

        private Point[] hanger =
        {
            new(275, 260),
            new(375, 260),
            new(325, 260),
            new(325, 100),
            new(250, 100),
            new(250, 125)
        };

        private Rectangle head = new(new(238, 125), new(25, 25));
        private Point[] body =
        {
            new(250, 150),
            new(250, 200)
        };
        private Point[] footR =
        {
            new(225, 225),
            new(250, 200)
        };
        private Point[] footL =
        {
            new(275, 225),
            new(250, 200)
        };
        private Point[] armR =
        {
            new(225, 175),
            new(250, 165)
        };
        private Point[] armL =
        {
            new(275, 175),
            new(250, 165)
        };
        #endregion

        public HangMan_Game()
        {
            InitializeComponent();
            this.Height = 500;
            this.Width = 500;
            this.KeyPreview = true;
            Reset(
                "Press \'Enter\' to Input a character." + "\n" +
                "Don't forget to check the Clues and Used Words to help you find the answer." + "\n" +
                "Made by Jay B.");
        }

        private void HangMan_Game_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);

            if (Won)
                Reset("YOU WON!!\n" +
                    "Try again?",MessageBoxButtons.YesNo);

            if (Lose)
                Reset($"Correct Answer is: {keyWord}\n" +
                    $"Try again?", MessageBoxButtons.YesNo);
        }

        private void Draw(Graphics g)
        {
            g.ResetClip();
            g.DrawLines(myPen, hanger);

            for (int i = 0; i <= misses; i++)
                if (i == 1)
                    g.DrawArc(myPen, head, 0f, 360f);
                else if (i == 2)
                    g.DrawLines(myPen, body);
                else if (i == 3)
                    g.DrawLines(myPen, footR);
                else if (i == 4)
                    g.DrawLines(myPen, footL);
                else if (i == 5)
                    g.DrawLines(myPen, armR);
                else if (i == 6)
                    g.DrawLines(myPen, armL);
        }

        private void HangMan_Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = Convert.ToChar(e.KeyChar.ToString().ToUpper());
            
            KeyPressed(key);
        }

        private void KeyPressed(char key)
        {
            bool checkPressedChar = CheckInAlphabet(Convert.ToChar(label2.Text)) || CheckInAlphabet(key) ? true : false;
            bool pressedEnter = key == Convert.ToChar(Keys.Enter) && checkPressedChar ? true : false;

            if (pressedEnter)
            {
                CheckIfInKeyword(label2.Text);
                label5.Text += label2.Text;
                label2.Text = "_";
                this.Invalidate();
            }
            else if (CheckInAlphabet(key))
                label2.Text = Convert.ToString(key);
        } 

        private void CheckIfInKeyword(string s)
        {
            bool hasFound = false;

            for (int i = 0, c = 0; i < keyWord.Length; i++, c += 2)
                if (keyWord[i].Equals(s[0]) && c != playerWord.Length)
                {
                    hasFound = true;
                    StringBuilder sb = new StringBuilder(playerWord);
                    sb[c] = s[0];
                    label1.Text = playerWord = sb.ToString();
                }

            if (!hasFound)
                misses++;
        }

        private void Reset(string msg = "NO_MESSAGE", MessageBoxButtons msgButtons = MessageBoxButtons.OK)
        {
            string caption = "Hangman Graphical";
            DialogResult res = MessageBox.Show(msg, caption, msgButtons);
            if (res == DialogResult.No)
                this.Close();

            label5.Text = "Used Letters: ";
            keyWord = "";
            playerWord = "";
            misses = 0;

            string[] baseKey = storage.GetRandom;

            foreach (char c in baseKey[1].ToUpper())
                if (CheckInAlphabet(c))
                {
                    keyWord += c;
                    playerWord += "_ ";
                }

            label4.Text = $"CLUE: {baseKey[0].ToUpper()}";
            label1.Text = playerWord;
            this.Invalidate();
        }

        private bool CheckInAlphabet(char c) => ((int)c >= 97 && (int)c <= 122) || ((int)c >= 65 && (int)c <= 90) ? true : false;
    }
}
