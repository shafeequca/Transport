using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
