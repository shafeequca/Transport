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
    public partial class FixedExpenses : Form
    {
        public FixedExpenses()
        {
            InitializeComponent();
        }

        private void FixedExpenses_Load(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("dbo.GetFixedExpenses", Connections.Instance.con);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExpense.Text.Trim()=="")
                {
                    MessageBox.Show("Please enter the value");
                    txtExpense.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtAmount.Text) == 0)
                {
                    MessageBox.Show("Please enter the value");
                    txtAmount.Focus();
                    return;
                }
                if (cboVehicle.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a vehicle");
                    cboVehicle.Focus();
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to save the data?", "Expense Entry", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("FixedExpenseEntry", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                    cmd.Parameters.Add("@DTFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    cmd.Parameters.Add("@DTTo", SqlDbType.DateTime).Value = dateTimePicker2.Value;

                    cmd.Parameters.Add("@Expense", SqlDbType.Text).Value = txtExpense.Text;
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtAmount.Text;

                    if (lblID.Text != "")
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboVehicle.SelectedIndex = -1;
            cboVehicle.Text = "";
            txtSearch.Text = "";
            txtExpense.Text = "";
            txtAmount.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;

            lblID.Text = "";

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
                DialogResult dialogResult = MessageBox.Show("Do you want to delete the data?", "Expense Entry", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("FixedExpenseEntry", Connections.Instance.con);
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
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    
                    txtExpense.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    cboVehicle.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                }
            }
            catch (Exception ex)
            { }

        }

    }
}
