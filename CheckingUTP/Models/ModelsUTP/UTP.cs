

using System.Collections.Generic;

namespace CheckingUTP.Models.ModelsUTP
{
    public class UTP
    {
        public PropertyUTP propertyUTP { get; set; } = new PropertyUTP();
        public List<TableModel> table { get; set; } = new List<TableModel>() ;
    }
}
