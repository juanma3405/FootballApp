using System.ComponentModel.DataAnnotations;

namespace BusinessLogicFootballApp.Entities
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
