namespace stocktake.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<stocktake.DAL.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled =true;
            ContextKey = "stocktake.DAL.MyContext";
        }

        protected override void Seed(stocktake.DAL.MyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
