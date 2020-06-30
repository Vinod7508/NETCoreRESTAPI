using ParkyWeb.Interface;
using ParkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class TrailRepository : Repository<Trail>, ITrail
    {


        private readonly IHttpClientFactory _clientFactory;

        public TrailRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }


    }
}
