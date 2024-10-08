using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage<T> : IStorage <T>
    {
       public void AddAccount (T account);
       public void DeleteAccount(T account);
       public void UpdateAccount(T account);
    }
}
