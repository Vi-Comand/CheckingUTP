using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CheckingUTP.Models.ModelsUTP;
using static CheckingUTP.Controllers.CheckUTPExcelController;

namespace CheckingUTP.Controllers.Classes.CheckUTPExcelController
{
    public class WorkPropertyUTP
    {
        public WorkPropertyUTP()
        {

        }
        public PropertyUTP GetPropertyUTP(List<ModelVrem> propUTP)
       {
           PropertyUTP propertyUTP = new PropertyUTP();
            foreach (var row in propUTP)
            {  
                if(row.str1== "Режим занятий")
                propertyUTP.Rejim_zanyati = int.Parse(string.Join("", row.str2.Where(c => char.IsDigit(c))));
                
                if (row.str1 == "Форма обучения:")
                    propertyUTP.Forma_obuchen = row.str2;
               
                if (row.str1 == "Количество групп:")
                    propertyUTP.Kol_groups = int.Parse(row.str2);
                
                if (row.str1 == "Количество слушателей в группе:")
                    propertyUTP.Kol_slushatel_v_group = int.Parse(row.str2);

                    }
            return propertyUTP;
        }
    }
}
