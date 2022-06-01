using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oMarket
{
    public partial class DetaliiUtilizator : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();

        ContUtilizator account;
        public string username;
        public DetaliiUtilizator(ContUtilizator user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            account = user;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

  
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Sunteti sigur ca vreti sa modificati propritatile contului?", "Schimbare proprietati cont", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                 {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tbUser SET name=@name, role=@role, isactivate=@isactivate WHERE username='" + username + "'",cn);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@role", cbRole.Text);
                    cm.Parameters.AddWithValue("@isactivate", cbActivate.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Proprietatile contului au fost actualizate!", "Actualizare proprietati cont", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    account.LoadUser();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UserProperties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
