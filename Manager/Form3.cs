using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Manager
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public Form3()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Shown += Form3_Shown;
            label3.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form3_Shown(object sender, EventArgs e)
        {
            Load("select p.* from (SELECT (CAST(Date_Trip as DATETIME) + CAST(Start_Time as DATETIME)) as combine,ID FROM TRIP) AS C,Trip p  where p.ID=c.ID AND c.combine >= getdate();");
        }
        private void Load(String sql)
        {
            DataSet ds = new DataSet();
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-00L2TGG;Initial Catalog=TicketSelling1;Integrated Security=True";
            {
                con.Open();
                SqlDataAdapter dap = new SqlDataAdapter(sql, con);
                dap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shown += Form3_Shown;
            label3.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }
}
