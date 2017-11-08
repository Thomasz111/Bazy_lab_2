using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2Zad
{
    public partial class CustomersOrdersForm : Form
    {
        ProdContentBase db;
        public CustomersOrdersForm()
        {
            InitializeComponent();
            db = new ProdContentBase();
            Setup();
        }

        private void Setup()
        {
            var customers = from c in db.Customers
                            orderby c.CompanyName
                            select c.CompanyName;

            foreach (var c in customers)
            {
                comboBox1.Items.Add(c);
            }

           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//zmiana klienta
        {
            Customer customerOrders = db.Customers.
                Include("Orders").
                Where(c => c.CompanyName == (string)comboBox1.SelectedItem).
                FirstOrDefault<Customer>();

            if(customerOrders.Orders.Count() == 0)
            {
                dataGridView1.DataSource = null;
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                return;
            }

            dataGridView1.DataSource = customerOrders.Orders;
            dataGridView1.Update();


            textBox1.Text = customerOrders.Orders.Count().ToString();

            int sum = 0;
            foreach(var order in customerOrders.Orders)
            {
                sum += order.Quantity;
            }
            textBox2.Text = sum.ToString();

            sum = 0;
            foreach (var order in customerOrders.Orders)
            {
                var orderUnitPrice = from p in db.Products
                                     where p.ProductId == order.ProductId
                                     select p.Unitprice;

                sum += order.Quantity * (int)orderUnitPrice.First();
            }
            textBox3.Text = sum.ToString();

        }
    }
}
