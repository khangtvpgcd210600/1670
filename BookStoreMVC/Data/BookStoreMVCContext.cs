using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreMVC.Models;

namespace BookStoreMVC.Data
{
    public class BookStoreMVCContext : DbContext
    {
        public BookStoreMVCContext (DbContextOptions<BookStoreMVCContext> options)
            : base(options)
        {
        }

        public DbSet<BookStoreMVC.Models.Author> Author { get; set; } = default!;

        public DbSet<BookStoreMVC.Models.Book>? Book { get; set; }

        public DbSet<BookStoreMVC.Models.Genre>? Genre { get; set; }

        public DbSet<BookStoreMVC.Models.Order>? Order { get; set; }

        public DbSet<BookStoreMVC.Models.OrderDetail>? OrderDetail { get; set; }

        public DbSet<BookStoreMVC.Models.User>? User { get; set; }
    }
}
