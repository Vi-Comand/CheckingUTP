using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CheckingUTP.Models.ModelsUTP;
namespace CheckingUTP.Controllers.Classes.CheckUTPExcelController
{
    public class DeterminateTypeLoad
    {
        private List<TypeLoadAndCoefficientModel> ListLoad;
        private int type;
        public DeterminateTypeLoad(List<TypeLoadAndCoefficientModel> listLoad)
        {
            ListLoad = listLoad;
        }
        public int SearchType(string ExcelNameLoad)
        {
            DeterminateWeightMatchsOnLsit(ExcelNameLoad);
            return type;
        }
        private int DeterminateWeightMatchsOnLsit(string NameLoad)
        {
            NameLoad = NameLoad.Replace(" ", "");

            foreach (var row in ListLoad)
            {
                row.WeightLenght = LenghtListLoad(row.NameLoad.Length, NameLoad.Length);
            }

            ListLoad = ListLoad.Where(x => x.WeightLenght > 0.8 && x.WeightLenght < 1.2).ToList();

            foreach (var row in ListLoad)
            {
                if (row.WeightLenght > 1)
                {
                    row.WeightLenght = 2 - row.WeightLenght;
                }
                row.WeightMatch = MatchListLoad(NameLoad, row.NameLoad);
            }
            if (ListLoad.Count != 0)
            {
                ListLoad = ListLoad.OrderByDescending(x => x.WeightMatch).ToList();
                foreach (var row in ListLoad)
                {
                    float perem = 0;
                    if (row.WeightMatch != 0)
                        perem = 1 - (row.WeightMatch / ListLoad[0].WeightMatch);
                    else
                    {
                        perem = 1;
                    }
                    row.WeightLenght = row.WeightLenght + perem;
                }

                ListLoad = ListLoad.OrderByDescending(x => x.WeightLenght).ToList();
                type = ListLoad[0].TypeLoad;
                return ListLoad[0].TypeLoad;
            }
            else
            {
                type = 0;
                return 0;
            }

        }
        private int MatchListLoad(string NameLoad, string RowNameLoad)
        {

            while (NameLoad != "")
            {
                RowNameLoad = RowNameLoad.Replace(NameLoad[0].ToString(), "");
                NameLoad = NameLoad.Replace(NameLoad[0].ToString(), "");

            }


            return RowNameLoad.Length;

        }

        private float LenghtListLoad(int LengthRow, int LengthNameLoad)
        {

            return (float)LengthNameLoad / LengthRow;

        }



    }
}