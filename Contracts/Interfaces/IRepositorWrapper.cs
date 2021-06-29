using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface IRepositorWrapper
    {
        IEmployeesRepository Employees { get; }

        IVolunteersRepository Volunteers { get; }

        IUsersRepository Users { get; }

        IVehiclesRepository Vehicles { get; }

        void Save();
    }
}
