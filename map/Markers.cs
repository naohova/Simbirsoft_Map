using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace map
{
    public partial class Markers : Form
    {
        double X, Y;
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");
        public Markers(double x, double y)
        {
            InitializeComponent();
            X = y;
            Y = x;
           comboBox1.DataSource = db.getTableInfo($"SELECT `id`, `Name` FROM `Liorkin`.`Place` where Rate_id = {User.lvl}");
           comboBox1.DisplayMember = "Name";
           comboBox1.ValueMember = "id";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "")|| (textBox2.Text == "") || (textBox3.Text == "")|| (comboBox1.Text == ""))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            db.download(Convert.ToInt32(comboBox1.SelectedValue), User.City_id, textBox1.Text, textBox2.Text, textBox3.Text, X, Y);
            this.Close();
        }
    }
}
