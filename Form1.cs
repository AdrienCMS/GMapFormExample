using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.Internals;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;



namespace GMapFormExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;
            gMapControl1.SetPositionByKeywords("Paris, France");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxLat.Text != "" && textBoxLat.Text != "")
            {
                try
                {
                    gMapControl1.Position = new GMap.NET.PointLatLng(Convert.ToDouble(textBoxLat.Text), Convert.ToDouble(textBoxLon.Text));
                    gMapControl1.Zoom = 21;
                }
                catch
                {
                    MessageBox.Show("ERROR");
                }
                
            }
            else
            {
                MessageBox.Show("Please enter coordinates");

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;

            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                gMapControl1.CacheLocation = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GMap.NET.RectLatLng area = gMapControl1.SelectedArea;

            if (!area.IsEmpty)
            {
                for (int i = gMapControl1.MinZoom; i <= gMapControl1.MaxZoom; i++)
                {
                    GMap.NET.TilePrefetcher obj = new GMap.NET.TilePrefetcher();
                    obj.Text = "Prefetching Tiles";
                    obj.Icon = this.Icon;
                    obj.Owner = this;

                    
                    
                    obj.ShowCompleteMessage = false;
                    obj.Start(area, i, gMapControl1.MapProvider, 100,0);
                }

                DialogResult = DialogResult.OK;
                //Close();
            }
            else
            {
                MessageBox.Show("No Area Chosen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Cache":
                    GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;
                    break;

                case "Server and Cache":
                    GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                    break;

                case "Server":
                    GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
                    break;

            }

            gMapControl1.Refresh();
        }
    }
}
