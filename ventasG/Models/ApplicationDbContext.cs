using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ventasG.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Company_TB { get; set; }
        public DbSet<employee> Employee_TB { get; set; }
        public DbSet<Invoice> Invoice_TB { get; set; }
        public DbSet<Order> Order_TB { get; set; }
        public DbSet<OrderDetails> OrderDetails_TB { get; set; }
        public DbSet<Product> Product_TB { get; set; }

        public DbSet<User> User_TB { get; set; }
    }
}