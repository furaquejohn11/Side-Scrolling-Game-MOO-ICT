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
    public partial class FormModify : Form
    {
        public FormModify()
        {
            InitializeComponent();
        }

        private string strConnection = "Data Source = usersdb.sqlite3";

        private void FormModify_Load(object sender, EventArgs e)
        {
            btnDelete.Visible = false;
            LoadUsers();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult msg = MessageBox.Show("Are you sure you want to delete user: " +
                    comboBox1.SelectedItem.ToString() + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msg == DialogResult.Yes)
                {

                    using (var con = new SQLiteConnection(strConnection))
                    {
                        con.Open();

                        string query = "DELETE FROM tblUsers WHERE users = @users";

                        using (var command = new SQLiteCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@users", comboBox1.SelectedItem.ToString());

                            command.ExecuteNonQuery();
                        }
                    }
                    this.Close();
                   
                }
                else
                {
                    comboBox1.SelectedIndex = -1;
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
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" || textBox2.Text != "")
                {
                    if (textBox1.Text == textBox2.Text)
                    {
                        using (var con = new SQLiteConnection(strConnection))
                        {
                            con.Open();


                            // Check if the user is existing
                            string checkQuery = "SELECT COUNT(*) FROM tblUsers WHERE users = @users";
                            using (var checkCommand = new SQLiteCommand(checkQuery, con))
                            {
                                checkCommand.Parameters.AddWithValue("@users", textBox2.Text);

                                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());


                                // Insert Account if it is not existing
                                if (userCount == 0)
                                {
                                    string addUser = "INSERT INTO tblUsers(users, scores)VALUES(@users, @scores)";

                                    using (var addCommand = new SQLiteCommand(addUser, con))
                                    {
                                        addCommand.Parameters.AddWithValue("@users", textBox2.Text);
                                        addCommand.Parameters.AddWithValue("@scores", 0);

                                        addCommand.ExecuteNonQuery();

                                        MessageBox.Show("New User Added");



                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("User is already existing! Try Again!");
                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Users not matched");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Users must not be empty!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void FormModify_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormHome formHome = new FormHome();
            formHome.Show();
        }
    }
}
