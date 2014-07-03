namespace MarketTestApp.Migrations
{
    using MarketTestApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MarketTestApp.Models.MarketAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MarketTestApp.Models.MarketAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Item.AddOrUpdate<Item>(
            //    p => p.Name,
            //    new Item { Name = "Coin" },
            //    new Item { Name = "Sword 1" },
            //    new Item { Name = "Sword 2" },
            //    new Item { Name = "Sword 3" }
            //);
        }
    }
}
