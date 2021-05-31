//using Back_End.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Back_End.Services
//{
//    public class VolunteersRepository : ICruzRojaRepository<Volunteer>, IDisposable
//    {
//        public readonly CruzRojaContext2 _context;

//        public VolunteersRepository(CruzRojaContext2 context)
//        {
//            _context = context ?? throw new ArgumentException(nameof(context));
//        }
//        public IEnumerable<Volunteer> GetList()
//        {
//            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
//            return _context.Volunteer
//                    .ToList();
//        }

//        public void Add(Volunteer volunteer)
//        {
//            //Verifico que el Usuario no sea null
//            if (volunteer == null)
//            {
//                throw new ArgumentNullException(nameof(volunteer));
//            }

//            _context.Volunteer.Add(volunteer);
//        }

//        public void Delete(Volunteer volunteer)
//        {
//            if (volunteer == null) //Verifico que el Usuario no sea null
//            {
//                throw new ArgumentNullException(nameof(volunteer));
//            }
//            //Se retorna al Controller que no hay errores
//            _context.Volunteer.Remove(volunteer);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        protected virtual void Dispose(bool disposing)
//        {

//        }

//        public Volunteer GetListId(int VolunteerID)
//        {
//            if (VolunteerID.ToString() == "") // si el usuario esta vacio
//            {
//                throw new ArgumentNullException(nameof(VolunteerID));
//            }

//            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
//            return _context.Volunteer
//                 .FirstOrDefault(a => a.VolunteerID == VolunteerID);
//        }

//        public bool save()
//        {
//            return (_context.SaveChanges() >= 0);
//        }

//        public void Update(Volunteer volunteer)
//        {
//        }
//    }
//}
