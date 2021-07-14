using System.ComponentModel.DataAnnotations;

namespace CheckingUTP.Models.DataBase
{
    public class UTP_type_training_load
    {
        [Key]
      public int UTP_type_training_load_id { get; set; }
        public int UTP_id { get; set; }
        public int Type_training_load_id { get; set; }

    }
}
