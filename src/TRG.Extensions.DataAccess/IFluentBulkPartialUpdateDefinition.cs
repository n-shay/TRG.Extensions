﻿namespace TRG.Extensions.DataAccess
{
    using System;
    using System.Linq.Expressions;

    public interface IFluentBulkPartialUpdateDefinition<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Defines the field for update (inclusive).
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldSelector"></param>
        /// <returns></returns>
        IFluentBulkPartialUpdateDefinition<TEntity> Include<TField>(Expression<Func<TEntity, TField>> fieldSelector);

        /// <summary>
        /// Adds an update operation to the bulk operation queue.
        /// </summary>
        void Update();

        /// <summary>
        /// Adds an upsert operation to the bulk operation queue.
        /// </summary>
        void Upsert();
    }
}