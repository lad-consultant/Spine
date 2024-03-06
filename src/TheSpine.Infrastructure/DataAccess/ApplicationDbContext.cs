
using Microsoft.EntityFrameworkCore;
using TheSpine.Core;

namespace TheSpine.Infrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<SegmentItem> SegmentItems { get; set; }
        public DbSet<ItemDetailedInfo> ItemDetailedInfos { get; set; }
        public DbSet<Activity> Activitys { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>().HasData(
                new Node
                { 
                    Id= 1,
                    Title = "Pursue",
                    OrderingIndex = 0
                },
                new Node
                {
                    Id = 2,
                    Title = "Secure",
                    OrderingIndex = 1
                }, 
                new Node
                {
                    Id = 3,
                    Title = "Visualize",
                    OrderingIndex = 2
                }, 
                new Node
                {
                    Id = 4,
                    Title = "Exchange",
                    OrderingIndex = 3
                }, 
                new Node
                {
                    Id = 5,
                    Title = "Resolve",
                    OrderingIndex = 4
                },
                new Node
                {
                    Id = 6,
                    Title = "Administer",
                    OrderingIndex = 5
                },
                new Node
                {
                    Id = 7,
                    Title = "Market",
                    OrderingIndex = 6
                },
                new Node
                {
                    Id = 8,
                    Title = "Manage",
                    OrderingIndex = 7
                },
                new Node
                {
                    Id = 9,
                    Title = "Design",
                    OrderingIndex = 8
                },
                new Node { Id = 11, Title = "Capture", ParentId = 9, OrderingIndex = 0 },
                new Node { Id = 12, Title = "Create", ParentId = 9, OrderingIndex = 1 },
                new Node { Id = 13, Title = "Plan", ParentId = 9, OrderingIndex = 2 },
                new Node { Id = 14, Title = "Visualize", ParentId = 9, OrderingIndex = 3 },
                new Node { Id = 15, Title = "Compute", ParentId = 9, OrderingIndex = 4 },
                new Node { Id = 16, Title = "Simulate", ParentId = 9, OrderingIndex = 5 },
                new Node { Id = 17, Title = "Analyze", ParentId = 9, OrderingIndex = 6 },
                new Node { Id = 18, Title = "Illustrate", ParentId = 9, OrderingIndex = 7 },
                new Node { Id = 19, Title = "Delineate", ParentId = 9, OrderingIndex = 8 },
                new Node { Id = 20, Title = "Narrate", ParentId = 9, OrderingIndex = 9 },
                new Node { Id = 21, Title = "Specify", ParentId = 9, OrderingIndex = 10 });


            modelBuilder.Entity<Segment>().HasData(
                new Segment { Id = 31, Title = "Rendering/Animation" },
                new Segment { Id = 32, Title = "Reports/Postproduction" });
        }
    }
}
