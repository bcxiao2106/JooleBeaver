using JooleWebAPI.Interfaces;
using System;

namespace JooleWebAPI.Unitofwork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}
