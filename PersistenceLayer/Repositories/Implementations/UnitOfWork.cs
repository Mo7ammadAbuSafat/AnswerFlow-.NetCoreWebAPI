﻿using PersistenceLayer.DbContexts;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AnswerFlowContext context;
        public UnitOfWork(AnswerFlowContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChangesAsync()
        {

            return await context.SaveChangesAsync();
        }
    }
}
