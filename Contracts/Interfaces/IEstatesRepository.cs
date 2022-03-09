using Back_End.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEstatesRepository : IRepositoryBase<Locations>
    {
       Task<IEnumerable<Locations>> GetAllEstates();
        Task<IEnumerable<Locations>> GetAllEstatesType();

    }
}
