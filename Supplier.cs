using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillSwap
{
    public partial class Supplier: Form
    {
        DBConnect Rinn = new DBConnect();
        public Supplier()
        {
            InitializeComponent();
            GetStatesDDL();
            GetBankDDL();
            ddlbank.SelectedIndex = -1;
            ddlbank.Text = "Select Bank";
            ddlstate.SelectedIndex = -1;
            ddlstate.Text = "Select State";
            txtCOM.Focus();
        }

        private void GetBankDDL()
        {
            Rinn.OpenCon();
            SqlCommand dd = new SqlCommand("Select * from tblBanks", Rinn.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(dd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlbank.DataSource = dt;
            ddlbank.DisplayMember = "BankName";
            ddlbank.ValueMember = "BankId";
        }

        private void GetStatesDDL()
        {
            Rinn.OpenCon();
            string qrry = "Select * from tblStates";
            SqlCommand ff = new SqlCommand(qrry, Rinn.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(ff);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlstate.DataSource = dt;
            ddlstate.DisplayMember = "StateName";
            ddlstate.ValueMember = "StateId";
        }

        private void Clearner()
        {
            txtCOM.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtcity.Text = string.Empty;
            ddlstate.Text = "Select State";
            txtpincode.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtphone.Text = string.Empty;
            ddlbank.Text = "Select Bank";
            txtaccno.Text = string.Empty;
            txtifsc.Text = string.Empty;
            txtpan.Text = string.Empty;
            txtgstin.Text = string.Empty;
            txtstate2.Text = string.Empty;
            txtconperson.Text = string.Empty;
            txtconno.Text = string.Empty;
            txtremark.Text = string.Empty;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCOM.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Company Name", "Using Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCOM.Focus();
                    return;
                }
                if (txtcity.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Your City Here", "Using Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtcity.Focus();
                    return;
                }
                if (txtconno.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Your Contact Number", "Using Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtconno.Focus();
                    return;
                }
                if (txtphone.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Phone Number", "Using Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtphone.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("All sets we can gooo", "Using Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clearner();
        }

        private void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtstate2.Text = ddlstate.Text;
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtphone.Text.Length >= 10)
            {
                e.Handled = true;
            }
        }

        private void txtconno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtconno.Text.Length >= 10)
            {
                e.Handled = true;
            }
        }
    }
}
