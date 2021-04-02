using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;



namespace EFGetStarted
{
    public class BloggingContext : DbContext
    {
        public DbSet<people> customers_orders2 { get; set; }
        static ManualResetEvent resetEvent = new ManualResetEvent(false);

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Database=my_ver_nice_db;Username=postgres;Password=1");

        private static void Main()
        {
            using (var db = new BloggingContext())
            {
                Main2(db).Wait();
            }
        }

        private static async Task Main2(BloggingContext db)
        {
            // Note: This sample requires the database to be created before running.

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new people { customer_id = 12 });
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = db.customers_orders2;
            db.SaveChanges();

            //Console.WriteLine(blog.Find(1).customer_id);

            CancellationToken token = default;
            Console.WriteLine("Test");
            foreach (var item in await blog.ToListAsync(token))
            {
                Console.WriteLine(item.customer_id);
            }

            db.SaveChanges();

            //Thread.Sleep(1000);
        }

    }

    public class people
    {
        public int id { get; set; }

        public int customer_id { get; set; }
    }




}