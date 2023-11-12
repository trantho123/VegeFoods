using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VEGEFOODS.Models
{
    public partial class VegeFoodsDbContext : DbContext
    {
        public VegeFoodsDbContext()
            : base("name=VegeFoodsDbContext")
        {
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<order_details> order_details { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .HasMany(e => e.products)
                .WithOptional(e => e.category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<order>()
                .HasMany(e => e.order_details)
                .WithOptional(e => e.order)
                .HasForeignKey(e => e.order_id);

            modelBuilder.Entity<product>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.order_details)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<role>()
                .HasMany(e => e.users)
                .WithOptional(e => e.role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);
        }
    }
}
