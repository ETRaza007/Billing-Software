using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace BillSwap
{
    public partial class LogIN : Form
    {
        private SpeechHelper _speech;
        //string connectionString = @"Data Source=RAZA;Initial Catalog=SwapBilling;Integrated Security=True;Connect Timeout=30";
        DBConnect Rinn = new DBConnect();
        public LogIN()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtuser.Text == "" || txtpass.Text == "")
                {
                    MessageBox.Show("Please Fill Username And Password");
                }
                else
                {
                    string query = "SELECT Username, Password, Fullname FROM AdminLogin WHERE Username = '" + txtuser.Text.Trim() + "' AND Password = '" + txtpass.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(query, Rinn.GetCon());
                    Rinn.OpenCon();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"Login Successfully..!! Welcome Back " + dt.Rows[0]["Fullname"].ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _speech.SpeakAsync($"Login successful. Welcome to SwapDigital Billing software ");
                        DashboardNew objj = new DashboardNew();
                        objj.Show();
                        this.Hide();
                    }
                    else
                    {
                        _speech.SpeakAsync($" Oops ! Something went wrong, please check your details.");
                        MessageBox.Show("❌ Check your ID and Password");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtpass.Text = "123";
            txtuser.Text = "raza";
            _speech = new SpeechHelper(volume: 100, rate: 0); // rate -3..+3 is comfy
            _speech.SetVoiceByGender(VoiceGender.Female);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
