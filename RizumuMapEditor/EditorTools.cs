using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RizumuMapEditor
{
    public partial class EditorTools : Form
    {
        public EditorTools()
        {
            InitializeComponent();
            SharedEvents.PlaybarEvent += (sender, e) =>
            {
                playBar.Value = e.Progress;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                backgroundPath.Text = openFileDialog1.FileName;
                Game1.newbackgroundpath = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                mp3Path.Text = openFileDialog1.FileName;
                Game1.newmp3path = openFileDialog1.FileName;
            }
        }
    }
    public class SharedEvents
    {
        public static event EventHandler<PlaybarEventArgs> PlaybarEvent;

        public static void InvokePlaybar(int progress)
        {
            PlaybarEvent.BeginInvoke(null, new PlaybarEventArgs(progress), null, null);
        }
    }
}
