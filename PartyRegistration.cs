using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Transport
{
    public partial class PartyRegistration : Form
    {
        public PartyRegistration()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PartyRegistration_Load(object sender, EventArgs e)
        {
            GridShow();
        }
        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetPartyRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Party";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                

            }
            catch (Exception ex)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter the name");
                    txtName.Focus();
                    return;
                }
                

                DialogResult dialogResult = MessageBox.Show("Do you want to save the data?", "Party Registration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("PartyRegistration", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PartyName", SqlDbType.VarChar).Value = txtName.Text;
                    cmd.Parameters.Add("@PartyAddress", SqlDbType.VarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@PartyNumber", SqlDbType.VarChar).Value = txtContactNumber.Text;
                    
                    if (lblID.Text != "")
                    {
                        cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = lblID.Text;
                    }

                    cmd.ExecuteScalar();
                    Connections.Instance.CloseConnection();
                    cmd.Dispose();

                    GridShow();
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            { } 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtContactNumber.Text = "";
            txtSearch.Text = "";
            lblID.Text = "";
            txtName.Focus();
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridShow();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "")
                {
                    MessageBox.Show("Please select a valid party");

                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to delete the data?", "Party Registration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("PartyRegistration", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Partyid", SqlDbType.Int).Value = lblID.Text;
                    cmd.Parameters.Add("@IsDelete", SqlDbType.Int).Value = "1";

                    cmd.ExecuteScalar();
                    Connections.Instance.CloseConnection();
                    cmd.Dispose();

                    GridShow();
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            { } 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtContactNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    
                }
            }
            catch (Exception ex)
            { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
