using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;


namespace map
{
    public partial class Login : Form
    {
        MySqlConnectionStringBuilder Connect = new MySqlConnectionStringBuilder();
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");

        public Login()
        {
            InitializeComponent();
            login_string.Text = "Логин";
            password_string.Text = "Пароль";
            login_string.ForeColor = Color.LightGray;
            password_string.ForeColor = Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.login(login_string.Text, password_string.Text))
            { this.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_string_Enter(object sender, EventArgs e)
        {
            if (login_string.Text == "Логин")
            {
                login_string.Text = "";
                login_string.ForeColor = Color.Black;
            }
        }

        private void login_string_Leave(object sender, EventArgs e)
        {
            if (login_string.Text == "")
            {
                login_string.Text = "Логин";
                login_string.ForeColor = Color.LightGray;
            }
        }

        private void password_string_Enter(object sender, EventArgs e)
        {
            if (password_string.Text == "Пароль")
            {
                password_string.Text = "";
                password_string.ForeColor = Color.Black;
            }
        }

        private void password_string_Leave(object sender, EventArgs e)
        {
            if (password_string.Text == "")
            {
                password_string.Text = "Пароль";
                password_string.ForeColor = Color.LightGray;
            }
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.password_string = new System.Windows.Forms.TextBox();
            this.login_string = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.password_string);
            this.panel1.Controls.Add(this.login_string);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 135);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(17, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(115, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "Войти";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // password_string
            // 
            this.password_string.BackColor = System.Drawing.SystemColors.Control;
            this.password_string.Location = new System.Drawing.Point(17, 64);
            this.password_string.Name = "password_string";
            this.password_string.Size = new System.Drawing.Size(181, 20);
            this.password_string.TabIndex = 4;
            this.password_string.Enter += new System.EventHandler(this.password_string_Enter);
            this.password_string.Leave += new System.EventHandler(this.password_string_Leave);
            // 
            // login_string
            // 
            this.login_string.BackColor = System.Drawing.SystemColors.Control;
            this.login_string.Location = new System.Drawing.Point(17, 38);
            this.login_string.Name = "login_string";
            this.login_string.Size = new System.Drawing.Size(181, 20);
            this.login_string.TabIndex = 3;
            this.login_string.Enter += new System.EventHandler(this.login_string_Enter);
            this.login_string.Leave += new System.EventHandler(this.login_string_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Авторизация";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 135);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Login";
            this.Text = "Авторизация";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox password_string;
        private System.Windows.Forms.TextBox login_string;
        private System.Windows.Forms.Button button2;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
