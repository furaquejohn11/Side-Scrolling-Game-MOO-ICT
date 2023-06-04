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
    public partial class FormLeaderBoard : Form
    {
        private string strConnection = "Data Source = usersdb.sqlite3";
        public FormLeaderBoard()
        {
            InitializeComponent();
        }

        private void FormLeaderBoard_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (var con = new SQLiteConnection(strConnection))
                {
                    con.Open();

                    string query = "SELECT * FROM tblUsers ORDER BY scores DESC";

                    using (var command = new SQLiteCommand(query, con))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader["users"].ToString(), reader["scores"]);
                                
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

       
    }
}
