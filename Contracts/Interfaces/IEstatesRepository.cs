using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEstatesRepository : IRepositoryBase<Estates>
    {
       Task<IEnumerable<Estates>> GetAllEstates();
        Task<IEnumerable<Estates>> GetAllEstatesType();

    }
}
