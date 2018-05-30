using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Transport.DataSet;
using CrystalDecisions.CrystalReports.Engine;

namespace Transport
{
    public partial class DriverBataDetails : Form
    {
        DataSet1 ds;
        public DriverBataDetails()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void DriverBataDetails_Load(object sender, EventArgs e)
        {
            ds = new DataSet1();
            getDriver();
        }
        private void GridShow()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetDriverBata", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = cboDriver.SelectedValue.ToString();
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value.ToString();

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;


                SqlCommand cmd1 = new SqlCommand("dbo.GetDriverBataSummary", Connections.Instance.con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DriverId", SqlDbType.Int).Value = cboDriver.SelectedValue.ToString();
                cmd1.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                cmd1.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value.ToString();

                DataTable dt1 = new DataTable();

                dt1.Load(cmd1.ExecuteReader());
                if (dt1.Rows.Count > 0)
                {
                    lblDriverBata.Text = (dt1.Rows[0][0].ToString() == "") ? "0.00" : dt1.Rows[0][0].ToString();
                    lblCleanerBata.Text = (dt1.Rows[0][1].ToString() == "") ? "0.00" : dt1.Rows[0][1].ToString();
                    lblAdvance.Text = (dt1.Rows[0][2].ToString() == "") ? "0.00" : dt1.Rows[0][2].ToString();
                    lblExpense.Text = (dt1.Rows[0][3].ToString() == "") ? "0.00" : dt1.Rows[0][3].ToString();
                    lblBalance.Text = (dt1.Rows[0][4].ToString() == "") ? "0.00" : dt1.Rows[0][4].ToString(); 
                }
                else
                {
                    lblDriverBata.Text = "0.00";
                    lblCleanerBata.Text = "0.00";
                    lblAdvance.Text = "0.00";
                    lblExpense.Text = "0.00";
                    lblBalance.Text = "0.00";
                }
                Connections.Instance.CloseConnection();
                cmd.Dispose();
               

            }
            catch (Exception ex)
            { }
        }
        private void getDriver()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetDriverRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = "";
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboDriver.DataSource = null;
                cboDriver.DataSource = dt;
                cboDriver.DisplayMember = "DriverName";
                cboDriver.ValueMember = "DriverId";

                if (cboDriver.Items.Count > 1)
                {
                    cboDriver.SelectedIndex = 0;
                }
               
                Connections.Instance.CloseConnection();
                cmd.Dispose();
               
            }
            catch (Exception ex)
            { }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            GridShow();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            System.Data.DataColumn Driver = new System.Data.DataColumn("Driver", typeof(System.String));
            Driver.DefaultValue = cboDriver.Text;
            
            System.Data.DataColumn DtFrom = new System.Data.DataColumn("DtFrom", typeof(System.String));
            DtFrom.DefaultValue = dateTimePicker1.Value.ToString("dd-MM-yyyy"); 
            System.Data.DataColumn DtTo = new System.Data.DataColumn("DtTo", typeof(System.String));
            DtTo.DefaultValue = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            DataTable dt = new DataTable();

            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetDriverBata", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = cboDriver.SelectedValue.ToString();
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value.ToString();


                dt.Load(cmd.ExecuteReader());
                Connections.Instance.CloseConnection();
                cmd.Dispose();


            }
            catch (Exception ex)
            { }


            dt.Columns.Add(Driver);
            dt.Columns.Add(DtTo);
            dt.Columns.Add(DtFrom);

            //dtCloned.Columns[4].DataType = typeof(Decimal);
            //dtCloned.Columns[5].DataType = typeof(Decimal);
            //dtCloned.Columns[6].DataType = typeof(Decimal);
            //dtCloned.Columns[7].DataType = typeof(Decimal);
            //dtCloned.Columns[8].DataType = typeof(Decimal);
            //dtCloned.Columns[9].DataType = typeof(Decimal);
            //dtCloned.Columns[10].DataType = typeof(Decimal);
            //dtCloned.Columns[11].DataType = typeof(Decimal);
            //dtCloned.Columns[12].DataType = typeof(Decimal);

            //foreach (DataRow row in dt.Rows)
            //{
            //    dtCloned.ImportRow(row);
            //}



            ds.Tables["DriverBata"].Clear();
            ds.Tables["DriverBata"].Merge(dt);

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Reports\rptDriverBata.rpt");
            cryRpt.SetDataSource(ds);
            cryRpt.Refresh();
            cryRpt.PrintToPrinter(1, true, 0, 0);
        }
    }
}
