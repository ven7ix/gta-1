using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gta_1
{
    public partial class Options : Form
    {
        public static float SoundVolume = 0.5f;
        public static float MusicVolume = 0.25f;
        public static int RenderDistance = 40;

        public Options()
        {
            InitializeComponent();
            TrackBarSoundVolume.Value = (int)(SoundVolume * 100);
            TrackBarMusicVolume.Value = (int)(MusicVolume * 100);
            TrackBarRenderDistance.Value = RenderDistance;
        }

        private void TrackBarSoundVolume_Scroll(object sender, EventArgs e)
        {
            SoundVolume = (float)TrackBarSoundVolume.Value / 100;
        }

        private void TrackBarMusicVolume_Scroll(object sender, EventArgs e)
        {
            MusicVolume = (float)TrackBarMusicVolume.Value / 100;
        }

        private void TrackBarRenderDistance_Scroll(object sender, EventArgs e)
        {
            RenderDistance = TrackBarRenderDistance.Value;
        }

        private void ButtonResetSettings_Click(object sender, EventArgs e)
        {
            TrackBarSoundVolume.Value = 50;
            TrackBarMusicVolume.Value = 25;
            TrackBarRenderDistance.Value = 40;

            SoundVolume = 0.5f;
            MusicVolume = 0.25f;
            RenderDistance = 40;
        }
    }
}
