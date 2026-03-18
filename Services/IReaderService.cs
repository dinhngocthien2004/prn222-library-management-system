using BusinessObjects;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IReaderService
    {
        IEnumerable<Reader> GetReaders();

        Reader? GetReaderById(int id);

        void SaveReader(Reader r);

        void UpdateReader(Reader r);

        void DeleteReader(Reader r);
    }
}
