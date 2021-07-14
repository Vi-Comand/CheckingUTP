using System.ComponentModel.DataAnnotations;


namespace CheckingUTP.Models.DataBase
{
    public class UTP
    {[Key]
        public int Utp_id { get; set; }
        public string Name { get; set; }
        public float Hour { get; set; }
        public int Kol_slushatel_v_group { get; set; }
        public int Kol_groups { get; set; }
        public int Rejim_zanyati{get;set;}
        public int Forma_obuchen_id { get; set; }
    }
}
