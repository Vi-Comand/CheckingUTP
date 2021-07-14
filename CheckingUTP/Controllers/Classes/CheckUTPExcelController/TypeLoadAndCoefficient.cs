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
    public class TypeLoadAndCoefficientModel
    {
        public string NameLoad{get;set;}
        public int TypeLoad{get;set;}
        public float WeightLenght{get;set;}=0;
        public float WeightMatch{get;set;}=0;


    }
}