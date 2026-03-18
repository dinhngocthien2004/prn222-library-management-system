using BusinessObjects;
using BusinessObjects.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _repo;

        public ReaderService(IReaderRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Reader> GetReaders()
        {
            return _repo.GetReaders();
        }

        public Reader? GetReaderById(int id)
        {
            return _repo.GetReaderById(id);
        }

        public void SaveReader(Reader r)
        {
            _repo.SaveReader(r);
        }

        public void UpdateReader(Reader r)
        {
            _repo.UpdateReader(r);
        }

        public void DeleteReader(Reader r)
        {
            _repo.DeleteReader(r);
        }
    }
}


