using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkWindowsFormsApp
{
    public partial class Form1 : Form
    {
        int searchID;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            DemoEntities db = new DemoEntities();
            string rb;

            if (rb_Male.Checked == true)
                rb = "Male";
            else
                rb = "Female";

            db.Employees.Add(new Employee() { Name = tb_Name.Text, Position = cmb_Position.Text, Gender = rb, City = tb_City.Text });
            db.SaveChanges();

            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = "";
            }

            rb_Male.Checked = false;
            rb_Female.Checked = false;
            cmb_Position.Text = "";
            dgv.DataSource = (db.Employees).ToList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DemoEntities db = new DemoEntities();

            dgv.DataSource = (db.Employees).ToList();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            using(DemoEntities db = new DemoEntities())
            {
                var Emp = db.Employees.Find(Convert.ToInt32(tb_Name.Text));


                if (Emp != null)
                {
                    searchID = Convert.ToInt32(tb_Name.Text);

                    tb_Name.Text = Emp.Name;
                    tb_City.Text = Emp.City;
                    if (rb_Male.Text == Emp.Gender)
                        rb_Male.Checked = true;
                    else
                        rb_Female.Checked = true;
                    cmb_Position.Text = Emp.Position;

                }
                else
                {
                    MessageBox.Show("Results Not Found !!!");
                }
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            using (DemoEntities db = new DemoEntities())
            {
                var Emp = db.Employees.Find(searchID);

                db.Employees.Remove(Emp);

                db.SaveChanges();

                dgv.DataSource = (db.Employees).ToList();
            }
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = "";
            }

            rb_Male.Checked = false;
            rb_Female.Checked = false;
            cmb_Position.Text = "";
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            using (DemoEntities db = new DemoEntities())
            {
                var Emp = db.Employees.Find(searchID);

                string rb;

                if (rb_Male.Checked == true)
                    rb = "Male";
                else
                    rb = "Female";

                if (Emp != null)
                {
                    Emp.Name = tb_Name.Text;
                    Emp.Position = cmb_Position.Text;
                    Emp.City = tb_City.Text;
                    Emp.Gender = rb;
                }
                db.SaveChanges();

                dgv.DataSource = (db.Employees).ToList();
            }
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                    control.Text = "";
            }

            rb_Male.Checked = false;
            rb_Female.Checked = false;
            cmb_Position.Text = "";
        }
    }
}
