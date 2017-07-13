using System.Data.Common;
using Abp.EntityFramework;
using TradingAnalyzer.Entities;
using System.Data.Entity;

namespace TradingAnalyzer.EntityFramework
{
    public class TradingAnalyzerDbContext : AbpDbContext
    {
        public virtual IDbSet<Trade> Trades { get; set; }
        public virtual IDbSet<TradingAccount> TradingAccounts { get; set; }
        public virtual IDbSet<TradingDay> TradingDays { get; set; }
        public virtual IDbSet<MarketLogEntry> MarketLogEntries { get; set; }
        public virtual IDbSet<TradingDirective> TradingDirectives { get; set; }
        public virtual IDbSet<Market> Markets { get; set; }

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TradingAnalyzerDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TradingAnalyzerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TradingAnalyzerDbContext since ABP automatically handles it.
         */
        public TradingAnalyzerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public TradingAnalyzerDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public TradingAnalyzerDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
