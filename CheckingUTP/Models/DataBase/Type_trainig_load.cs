using System.ComponentModel.DataAnnotations;

namespace CheckingUTP.Models.DataBase
{
    public class Type_trainig_load
    {[Key]
      public int Type_trainig_load_id { get; set; }
       public string Name { get; set; }
         public float Voulme_hours_per_listener { get; set; }
       public float Number_groups { get; set; }
        public float Number_subgroups { get; set; }
        public float Number_control_forms { get; set; }
        public float Number_listeners { get; set; }
        public float Voulme_hours { get; set; }
        public string Status { get; set; }
        public int Type { get; set; }
        public int Utp_id { get; set; }
    }
}
