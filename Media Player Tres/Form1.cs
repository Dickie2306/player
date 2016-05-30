using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Media_Player_Tres
{
    public partial class MainWindow : Form
    {
        private int volume { get; set; }

        private bool isMute = true;
        
        public MainWindow()
        {
            InitializeComponent();

            volume = 36;
        }

        string[] files, paths;

        private void btnAddMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = openFileDialog.SafeFileNames; //Saves only the names
                paths = openFileDialog.FileNames; //Saves the full paths
                for (int i = 0; i < files.Length; i++)
                {
                    lbPlaylist.Items.Add(files[i]); // Add songs to the listbox
                }
            }

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void lbPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPlaylist.SelectedIndex >= 0)
            {
                axWindowsMediaPlayer1.URL = paths[lbPlaylist.SelectedIndex];
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            volume = trackBar1.Value;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.lbPlaylist.SelectedIndex = lbPlaylist.SelectedIndex - 1; axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.lbPlaylist.SelectedIndex = lbPlaylist.SelectedIndex + 1; axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        
        private void btnMute_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (isMute)
            {
                case false:
                    axWindowsMediaPlayer1.settings.volume = volume;
                    isMute = true;
                    btnMute.Image = Image.FromFile(@"Images\unmute_light_grey_25x25.png");
                    break;

                default:
                    volume = axWindowsMediaPlayer1.settings.volume;
                    axWindowsMediaPlayer1.settings.volume = 0;
                    isMute = false;
                    btnMute.Image = Image.FromFile(@"Images\mute_light_grey_25x25.png");
                    break;
            }            
        }

        private void lbPlaylist_DoubleClick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnDeleteMedia_Click(object sender, EventArgs e)
        {
            lbPlaylist.Items.RemoveAt(lbPlaylist.SelectedIndex);
        }
    }
}
