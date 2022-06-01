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
    public partial class SchimbaParola : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        MainformCasier cashier;
        public SchimbaParola(MainformCasier cash)
        {
            InitializeComponent();
            cashier = cash;
            lblUsername.Text = cashier.lblUsername.Text;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string oldpass = dbcon.getPassword(lblUsername.Text);
                if(oldpass != txtPass.Text)
                {
                    MessageBox.Show("Parola gresita, incercati din nou!", "Atentionare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    txtPass.Visible = false;
                    btnNext.Visible = false;

                    txtNewPass.Visible = true;
                    txtComPass.Visible = true;
                    btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtNewPass.Text != txtComPass.Text)
                {
                    MessageBox.Show("Noua parola si confirmarea ei nu corespund!", "Atentionare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if(MessageBox.Show("Schimbati parola?","Confirmare",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                    {
                        dbcon.ExecuteQuery("UPDATE tbUser set password = '" + txtNewPass.Text + "' WHERE username = '" + lblUsername.Text + "'");
                        MessageBox.Show("Parola a fost actualizata!", "Salvare actualizari", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare");
            }
        }

        private void ChangePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }

        }
    }
}
