using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillSwap
{
    public partial class AddGroup : Form
    {
        DBConnect Rinn = new DBConnect();
        public AddGroup()
        {
            InitializeComponent();
        }

        private void Addcategory_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btndelte.Visible = false;
            GroupId.Visible = false;
            BindCategory();
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

        }

        private void btnsave_Click(object sender, EventArgs e)
        {

        }

        private void Clearup()
        {
            txtcode.Clear();
            txtgrp.Clear();
        }

        private void BindCategory()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select GroupId As ID,GroupName as Name,GroupCode as Code,CGST as [CGST (%)],SGST as [SGST (%)],CESS as [CESS (%)],IGST as [IGST (%)] from tblGroup where sts=1", Rinn.GetCon());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Rinn.OpenCon();
                if (dt.Rows.Count > 0)
                {                   
                    dataGridView1.DataSource = dt;                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btndelte_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to Delete ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SqlCommand cmd = new SqlCommand("Update tblGroup Set sts=0 where GroupId ='" + GroupId.Text + "'", Rinn.GetCon());
                    Rinn.OpenCon();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clearup();
                        BindCategory();
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            if(btnsave.Text == "Save")
            {
                try
                {
                    if (txtgrp.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter Category Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtgrp.Focus();
                        return;
                    }
                    else if (txtcode.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter Category Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtcode.Focus();
                        return;
                    }
                    else if (txtcgst.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter CGST Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtgrp.Focus();
                        return;
                    }
                    else
                    {

                        SqlCommand cmd = new SqlCommand("Select GroupName from tblGroup where GroupName='" + txtgrp.Text.Trim() + "'", Rinn.GetCon());
                        Rinn.OpenCon();
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show("Category Already Existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Clearup();
                        }
                        else
                        {
                            cmd = new SqlCommand("Insert into tblGroup (GroupName,GroupCode,CGST,SGST,CESS,IGST,CreatedBy,ModifierDate) Values ('" + txtgrp.Text.Trim() + "','" + txtcode.Text.Trim() + "','"+txtcgst.Text.Trim()+"','"+txtsgst.Text.Trim() + "','"+txtcess.Text.Trim() + "','"+txtigst.Text.Trim() + "','Owner',Null)", Rinn.GetCon());
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                MessageBox.Show("Category Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clearup();
                                BindCategory();
                            }
                            else
                            {
                                MessageBox.Show("Something went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }


                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                try
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to Update the Record ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        SqlCommand cmd = new SqlCommand("Update tblGroup set GroupName = '" + txtgrp.Text.Trim() + "',GroupCode='" + txtcode.Text.Trim() + "',CGST='" + txtcgst.Text.Trim() + "',SGST='" + txtsgst.Text.Trim() + "',CESS='" + txtcess.Text.Trim() + "',IGST='" + txtigst.Text.Trim() + "' where GroupId ='" + GroupId.Text + "'", Rinn.GetCon());
                        Rinn.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clearup();
                            BindCategory();
                            btnsave.Text = "Save";
                        }
                        else
                        {
                            MessageBox.Show("Update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtcode.Text = string.Empty;
            txtgrp.Text = string.Empty;
            btnsave.Text = "Save";
            btndelte.Visible = false;
            btnsave.Visible = true;
        }



        private void dataGridView1_Click(object sender, EventArgs e)
        {

            //dataGridView1.Columns[0].Visible = false;
            btndelte.Visible = true;
            btnsave.Text = "Update";
            GroupId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtgrp.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtcode.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtcgst.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtsgst.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtcess.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtigst.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void txtcgst_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtcgst.Text))
            {
                decimal cgst;
                if (decimal.TryParse(txtcgst.Text, out cgst))
                {
                    txtsgst.Text = cgst.ToString(); // same as CGST
                    txtcess.Text = "0";             // default 0
                    txtigst.Text = (cgst + cgst).ToString(); // CGST + SGST
                }
                else
                {
                    txtsgst.Clear();
                    txtcess.Clear();
                    txtigst.Clear();
                }
            }
            else
            {
                txtsgst.Clear();
                txtcess.Clear();
                txtigst.Clear();
            }
        }
    }
}
