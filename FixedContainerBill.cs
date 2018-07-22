using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transport.DataSet;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

namespace Transport
{
    public partial class FixedContainerBill : Form
    {
        DataSet1 ds;
        public FixedContainerBill()
        {
            InitializeComponent();
        }

       
        private void FixedContainerBill_Load(object sender, EventArgs e)
        {
            ds = new DataSet1();
            GridShow();
            Combo();
            getDestination();
            cboParty.SelectedIndex = -1;
            cboDestination.SelectedIndex = -1;

        }
        private void getDestination()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetDestination", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboDestination.DataSource = null;
                cboDestination.DataSource = dt;
                cboDestination.DisplayMember = "Destination";
                cboDestination.ValueMember = "DestId";

                if (cboDestination.Items.Count > 1)
                {
                    cboDestination.SelectedIndex = 0;
                }

                cmd.Dispose();

            }
            catch (Exception ex)
            { }
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

                
            }
            catch (Exception ex)
            { }
        }
        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetFixedBill", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 10;
                dataGridView1.Columns[2].Visible = false;

                dataGridView1.Columns[3].Visible = false;
                //dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;

                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;

                dataGridView1.Columns[12].Visible = false;

            }
            catch (Exception ex)
            { }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridShow();
        }

        private void txtNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                calc();
            }
            catch (Exception ex)
            {
                txtTotal.Text = "0";
                //MessageBox.Show(ex.Message);
            }
        }
        private void calc()
        {
            try
            {
                txtTotal.Text = (Convert.ToDecimal(txtNo.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
            }
            catch (Exception ex)
            {
                txtTotal.Text = "0";
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            calc();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInvoice.Text = "";
            BillDate.Value = DateTime.Today;
            cboParty.SelectedIndex = -1;

            cboDestination.SelectedIndex = -1;
            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today;
            txtType.Text = "";
            txtNo.Text = "";
            txtRate.Text = "";
            txtTotal.Text = "";

            lblID.Text = "";

        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtInvoice.Text == "")
                {
                    MessageBox.Show("Please enter the invoice number");
                    txtInvoice.Focus();
                    return;
                }
                if (cboParty.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid party");
                    cboParty.Focus();
                    return;
                }
                if (cboDestination.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid destination");
                    cboDestination.Focus();
                    return;
                }
                if (txtType.Text == "")
                {
                    MessageBox.Show("Please enter the type");
                    txtType.Focus();
                    return;
                }
                try
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("dbo.GetFixedBillReport", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text;
                    DataTable dt = new DataTable();

                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Invoice number already exists.");
                        txtInvoice.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                { }

                DialogResult dialogResult = MessageBox.Show("Do you want to save the bill?", "Fixed Bill", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("FixedBillEntry", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text;
                    cmd.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = BillDate.Value;
                    cmd.Parameters.Add("@partyid", SqlDbType.Int).Value = cboParty.SelectedValue;
                    cmd.Parameters.Add("@DestId", SqlDbType.Int).Value = cboDestination.SelectedValue;                    
                    cmd.Parameters.Add("@DtFrom", SqlDbType.DateTime).Value = dtFrom.Value;
                    cmd.Parameters.Add("@DtTo", SqlDbType.DateTime).Value = dtTo.Value;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = txtType.Text;
                    cmd.Parameters.Add("@NoOfContainers", SqlDbType.Int).Value = txtNo.Text;
                    cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = txtRate.Text;
                    cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = txtTotal.Text;


                    if (lblID.Text != "")
                    {
                        cmd.Parameters.Add("@BillNo", SqlDbType.Int).Value = lblID.Text;
                    }

                    cmd.ExecuteScalar();
                    Connections.Instance.CloseConnection();
                    cmd.Dispose();

                    GridShow();

                    DialogResult dialogResult1 = MessageBox.Show("Do you want to print the bill?", "Fixed Bill", MessageBoxButtons.YesNo);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        btnPrint_Click(null, null);
                    }
                    btnClear_Click(null, null);
                }
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
                SqlCommand cmd = new SqlCommand("dbo.GetFixedBillReport", Connections.Instance.con);
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
                SqlCommand cmd2 = new SqlCommand("dbo.GetFixedBillReport", Connections.Instance.con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@InvoiceNo", SqlDbType.VarChar).Value = txtInvoice.Text.ToString();
                dt2.Load(cmd2.ExecuteReader());
                Connections.Instance.CloseConnection();
                cmd2.Dispose();


            }
            catch (Exception ex)
            { }

            ds.Tables["FixedBill"].Clear();
            ds.Tables["FixedBill"].Merge(dt2);

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Reports\rptFixedBill.rpt");
            cryRpt.SetDataSource(ds);
            cryRpt.Refresh();
            cryRpt.PrintToPrinter(1, true, 0, 0);

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtInvoice.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    BillDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    cboParty.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cboDestination.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                   
                    dtFrom.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                    dtFrom.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());

                    txtType.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtNo.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    txtRate.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();


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
