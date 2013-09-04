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

            string dir = "C:\\Users\\Jordan\\Desktop\\MusicDiff-Test\\";
            List<string> dirs = new List<string>(Directory.EnumerateFiles(dir, "*.*", SearchOption.AllDirectories));
            foreach (var subDir in dirs)
            {
                listView1.Items.Add(new ListViewItem(subDir.Replace(dir, "")));
            }
            Console.WriteLine("{0} directories found.", dirs.Count);
        }
    }
}
