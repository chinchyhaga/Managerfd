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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            Shown += Form2_Shown;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
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
            Load("select * from employees");
            button6.Enabled = false;
            button7.Enabled = false;
            ShowDetail(false);
        }
        private void Load(String sql)
        {
            DataSet ds = new DataSet();
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-00L2TGG;Initial Catalog=employee;Integrated Security=True";
            {
                con.Open();
                SqlDataAdapter dap = new SqlDataAdapter(sql, con);
                dap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Text = "SEARCH";
            button6.Enabled = false;
            button7.Enabled = false;
            String sql = "SELECT * FROM employees";
            String dk = "";
            if (textBox3.Text.Trim() != "")
            {
                dk += " Street like '%" + textBox3.Text + "%'";
            }
            if (textBox4.Text.Trim() != "" && dk != "")
            {
                dk += " AND City like N'%" + textBox4.Text + "%'";
            }
            if (textBox4.Text.Trim() != "" && dk == "")
            {
                dk += " City like N'%" + textBox4.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            Load(sql);
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
            textBox5.Enabled = showup;
            textBox6.Enabled = showup;
            textBox7.Enabled = showup;
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
                textBox5.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                textBox6.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                //dtpNgaySX.Value = (DateTime)dgvKetQua[2, e.RowIndex].Value;
                textBox7.Text = dataGridView1[2, e.RowIndex].Value.ToString();
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
            sql = "INSERT INTO employees([Employee-name],Street,City)VALUES (";
            sql += "'" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "');";
            if (button6.Enabled == true)
            {
                sql = "Update employees SET ";
                sql += "[Employee-name] = '" + textBox5.Text + "',";
                //sql += "NgaySX = '" + dtpNgaySX.Value.Date + "',";
                sql += "Street = '" + textBox6.Text + "',";
                sql += "City = '" + textBox7.Text + "'";
                sql += "Where [Employee-name] ='" + textBox5.Text + "';";
            }
            if (button7.Enabled == true)
            {
                sql = "Delete From employees Where [Employee-name] ='" + textBox5.Text + "';";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from employees";
            Load(sql);
            con.Close();
            ShowDetail(false);
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}


