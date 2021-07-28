using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IEstatesRepository : IRepositoryBase<Estates>
    {
        IEnumerable<Estates> GetAllEstates();
    }
}
