using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratorka
{
    public partial class Form1 : Form
    {

        DataBase dataBase = new DataBase();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
            pictureBox2.Visible = true;
            textBox1.MaxLength = 60;
            textBox2.MaxLength = 60;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passwordUser = md5.hashPassword(textBox2.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, login_user, password_user, is_admin, is_operator from accountDetails where login_user = '{loginUser}' and password_user = '{passwordUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                var user = new CheckUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]), Convert.ToBoolean(table.Rows[0].ItemArray[4]));

               
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (user.IsAdmin)
                {
                    AdminForm admin = new AdminForm(user);
                    this.Hide();
                    admin.ShowDialog();
                }else if (user.IsOperator)
                {
                    OperatorForm operatorForm = new OperatorForm(user);
                    this.Hide();
                    operatorForm.ShowDialog();
                }
                else
                {
                    SubscriberForm subscriber = new SubscriberForm(user);
                    this.Hide();
                    subscriber.ShowDialog();
                }
            }
          
            else MessageBox.Show("Неправильный логин или пароль!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp frm_sign = new SignUp();
            frm_sign.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = (char)0;
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '●';
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;

        }

      
    }

}
