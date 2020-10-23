using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stocktake.Entity;
using System.Data.Common;
using System.Data.OleDb;

namespace stocktake.DAL
{
    public partial class MyContext : DbContext
    {
        public MyContext():base("name=AccessContext")
        {  
        //{
        //    var dbConnectionString = "Provider=Microsoft.ACE.OleDb.12.0;Data source=Provider=Microsoft.ACE.OleDb.12.0;Data Source=D:\\stocktake_forjhh\\stocktake\\StoreTake.mdb" + ";Persist Security Info=False";
        //    var conn = DbProviderFactories.GetFactory("JetEntityFrameworkProvider").CreateConnection();
        //    conn.ConnectionString = dbConnectionString;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var dbConnectionString = "Provider=Microsoft.ACE.OleDb.12.0;Data source=Provider=Microsoft.ACE.OleDb.12.0;Data Source=D:\\stocktake_forjhh\\stocktake\\StoreTake.mdb" + ";Persist Security Info=False";
            //OleDbConnection conn = new OleDbConnection(dbConnectionString);
            //conn.Open();
            //var exists = conn.GetSchema("Tables", new string[4] { null, null, "__MigrationHistory", "TABLE" }).Rows.Count > 0;

            //if (!exists)
            //{
            //    OleDbCommand cmd = new OleDbCommand("CREATE TABLE __MigrationHistory([MigrationId] TEXT, [ContextKey] MEMO, [Model] OleObject, [ProductVersion] TEXT)", conn);
            //    cmd.ExecuteNonQuery();

            //    cmd = new OleDbCommand("INSERT INTO __MigrationHistory(MigrationId, ContextKey, ProductVersion) VALUES('" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "', '" + typeof(Form1).Namespace + ".Context', '6.2.0 - 61023')", conn);
            //    cmd.ExecuteNonQuery();
           // }
        }

        public DbSet<BarCodeInfo> barCodeInfo { get; set; }
        public DbSet<TakeCode> takeCode { get; set; }
        public DbSet<NoexitsBar> noexitsBar { get; set; }
    }
}
