using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INParkRepository
    {

        ICollection<NPark> GetAllNParks();

        NPark GetPark(int NParkId);

        bool NparkExist(string Name);

        bool NparkExist(int NParkId);

        bool CreateNpark(NPark nPark);


        bool UpdateNpark(NPark nPark);

        bool DeleteNpark(NPark nPark);

        bool Save();
    }
}
