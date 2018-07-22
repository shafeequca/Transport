using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using Transport.DataSet;

namespace Transport
{
    public partial class BillGeneration : Form
    {
        DataSet1 ds;
        public BillGeneration()
        {
            InitializeComponent();
        }

        private void BillGeneration_Load(object sender, EventArgs e)
        {
            ds = new DataSet1();
            GridShow();
            Combo();

        }
        private void Combo()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetParty", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboParty.DataSource = null;
                cboParty.DataSource = dt;
                cboParty.DisplayMember = "PartyName";
                cboParty.ValueMember = "PartyId";

               

                cmd.Dispose();

                SqlCommand cmd1 = new SqlCommand("dbo.GetCategory", Connections.Instance.con);
                cmd1.CommandType = CommandType.StoredProcedure;
                DataTable dt1 = new DataTable();

                dt1.Load(cmd1.ExecuteReader());
                cboCategory.DataSource = null;
                cboCategory.DataSource = dt1;
                cboCategory.DisplayMember = "Category";
                cboCategory.ValueMember = "CatID";

                if (cboCategory.Items.Count > 1)
                {
                    cboCategory.SelectedIndex = 0;
                }

                cmd1.Dispose();

                SqlCommand cmd2 = new SqlCommand("dbo.GetVehicleRegistration", Connections.Instance.con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@Search", SqlDbType.VarChar).Value = "";                
                DataTable dt2 = new DataTable();

                dt2.Load(cmd2.ExecuteReader());
                cboVehicle.DataSource = null;
                cboVehicle.DataSource = dt2;
                cboVehicle.DisplayMember = "VehicleNumber";
                cboVehicle.ValueMember = "VehID";

                cboVehicle.SelectedIndex = -1;
                

                cmd2.Dispose();
            }
            catch (Exception ex)
            { }
        }
        private void Show()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetTripForBill", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = cboParty.SelectedValue.ToString();
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = cboCategory.Text.ToString();
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value;
                cmd.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                
                dataGridView1.Columns[0].Visible = false;
                

            }
            catch (Exception ex)
            { }
        }

        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetBillDetails", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                dataGridView2.Columns[0].Visible = false;
                            
                dataGridView2.Columns[2].Visible = false;
                            
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[7].Visible = false;
                


            }
            catch (Exception ex)
            { }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtInvoice.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the Invoice number");
                txtInvoice.Focus();
                return;
            }
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetBillEntry", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Invoice number already exits");
                    txtInvoice.Focus();
                    return;
                }
                cmd.Dispose();
                dt.Dispose();
                 DialogResult dialogResult = MessageBox.Show("Do you want to generate the bill?", "Bill Generation", MessageBoxButtons.YesNo);
                 if (dialogResult == DialogResult.Yes)
                 {
                     SqlCommand cmd1 = new SqlCommand("dbo.BillEntry", Connections.Instance.con);
                     cmd1.CommandType = CommandType.StoredProcedure;
                     cmd1.Parameters.Add("@PartyId", SqlDbType.Int).Value = cboParty.SelectedValue;
                     cmd1.Parameters.Add("@Category", SqlDbType.VarChar).Value = cboCategory.Text;
                     cmd1.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text;

                     cmd1.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                     cmd1.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value;

                     cmd1.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = dateTimePicker3.Value;
                     cmd1.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue;
                     
                     cmd1.ExecuteReader();
                 }
                DialogResult dialogResult1 = MessageBox.Show("Do you want to print the bill statement?", "Bill Generation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    btnPrint_Click (null,null);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            GridShow();
            clear();

        }
        private void clear()

        {
            cboCategory.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            dateTimePicker3.Value = DateTime.Today;
            cboParty.SelectedIndex = -1;
            dataGridView1.DataSource = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetBillDetailsSearch", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                dataGridView2.Columns[0].Visible = false;

                dataGridView2.Columns[2].Visible = false;

                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].Visible = false;



            }
            catch (Exception ex)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtInvoice.Text.Trim() == "")
            {
                MessageBox.Show("Please select a proper bill");
                txtInvoice.Focus();
                return;
            }
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetBillDetails", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invoice number does not exits. Please generate bill and try again");
                    txtInvoice.Focus();
                    return;
                }
            }
            catch (Exception ex)
            { }

            DataTable dt2 = new DataTable();

            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd2 = new SqlCommand("dbo.GetBill", Connections.Instance.con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text.ToString();
                dt2.Load(cmd2.ExecuteReader());
                Connections.Instance.CloseConnection();
                cmd2.Dispose();


            }
            catch (Exception ex)
            { }

            ds.Tables["Bill1"].Clear();
            ds.Tables["Bill1"].Merge(dt2);

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Reports\rptBill1.rpt");
            cryRpt.SetDataSource(ds);
            cryRpt.Refresh();
            cryRpt.PrintToPrinter(1, true, 0, 0);


            DataTable dt1 = new DataTable();

            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd1 = new SqlCommand("dbo.GetBillEntry", Connections.Instance.con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text.ToString();
                dt1.Load(cmd1.ExecuteReader());
                Connections.Instance.CloseConnection();
                cmd1.Dispose();


            }
            catch (Exception ex)
            { }

            ds.Tables["Bill"].Clear();
            ds.Tables["Bill"].Merge(dt1);

            ReportDocument cryRpt1 = new ReportDocument();
            cryRpt1.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Reports\BillStatement.rpt");
            cryRpt1.SetDataSource(ds);
            cryRpt1.Refresh();
            cryRpt1.PrintToPrinter(1, true, 0, 0);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    txtInvoice.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dateTimePicker3.Value = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
                    cboParty.SelectedValue = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cboCategory.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString());
                    dateTimePicker2.Value = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString());
                    cboVehicle.SelectedValue = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                    try
                    {
                        Connections.Instance.OpenConection();
                        SqlCommand cmd = new SqlCommand("dbo.GetBillEntry", Connections.Instance.con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text.ToString();

                        DataTable dt = new DataTable();

                        dt.Load(cmd.ExecuteReader());
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;
                        Connections.Instance.CloseConnection();
                        cmd.Dispose();

                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[3].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[10].Visible = false;
    

                    }
                    catch (Exception ex)
                    { }
                }
            }
            catch (Exception ex)
            { }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      }
}
