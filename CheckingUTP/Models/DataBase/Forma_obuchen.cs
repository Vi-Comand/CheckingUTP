using System.ComponentModel.DataAnnotations;


namespace CheckingUTP.Models.DataBase
{
    public class Forma_obuchen
    {
        [Key]
        public int Forma_obucen_id { get; set; }
        public string Name { get; set; }
    }
}
