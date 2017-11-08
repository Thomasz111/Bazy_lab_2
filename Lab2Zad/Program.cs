using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Zad
{
    class Program
    {
        static void Main(string[] args)
        {
           
            MainPageForm form = new MainPageForm();
            form.ShowDialog();

        }
    }
}

public class ProdContentBase : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string CompanyName { get; set; }
    public int Quantity { get; set; }

}
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int UnitsInStock { get; set; }
    public int CategoryId { get; set; }

    [Column(TypeName = "Money")]
    public decimal Unitprice { get; set; }

    public virtual List<Order> Orders { get; set; }
}
public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual List<Product> Products { get; set; }
}

public class Customer
{
    [Key]
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public virtual List<Order> Orders { get; set; }
}