using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicDiff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Path");
            listView1.Columns[0].Width = listView1.Width - 4;
            listView1.HeaderStyle = ColumnHeaderStyle.None;

            string dir = "D:\\iTunes Music\\Music";
            //dir = "C:\\Users\\Jordan\\Desktop\\MusicDiff-Test";
            List<System.IO.FileInfo> files = new List<FileInfo>();
            List<string> dirs = new List<string>(Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories));
            foreach (var subDir in dirs)
            {
                FileInfo file = new FileInfo(subDir);
                TagLib.File sameFile = TagLib.File.Create(subDir);
                Console.WriteLine(subDir);
                int length = sameFile.Tag.Performers.Length;
                listView1.Items.Add(new ListViewItem("Title: " + sameFile.Tag.Title + "  -  Artist: " + (length > 0 ? sameFile.Tag.Performers[0] : "")));
            }
            Console.WriteLine("\n{0} directories found.", dirs.Count);

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("HttpListener is not supported");
                return;
            }

            //HttpListener listener = new HttpListener();
            //listener.Start();
            //Console.WriteLine("Listening..");
            ////listener.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
