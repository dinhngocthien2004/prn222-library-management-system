using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ReaderDAO _dao;

        public ReaderRepository(ReaderDAO dao)
        {
            _dao = dao;
        }

        public IEnumerable<Reader> GetReaders()
        {
            return _dao.GetAll();
        }

        public Reader? GetReaderById(int id)
        {
            return _dao.GetById(id);
        }

        public void SaveReader(Reader r)
        {
            _dao.Create(r);
        }

        public void UpdateReader(Reader r)
        {
            _dao.Update(r);
        }

        public void DeleteReader(Reader r)
        {
            _dao.Delete(r);
        }
    }
}



