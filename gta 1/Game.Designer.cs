namespace gta_1
{
    partial class Game
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimerGameLoop = new System.Windows.Forms.Timer(this.components);
            this.Screen = new gta_1.DoubleBufferedPanel();
            this.ButtonQuit = new System.Windows.Forms.Button();
            this.ButtonOptions = new System.Windows.Forms.Button();
            this.ButtonStartNewGame = new System.Windows.Forms.Button();
            this.ButtonResume = new System.Windows.Forms.Button();
            this.PictureBoxMenu = new System.Windows.Forms.PictureBox();
            this.Screen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // TimerGameLoop
            // 
            this.TimerGameLoop.Interval = 20;
            this.TimerGameLoop.Tick += new System.EventHandler(this.TimerGameLoop_Tick);
            // 
            // Screen
            // 
            this.Screen.Controls.Add(this.ButtonQuit);
            this.Screen.Controls.Add(this.ButtonOptions);
            this.Screen.Controls.Add(this.ButtonStartNewGame);
            this.Screen.Controls.Add(this.ButtonResume);
            this.Screen.Controls.Add(this.PictureBoxMenu);
            this.Screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Screen.Location = new System.Drawing.Point(0, 0);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(1264, 681);
            this.Screen.TabIndex = 0;
            this.Screen.Paint += new System.Windows.Forms.PaintEventHandler(this.Screen_Paint);
            this.Screen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseClick);
            this.Screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
            // 
            // ButtonQuit
            // 
            this.ButtonQuit.AutoSize = true;
            this.ButtonQuit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonQuit.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ButtonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonQuit.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonQuit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonQuit.Location = new System.Drawing.Point(12, 242);
            this.ButtonQuit.Name = "ButtonQuit";
            this.ButtonQuit.Size = new System.Drawing.Size(120, 72);
            this.ButtonQuit.TabIndex = 3;
            this.ButtonQuit.TabStop = false;
            this.ButtonQuit.Text = "Quit";
            this.ButtonQuit.UseVisualStyleBackColor = false;
            this.ButtonQuit.Click += new System.EventHandler(this.ButtonQuit_Click);
            // 
            // ButtonOptions
            // 
            this.ButtonOptions.AutoSize = true;
            this.ButtonOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonOptions.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ButtonOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonOptions.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonOptions.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonOptions.Location = new System.Drawing.Point(12, 164);
            this.ButtonOptions.Name = "ButtonOptions";
            this.ButtonOptions.Size = new System.Drawing.Size(196, 72);
            this.ButtonOptions.TabIndex = 2;
            this.ButtonOptions.TabStop = false;
            this.ButtonOptions.Text = "Options";
            this.ButtonOptions.UseVisualStyleBackColor = false;
            this.ButtonOptions.Click += new System.EventHandler(this.ButtonOptions_Click);
            // 
            // ButtonStartNewGame
            // 
            this.ButtonStartNewGame.AutoSize = true;
            this.ButtonStartNewGame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonStartNewGame.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonStartNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ButtonStartNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonStartNewGame.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonStartNewGame.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonStartNewGame.Location = new System.Drawing.Point(12, 88);
            this.ButtonStartNewGame.Name = "ButtonStartNewGame";
            this.ButtonStartNewGame.Size = new System.Drawing.Size(358, 72);
            this.ButtonStartNewGame.TabIndex = 1;
            this.ButtonStartNewGame.TabStop = false;
            this.ButtonStartNewGame.Text = "Start New Game";
            this.ButtonStartNewGame.UseVisualStyleBackColor = false;
            this.ButtonStartNewGame.Click += new System.EventHandler(this.ButtonStartNewGame_Click);
            // 
            // ButtonResume
            // 
            this.ButtonResume.AutoSize = true;
            this.ButtonResume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonResume.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ButtonResume.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ButtonResume.Enabled = false;
            this.ButtonResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonResume.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonResume.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ButtonResume.Location = new System.Drawing.Point(12, 12);
            this.ButtonResume.Name = "ButtonResume";
            this.ButtonResume.Size = new System.Drawing.Size(204, 72);
            this.ButtonResume.TabIndex = 0;
            this.ButtonResume.TabStop = false;
            this.ButtonResume.Text = "Resume";
            this.ButtonResume.UseVisualStyleBackColor = false;
            this.ButtonResume.Click += new System.EventHandler(this.ButtonResume_Click);
            // 
            // PictureBoxMenu
            // 
            this.PictureBoxMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxMenu.Enabled = false;
            this.PictureBoxMenu.Image = global::gta_1.Properties.Resources.gta_1_level;
            this.PictureBoxMenu.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxMenu.Name = "PictureBoxMenu";
            this.PictureBoxMenu.Size = new System.Drawing.Size(1264, 681);
            this.PictureBoxMenu.TabIndex = 5;
            this.PictureBoxMenu.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.Screen);
            this.KeyPreview = true;
            this.Name = "Game";
            this.Text = "gta 1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            this.Resize += new System.EventHandler(this.Game_Resize);
            this.Screen.ResumeLayout(false);
            this.Screen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private gta_1.DoubleBufferedPanel Screen;
        private System.Windows.Forms.Timer TimerGameLoop;
        private System.Windows.Forms.Button ButtonResume;
        private System.Windows.Forms.Button ButtonQuit;
        private System.Windows.Forms.Button ButtonOptions;
        private System.Windows.Forms.Button ButtonStartNewGame;
        private System.Windows.Forms.PictureBox PictureBoxMenu;
    }
}

