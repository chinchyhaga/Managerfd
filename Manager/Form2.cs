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
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            Shown += Form2_Shown;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            Load("select * from TRIP");
            button6.Enabled = false;
            button7.Enabled = false;
            ShowDetail(false);
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
        private void button5_Click(object sender, EventArgs e)
        {
            label4.Text = "ADD";
            //DeleteDetail();
            button6.Enabled = false;
            button7.Enabled = false;
            ShowDetail(true);

        }
        private void ShowDetail(Boolean showup)
        {
            textBox12.Enabled = false;
            textBox5.Enabled = showup;
            textBox6.Enabled = showup;
            dateTimePicker1.Enabled = showup;
            textBox3.Enabled = showup;
            textBox4.Enabled = showup;
            textBox8.Enabled = showup;
            textBox9.Enabled = showup;
            textBox10.Enabled = showup;
            textBox11.Enabled = showup;
            button8.Enabled = showup;
            button9.Enabled = showup;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button6.Enabled = true;
            button7.Enabled = true;
            button5.Enabled = false;
            try
            {
                textBox12.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                textBox5.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textBox6.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1[4, e.RowIndex].Value;
                textBox3.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[6, e.RowIndex].Value.ToString();
                textBox8.Text = dataGridView1[8, e.RowIndex].Value.ToString();
                textBox9.Text = dataGridView1[10, e.RowIndex].Value.ToString();
                textBox10.Text = dataGridView1[11, e.RowIndex].Value.ToString();
                textBox11.Text = dataGridView1[12, e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label4.Text = "UPDATE";
            button5.Enabled = false;
            button7.Enabled = false;
            ShowDetail(true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete " + textBox5.Text + " \n\tIf Yes,Save, NO Cancel", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                label4.Text = "DELETE";
                button5.Enabled = false;
                button6.Enabled = false;
                //Hiện gropbox chi tiết
                ShowDetail(true);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
                //Thiết lập lại các nút như ban đầu
                button7.Enabled = false;
                button6.Enabled = false;
                button5.Enabled = true;
                //xoa trang
                //XoaTrangChiTiet();
                //Cam nhap
                ShowDetail(false);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (con.State != ConnectionState.Open)
                con.Open();
            if (textBox5.Text.Trim() == "")
            {
                errChitiet.SetError(textBox5, "Blank");
                return;
            }
            else
            {
                errChitiet.Clear();
            }
            sql = "INSERT INTO TRIP(Source, Destination, Date_Trip,Start_Time,Arrival_Time, Total_Seat, Price, Discount, ID_Driver) VALUES (";
            sql += "'" + textBox5.Text + "','" + textBox6.Text + "','" + dateTimePicker1.Value.Date + "','"+textBox3.Text+ "','"+textBox4.Text+ "','"+textBox8.Text+ "','"+textBox9.Text+ "','"+ textBox10.Text+ "','"+textBox11.Text+"');";
            if (button6.Enabled == true)
            {
                sql = "Update employees SET ";
                sql += "[Employee-name] = '" + textBox5.Text + "',";
                //sql += "NgaySX = '" + dtpNgaySX.Value.Date + "',";
                sql += "Street = '" + textBox6.Text + "',";
                //sql += "City = '" + textBox7.Text + "'";
                sql += "Where [ID_Trip] ='" + textBox12.Text + "';";
            }
            if (button7.Enabled == true)
            {
                sql = "Delete From TRIP Where [ID_Trip] ='" + textBox12.Text + "';";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from TRIP";
            Load(sql);
            con.Close();
            ShowDetail(false);
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "TripID")
                panel4.Visible = true;
            if (comboBox1.SelectedItem.ToString() == "StartDate")
                panel5.Visible = true;
            if (comboBox1.SelectedItem.ToString() == "Source")
                panel6.Visible = true;
            if (comboBox1.SelectedItem.ToString() == "Destination")
                panel7.Visible = true;
            if (comboBox1.SelectedItem.ToString() == "DriverID")
                panel8.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM TRIP";
            if (textBox7.Text.Trim() != "")
            {
                sql += " Where ID_Trip like '%" + textBox7.Text + "%'";
            }
            Load(sql);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            Load("select * from TRIP");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM TRIP";
            if (dateTimePicker2.Value.Date.ToString() != "")
            {
                sql += " Where Date_Trip = '" + dateTimePicker2.Value.Date.ToString("MM/dd/yyyy") + "';";
            }
            Load(sql);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            Load("select * from TRIP");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            Load("select * from TRIP");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM TRIP";
            if (textBox13.Text.Trim() != "")
            {
                sql += " Where Source like '%" + textBox13.Text + "%'";
            }
            Load(sql);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM TRIP";
            if (textBox13.Text.Trim() != "")
            {
                sql += " Where Destination like '%" + textBox14.Text + "%'";
            }
            Load(sql);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            Load("select * from TRIP");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM TRIP";
            if (textBox15.Text.Trim() != "")
            {
                sql += " Where ID_Driver like '%" + textBox15.Text + "%'";
            }
            Load(sql);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
            Load("select * from TRIP");
        }
    }
}


