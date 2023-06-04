using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Side_Scrolling_Game_MOO_ICT
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();
        }



        private string strConnection = "Data Source = usersdb.sqlite3";
        private void FormHome_Load(object sender, EventArgs e)
        {
            LoadUsers();
            btnPlay.Visible = false;
        }


        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString());
            Form1 form1 = new Form1(comboBox1.SelectedItem.ToString());
            form1.Show();
            this.Hide();
        }
        private void LoadUsers()
        {
            try
            {
                using (var con = new SQLiteConnection(strConnection))
                {
                    con.Open();

                    string query = "SELECT * FROM tblUsers";

                    using (var command = new SQLiteCommand(query, con))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string username = reader.GetString(0);
                                comboBox1.Items.Add(username);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                btnPlay.Visible = true;
            }
            else
            {
                btnPlay.Visible = false;
            }
        }

        //Link label modify
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormModify formModify = new FormModify();
            formModify.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormLeaderBoard formLeaderBoard = new FormLeaderBoard();
            formLeaderBoard.ShowDialog();
        }
    }
}
