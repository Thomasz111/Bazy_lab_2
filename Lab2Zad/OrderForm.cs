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

    public partial class OrderForm : Form
    {

        ProdContentBase db;
        public OrderForm()
        {
            InitializeComponent();
            db = new ProdContentBase();
            Setup();
        }

        private void Setup()
        {

            var customers = (from c in db.Customers
                            orderby c.CompanyName
                            select c.CompanyName).ToList();

            foreach (var c in customers)
            {
                comboBox1.Items.Add(c);
            }
            


            var products = (from p in db.Products
                            orderby p.Name
                            select p.Name).ToList();

            foreach (var p in products)
            {
                comboBox2.Items.Add(p);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//add customer
        {
            CustomerForm form = new CustomerForm(comboBox1);
            form.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//order
        {
            if(comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || textBox1.Text == "")
            {
                MessageBox.Show("Błędne dane");
                return;
            }

                var selectedCustomer = (string)comboBox1.SelectedItem;
            var selectedProduct = (string)comboBox2.SelectedItem;
            var quantity = Int32.Parse(textBox2.Text);


            var unitsInStock = from p in db.Products
                                where p.Name == selectedProduct
                                select p.UnitsInStock;

            if (unitsInStock.First() < quantity)
            {
                MessageBox.Show("Nie ma tylu towarów");
                return;
            }

            var prodId = from p in db.Products
                            where p.Name == selectedProduct
                            select p.ProductId;

            int prodIdInt = prodId.First();
            var newOrder = new Order { ProductId = prodId.First(), CompanyName = selectedCustomer, Quantity = quantity };
            db.Orders.Add(newOrder);
            db.SaveChanges();
            Hide();
            DestroyHandle();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//quantity
        {
            if(comboBox2.SelectedItem != null && textBox2.Text != "")
            {
                var productName = (string)comboBox2.SelectedItem;


                var productUnitprice = from p in db.Products
                                where p.Name == productName
                                select p.Unitprice;
                int productUnitpriceInt = (int)productUnitprice.First();

                int number = Int32.Parse(textBox2.Text);

                textBox1.Text = (number * productUnitpriceInt).ToString();
                

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//sum
        { 

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//product
        {
            if (textBox2.Text != "" && comboBox2.SelectedItem != null)
            {
                var productName = (string)comboBox2.SelectedItem;


                var productUnitprice = from p in db.Products
                                       where p.Name == productName
                                       select p.Unitprice;


                int productUnitpriceInt = (int)productUnitprice.First();

                int number = Int32.Parse(textBox2.Text);

                textBox1.Text = (number * productUnitpriceInt).ToString();

            }
        }
    }
}
