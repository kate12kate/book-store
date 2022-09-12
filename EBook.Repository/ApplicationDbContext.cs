using EBook.Domain;
using EBook.Domain.DomainModels;
using EBook.Domain.Identity;
using EBook.Domain.Relations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EBook.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //za nov zapis
            builder.Entity<Book>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();


            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            //primaren kluc na ovaa tabela praime kompoziten kluc
            builder.Entity<BookInShoppingCart>()
                .HasKey(z => new { z.BookId, z.ShoppingCartId });


            //deka se relacii
            builder.Entity<BookInShoppingCart>()
                .HasOne(z => z.CurrnetBook)
                .WithMany(z => z.BookInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<BookInShoppingCart>()
                .HasOne(z => z.UserCart)
                .WithMany(z => z.BookInShoppingCarts)
                .HasForeignKey(z => z.BookId);

            //za 1-1 
            builder.Entity<ShoppingCart>()
                .HasOne<EShopAppUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

        }
    
}
}
