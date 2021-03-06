﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.SqlServer.Management.Common;  
using Microsoft.SqlServer.Management.Smo; 
namespace Transport
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "VehicleRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            VehicleRegistration frm = new VehicleRegistration();
            frm.Show(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "DriverRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            DriverRegistration frm = new DriverRegistration();
            frm.Show(this);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "PartyRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            PartyRegistration frm = new PartyRegistration();
            frm.Show(this);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "TripEntry")
                {
                    f.Activate();
                    return;
                }
            }
            TripEntry frm = new TripEntry();
            frm.Show(this);
        }

        private void tripEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "TripEntry")
                {
                    f.Activate();
                    return;
                }
            }
            TripEntry frm = new TripEntry();
            frm.Show(this);
        }

        private void vehicleRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "VehicleRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            VehicleRegistration frm = new VehicleRegistration();
            frm.Show(this);
        }

        private void driverRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "DriverRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            DriverRegistration frm = new DriverRegistration();
            frm.Show(this);
        }

        private void partyDestinationRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "PartyRegistration")
                {
                    f.Activate();
                    return;
                }
            }
            PartyRegistration frm = new PartyRegistration();
            frm.Show(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name != "Menu")
                    f.Close();
            }
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void bataReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
             List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "DriverBataDetails")
                    f.Close();
            }
            DriverBataDetails frm = new DriverBataDetails();
            frm.ShowDialog();
             
        }

        private void dieselEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "DieselExpense")
                    f.Close();
            }
            DieselExpense frm = new DieselExpense();
            frm.ShowDialog();
        }

        private void expenseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "VehicleExpenses")
                    f.Close();
            }
            VehicleExpenses frm = new VehicleExpenses();
            frm.ShowDialog();
        }

        private void profitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "ProfitAndLose")
                    f.Close();
            }
            ProfitAndLose frm = new ProfitAndLose();
            frm.ShowDialog();
        }

        private void dieselDetailsVeicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "DieselDetails")
                    f.Close();
            }
            DieselDetails frm = new DieselDetails();
            frm.ShowDialog();
        }

        private void fixedExpenseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "FixedExpenses")
                    f.Close();
            }
            FixedExpenses frm = new FixedExpenses();
            frm.ShowDialog();
        }

        private void billGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "BillGeneration")
                    f.Close();
            }
            BillGeneration frm = new BillGeneration();
            frm.ShowDialog();
        }

        private void destinationEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "Destination")
                    f.Close();
            }
            Destination frm = new Destination();
            frm.ShowDialog();
        }

        private void fixedContainersBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name == "FixedContainerBill")
                    f.Close();
            }
            FixedContainerBill frm = new FixedContainerBill();
            frm.ShowDialog();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

            //try
            //{
            //    Connections.Instance.OpenConection();
            //    //Connections.Instance.ExecuteQueries(@"BACKUP DATABASE Transport TO  DISK = 'F:\Transport_2018-09-05.bak'");
            //    string FileName = System.Configuration.ConfigurationSettings.AppSettings["Backup_Path"].ToString() + @"\Transport-'+convert(varchar(30),getdate(),113)+'.bak";//" @"BACKUP DATABASE ForBackupTest TO  DISK = '" + System.Configuration.ConfigurationSettings.AppSettings["Backup_Path"].ToString() + @"\'Transport-'+convert(varchar(30),getdate(),113)+'.bak'";
            //    Connections.Instance.ExecuteQueries(@"USE MASTER;DECLARE @file varchar(5000); SET @file='" + FileName + "'; BACKUP DATABASE Transport TO  DISK = @file");
            //    MessageBox.Show("Backup file created");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Backup failed : " + ex.Message);
            //}
            
            
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Backup Files|*.bak";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Connections.Instance.OpenConection();
                    Connections.Instance.ExecuteQueries(@"USE MASTER;RESTORE DATABASE Test FROM  DISK = '" + ofd.FileName + "' WITH REPLACE");
                    MessageBox.Show("Database restored");
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Restored failed : " + ex.Message);
                }
            }
        }
    }
}
