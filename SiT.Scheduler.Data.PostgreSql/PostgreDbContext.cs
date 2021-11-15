using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiT.Scheduler.Data.PostgreSql
{
    class PostgreDbContext : SchedulerDbContext
    {
        public PostgreDbContext(DbContextOptions<PostgreDbContext> options)
        {
        }
    }
}
