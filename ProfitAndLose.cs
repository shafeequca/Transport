﻿using System;
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
    public partial class ProfitAndLose : Form
    {
           DataSet1 ds;

        public ProfitAndLose()
        {
            InitializeComponent();
        }

        private void ProfitAndLose_Load(object sender, EventArgs e)
        {
            ds = new DataSet1();

            comboLoad();
            //GridShow();
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
                SqlCommand cmd = new SqlCommand("dbo.GetProfitAndLoss", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue;
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value;

                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                
                cmd.Dispose();

                SqlCommand cmd1 = new SqlCommand("dbo.GetDieselSummary", Connections.Instance.con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                cmd1.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                cmd1.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value.ToString();

                DataTable dt1 = new DataTable();

                dt1.Load(cmd1.ExecuteReader());
                if (dt1.Rows.Count > 0)
                {
                    lblDiesel.Text = (dt1.Rows[0][1].ToString() == "") ? "0.00" : dt1.Rows[0][1].ToString();
                    lblQty.Text = "("+((dt1.Rows[0][0].ToString() == "") ? "0.00" : dt1.Rows[0][0]).ToString()+" Ltr.)";

                }
                else
                {
                    lblDiesel.Text = "0.00";
                    lblQty.Text = "";
                }
                
                cmd1.Dispose();

                SqlCommand cmd2 = new SqlCommand("dbo.GetTripSummary", Connections.Instance.con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                cmd2.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToString();
                cmd2.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value.ToString();

                DataTable dt2 = new DataTable();

                dt2.Load(cmd2.ExecuteReader());
                if (dt2.Rows.Count > 0)
                {
                    lblTripAmount.Text = (dt2.Rows[0][0].ToString() == "") ? "0.00" : dt2.Rows[0][0].ToString();
                    lblBata.Text = (dt2.Rows[0][1].ToString() == "") ? "0.00" : dt2.Rows[0][1].ToString();
                    lblExpense.Text = (dt2.Rows[0][2].ToString() == "") ? "0.00" : dt2.Rows[0][2].ToString();
                    lblBalance.Text = (dt2.Rows[0][3].ToString() == "") ? "0.00" : dt2.Rows[0][3].ToString();

                }
                else
                {
                    lblTripAmount.Text = "0.00";
                    lblBata.Text = "0.00";
                    lblExpense.Text = "0.00";
                    lblBalance.Text = "0.00";

                }
                Connections.Instance.CloseConnection();
                cmd2.Dispose();

                lblProfit.Text = (Convert.ToDecimal((lblBalance.Text == "") ? "0.00" : lblBalance.Text.ToString())-Convert.ToDecimal((lblDiesel.Text == "") ? "0.00" : lblDiesel.Text.ToString())).ToString();

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
            if (dataGridView1.RowCount <= 0)
            {
                GridShow();
            }
            System.Data.DataColumn Vehicle = new System.Data.DataColumn("Vehicle", typeof(System.String));
            Vehicle.DefaultValue = cboVehicle.Text;
            System.Data.DataColumn Diesel = new System.Data.DataColumn("Diesel", typeof(System.Decimal));
            Diesel.DefaultValue = lblDiesel.Text;
            System.Data.DataColumn Profit = new System.Data.DataColumn("Profit", typeof(System.Decimal));
            Profit.DefaultValue = lblProfit.Text;

            System.Data.DataColumn DieselLtr = new System.Data.DataColumn("DieselLtr", typeof(System.String));
            DieselLtr.DefaultValue = lblQty.Text;
            

            System.Data.DataColumn DtFrom = new System.Data.DataColumn("DtFrom", typeof(System.String));
            DtFrom.DefaultValue = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            System.Data.DataColumn DtTo = new System.Data.DataColumn("DtTo", typeof(System.String));
            DtTo.DefaultValue = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            DataTable dt = new DataTable();

            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetProfitAndLoss", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VehId", SqlDbType.Int).Value = cboVehicle.SelectedValue;
                cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dateTimePicker2.Value;

                dt.Load(cmd.ExecuteReader());
                Connections.Instance.CloseConnection();
                cmd.Dispose();


            }
            catch (Exception ex)
            { }


            dt.Columns.Add(Vehicle);
            dt.Columns.Add(DtTo);
            dt.Columns.Add(DtFrom);
            dt.Columns.Add(Diesel);
            dt.Columns.Add(Profit);
            dt.Columns.Add(DieselLtr);

            ds.Tables["ProfitAndLoss"].Clear();
            ds.Tables["ProfitAndLoss"].Merge(dt);

            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(System.IO.Path.GetDirectoryName(Application.ExecutablePath).ToString() + @"\Reports\rptProfitAndLoss.rpt");
            cryRpt.SetDataSource(ds);
            cryRpt.Refresh();
            cryRpt.PrintToPrinter(1, true, 0, 0);
            dt.Dispose();
        }

    }
}
