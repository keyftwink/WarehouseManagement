using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace laboratorka
{
    public partial class SignUp : Form
    {

        DataBase dataBase = new DataBase();

        public SignUp()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Вы не заполнили все поля!");
                return;
            }

            if (checkUser())
            {
                return;
            }

            var name = textBox4.Text;
            var surname = textBox5.Text;
            var post = textBox6.Text;
            var login = textBox1.Text;
            var password = md5.hashPassword(textBox2.Text);

            string querystring = $"insert into accountDetails(name, surname, post, login_user, password_user, is_admin, is_operator) values('{name}', '{surname}', '{post}', '{login}', '{password}', 0, 0)";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан", "Успешно");
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.closeConnection();
        }

        private Boolean checkUser()
        {
            var loginUser = textBox1.Text;
            var passwordUser = textBox2.Text;
            var passwordUserConfirm = textBox3.Text;

            if(passwordUser != passwordUserConfirm)
            {
                MessageBox.Show("Пароли не совпадают");
                return true;

            }
            string querystring = $"select COUNT(*) from accountDetails where login_user = '{loginUser}'";
            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.openConnection();
            if (Convert.ToInt32(command.ExecuteScalar()) > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                dataBase.closeConnection();
                return true;
            }
            else
            {
                dataBase.closeConnection();
                return false; 
            }

        }

    }
}
