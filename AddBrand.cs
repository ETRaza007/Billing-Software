using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BillSwap
{
    public partial class AddBrand: Form
    {
        
        DBConnect Rinn = new DBConnect();
        public AddBrand()
        {
            InitializeComponent();
        }

        private void AddBrand_Load(object sender, EventArgs e)
        {
            btndelte.Visible = false;
            getbrand();
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void getbrand()
        {
            string qiz = "select BrandId,BrandName as Name,CreatedDate as [Created Date],Createdby as [Created by] from tblBrand where sts=1";
            SqlCommand dd = new SqlCommand(qiz, Rinn.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(dd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(btnadd.Text == "Add")
            {
                try
                {
                    if (txtbrand.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter Brand Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Select * from tblBrand Where BrandName = '" + txtbrand.Text.Trim() + "' And sts=1", Rinn.GetCon());
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        Rinn.OpenCon();
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Brand Already Existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string Query = "insert into tblBrand (BrandName,CreatedDate,Createdby,sts) values ('" + txtbrand.Text.Trim() + "',GETDATE(),'Owner','1')";
                            SqlCommand md = new SqlCommand(Query, Rinn.GetCon());
                            int i = md.ExecuteNonQuery();

                            if (i > 0)
                            {
                                MessageBox.Show("Brand Added Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                getbrand();
                                txtbrand.Text = string.Empty;
                            }
                            else
                            {
                                MessageBox.Show("Brand Not Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtbrand.Text = string.Empty;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    Rinn.OpenCon();
                    string Query = "Update tblBrand set BrandName='" + txtbrand.Text.Trim() + "' ,ModifierName='Owner' ,ModifierDate=GETDATE() where BrandId= '" + BrandID.Text + "'";
                    SqlCommand md = new SqlCommand(Query, Rinn.GetCon());
                    int i = md.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("Brand Updated Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getbrand();
                        txtbrand.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Brand Not Update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtbrand.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
               
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Update";
            btndelte.Visible = true;
            
            BrandID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtbrand.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btnadd.Text = "Add";
            btndelte.Visible = false;
            txtbrand.Text = string.Empty;
        }

        private void btndelte_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to Delete This Product ??", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Rinn.OpenCon();
                    string Query = "Update tblBrand set sts='0' where BrandId= '" + BrandID.Text + "'";
                    SqlCommand md = new SqlCommand(Query, Rinn.GetCon());
                    int i = md.ExecuteNonQuery();
                    
                    if (i > 0)
                    {
                        MessageBox.Show("Brand Deleted Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getbrand();
                        txtbrand.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Brand Not Deleted..!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtbrand.Text = string.Empty;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
