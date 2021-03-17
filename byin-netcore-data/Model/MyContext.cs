using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace byin_netcore_data.Model
{
    public class MyContext : IdentityDbContext
    {
        public DbSet<Order> Orders {get; set;}
        public DbSet<OrderEntity> OrderEntities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasIndex(p => p.ProductName);
            builder.Entity<ProductCategory>().HasIndex(c => c.ProductCategoryName).IsUnique();
            builder.Entity<FilePath>().HasIndex(f => f.CloudStorageKey).IsUnique();

            builder.Entity<ProductAndImg>().HasKey(pi => new { pi.ProductId, pi.FilePathId });
            builder.Entity<ProductAndImg>().HasOne<Product>(pi => pi.Product).WithMany(p => p.IllustrationImgLink).HasForeignKey(pi => pi.ProductId);
            builder.Entity<ProductAndImg>().HasOne<FilePath>(pi => pi.FilePath).WithMany(f => f.Products).HasForeignKey(pi => pi.FilePathId);

            builder.Entity<ProductAndCategory>().HasKey(pc => new { pc.ProductId, pc.ProductCategoryId });
            builder.Entity<ProductAndCategory>().HasOne<Product>(pc => pc.Product).WithMany(p => p.ProductCategoriesLink).HasForeignKey(pc => pc.ProductId);
            builder.Entity<ProductAndCategory>().HasOne<ProductCategory>(pc => pc.ProductCategory).WithMany(c => c.ProductsLink).HasForeignKey(pc => pc.ProductCategoryId);

            base.OnModelCreating(builder);
        }

        public void DisableTracking()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public void DisableLazyLoading()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}
