using ParkyWeb.Interface;
using ParkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalPark
    {
        private readonly IHttpClientFactory _clientFactory;

        public NationalParkRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
