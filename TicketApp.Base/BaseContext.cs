using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Threading.Tasks;

namespace TicketApp.Base
{
    public abstract class BaseContext : DbContext
    {
        protected Func<Type, bool> ConfigurationTypePredicate => null;
        public static readonly LoggerFactory loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        protected BaseContext() { }

        public bool Commit() => SaveChanges() > 0;
        public async Task<bool> CommitAsync() => await SaveChangesAsync().ConfigureAwait(true) > 0;
    }
}
