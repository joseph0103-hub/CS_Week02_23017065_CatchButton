
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CatchButton
{
    public partial class Form1 : Form
    {
        private readonly Random random = new Random();
        private int score = 0;
        private int missCount = 0;
        private Size initialCatchButtonSize;
        private bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();
            initialCatchButtonSize = buttonCatch.Size;
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            this.Text = $"버튼 위치: ({buttonCatch.Left}, {buttonCatch.Top}) | 점수: {score} | 놓침: {missCount}/20";
        }

        private Point GetRandomLocationInsideForm(Control control)
        {
            int maxX = Math.Max(0, this.ClientSize.Width - control.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - control.Height - 70);

            int x = random.Next(0, maxX + 1);
            int y = random.Next(0, maxY + 1);

            return new Point(x, y);
        }

        private void buttonCatch_MouseEnter(object sender, EventArgs e)
        {
            if (isGameOver) return;

            SystemSounds.Beep.Play();

            score -= 10;
            missCount++;

            buttonCatch.Location = GetRandomLocationInsideForm(buttonCatch);

            UpdateTitle();
            CheckGameOver();
        }

        private void buttonCatch_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            SystemSounds.Asterisk.Play();

            score += 100;

            int newWidth = (int)(buttonCatch.Width * 0.9);
            int newHeight = (int)(buttonCatch.Height * 0.9);

            if (newWidth < 50) newWidth = 50;
            if (newHeight < 30) newHeight = 30;

            buttonCatch.Size = new Size(newWidth, newHeight);
            buttonCatch.Location = GetRandomLocationInsideForm(buttonCatch);

            UpdateTitle();

            MessageBox.Show("축하합니다~!", "성공");
        }

        private void CheckGameOver()
        {
            if (missCount >= 20)
            {
                isGameOver = true;
                buttonCatch.Enabled = false;
                buttonReset.Enabled = true;

                MessageBox.Show("Game Over");
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            score = 0;
            missCount = 0;
            isGameOver = false;

            buttonCatch.Enabled = true;
            buttonCatch.Size = initialCatchButtonSize;
            buttonCatch.Location = new Point(180, 160);

            UpdateTitle();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (buttonCatch.Right > this.ClientSize.Width)
                buttonCatch.Left = Math.Max(0, this.ClientSize.Width - buttonCatch.Width);

            int maxTop = Math.Max(0, this.ClientSize.Height - buttonCatch.Height - 70);

            if (buttonCatch.Top > maxTop)
                buttonCatch.Top = maxTop;

            UpdateTitle();
        }
    }
}
