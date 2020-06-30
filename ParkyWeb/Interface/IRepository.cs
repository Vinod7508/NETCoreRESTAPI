using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Interface
{
   public interface IRepository<T> where T: class

    { 
        //T is representing gereric class here
        //for all method..we are expecting two parameters...Url to which are making a call and id for indivial object.
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);
         
        //bool datatype is for denoting wheather operation from client side is succesfull or not.
        Task<bool> CreateAsync(string url, T objToCreate);
        Task<bool> UpdateAsync(string url, T objToUpdate);
        Task<bool> DeleteAsync(string url, int Id);
    }
}
