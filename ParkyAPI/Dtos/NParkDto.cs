using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Dtos
{
    public class NParkDto  //we need something that can handle or represent our API..and thats is DTO..Many times its possible that dtos are similar to modelclass
    {

        //for conversion nparkDTO object to npark model class object..we have used autommaper configuration.


        //[Key]
        public int Id { get; set; }

        //[Required]
        public string Name { get; set; }

        //[Required]
        public string State { get; set; }

        public DateTime Created { get; set; }

        public DateTime Established { get; set; }

    }
}
