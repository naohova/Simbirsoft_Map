using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.Common;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace map
{
    public partial class Point : UserControl
    {
        GMapControl Search;
        Coord Cord;
        int id;
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");
        public Point(Coord iD, Control G)
        {
            Cord = iD;
            List<string> a = db.A(iD.id);
            InitializeComponent();//a[0], a[1], a[2]
            if (User.id == 0)
            { checkBox1.Visible = false; }
            groupBox1.Text = a[0];
            textBox1.Text = a[1];
            textBox2.Text = a[2];
            id = Convert.ToInt32(a[3]); 
            this.Dock = DockStyle.Top;
            checkBox1.Checked = db.Visited_Check(id);
            Search = (GMapControl)G;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Search.Position = new PointLatLng(Cord.x, Cord.y);
            Search.Zoom = 17;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            db.visited(id, checkBox1.Checked);
        }
    }
}
