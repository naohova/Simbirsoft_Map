using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace map
{
    public partial class EditMarkers : UserControl
    {
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");
        int ID;
        public EditMarkers(int id)
        {
            ID = id;
            InitializeComponent();
            List<string> A = db.Edit_Information(id);
            textBox1.Text = A[0];
            textBox2.Text = A[1];
            textBox3.Text = A[2];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.Update_Markers(ID ,textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.Delete_Markers(ID);
            this.Parent.Controls.Remove(this);
        }
    }
}
