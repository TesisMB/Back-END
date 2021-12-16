using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Microsoft.Extensions.Options;

namespace Repository
{
    public class RepositoryWrapper : IRepositorWrapper
    {
        private CruzRojaContext _cruzRojaContext;
        private IEmployeesRepository _employees;
        private IVolunteersRepository _volunteers;
        private IUsersRepository _users;
        private IVehiclesRepository _vehicles;
        private IMaterialsRepository _materials;
        private IMedicinesRepository _medicines;
        private IEstatesRepository _estates;
        private IEmergenciesDisastersRepository _emergenciesDisasters;
        private ITypesEmergenciesDisastersRepository _typesEmergenciesDisasters;
        private IResources_RequestRepository _resources_Request;
        private IChatRoomsRepository _chatRooms;
        private IChatRepository _chats;

        private IMapper _mapper;

        public RepositoryWrapper(CruzRojaContext cruzRojaContext, IMapper mapper)
        {
            _cruzRojaContext = cruzRojaContext;
            _mapper = mapper;
        }

        public IEmployeesRepository Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new EmployeesRepository(_cruzRojaContext, _mapper);
                }
                return _employees;
            }
        }

        public IVolunteersRepository Volunteers
        {
            get
            {
                if (_volunteers == null)
                {
                    _volunteers = new VoluntersRepository(_cruzRojaContext);
                }
                return _volunteers;
            }
        }

        public IUsersRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UsersRepository(_cruzRojaContext, _mapper);
                }
                return _users;
            }
        }

        public IVehiclesRepository Vehicles
        {
            get
            {
                if (_vehicles == null)
                {
                    _vehicles = new VehiclesRepository(_cruzRojaContext);
                }
                return _vehicles;
            }
        }

        public IMaterialsRepository Materials
        {
            get
            {
                if (_materials == null)
                {
                    _materials = new MaterialsRepository(_cruzRojaContext);
                }
                return _materials;
            }
        }

        public IMedicinesRepository Medicines
        {
            get
            {
                if (_medicines == null)
                {
                    _medicines = new MedicinesRepository(_cruzRojaContext);
                }
                return _medicines;
            }
        }

        public IEstatesRepository Estates
        {
            get
            {
                if (_estates == null)
                {
                    _estates = new EstatesRepository(_cruzRojaContext);
                }
                return _estates;
            }
        }

        public IEmergenciesDisastersRepository EmergenciesDisasters
        {
            get
            {
                if (_emergenciesDisasters == null)
                {
                    _emergenciesDisasters = new EmergenciesDisastersRepository(_cruzRojaContext);
                }
                return _emergenciesDisasters;
            }
        }

        public ITypesEmergenciesDisastersRepository TypesEmergenciesDisasters
        {
            get
            {
                if (_typesEmergenciesDisasters == null)
                {
                    _typesEmergenciesDisasters = new TypesEmergenciesDisastersRepository(_cruzRojaContext);
                }
                return _typesEmergenciesDisasters;
            }
        }

        public IResources_RequestRepository Resources_Requests
        {
            get
            {
                if (_resources_Request == null)
                {
                    _resources_Request = new Resources_RequestRepository(_cruzRojaContext);
                }
                return _resources_Request;
            }
        }

        public IChatRoomsRepository ChatRooms
        {
            get
            {
                if (_chatRooms == null)
                {
                    _chatRooms = new ChatRoomsRepository(_cruzRojaContext);
                }
                return _chatRooms;
            }
        }


        public IChatRepository Chat
        {
            get
            {
                if (_chats == null)
                {
                    _chats = new ChatRepository(_cruzRojaContext);
                }
                return _chats;
            }
        }



        /*public void Save()
        {
            _cruzRojaContext2.SaveChanges();
        }*/
    }
}
