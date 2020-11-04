using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class MedicineRepository : ICruzRojaRepository<Medicine>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public MedicineRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public void Add(Medicine estate)
        {
            //Verifico que el Usuario no sea null
            if (estate == null)
            {
                throw new ArgumentNullException(nameof(estate));
            }

            _context.Medicine.Add(estate);
        }

        public void Delete(Medicine entity)
        {
            if (entity == null) //Verifico que el Usuario no sea null
            {
                throw new ArgumentNullException(nameof(entity));
            }
            //Se retorna al Controller que no hay errores
            _context.Medicine.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }

        public IEnumerable<Medicine> GetList()
        {
            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
            return _context.Medicine
                    .ToList();
        }

        public Medicine GetListId(int MedicineID)
        {
            if (MedicineID.ToString() == "") // si el usuario esta vacio
            {
                throw new ArgumentNullException(nameof(MedicineID));
            }

            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
            return _context.Medicine
                 .FirstOrDefault(a => a.MedicineID == MedicineID);
        }

        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Medicine entity)
        {
            throw new NotImplementedException();
        }
    }
}
