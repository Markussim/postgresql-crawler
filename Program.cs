using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    public class BloggingContext : DbContext
    {
        public DbSet<Persons4> people { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Database=my_ver_nice_db;Username=postgres;Password=1");

        private static void Main()
        {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Persons4 { personID = 12 });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.people;

                Console.WriteLine(blog);
                db.SaveChanges();
            }
        }

    }

    public class Persons4
    {
        public int Id { get; set; }

        public int personID { get; set; }
    }




}