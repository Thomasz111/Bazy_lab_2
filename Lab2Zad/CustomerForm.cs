using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Lab2Zad
{
    public partial class CustomerForm : Form
    {
        private ComboBox _customers;
        public CustomerForm(ComboBox customers)
        {
            InitializeComponent();
            _customers = customers;
        }

        private void button1_Click(object sender, EventArgs e)//save customer
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Błędne dane");
                return;
            }

            using (var db = new ProdContentBase())
            {
                var customers = db.Customers.Select(c => c.CompanyName).ToArray();
                foreach(var c in customers)
                {
                    if(c == textBox1.Text)
                    {
                        MessageBox.Show("Istnieje już taki klient");
                        return;
                    }
                }
            }

                var companyName = textBox1.Text;
            var description = textBox2.Text;

            using (var db = new ProdContentBase())
            {
                var newCustomer = new Customer { CompanyName = companyName, Description = description };
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                _customers.Items.Add(companyName);
                Hide();
                DestroyHandle();
            }
        }
    }
}
