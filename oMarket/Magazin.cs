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
    public partial class Store : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        bool havestoreinfo = false;
        public Store()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadStore();
        }

        public void LoadStore()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tbStore", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    havestoreinfo = true;
                    txtStName.Text = dr["store"].ToString();
                    txtAddress.Text = dr["address"].ToString();
                }
                else
                {
                    txtStName.Clear();
                    txtAddress.Clear();
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare");
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Salvati detaliile magazinului?", "Confirmare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    if(havestoreinfo)
                    {
                        dbcon.ExecuteQuery("UPDATE tbStore SET store = '" + txtStName.Text + "', address= '" + txtAddress.Text + "'");
                    }
                    else
                    {
                        dbcon.ExecuteQuery("INSERT INTO tbStore (store,address) VALUES ('" + txtStName.Text + "','" + txtAddress.Text + "')");
                    }
                MessageBox.Show("Detaliile magazinului au fost salvate!", "Salvare inregistrare", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Store_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)
            { this.Dispose(); }
        }
    }
}
