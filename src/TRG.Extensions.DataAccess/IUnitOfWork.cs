﻿namespace TRG.Extensions.DataAccess
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        // TODO: Implement transaction support.

        /// <summary>
        /// Persists all changes to the backing data store.
        /// </summary>
        /// <returns>True if successful.</returns>
        bool Save();

        /// <summary>
        /// Persists all changes to the backing data store.
        /// </summary>
        /// <returns>True if successful.</returns>
        Task<bool> SaveAsync();
    }
}
