//using Back_End.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Back_End.Services
//{
//    public class VehiclesRepository : ICruzRojaRepository<Vehicles>, IDisposable
//    {
//        public readonly CruzRojaContext2 _context;

//        public VehiclesRepository(CruzRojaContext2 context)
//        {
//            _context = context ?? throw new ArgumentException(nameof(context));
//        }
//        public IEnumerable<Vehicles> GetList()
//        {
//            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
//            return _context.Vehicles
//                    .ToList();
//        }

//        public void Add(Vehicles vehicles)
//        {
//            //Verifico que el Usuario no sea null
//            if (vehicles == null)
//            {
//                throw new ArgumentNullException(nameof(vehicles));
//            }

//            _context.Vehicles.Add(vehicles);
//        }

//        public void Delete(Vehicles vehicles)
//        {
//            if (vehicles == null) //Verifico que el Usuario no sea null
//            {
//                throw new ArgumentNullException(nameof(vehicles));
//            }
//            //Se retorna al Controller que no hay errores
//            _context.Vehicles.Remove(vehicles);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        protected virtual void Dispose(bool disposing)
//        {

//        }

//        public Vehicles GetListId(int VehiclesID)
//        {
//            if (VehiclesID.ToString() == "") // si el usuario esta vacio
//            {
//                throw new ArgumentNullException(nameof(VehiclesID));
//            }

//            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
//            return _context.Vehicles
//                 .FirstOrDefault(a => a.VehicleID == VehiclesID);
//        }

//        public bool save()
//        {
//            return (_context.SaveChanges() >= 0);
//        }

//        public void Update(Vehicles vehicles)
//        {
//        }
//    }
//}
