
namespace CatchButton
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.buttonCatch = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.buttonCatch.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.buttonCatch.Location = new System.Drawing.Point(180, 160);
            this.buttonCatch.Name = "buttonCatch";
            this.buttonCatch.Size = new System.Drawing.Size(170, 70);
            this.buttonCatch.Text = "나를 잡아봐";
            this.buttonCatch.UseVisualStyleBackColor = true;
            this.buttonCatch.Click += new System.EventHandler(this.buttonCatch_Click);
            this.buttonCatch.MouseEnter += new System.EventHandler(this.buttonCatch_MouseEnter);

            this.buttonReset.Location = new System.Drawing.Point(650, 380);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(120, 40);
            this.buttonReset.Text = "다시 시작";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);

            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCatch);
            this.Controls.Add(this.buttonReset);
            this.Name = "Form1";
            this.Text = "버튼 잡기 게임";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button buttonCatch;
        private System.Windows.Forms.Button buttonReset;
    }
}
