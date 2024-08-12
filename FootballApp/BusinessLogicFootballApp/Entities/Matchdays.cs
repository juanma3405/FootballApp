using System.ComponentModel.DataAnnotations;

namespace BusinessLogicFootballApp.Entities
{
    public class Matchdays
    {
        [Key]
        public int Mid { get; set; }
        public string Mmatchdays { get; set; }
        public int Lid { get; set; }
    }
}
