using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace ShoeCollectionMVC.Models
{
    public class ShoeDb : DbContext
    {
         public ShoeDb() : base("name=DefaultConnection") { }

        public DbSet<ShoeType> ShoeTypes { get; set; }

        public DbSet<ShoeItem> ShoeItems { get; set; }

        public DbSet<ShoeCategories> ShoeCategories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoeItem>()
                .HasMany(p => p.Categories)
                .WithMany(p => p.Products)
                .Map(m =>
                {
                    m.ToTable("ShoeCategory");
                    m.MapLeftKey("ShoeId");
                    m.MapRightKey("CategoryId");
                });
        }
    }
}