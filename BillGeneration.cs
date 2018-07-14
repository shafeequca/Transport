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
    public partial class BillGeneration : Form
    {
        public BillGeneration()
        {
            InitializeComponent();
        }

        private void BillGeneration_Load(object sender, EventArgs e)
        {

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
                cmd.Parameters.Add("@Search", SqlDbType.VarChar).Value = txtSearch.Text;
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
                Connections.Instance.CloseConnection();
                cmd.Dispose();
                dataGridView1.Columns[14].Width = 10;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Party and Location";
                dataGridView1.Columns[2].HeaderText = "Type";
                dataGridView1.Columns[2].Width = 10;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].HeaderText = "Date";
                dataGridView1.Columns[15].Visible = false;

                dataGridView1.Columns[16].Visible = false;

                dataGridView1.Columns[17].Visible = false;



            }
            catch (Exception ex)
            { }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            Show();
        }
    }
}
