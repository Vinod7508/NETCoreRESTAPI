using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NParkRepository : INParkRepository
    {

        private readonly ApplicationDbContext _db;

        public NParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        //Create Method for new nationalpark
        public bool CreateNpark(NPark nPark)
        {


            _db.NParks.Add(nPark);
            return Save();
           
        }


       //Delete Method for National Park

        public bool DeleteNpark(NPark nPark)
        {
            _db.NParks.Remove(nPark);
            return Save();
        }

        //Method for Getting All National Parks
        public ICollection<NPark> GetAllNParks()
        {
            return _db.NParks.OrderBy(a => a.Name).ToList();
        }


        //Method for Getting one National Parks based on ID
        public NPark GetPark(int NParkId)
        {
            return _db.NParks.FirstOrDefault(a => a.Id == NParkId);
        }

        //Method to check wheather National Park Exist with Name
        public bool NparkExist(string Name)
        {
            bool value = _db.NParks.Any(a => a.Name.ToLower().Trim() == Name.ToLower().Trim());
            return value;
        }

        //Method to check wheather National Park Exist with Passed NParkId
        public bool NparkExist(int NParkId)
        {
            bool value = _db.NParks.Any(a => a.Id == NParkId);
            return value;
        }


        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }


        //Method to update existing National Park
        public bool UpdateNpark(NPark nPark)
        {
            _db.NParks.Update(nPark);
            return Save();
        }
    }
}
