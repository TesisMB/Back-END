namespace Contracts.Interfaces
{
    public interface IRepositorWrapper
    {
        IEmployeesRepository Employees { get; }

        IVolunteersRepository Volunteers { get; }

        IUsersRepository Users { get; }

        IVehiclesRepository Vehicles { get; }

        IMaterialsRepository Materials { get; }

        IMedicinesRepository Medicines { get; }

        IEstatesRepository Estates { get; }
        IEmergenciesDisastersRepository EmergenciesDisasters { get; }

        ITypesEmergenciesDisastersRepository TypesEmergenciesDisasters { get; }

        IResources_RequestRepository Resources_Requests { get; }

        IChatRoomsRepository ChatRooms { get; }
        IChatRepository Chat { get; }
        IMessageRepository Messages { get; }

        IUsersChatRoomsRepository UsersChatRooms { get; }
        ITypesVehicles TypesVehicles { get; }
        IPDFRepository PDF { get; }


        // void Save();
    }
}
