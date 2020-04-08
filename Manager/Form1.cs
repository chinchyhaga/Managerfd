using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesing();
        }
        private void customizeDesing()
        {
            statuspanel.Visible = false;
            toolpanel.Visible = false;
        }
        private void hideSubMenu()
        {
            if (statuspanel.Visible == true)
                statuspanel.Visible = false;
            if (toolpanel.Visible == true)
                toolpanel.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void statusbutton_Click(object sender, EventArgs e)
        {
            showSubMenu(statuspanel);
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
            //hideSubMenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
            //hideSubMenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new Form4());
            //hideSubMenu();
        }

        private void toolbutton_Click(object sender, EventArgs e)
        {
            showSubMenu(toolpanel);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Top;
            formpanel.Controls.Add(childForm);
            formpanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void formpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
    }
}