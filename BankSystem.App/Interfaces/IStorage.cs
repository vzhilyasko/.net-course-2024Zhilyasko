using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Interfaces
{
    public interface IStorage<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
