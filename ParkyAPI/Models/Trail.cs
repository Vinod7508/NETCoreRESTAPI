using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }


        public enum DifficultyType {
            Easy,
            Moderate,
            Difficult,
            Expert
        }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NParkId  { get; set; }

        [ForeignKey("NParkId")]
        public NPark NPark { get; set; }

        public DateTime DateCreated { get; set; }  //we are not exposing this property to outsideword/ftos


    }
}
