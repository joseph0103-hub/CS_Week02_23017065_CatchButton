
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CatchButton
{
    public partial class Form1 : Form
    {
        private readonly Random random = new Random();
        private readonly List<Point> backgroundDots = new List<Point>();

        private int score = 0;
        private int missCount = 0;
        private Size initialCatchButtonSize;
        private bool isGameOver = false;

        private SoundPlayer? catchSound;
        private SoundPlayer? escapeSound;

        public Form1()
        {
            InitializeComponent();

            initialCatchButtonSize = buttonCatch.Size;
            InitializeSounds();
            UpdateTitle();
        }

        // 버튼 도망시, 버튼을 잡았을 때의 사운드 초기화
        private void InitializeSounds()
        {
            try
            {
                string soundsPath = Path.Combine(Application.StartupPath, "Sounds");
                string catchPath = Path.Combine(soundsPath, "catch.wav");
                string escapePath = Path.Combine(soundsPath, "escape.wav");

                if (File.Exists(catchPath))
                {
                    catchSound = new SoundPlayer(catchPath);
                    catchSound.LoadAsync();
                }

                if (File.Exists(escapePath))
                {
                    escapeSound = new SoundPlayer(escapePath);
                    escapeSound.LoadAsync();
                }
            }
            catch
            {
                catchSound = null;
                escapeSound = null;
            }
        }

        // 버튼을 잡았을 때, 사운드 출력
        private void PlayCatchSound()
        {
            try
            {
                if (catchSound != null)
                    catchSound.Play();
                else
                    SystemSounds.Asterisk.Play();
            }
            catch
            {
                SystemSounds.Asterisk.Play();
            }
        }

        // 버튼 도망시 사운드 출력
        private void PlayEscapeSound()
        {
            try
            {
                if (escapeSound != null)
                    escapeSound.Play();
                else
                    SystemSounds.Exclamation.Play();
            }
            catch
            {
                SystemSounds.Exclamation.Play();
            }
        }

        //점수 차감, 놓침 횟수 증가 시키기
        private void UpdateTitle()
        {
            Text = $"버튼 위치: ({buttonCatch.Left}, {buttonCatch.Top}) | 점수: {score} | 놓침: {missCount}/20";
        }

        private Point GetRandomLocationInsideForm(Control control)
        {
            int maxX = Math.Max(0, ClientSize.Width - control.Width);
            int maxY = Math.Max(0, ClientSize.Height - control.Height - 70);

            int x = random.Next(0, maxX + 1);
            int y = random.Next(0, maxY + 1);

            return new Point(x, y);
        }

        private void buttonCatch_MouseEnter(object sender, EventArgs e)
        {
            if (isGameOver) return;

            PlayEscapeSound();

            score -= 10;
            missCount++;

            buttonCatch.Location = GetRandomLocationInsideForm(buttonCatch);

            UpdateTitle();
            CheckGameOver();
        }

        private void buttonCatch_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            PlayCatchSound();

            score += 100;

            int newWidth = (int)(buttonCatch.Width * 0.9);
            int newHeight = (int)(buttonCatch.Height * 0.9);

            if (newWidth < 50) newWidth = 50;
            if (newHeight < 30) newHeight = 30;

            missCount = 0;
            isGameOver = false;
            buttonCatch.Enabled = true;

            buttonCatch.Size = new Size(newWidth, newHeight);
            buttonCatch.Location = GetRandomLocationInsideForm(buttonCatch);

            UpdateTitle();

            MessageBox.Show("축하합니다~!\n성공했어요!", "성공");
        }

        private void CheckGameOver()
        {
            if (missCount >= 20)
            {
                isGameOver = true;
                buttonCatch.Enabled = false;
                buttonReset.Enabled = true;
                buttonCatch.BackColor = Color.Gray;

                MessageBox.Show("Game Over\n다시 시작 버튼을 눌러 재도전하세요.", "게임 오버");
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
            buttonCatch.BackColor = Color.FromArgb(255, 240, 240, 240);

            UpdateTitle();
            Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateTitle();
            Invalidate();
        }

        // 배경 색상 지정
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = ClientRectangle;
            using LinearGradientBrush gradient = new LinearGradientBrush(
                rect,
                Color.FromArgb(35, 20, 80),
                Color.FromArgb(0, 180, 255),
                35f);
            e.Graphics.FillRectangle(gradient, rect);

            using SolidBrush overlay = new SolidBrush(Color.FromArgb(55, 255, 255, 255));
            foreach (Point point in backgroundDots)
            {
                int size = random.Next(10, 26);
                e.Graphics.FillEllipse(overlay, point.X, point.Y, size, size);
            }

            using LinearGradientBrush titleBrush = new LinearGradientBrush(
                new Rectangle(15, 15, 280, 50),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(255, 230, 120),
                0f);
            using Font titleFont = new Font("맑은 고딕", 18F, FontStyle.Bold);
        }
    }
}
