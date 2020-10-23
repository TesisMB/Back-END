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
        public void Add(Medicine entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Medicine entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medicine> GetList()
        {
            throw new NotImplementedException();
        }

        public Medicine GetListId(int EntityID)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            throw new NotImplementedException();
        }

        public void Update(Medicine entity)
        {
            throw new NotImplementedException();
        }
    }
}
