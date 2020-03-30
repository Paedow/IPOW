using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics;
using IPOWLib.IO;
using System.IO;

namespace IPOW.Editor
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(string tile in Tiles.Tile.Tiles.Keys)
            {
                ListViewItem itm = new ListViewItem(tile);
                itm.Text = tile;
                lvTiles.Items.Add(itm);
            }

            this.Shown += MainWindow_Shown;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            levelControl1.Initialize();
        }

        private void lvTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = lvTiles.FocusedItem.Text;
            Type t = Tiles.Tile.Tiles[item];
            levelControl1.SetType(t);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Xml|*.xml";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                WorldDescriptor wd = levelControl1.World.GetDescriptor();
                string text = Saver.SaveToString(wd);
                File.WriteAllText(sfd.FileName, text);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Xml|*.xml";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string text = File.ReadAllText(ofd.FileName);
                WorldDescriptor wd = Loader.Load(text);
                World w = World.FromDescriptor(wd);
                levelControl1.SetWorld(w);
            }
        }
    }
}
