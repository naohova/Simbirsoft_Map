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
using System.Text.RegularExpressions;

namespace map
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            name1.Text = "Имя";
            name2.Text = "Фамилия";
            log1.Text = "Логин";
            passw1.Text = "Пароль";
            passw2.Text = "Подтверждение пароля";
            name1.ForeColor = Color.LightGray;
            name2.ForeColor = Color.LightGray;
            log1.ForeColor = Color.LightGray;
            passw1.ForeColor = Color.LightGray;
            passw2.ForeColor = Color.LightGray;
        }

        private Button button1;
        private GroupBox groupBox1;
        private TextBox name1;
        private TextBox name2;
        private GroupBox groupBox2;
        private TextBox passw2;
        private TextBox passw1;
        private TextBox log1;
        private Button button2;
        private Panel panel1;
        dbworker db = new dbworker(bd_CON_VAL.server, bd_CON_VAL.user, bd_CON_VAL.pass, "Liorkin");

        private void name1_Enter(object sender, EventArgs e)
        {
            if (name1.Text == "Имя")
            {
                name1.Text = "";
                name1.ForeColor = Color.Black;
            }
        }

        private void name1_Leave(object sender, EventArgs e)
        {
            if (name1.Text == "")
            {
                name1.Text = "Имя";
                name1.ForeColor = Color.LightGray;
            }
        }

        private void name2_Enter(object sender, EventArgs e)
        {
            if (name2.Text == "Фамилия")
            {
                name2.Text = "";
                name2.ForeColor = Color.Black;
            }
        }

        private void name2_Leave(object sender, EventArgs e)
        {
            if (name2.Text == "")
            {
                name2.Text = "Фамилия";
                name2.ForeColor = Color.LightGray;
            }
        }

        private void log1_Enter(object sender, EventArgs e)
        {
            if (log1.Text == "Логин")
            {
                log1.Text = "";
                log1.ForeColor = Color.Black;
            }
        }

        private void log1_Leave(object sender, EventArgs e)
        {
            if (log1.Text == "")
            {
                log1.Text = "Логин";
                log1.ForeColor = Color.LightGray;
            }
        }

        private void passw1_Enter(object sender, EventArgs e)
        {
            if (passw1.Text == "Пароль")
            {
                passw1.Text = "";
                passw1.ForeColor = Color.Black;
            }
        }

        private void passw1_Leave(object sender, EventArgs e)
        {
            if (passw1.Text == "")
                passw1.Text = "Пароль";

            passw1.ForeColor = Color.LightGray;
        }

        private void passw2_Enter(object sender, EventArgs e)
        {
            if (passw2.Text == "Подтверждение пароля")
                passw2.Text = "";

            passw2.ForeColor = Color.Black;
        }

        private void passw2_Leave(object sender, EventArgs e)
        {
            if (passw2.Text == "")
                passw2.Text = "Подтверждение пароля";

            passw2.ForeColor = Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string check_login = log1.Text.ToString();
            string check_password = passw1.Text.ToString();
            int error_index = 0;
            for (int i = 0; i < log1.Text.Length; i++)
            {
                if (check_login[i] == ' ')
                {
                    error_index++;
                    break;
                }

            }
            if ((name1.Text.Length < 2) || (name1.Text.Length > 50))
            {
                MessageBox.Show("Неверная длина имени!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if ((name2.Text.Length < 2) || (name2.Text.Length > 50))
            {
                MessageBox.Show("Неверная длина фамилии!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if ((log1.Text.Length < 4) || (log1.Text.Length > 20))
            {
                MessageBox.Show("Неверная длина логина!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if ((passw1.Text.Length < 8) || (passw1.Text.Length > 15))
            {
                MessageBox.Show("Неверная длина пароля!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (!Regex.Match(name1.Text, @"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,50}$").Success)
            {
                MessageBox.Show("Имя не должно содержить цифр и символов!(кроме необходимых) )", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (!Regex.Match(name2.Text, @"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,20}$").Success)
            {
                MessageBox.Show("Фамилия не должна содержить цифр и символов!(кроме необходимых) )", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (error_index != 0)
            {
                MessageBox.Show("Логин не должен содержать пробела!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if ((name1.Text == "Имя") || (name2.Text == "Фамилия") || (log1.Text == "Логин") || (passw1.Text == "Пароль") || (passw2.Text == "Подтверждение пароля"))
            {
                MessageBox.Show("Вы заполнили не все поля...", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (!Regex.Match(passw1.Text, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]").Success)
            {
                MessageBox.Show("Пароль должен содержать цифры, буквы верхнего и нижнего регистра", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (db.check(log1.Text))
            {
                return;
            }
           
            db.Add_User(name1.Text, name2.Text, log1.Text, passw1.Text, passw2.Text, 1);
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.name1 = new System.Windows.Forms.TextBox();
            this.name2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.passw2 = new System.Windows.Forms.TextBox();
            this.passw1 = new System.Windows.Forms.TextBox();
            this.log1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(87, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "Зарегистрироваться";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.name1);
            this.groupBox1.Controls.Add(this.name2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Введите имя и фамилию";
            // 
            // name1
            // 
            this.name1.Location = new System.Drawing.Point(6, 19);
            this.name1.Name = "name1";
            this.name1.Size = new System.Drawing.Size(194, 20);
            this.name1.TabIndex = 3;
            this.name1.Enter += new System.EventHandler(this.name1_Enter);
            this.name1.Leave += new System.EventHandler(this.name1_Leave);
            // 
            // name2
            // 
            this.name2.Location = new System.Drawing.Point(6, 45);
            this.name2.Name = "name2";
            this.name2.Size = new System.Drawing.Size(194, 20);
            this.name2.TabIndex = 7;
            this.name2.Enter += new System.EventHandler(this.name2_Enter);
            this.name2.Leave += new System.EventHandler(this.name2_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.passw2);
            this.groupBox2.Controls.Add(this.passw1);
            this.groupBox2.Controls.Add(this.log1);
            this.groupBox2.Location = new System.Drawing.Point(12, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 100);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Введите логин и пароль";
            // 
            // passw2
            // 
            this.passw2.Location = new System.Drawing.Point(6, 74);
            this.passw2.Name = "passw2";
            this.passw2.Size = new System.Drawing.Size(194, 20);
            this.passw2.TabIndex = 6;
            this.passw2.Enter += new System.EventHandler(this.passw2_Enter);
            this.passw2.Leave += new System.EventHandler(this.passw2_Leave);
            // 
            // passw1
            // 
            this.passw1.Location = new System.Drawing.Point(6, 48);
            this.passw1.Name = "passw1";
            this.passw1.Size = new System.Drawing.Size(194, 20);
            this.passw1.TabIndex = 4;
            this.passw1.Enter += new System.EventHandler(this.passw1_Enter);
            this.passw1.Leave += new System.EventHandler(this.passw1_Leave);
            // 
            // log1
            // 
            this.log1.Location = new System.Drawing.Point(6, 22);
            this.log1.Name = "log1";
            this.log1.Size = new System.Drawing.Size(194, 20);
            this.log1.TabIndex = 8;
            this.log1.Enter += new System.EventHandler(this.log1_Enter);
            this.log1.Leave += new System.EventHandler(this.log1_Leave);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(240)))), ((int)(((byte)(210)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(12, 198);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 30);
            this.button2.TabIndex = 12;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 240);
            this.panel1.TabIndex = 1;
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 240);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "register";
            this.Text = "Регистрация";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
