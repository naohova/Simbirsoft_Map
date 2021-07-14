using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace map
{
    public partial class UsrCab : Form
    {
        
        int globalid = 0;

        private Label Subscribe;
        private Panel panel1;
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");
        public UsrCab(int id)
        {
           
            InitializeComponent();
            this.Refresh();
            PlaceBox.ReadOnly = true;
            CityBox.ReadOnly = true;
            All_About.Text = $"{db.Usr_name(id)} {db.Usr_surname(id)} {db.Usr_secname(id)} {db.Usr_age(id)} {db.Usr_city(id)}";
            globalid = id;
            comboBox1.DataSource = db.getTableInfo("SELECT * FROM Rate;");
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            if (db.Usr_status(id) == 1)
            {
                Subscribe.Text = "Подписка: Базовая";
            }
            else if (db.Usr_status(id) == 2)
            {
                Subscribe.Text = "Подписка: Арендодатель";
            }
            else if (db.Usr_status(id) == 3)
            {
                Subscribe.Text = "Подписка: Бизнес";// made with quwin
            }
            foreach(int Sunset_shimmer in db.Edit_Markers())
            {
                EditMarkers edit = new EditMarkers(Sunset_shimmer);
                edit.Dock = DockStyle.Top;
                panel1.Controls.Add(edit);
            }
            List<string> places = new List<string>();
            List<string> adresses = new List<string>();
            List<string> citys = new List<string>();
            places = db.selectattr(globalid);
            adresses = db.selectattraddress(globalid);
            //citys = db.selectcitys(globalid);
            for (int i = 0; i < db.selectattr(globalid).Count; i++)
                PlaceBox.Text += $"{places[i]} {adresses[i]} " + Environment.NewLine;
            citys = db.selectcity(globalid);
            for (int i = 0; i < db.selectcity(globalid).Count; i++)
                CityBox.Text += citys[i] + Environment.NewLine;
        }
        private void UsrCab_Load(object sender, EventArgs e)
        {

        }
        private void RenUser_Click(object sender, EventArgs e)
        {
            RenameProfile shitass = new RenameProfile(globalid);
            shitass.Show();
            this.Dispose();
        }
        private void offer_obj_Click(object sender, EventArgs e)
        {
            
            //AttrListBox.GetItemChecked()
        }

        private void InitializeComponent()
        {
            this.All_About = new System.Windows.Forms.Label();
            this.RenUser = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PlaceBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CityBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Subscribe = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // All_About
            // 
            this.All_About.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.All_About.AutoSize = true;
            this.All_About.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.All_About.Location = new System.Drawing.Point(29, 4);
            this.All_About.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.All_About.Name = "All_About";
            this.All_About.Size = new System.Drawing.Size(358, 29);
            this.All_About.TabIndex = 6;
            this.All_About.Text = "Информация о пользователе";
            // 
            // RenUser
            // 
            this.RenUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.RenUser.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.RenUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RenUser.Location = new System.Drawing.Point(744, 351);
            this.RenUser.Margin = new System.Windows.Forms.Padding(2);
            this.RenUser.Name = "RenUser";
            this.RenUser.Size = new System.Drawing.Size(139, 37);
            this.RenUser.TabIndex = 7;
            this.RenUser.Text = "Редактировать профиль";
            this.RenUser.UseVisualStyleBackColor = false;
            this.RenUser.Click += new System.EventHandler(this.RenUser_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Location = new System.Drawing.Point(11, 72);
            this.TabControl.Margin = new System.Windows.Forms.Padding(2);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(362, 324);
            this.TabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PlaceBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(354, 298);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Места";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // PlaceBox
            // 
            this.PlaceBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.PlaceBox.Location = new System.Drawing.Point(0, 0);
            this.PlaceBox.Margin = new System.Windows.Forms.Padding(2);
            this.PlaceBox.Multiline = true;
            this.PlaceBox.Name = "PlaceBox";
            this.PlaceBox.Size = new System.Drawing.Size(354, 298);
            this.PlaceBox.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CityBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(354, 298);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Города";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CityBox
            // 
            this.CityBox.BackColor = System.Drawing.SystemColors.Control;
            this.CityBox.Location = new System.Drawing.Point(5, 5);
            this.CityBox.Margin = new System.Windows.Forms.Padding(2);
            this.CityBox.Multiline = true;
            this.CityBox.Name = "CityBox";
            this.CityBox.Size = new System.Drawing.Size(498, 292);
            this.CityBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(720, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 95);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип подписки";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(11, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "Изменить подписку";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // Subscribe
            // 
            this.Subscribe.AutoSize = true;
            this.Subscribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Subscribe.Location = new System.Drawing.Point(727, 94);
            this.Subscribe.Name = "Subscribe";
            this.Subscribe.Size = new System.Drawing.Size(84, 20);
            this.Subscribe.TabIndex = 12;
            this.Subscribe.Text = "Подписка";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(374, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 297);
            this.panel1.TabIndex = 13;
            // 
            // UsrCab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(933, 407);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Subscribe);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.All_About);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.RenUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UsrCab";
            this.Text = "Личный кабинет";
            this.Load += new System.EventHandler(this.UsrCab_Load);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label All_About;
        private System.Windows.Forms.Button RenUser;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox CityBox;
        private GroupBox groupBox1;
        private Button button1;
        private ComboBox comboBox1;
        private System.Windows.Forms.TextBox PlaceBox;

        private void button1_Click(object sender, EventArgs e)
        {
            int ind = Convert.ToInt32(comboBox1.SelectedValue);
            db.Set_rate(Convert.ToInt32(comboBox1.SelectedValue), globalid);
            MessageBox.Show("Вы успешно оформили подписку " + comboBox1.Text + " !", "Успешно", MessageBoxButtons.OK,
MessageBoxIcon.Asterisk);
            
    
            if (db.Usr_status(globalid) == 1)
            {
                Subscribe.Text = "Подписка: Базовая";
            }
            else if (db.Usr_status(globalid) == 2)
            {
                Subscribe.Text = "Подписка: Арендодатель";

            }
            else if (db.Usr_status(globalid) == 3)
            {
                Subscribe.Text = "Подписка: Бизнес";
            }
        }
    }
}