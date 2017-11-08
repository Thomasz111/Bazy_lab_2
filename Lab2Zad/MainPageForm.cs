using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2Zad
{
    public partial class MainPageForm : Form
    {
        ProdContentBase db;
        public MainPageForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            db = new ProdContentBase();

            db.Categories.Load();

            this.categoryBindingSource.DataSource =db.Categories.Local.ToBindingList();
        }

        private void categoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void categoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.db.SaveChanges();
            this.categoryDataGridView.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm form = new OrderForm();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomersOrdersForm form = new CustomersOrdersForm();
            form.ShowDialog();
        }
    }
}
