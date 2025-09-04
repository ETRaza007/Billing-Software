using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BillSwap
{
    public partial class DashboardNew: Form
    {
        DashBoardN Dashnew;
        AddGroup Cates;
        AddProducts product;
        Purchases purchases;
        AddBrand brand;
        Sales sales;
        Supplier supplier;
        public DashboardNew()
        {
            InitializeComponent();
        }

        private void DashboardNew_Load(object sender, EventArgs e)
        {
            if (Dashnew == null)
            {
                Dashnew = new DashBoardN();
                Dashnew.FormClosed += Dashboard_FormClosed;
                Dashnew.MdiParent = this;
                Dashnew.Show();
                Dashnew.Dock = DockStyle.Fill;
            }
            else
            {
                Dashnew.Activate();
            }
        }
        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false)
            {
                menuMaster.Height += 10;
                if(menuMaster.Height >= 180)
                {
                    TMaster.Stop();
                    menuExpand = true;
                    CloseInventry();
                }
            }
            else
            {
                menuMaster.Height -= 10;
                if (menuMaster.Height <= 50)
                {
                    TMaster.Stop();
                    menuExpand = false;
                }
            }
        }

        
        bool SidebarExpand = true;
        private void SidebarTrans_Tick(object sender, EventArgs e)
        {
            int step = 7;

            if (SidebarExpand)
            {
                Sidebarpanel.Width -= step;
                if (Sidebarpanel.Width <= 60)
                {
                    SidebarExpand = false;
                    SidebarTrans.Stop();
                }
            }
            else
            {
                Sidebarpanel.Width += step;
                if (Sidebarpanel.Width >= 280)
                {
                    SidebarExpand = true;
                    SidebarTrans.Stop();
                }
            }
        }

        private void btnhum_Click(object sender, EventArgs e)
        {
            SidebarTrans.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SystemSounds.Asterisk.Play();
            DialogResult dg = MessageBox.Show("Do you want to close this application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                this.Close();
            }
        }

        bool inexpand = false;
        private void inventry_Tick(object sender, EventArgs e)
        {
            if (inexpand == false)
            {
                menuInventry.Height += 10;
                if (menuInventry.Height >= 232)
                {
                    TInventry.Stop();
                    inexpand = true;
                    CloseMaster();
                }
            }
            else
            {
                menuInventry.Height -= 10;
                if (menuInventry.Height <= 50)
                {
                    TInventry.Stop();
                    inexpand = false;
                }
            }
        }

        private void btnInvty_Click(object sender, EventArgs e)
        {
            TInventry.Start();
        }

        private void btnproduct_Click_1(object sender, EventArgs e)
        {
            TMaster.Start();
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            if(Dashnew == null)
            {
                Dashnew = new DashBoardN();
                Dashnew.FormClosed += Dashboard_FormClosed;
                Dashnew.MdiParent = this;
                Dashnew.Show();
                Dashnew.Dock = DockStyle.Fill;
            }
            else
            {
                Dashnew.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dashnew = null;
            
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            if (Cates == null)
            {
                Cates = new AddGroup();
                Cates.FormClosed += Cates_FormClosed;
                Cates.MdiParent = this;
                Cates.Dock = DockStyle.Fill;
                Cates.Show();
            }
            else
            {
                Cates.Activate();
            }
        }

        private void Cates_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cates = null;
        }

        private void btnAddPro_Click(object sender, EventArgs e)
        {
            if (product == null)
            {
                product = new AddProducts();
                product.FormClosed += product_FormClosed;
                product.MdiParent = this;
                product.Dock = DockStyle.Fill;
                product.Show();
            }
            else
            {
                product.Activate();
            }
        }

        private void product_FormClosed(object sender, FormClosedEventArgs e)
        {
            product = null;
        }

      

        private void purchases_FormClosed(object sender, FormClosedEventArgs e)
        {
            purchases = null;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you want to log out?",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question))
            {

                this.Hide();  // hide current form (Dashboard)
                LogIN login = new LogIN();
                login.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (brand == null)
            {
                brand = new AddBrand();
                brand.FormClosed += brand_FormClosed;
                brand.MdiParent = this;
                brand.Dock = DockStyle.Fill;
                brand.Show();
            }
            else
            {
                brand.Activate();
            }
        }
        private void brand_FormClosed(object sender, FormClosedEventArgs e)
        {
            brand = null;
        }

       

        private void btnsupplier_Click(object sender, EventArgs e)
        {
            Tsupplirr.Start();
        }

        private void supplier_FormClosed(object sender, FormClosedEventArgs e)
        {
            supplier = null;
        }      

        private void button2_FormClosed(object sender, FormClosedEventArgs e)
        {
            sales = null;
        }        

        bool sbexpand = false;
        private void supplirr_Tick(object sender, EventArgs e)
        {
            if (sbexpand == false)
            {
                SubSupplier.Height += 10;
                if (SubSupplier.Height >= 115)
                {
                    Tsupplirr.Stop();
                    sbexpand = true;

                    // ✅ Dusre panels band karo
                    ClosePurchase();
                    CloseSales();
                }
            }
            else
            {
                SubSupplier.Height -= 10;
                if (SubSupplier.Height <= 35)
                {
                    Tsupplirr.Stop();
                    sbexpand = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (supplier == null)
            {
                supplier = new Supplier();
                supplier.FormClosed += supplier_FormClosed;
                supplier.MdiParent = this;
                supplier.Dock = DockStyle.Fill;
                supplier.Show();
            }
            else
            {
                supplier.Activate();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (purchases == null)
            {
                purchases = new Purchases();
                purchases.FormClosed += purchases_FormClosed;
                purchases.MdiParent = this;
                purchases.Dock = DockStyle.Fill;
                purchases.Show();
            }
            else
            {
                purchases.Activate();
            }
        }

        private void btnpuchase_Click(object sender, EventArgs e)
        {
            Tpurchase.Start();
        }

        private void Searchpuchase_Click(object sender, EventArgs e)
        {

        }

        private void Searchsupply_Click(object sender, EventArgs e)
        {

        }
        
        private void btnsales_Click(object sender, EventArgs e)
        {
            Tsales.Start();
        }

        private void salesinvoice_Click(object sender, EventArgs e)
        {
            if (sales == null)
            {
                sales = new Sales();
                sales.FormClosed += button2_FormClosed;
                sales.MdiParent = this;
                sales.Dock = DockStyle.Fill;
                sales.Show();
            }
            else
            {
                sales.Activate();
            }
        }

        private void searchsales_Click(object sender, EventArgs e)
        {

        }
        bool trt = false;
        private void Tpurchase_Tick(object sender, EventArgs e)
        {
            if (trt == false)
            {
                SubPurchase.Height += 10;
                if (SubPurchase.Height >= 115)
                {
                    Tpurchase.Stop();
                    trt = true;

                    // ✅ Dusre panels band karo
                    CloseSupplier();
                    CloseSales();
                }
            }
            else
            {
                SubPurchase.Height -= 10;
                if (SubPurchase.Height <= 35)
                {
                    Tpurchase.Stop();
                    trt = false;
                }
            }
        }

        bool RTR = false;
        private void Tsales_Tick(object sender, EventArgs e)
        {
            if (RTR == false)
            {
                SubSales.Height += 10;
                if (SubSales.Height >= 115)
                {
                    Tsales.Stop();
                    RTR = true;                    
                    CloseSupplier();
                    ClosePurchase();
                }
            }
            else
            {
                SubSales.Height -= 10;
                if (SubSales.Height <= 35)
                {
                    Tsales.Stop();
                    RTR = false;
                }
            }
        }

        private void CloseSupplier()
        {
            if (sbexpand)
            {
                SubSupplier.Height = 35;
                sbexpand = false;
            }
        }
        private void ClosePurchase()
        {
            if (trt)
            {
                SubPurchase.Height = 35;
                trt = false;
            }
        }
        private void CloseSales()
        {
            if (RTR)
            {
                SubSales.Height = 35;
                RTR = false;
            }
        }
        private void CloseMaster()
        {
            if (menuExpand)
            {
                menuMaster.Height = 35;
                menuExpand = false;
            }
        }
        private void CloseInventry()
        {
            if (inexpand)
            {
                menuInventry.Height = 35;
                inexpand = false;
            }
        }
    }
    
}
