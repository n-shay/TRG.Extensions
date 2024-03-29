﻿namespace TRG.Extensions.DataAccess.DynamoDB
{
    using System;
    using System.Threading.Tasks;

    using TRG.Extensions.Logging;

    public abstract class WritableKeyedRepository<TEntity, TPrimaryKey>
        : WritableRepository<TEntity>, 
          IWritableKeyedRepository<TEntity, TPrimaryKey>
        where TEntity : class, IKeyedEntity<TPrimaryKey>
    {
        protected WritableKeyedRepository(ILogger logger, IContextProvider<IDbContext> contextProvider)
            : base(logger, contextProvider)
        {
        }

        public IFluentBulkUpdateDefinitionBuilder<TEntity> BulkOperation => throw new NotImplementedException();

        public Task<int> ExecuteBulk(bool isOrdered = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
        {
            return await this.Context.LoadAsync<TEntity>(key);
        }
    }
}