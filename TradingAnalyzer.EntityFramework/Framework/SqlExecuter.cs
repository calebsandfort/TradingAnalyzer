using Abp.Dependency;
using Abp.EntityFramework;
using TradingAnalyzer.EntityFramework;
using TradingAnalyzer.Shared.SqlExecuter;

namespace TradingAnalyzer.Framework
{
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<TradingAnalyzerDbContext> _dbContextProvider;

        public SqlExecuter(IDbContextProvider<TradingAnalyzerDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public int Execute(string sql, params object[] parameters)
        {
            return _dbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
