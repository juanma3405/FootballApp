using System.ComponentModel.DataAnnotations;

namespace FootballAppV2.Models
{
    public class League
    {
        [Key]
        public int Lid { get; set; }
        public string Lname { get; set; }
        public string Lcode { get; set; }
        //public int matchdays { get; set; }
    }
}
