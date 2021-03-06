﻿using System;
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
    public partial class TripEntry : Form
    {
        public TripEntry()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TripEntry_Load(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            getDriver();
            cboDriver.SelectedIndex = -1;
            cboCleaner.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Today;
            GridShow();
            getCategory();
            getDestination();
            cboVehicle.SelectedIndex = -1;
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
        private void getCategory()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetCategory", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboCategory.DataSource = null;
                cboCategory.DataSource = dt;
                cboCategory.DisplayMember = "Category";
                cboCategory.ValueMember = "CatID";

                if (cboCategory.Items.Count > 1)
                {
                    cboCategory.SelectedIndex = 0;
                }
               
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
                SqlCommand cmd = new SqlCommand("dbo.GetTripRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                //dataGridView1.Columns[14].Width = 10;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Party";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Type";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].Visible = false;

                dataGridView1.Columns[16].HeaderText = "Date";
                dataGridView1.Columns[16].Width = 30;
                dataGridView1.Columns[4].Width = 15;

                dataGridView1.Columns[17].Visible = false;
                dataGridView1.Columns[18].Visible = false;

                dataGridView1.Columns[19].Visible = false;
                dataGridView1.Columns[20].Visible = false;


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
                    
                    if (cboDriver.Items.Count>1)
                    {
                        cboDriver.SelectedIndex=0;
                    }
                    DataTable dt1 = new DataTable();

                    dt1.Load(cmd.ExecuteReader());
                    cboCleaner.DataSource = null;

                    cboCleaner.DataSource = dt1;
                    cboCleaner.DisplayMember = "DriverName";
                    cboCleaner.ValueMember = "DriverId";
                    Connections.Instance.CloseConnection();
                    cmd.Dispose();
                    if (cboCleaner.Items.Count > 1)
                    {
                        cboCleaner.SelectedIndex = 0;
                    }
            }
            catch (Exception ex)
            {}
        }
        private void GridShowParty()
        {
            try
            {
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetPartyRegistration", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtName.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Party";
                dataGridView2.Columns[1].Width = 80;
                dataGridView2.Columns[2].Visible = false;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                dataGridView2.Columns[6].HeaderText = "Km.";
                dataGridView2.Columns[7].Visible = false;
                dataGridView2.Columns[8].Visible = false;
                dataGridView2.Columns[9].Visible = false;
                dataGridView2.Columns[10].Visible = false;
                dataGridView2.Columns[11].HeaderText = "Rate 20";
                dataGridView2.Columns[12].HeaderText = "Rate 40";


            }
            catch (Exception ex)
            { }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            GridShowParty();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblPartyID.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAddress.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtContactNumber.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                
                txtName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                dataGridView2.Visible = false;
                cboType_SelectedIndexChanged(null, null);
                cboVehicle.Focus();

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboVehicle.DataSource = null;
                Connections.Instance.OpenConection();
                SqlCommand cmd = new SqlCommand("dbo.GetVehicleByType", Connections.Instance.con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = cboType.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                cboVehicle.DataSource = dt;
                cboVehicle.DisplayMember = "Vehicle";
                cboVehicle.ValueMember = "VehId";

                if (cboVehicle.Items.Count > 1)
                {
                    cboVehicle.SelectedIndex = 0;
                }
                if (cboDestination.SelectedIndex!=-1)
                {
                    string query = "SELECT DriverBata20,DriverBata40,CleanerBata20,CleanerBata40,Rate20,Rate40 FROM tblDestination WHERE DestId='" + cboDestination.SelectedValue + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    DataTable dt1 = new DataTable();

                    dt1.Load(cmd.ExecuteReader());
                    if (cboType.Text == "20 FT")
                    {
                        txtTripRate.Text = dt1.Rows[0]["Rate20"].ToString();
                        txtDriverBata.Text = dt1.Rows[0]["DriverBata20"].ToString();
                        txtCleanerBata.Text = dt1.Rows[0]["CleanerBata20"].ToString();

                    }
                    else
                    {
                        txtTripRate.Text = dt1.Rows[0]["Rate40"].ToString();
                        txtDriverBata.Text = dt1.Rows[0]["DriverBata40"].ToString();
                        txtCleanerBata.Text = dt1.Rows[0]["CleanerBata40"].ToString();
                    }
                }
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                cboVehicle.SelectedIndex = -1;

            }
            catch (Exception ex)
            { }

        }
                
            
        

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboType.SelectedIndex = 0;
            cboVehicle.SelectedIndex = -1;
            cboDriver.SelectedIndex = -1;
            cboCleaner.SelectedIndex = -1;
            txtCleanerBata.Text = "";
            txtAddress.Text = "";
            txtAdvance.Text = "";
            txtContactNumber.Text = "";
            txtDriverBata.Text = "";
            txtExpence.Text = "";
            txtTripRate.Text = "";
            txtName.Text = "";
            lbltripId.Text = "";
            txtHalting.Text = "";
            lblPartyID.Text = "";
            dataGridView2.Visible = false;
            cboCategory.SelectedIndex = 0;
            txtCont1.Text = "";
            txtCont2.Text = "";
            cboDestination.SelectedIndex = -1;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbltripId.Text == "")
                {
                    MessageBox.Show("Please select a valid trip");

                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Do you want to delete the data?", "Trip Registration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("TripRegistration", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tripid", SqlDbType.Int).Value = lbltripId.Text;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblPartyID.Text == "")
                {
                    MessageBox.Show("Please enter the party");
                    txtName.Focus();
                    return;
                }
                if (cboVehicle.SelectedIndex ==-1)
                {
                    MessageBox.Show("Please select a vehicle");
                    cboVehicle.Focus();
                    return;
                }
                if (cboDriver.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a driver");
                    cboDriver.Focus();
                    return;
                }
                if (cboCleaner.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a cleaner");
                    cboCleaner.Focus();
                    return;
                }
                if (txtTripRate.Text == "")
                {
                    MessageBox.Show("Please enter the trip rate");
                    txtTripRate.Focus();
                    return;
                }
                if (txtDriverBata.Text == "")
                {
                    MessageBox.Show("Please enter the driver bata");
                    txtDriverBata.Focus();
                    return;
                }
                if (txtCleanerBata.Text == "")
                {
                    MessageBox.Show("Please enter the cleaner bata");
                    txtCleanerBata.Focus();
                    return;
                }
                

                DialogResult dialogResult = MessageBox.Show("Do you want to save the data?", "Trip Registration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Connections.Instance.OpenConection();
                    SqlCommand cmd = new SqlCommand("TripRegistration", Connections.Instance.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PartyId", SqlDbType.Int).Value = lblPartyID.Text;
                    cmd.Parameters.Add("@VehType", SqlDbType.VarChar).Value = cboType.Text;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = txtContactNumber.Text;

                    cmd.Parameters.Add("@VehicleId", SqlDbType.Int).Value = cboVehicle.SelectedValue.ToString();
                    cmd.Parameters.Add("@DriverId", SqlDbType.Int).Value = cboDriver.SelectedValue.ToString();
                    cmd.Parameters.Add("@CleanerId", SqlDbType.Int).Value = cboCleaner.SelectedValue.ToString();
                    cmd.Parameters.Add("@TripRate", SqlDbType.Decimal).Value = txtTripRate.Text;
                    cmd.Parameters.Add("@DriverBata", SqlDbType.Decimal).Value = txtDriverBata.Text;
                    cmd.Parameters.Add("@CleanerBata", SqlDbType.Decimal).Value = txtCleanerBata.Text;
                    cmd.Parameters.Add("@Advance", SqlDbType.Decimal).Value =  (txtAdvance.Text=="")?"0":txtAdvance.Text;
                    cmd.Parameters.Add("@Expense", SqlDbType.Decimal).Value = (txtExpence.Text == "") ? "0" : txtExpence.Text;

                    cmd.Parameters.Add("@TripDate", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = cboCategory.Text;
                    cmd.Parameters.Add("@Cont1", SqlDbType.VarChar).Value = txtCont1.Text;
                    cmd.Parameters.Add("@Cont2", SqlDbType.VarChar).Value = txtCont2.Text;
                    cmd.Parameters.Add("@DestId", SqlDbType.Int).Value = cboDestination.SelectedValue.ToString();
                    cmd.Parameters.Add("@HaltingCharge", SqlDbType.Decimal).Value = txtHalting.Text.ToString();
                    
                    

                    if (lbltripId.Text != "")
                    {
                        cmd.Parameters.Add("@TripId", SqlDbType.Int).Value = lbltripId.Text;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GridShow();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    lbltripId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    
                    cboDestination.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cboType.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtContactNumber.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    cboVehicle.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    lblPartyID.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                    txtTripRate.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    cboDriver.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    cboCleaner.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    txtDriverBata.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                    txtCleanerBata.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    txtAdvance.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    txtExpence.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString());
                    if (dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString() == "")
                    {
                        cboCategory.SelectedIndex = 0;
                    }
                    else
                    {
                        cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();

                    }
                    
                    txtCont1.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                    txtCont2.Text = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                    txtHalting.Text = dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();
                    
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                    dataGridView2.Visible = false;

                }
            }
            catch (Exception ex)
            { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboType_SelectedIndexChanged(null, null);
        }
    }
}
