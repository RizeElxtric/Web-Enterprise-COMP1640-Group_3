using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingEvent.Database.Common
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
