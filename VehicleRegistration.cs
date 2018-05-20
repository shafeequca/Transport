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
    public partial class VehicleRegistration : Form
    {
        public VehicleRegistration()
        {
            InitializeComponent();
        }

        private void VehicleRegistration_Load(object sender, EventArgs e)
        {
            GridShow();
        }
        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetVehicleRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Vehicle Name";
                dataGridView1.Columns[2].HeaderText = "Vehicle Number";
                

            }
            catch (Exception ex)
            { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtVehicleName.Text = "";
            txtNumber.Text = "";
            txtSearch.Text = "";
            lblID.Text = "";
            txtVehicleName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVehicleName.Text == "")
                {
                    MessageBox.Show("Please enter the vehicle name");
                    txtVehicleName.Focus();
                    return;
                }
                if (txtNumber.Text =="") 
                {
                    MessageBox.Show("Please enter the vehicle number");
                    txtNumber.Focus();
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to save the data?", "Vehicle Registration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("VehicleRegistration", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VehicleName", SqlDbType.VarChar).Value = txtVehicleName.Text;
                    cmd.Parameters.Add("@VehicleNumber", SqlDbType.VarChar).Value = txtNumber.Text;
                    cmd.Parameters.Add("@VehicleType", SqlDbType.VarChar).Value = cboType.Text;
                    if (lblID.Text != "")
                    {
                        cmd.Parameters.Add("@Vehid", SqlDbType.Int).Value = lblID.Text;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtVehicleName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            { }

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
                    MessageBox.Show("Please select a valid vehicle");
                    
                    return;
                }
               DialogResult dialogResult = MessageBox.Show("Do you want to delete the data?", "Vehicle Registration", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {
                   Connections.Instance.OpenConection();
                   SqlCommand cmd = new SqlCommand("VehicleRegistration", Connections.Instance.con);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.Add("@Vehid", SqlDbType.Int).Value = lblID.Text;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
