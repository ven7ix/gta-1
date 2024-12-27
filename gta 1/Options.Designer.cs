namespace gta_1
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TrackBarSoundVolume = new System.Windows.Forms.TrackBar();
            this.LabelSoundVolume = new System.Windows.Forms.Label();
            this.LabelRenderDistance = new System.Windows.Forms.Label();
            this.TrackBarRenderDistance = new System.Windows.Forms.TrackBar();
            this.ButtonResetSettings = new System.Windows.Forms.Button();
            this.LabelMusicVolume = new System.Windows.Forms.Label();
            this.TrackBarMusicVolume = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSoundVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarRenderDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMusicVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackBarSoundVolume
            // 
            this.TrackBarSoundVolume.Location = new System.Drawing.Point(12, 25);
            this.TrackBarSoundVolume.Maximum = 100;
            this.TrackBarSoundVolume.Name = "TrackBarSoundVolume";
            this.TrackBarSoundVolume.Size = new System.Drawing.Size(776, 45);
            this.TrackBarSoundVolume.TabIndex = 0;
            this.TrackBarSoundVolume.Value = 50;
            this.TrackBarSoundVolume.Scroll += new System.EventHandler(this.TrackBarSoundVolume_Scroll);
            // 
            // LabelSoundVolume
            // 
            this.LabelSoundVolume.AutoSize = true;
            this.LabelSoundVolume.Location = new System.Drawing.Point(12, 9);
            this.LabelSoundVolume.Name = "LabelSoundVolume";
            this.LabelSoundVolume.Size = new System.Drawing.Size(76, 13);
            this.LabelSoundVolume.TabIndex = 1;
            this.LabelSoundVolume.Text = "Sound Volume";
            // 
            // LabelRenderDistance
            // 
            this.LabelRenderDistance.AutoSize = true;
            this.LabelRenderDistance.Location = new System.Drawing.Point(12, 111);
            this.LabelRenderDistance.Name = "LabelRenderDistance";
            this.LabelRenderDistance.Size = new System.Drawing.Size(87, 13);
            this.LabelRenderDistance.TabIndex = 5;
            this.LabelRenderDistance.Text = "Render Distance";
            // 
            // TrackBarRenderDistance
            // 
            this.TrackBarRenderDistance.Location = new System.Drawing.Point(12, 127);
            this.TrackBarRenderDistance.Maximum = 100;
            this.TrackBarRenderDistance.Name = "TrackBarRenderDistance";
            this.TrackBarRenderDistance.Size = new System.Drawing.Size(776, 45);
            this.TrackBarRenderDistance.TabIndex = 4;
            this.TrackBarRenderDistance.Value = 50;
            this.TrackBarRenderDistance.Scroll += new System.EventHandler(this.TrackBarRenderDistance_Scroll);
            // 
            // ButtonResetSettings
            // 
            this.ButtonResetSettings.Location = new System.Drawing.Point(12, 396);
            this.ButtonResetSettings.Name = "ButtonResetSettings";
            this.ButtonResetSettings.Size = new System.Drawing.Size(136, 42);
            this.ButtonResetSettings.TabIndex = 8;
            this.ButtonResetSettings.Text = "Reset Settings";
            this.ButtonResetSettings.UseVisualStyleBackColor = true;
            this.ButtonResetSettings.Click += new System.EventHandler(this.ButtonResetSettings_Click);
            // 
            // LabelMusicVolume
            // 
            this.LabelMusicVolume.AutoSize = true;
            this.LabelMusicVolume.Location = new System.Drawing.Point(12, 60);
            this.LabelMusicVolume.Name = "LabelMusicVolume";
            this.LabelMusicVolume.Size = new System.Drawing.Size(73, 13);
            this.LabelMusicVolume.TabIndex = 10;
            this.LabelMusicVolume.Text = "Music Volume";
            // 
            // TrackBarMusicVolume
            // 
            this.TrackBarMusicVolume.Location = new System.Drawing.Point(12, 76);
            this.TrackBarMusicVolume.Maximum = 100;
            this.TrackBarMusicVolume.Name = "TrackBarMusicVolume";
            this.TrackBarMusicVolume.Size = new System.Drawing.Size(776, 45);
            this.TrackBarMusicVolume.TabIndex = 9;
            this.TrackBarMusicVolume.Value = 50;
            this.TrackBarMusicVolume.Scroll += new System.EventHandler(this.TrackBarMusicVolume_Scroll);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LabelMusicVolume);
            this.Controls.Add(this.LabelRenderDistance);
            this.Controls.Add(this.TrackBarMusicVolume);
            this.Controls.Add(this.ButtonResetSettings);
            this.Controls.Add(this.TrackBarRenderDistance);
            this.Controls.Add(this.LabelSoundVolume);
            this.Controls.Add(this.TrackBarSoundVolume);
            this.Name = "Options";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarSoundVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarRenderDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarMusicVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar TrackBarSoundVolume;
        private System.Windows.Forms.Label LabelSoundVolume;
        private System.Windows.Forms.Label LabelRenderDistance;
        private System.Windows.Forms.TrackBar TrackBarRenderDistance;
        private System.Windows.Forms.Button ButtonResetSettings;
        private System.Windows.Forms.Label LabelMusicVolume;
        private System.Windows.Forms.TrackBar TrackBarMusicVolume;
    }
}