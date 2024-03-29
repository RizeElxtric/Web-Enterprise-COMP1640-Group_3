using MarketingEvent.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingEvent.Database.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketingEventDbContext _context;

        public UnitOfWork(MarketingEventDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
