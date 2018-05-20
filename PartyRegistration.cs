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
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].HeaderText = "Km.";
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Rate 20";
                dataGridView1.Columns[12].HeaderText = "Rate 40";


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
                if (txtLocation.Text == "")
                {
                    MessageBox.Show("Please enter the location");
                    txtLocation.Focus();
                    return;
                }
                if (txtKilometer.Text == "")
                {
                    MessageBox.Show("Please enter the kilometer");
                    txtKilometer.Focus();
                    return;
                }
                if (txtDriverBata20.Text == "")
                {
                    MessageBox.Show("Please enter the driver bata(20)");
                    txtDriverBata20.Focus();
                    return;
                }
                if (txtDriverBata40.Text == "")
                {
                    MessageBox.Show("Please enter the driver bata(40)");
                    txtDriverBata40.Focus();
                    return;
                }
                if (txtCleanerBata20.Text == "")
                {
                    MessageBox.Show("Please enter the cleaner bata(20)");
                    txtCleanerBata20.Focus();
                    return;
                }
                if (txtCleanerBata40.Text == "")
                {
                    MessageBox.Show("Please enter the cleaner bata(40)");
                    txtCleanerBata40.Focus();
                    return;
                }
                if (txtRate20.Text == "")
                {
                    MessageBox.Show("Please enter the 20 FT Rate");
                    txtRate20.Focus();
                    return;
                }
                if (txtRate40.Text == "")
                {
                    MessageBox.Show("Please enter the 40 FT Rate");
                    txtRate40.Focus();
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
                    cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = txtLocation.Text;
                    cmd.Parameters.Add("@Kilometer", SqlDbType.Decimal).Value = txtKilometer.Text;
                    cmd.Parameters.Add("@DriverBata20", SqlDbType.Decimal).Value = txtDriverBata20.Text;
                    cmd.Parameters.Add("@CleanerBata20", SqlDbType.Decimal).Value = txtCleanerBata20.Text;
                    cmd.Parameters.Add("@DriverBata40", SqlDbType.Decimal).Value = txtDriverBata40.Text;
                    cmd.Parameters.Add("@CleanerBata40", SqlDbType.Decimal).Value = txtCleanerBata40.Text;
                    cmd.Parameters.Add("@Rate20", SqlDbType.Decimal).Value = txtRate20.Text;
                    cmd.Parameters.Add("@Rate40", SqlDbType.Decimal).Value = txtRate40.Text;


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
            txtLocation.Text = "";
            txtKilometer.Text = "";
            txtDriverBata40.Text = "";
            txtCleanerBata20.Text = "";
            txtDriverBata20.Text = "";
            txtCleanerBata40.Text = "";
            txtRate20.Text = "";
            txtRate40.Text = "";
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
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtContactNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtLocation.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    txtKilometer.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtDriverBata20.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtDriverBata40.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtCleanerBata20.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtCleanerBata40.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txtRate20.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtRate40.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();


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
