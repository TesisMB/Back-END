using AutoMapper;
using Back_End.Entities;
using Contracts.Interfaces;
using Entities.DataTransferObjects.Email;
using Microsoft.Extensions.Options;

namespace Repository
{
    public class RepositoryWrapper : IRepositorWrapper
    {
        private CruzRojaContext _cruzRojaContext2;
        private IEmployeesRepository _employees;
        private IVolunteersRepository _volunteers;
        private IUsersRepository _users;
        private IVehiclesRepository _vehicles;
        private IMaterialsRepository _materials;
        private IMedicinesRepository _medicines;
        private IEstatesRepository _estates;

        private IMapper _mapper;
        public RepositoryWrapper(CruzRojaContext cruzRojaContext2, IMapper mapper)
        {
            _cruzRojaContext2 = cruzRojaContext2;
            _mapper = mapper;
        }

        public IEmployeesRepository Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = new EmployeesRepository(_cruzRojaContext2, _mapper);
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
                    _volunteers = new VoluntersRepository(_cruzRojaContext2);
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
                    _users = new UsersRepository(_cruzRojaContext2, _mapper);
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
                    _vehicles = new VehiclesRepository(_cruzRojaContext2);
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
                    _materials = new MaterialsRepository(_cruzRojaContext2);
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
                    _medicines = new MedicinesRepository(_cruzRojaContext2);
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
                    _estates = new EstatesRepository(_cruzRojaContext2);
                }
                return _estates;
            }
        }

        /*public void Save()
        {
            _cruzRojaContext2.SaveChanges();
        }*/
    }
}
