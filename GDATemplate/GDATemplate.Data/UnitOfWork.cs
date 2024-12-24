using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDATemplate.Data.Context;
using GDATemplate.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GDATemplate.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlContext _context;

        public UnitOfWork(SqlContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Commit()
            => await _context.SaveChangesAsync() > 0;

        public Task Rollback()
            => Task.CompletedTask;

    }
}
