using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rizumu.Objects;
using Newtonsoft.Json.Linq;

namespace RizumuMapConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    RizumuMap m = new RizumuMap()
                    {
                        BackgroundFile = bgfilename.Text,
                        Creator = creator.Text,
                        Description = mapdesc.Text,
                        FileName = mp3name.Text,
                        Name = mapname.Text
                    };
                    string[] lines = File.ReadAllLines(Path.Combine(fbd.SelectedPath, "fnotes.rizum"));
                    List<int> fnotes = new List<int>();
                    foreach (string note in lines)
                    {
                        try
                        {
                            fnotes.Add(int.Parse(note));
                        }
                        catch (Exception)
                        {

                        }
                    }

                    lines = File.ReadAllLines(Path.Combine(fbd.SelectedPath, "gnotes.rizum"));
                    List<int> gnotes = new List<int>();
                    foreach (string note in lines)
                    {
                        try
                        {
                            gnotes.Add(int.Parse(note));
                        }
                        catch (Exception)
                        {

                        }
                    }

                    lines = File.ReadAllLines(Path.Combine(fbd.SelectedPath, "hnotes.rizum"));
                    List<int> hnotes = new List<int>();
                    foreach (string note in lines)
                    {
                        try
                        {
                            hnotes.Add(int.Parse(note));
                        }
                        catch (Exception)
                        {

                        }
                    }

                    lines = File.ReadAllLines(Path.Combine(fbd.SelectedPath, "jnotes.rizum"));
                    List<int> jnotes = new List<int>();
                    foreach (string note in lines)
                    {
                        try
                        {
                            jnotes.Add(int.Parse(note));
                        }
                        catch (Exception)
                        {

                        }
                    }

                    m.LeftNotes = fnotes.ToArray();
                    m.UpNotes = gnotes.ToArray();
                    m.RightNotes = hnotes.ToArray();
                    m.DownNotes = jnotes.ToArray();
                    Directory.CreateDirectory("ConvertedMap");
                    File.Copy(Path.Combine(fbd.SelectedPath, "back.png"), Path.Combine(Environment.CurrentDirectory, "ConvertedMap", bgfilename.Text));
                    File.Copy(Path.Combine(fbd.SelectedPath, "song.mp3"), Path.Combine(Environment.CurrentDirectory, "ConvertedMap", mp3name.Text));
                    File.Create(Path.Combine(Environment.CurrentDirectory, "ConvertedMap", "map.json")).Close();
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "ConvertedMap", "map.json"), JObject.FromObject(m).ToString());
                }
            }
        }
    }
}
