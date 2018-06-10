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
    public partial class DieselExpense : Form
    {
        public DieselExpense()
        {
            InitializeComponent();
        }

        private void txtExpence_TextChanged(object sender, EventArgs e)
        {
            calc();
        }
        private void calc()
        {
            txtTotal.Text = (Convert.ToDecimal(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDecimal(txtRate.Text == "" ? "0" : txtRate.Text)).ToString(); 
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboVehicle.SelectedIndex = -1;
            cboVehicle.Text ="";
            
            txtRate.Text = "";
            txtQty.Text = "";
            txtTotal.Text = "";
            lblID.Text = "";


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtTotal.Text) == 0)
                {
                    MessageBox.Show("Please enter the value");
                    txtQty.Focus();
                    return;
                }
                if (cboVehicle.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a vehicle");
                    cboVehicle.Focus();
                    return;
                }
               
                DialogResult dialogResult = MessageBox.Show("Do you want to save the data?", "Diesel Entry", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("DieselEntry", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                    cmd.Parameters.Add("@Qty", SqlDbType.Decimal).Value = txtQty.Text;
                    cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = txtRate.Text;
                    cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = txtTotal.Text;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    if (lblID.Text !="" )
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = lblID.Text.ToString();
                        
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

        private void DieselExpense_Load(object sender, EventArgs e)
        {
            comboLoad();
            GridShow();
        }
        private void comboLoad()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetVehicleRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = "";
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboVehicle.DataSource = null;
                cboVehicle.DataSource = dt;
                cboVehicle.DisplayMember = "VehicleNumber";
                cboVehicle.ValueMember = "VehId";

               
            }
            catch (Exception ex)
            { }
        }
        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetDieselEntry", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[4].Visible = false;


            }
            catch (Exception ex)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblID.Text == "")
                {
                    MessageBox.Show("Please select a valid entry");

                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to delete the data?", "Diesel Entry", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("DieselEntry", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = lblID.Text;
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
                    cboVehicle.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtRate.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                }
            }
            catch (Exception ex)
            { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridShow();
        }
    }
}
