using Domain.Posts;
using Domain.Entiteit;
using System.Data.Entity;
using System;
using Domain.Alert;
using Domain.Account;

namespace DAL.EF
{
    internal class EFContext : DbContext
    {
        //DelaySave zorgt ervoor dat de gewone SaveChanges niet uitgevoerd wordt
        // indien deze boolean op true staat. 
        //Wordt gebruikt in het kader van het unit-of-work pattern. 
        private readonly bool delaySave;

        /// <summary>
        /// Constructor of EFContext, loads the connectionstring based on de
        /// configuration key "PizzaDB"
        /// </summary>
        /// <param name="unitOfWorkPresent">
        /// Indicates is this context class operates under a Unit-Of-Work pattern. If so
        /// SaveChanges will not be executed on the database, instead you'll need to use
        /// CommitChanges (but that method is not public available)
        /// By default, unitOfWorkPresent will be set to false
        /// </param>
        /// 
        public EFContext(bool unitOfWorkPresent = false) :base("DebugConn")
        {
            Database.SetInitializer<EFContext>(new EFDbInitialiser());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFContext, EFConfiguration>("DebugConn"));
            delaySave = unitOfWorkPresent;
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Entiteit> Entiteiten { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Trend> Trends { get; set; }

        public override int SaveChanges()
        {
            if (delaySave) return -1;
            return base.SaveChanges();
        }

        internal int CommitChanges()
        {
            if (delaySave)
            {
                return base.SaveChanges();
            }
            throw new InvalidOperationException("No UnitOfWork present, use SaveChanges instead");
        }
    }
}
