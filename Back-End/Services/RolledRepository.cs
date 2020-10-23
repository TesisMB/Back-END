using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{

    public class RolledRepository : ICruzRojaRepository<Rolled>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public RolledRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public void Add(Rolled entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Rolled entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rolled> GetList()
        {
            throw new NotImplementedException();
        }

        public Rolled GetListId(int EntityID)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            throw new NotImplementedException();
        }

        public void Update(Rolled entity)
        {
            throw new NotImplementedException();
        }
    }
}
