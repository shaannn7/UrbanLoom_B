using Microsoft.EntityFrameworkCore;
using System.Data;
using UrbanLoom_B.Entity;

namespace UrbanLoom_B.DBcontext
{
    public class DbContextClass : DbContext
    {
        private readonly IConfiguration _configuration;
        public string CString;

        public DbContextClass(IConfiguration configuration)
        {
            _configuration = configuration;
            CString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public DbSet<User> Users_ul { get; set; }
        public DbSet<Product> Products_ul { get; set; }
        public DbSet<Category> Categories_ul { get; set; }
        public DbSet<Cart> Cart_ul { get; set; }
        public DbSet<CartItem> Cartitem_ul { get; set; }
        public DbSet<Order> Orders_ul { get; set; }
        public DbSet<OrderItem> Orderitems_ul { get; set; }
        public DbSet<WhishList> Whishlist_ul { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(CString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.cart)
                .WithOne(c => c.user)
                .HasForeignKey<Cart>(Ui => Ui.UserId);
 
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.cartitem)
                .WithOne(Ci => Ci.cart)
                .HasForeignKey(Ci => Ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(Ci=>Ci.products)
                .WithMany(P=>P.cartitems)
                .HasForeignKey(Ci => Ci.ProductId);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("User");

            modelBuilder.Entity<Category>()
                .HasMany(p => p.products)
                .WithOne(Cg => Cg.category)
                .HasForeignKey(P => P.CategoryId);

            modelBuilder.Entity<WhishList>()
                .HasOne(p => p.products)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(O => O.users)
                .WithMany(U => U.order)
                .HasForeignKey(u => u.userId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(Oi => Oi.order)
                .WithMany(O => O.OrderItems)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi=>oi.products)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<User>()
                .Property(u=>u.isBlocked)
                .HasDefaultValue(false);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderStatus)
                .HasDefaultValue("Processing");

            base.OnModelCreating(modelBuilder);
        }

    }
}
