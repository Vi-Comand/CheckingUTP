using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CheckingUTP.Models.ModelsUTP;
using CheckingUTP.Controllers.Classes.CheckUTPExcelController;
using CheckingUTP.Models.DataBase;

namespace CheckingUTP.Controllers
{
    public class CheckUTPExcelController : Controller
    {
        Context db;
        public CheckUTPExcelController(Context context)
        {
            db = context;
        }


        List<ModelVrem> propUTP = new List<ModelVrem>();
        public IActionResult Index()
        {
            return View("UploadFile");
        }


        public IActionResult SaveOnChange(int ID, int Type, float Value)
        {
            Type_trainig_load type_Training_Load = new Type_trainig_load();
            type_Training_Load = db.Type_trainig_load.Find(ID);
            if (Type == 1)
                type_Training_Load.Voulme_hours_per_listener = Value;
            if (Type == 2)
                type_Training_Load.Number_groups = Value;
            if (Type == 3)
                type_Training_Load.Number_subgroups = Value;
            if (Type == 4)
                type_Training_Load.Number_control_forms = Value;
            if (Type == 5)
                type_Training_Load.Number_listeners = Value;
            if (Type == 6)
                type_Training_Load.Voulme_hours = Value;
            db.Type_trainig_load.Update(type_Training_Load);
            db.SaveChanges();
            return Json("Изменения внесены!");
        }
        public IActionResult SaveUTPOnline(Models.ModelsUTP.UTP model)
        {
            Models.DataBase.UTP uTP = db.UTPs.Find(model.propertyUTP.Utp_id);
            uTP.Hour = model.propertyUTP.Hour;
            uTP.Kol_groups = model.propertyUTP.Kol_groups;
            uTP.Kol_slushatel_v_group = model.propertyUTP.Kol_slushatel_v_group;
            uTP.Name = model.propertyUTP.Name;
            uTP.Rejim_zanyati = model.propertyUTP.Rejim_zanyati;

            db.UTPs.Update(uTP);
            db.Type_trainig_load.RemoveRange(db.Type_trainig_load.Where(x => x.Utp_id == model.propertyUTP.Utp_id && x.Type == 0));
            db.SaveChanges();
            return RedirectToAction("UTPViews", new { ID = model.propertyUTP.Utp_id });

        }


        public int SaveUTP(Models.ModelsUTP.UTP model)
        {

            List<Type_trainig_load> all = new List<Type_trainig_load>();
            Models.DataBase.UTP uTP = new Models.DataBase.UTP();
            uTP.Hour = model.propertyUTP.Hour;
            uTP.Kol_groups = model.propertyUTP.Kol_groups;
            uTP.Kol_slushatel_v_group = model.propertyUTP.Kol_slushatel_v_group;
            uTP.Name = model.propertyUTP.Name;
            uTP.Rejim_zanyati = model.propertyUTP.Rejim_zanyati;
            db.UTPs.AddRange(uTP);

            db.SaveChanges();

            foreach (var rows in model.table)
                foreach (var row in rows.Row)
                    all.Add(new Type_trainig_load { Name = row.NameTypeTrainingLoad, Voulme_hours_per_listener = row.VoulmeHoursPerListener, Number_control_forms = row.NumberControlForms, Number_groups = row.NumberGroups, Number_listeners = row.NumberListeners, Number_subgroups = row.NumberSubgroups, Type = row.Type, Utp_id = uTP.Utp_id });

            db.Type_trainig_load.AddRange(all);

            db.SaveChanges();
            return uTP.Utp_id;
        }

        public IActionResult CheckExcel()
        {
            string dirName = Directory.GetCurrentDirectory() + "/wwwroot/Files/";
            Models.ModelsUTP.UTP uTP = new Models.ModelsUTP.UTP();


            List<TableModel> listTable = new List<TableModel>();
            if (Directory.Exists(dirName))
            {
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {

                    using (var pck = new OfficeOpenXml.ExcelPackage())
                    {
                        FileInfo existingFile = new FileInfo(s);
                        //uTP.propertyUTP.Name 
                        using (ExcelPackage package = new ExcelPackage(existingFile))
                        {
                            //get the first worksheet in the workbook
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                            var colRow = worksheet.Dimension.End.Row;
                            var Table = InfoTable(worksheet, colRow);
                            listTable.Add(Table);
                        }
                        existingFile.Delete();
                    }
                }
            }

            WorkPropertyUTP workProperty = new WorkPropertyUTP();
            uTP.propertyUTP = workProperty.GetPropertyUTP(propUTP);
            uTP.table = listTable;
            if (listTable.Count != 0)
            {
                var model = uTP.table;
                int ID = SaveUTP(uTP);
                return RedirectToAction("UTPViews", new { ID });

            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [Route("CheckUTPExcelController/UTPViews/{ID}")]
        public IActionResult UTPViews(int ID)
        {
            Models.ModelsUTP.UTP Utp = new Models.ModelsUTP.UTP();
            Utp.propertyUTP = db.UTPs.Where(x => x.Utp_id == ID).Select(x => new PropertyUTP { Name = x.Name, Hour = x.Hour, Kol_groups = x.Kol_groups, Kol_slushatel_v_group = x.Kol_slushatel_v_group, Rejim_zanyati = x.Rejim_zanyati, Utp_id = x.Utp_id }).First();


            TableModel tableModel = new TableModel();
            tableModel.Row.AddRange(db.Type_trainig_load.Where(x => x.Utp_id == ID).Select(x => new RowTableModel { ID = x.Type_trainig_load_id, VoulmeHoursPerListener = x.Voulme_hours_per_listener, NameTypeTrainingLoad = x.Name, NumberControlForms = x.Number_control_forms, NumberGroups = x.Number_groups, NumberHours = x.Voulme_hours, NumberListeners = x.Number_listeners, NumberSubgroups = x.Number_subgroups, Type = x.Type, Status = x.Status, VolumeHours = x.Voulme_hours }));



            Utp.table.Add(tableModel);

            return View("UTPViews", Utp);

        }


        //public Models.ModelsUTP.UTP UTPViews(int ID)
        //{
        //    Models.ModelsUTP.UTP Utp = new Models.ModelsUTP.UTP();
        //    Utp.propertyUTP = db.UTPs.Where(x => x.Utp_id == ID).Select(x => new PropertyUTP { Name = x.Name, Hour = x.Hour, Kol_groups = x.Kol_groups,Kol_slushatel_v_group=x.Kol_slushatel_v_group,Rejim_zanyati=x.Rejim_zanyati,Utp_id=x.Utp_id }).First();


        //    TableModel tableModel = new TableModel();
        //    tableModel.Row.AddRange(db.Type_trainig_load.Where(x => x.Utp_id == ID).Select(x => new RowTableModel { ID=x.Type_trainig_load_id,VoulmeHoursPerListener=x.Voulme_hours_per_listener  ,NameTypeTrainingLoad = x.Name, NumberControlForms = x.Number_control_forms, NumberGroups = x.Number_groups, NumberHours = x.Voulme_hours, NumberListeners = x.Number_listeners, NumberSubgroups = x.Number_subgroups, Type = x.Type, Status = x.Status, VolumeHours = x.Voulme_hours })) ;



        //    Utp.table.Add(tableModel);

        //    return Utp;

        //}

        public class ModelVrem
        {
            public string str1 { get; set; }
            public string str2 { get; set; }
        }
        public TableModel InfoTable(ExcelWorksheet sheet, int colRow)
        {



            TableModel Table = new TableModel();
            int endTable = 0;
            int startTable = 0;
            bool startTableBool = false;
            bool endTableBool = false;


            for (int i = colRow; i > 0; i--)
            {
                if (sheet.Cells[i, 1].Text == "Итого часов по УТП:" && endTable == 0)
                {

                    startTable = i;
                    startTableBool = true;
                }

                if (endTableBool == true)
                {
                    propUTP.Add(new ModelVrem { str1 = sheet.Cells[i, 1].Text, str2 = sheet.Cells[i, 2].Text });
                }
                if (sheet.Cells[i, 1].Text == "Вид учебной работы" || sheet.Cells[i, 1].Text == "Вид учебной нагрузки")
                {
                    endTable = i;
                    startTableBool = false;
                    endTableBool = true;


                }



                if (startTableBool == true)
                {



                    var row = sheet;
                    endTable = i;



                    RowTableModel NewRow = new RowTableModel();
                    NewRow.NameTypeTrainingLoad = sheet.Cells[i, 1].Text;

                    NewRow.NumberControlForms = float.Parse(sheet.Cells[i, 5].Text != "" ? sheet.Cells[i, 5].Text : "0");
                    NewRow.NumberGroups = float.Parse(sheet.Cells[i, 3].Text != "" ? sheet.Cells[i, 3].Text : "0");
                    NewRow.NumberHours = float.Parse(sheet.Cells[i, 2].Text != "" ? sheet.Cells[i, 2].Text : "0");
                    NewRow.NumberListeners = float.Parse(sheet.Cells[i, 6].Text != "" ? sheet.Cells[i, 6].Text : "0");
                    NewRow.NumberSubgroups = float.Parse(sheet.Cells[i, 4].Text != "" ? sheet.Cells[i, 4].Text : "0");
                    NewRow.VolumeHours = float.Parse(sheet.Cells[i, 7].Text != "" ? sheet.Cells[i, 7].Text : "0");

                    if (NewRow.NumberHours != 0 && NewRow.VolumeHours == 0 && TypeTrainingLoad(NewRow.NameTypeTrainingLoad) != 0)
                    {
                        NewRow.Type = 3;

                    }

                    else
                        NewRow.Type = TypeTrainingLoad(NewRow.NameTypeTrainingLoad);



                    if (NewRow.VolumeHours != 0)
                    {
                        if (((NewRow.NumberHours == 0 ? 1 : NewRow.NumberHours) * (NewRow.NumberGroups == 0 ? 1 : NewRow.NumberGroups) * (NewRow.NumberSubgroups == 0 ? 1 : NewRow.NumberSubgroups) * (NewRow.NumberControlForms == 0 ? 1 : NewRow.NumberControlForms) * (NewRow.NumberListeners == 0 ? 1 : NewRow.NumberListeners)) != NewRow.VolumeHours)
                        {
                            NewRow.Status = "неверно";
                        }
                    }





                    Table.Add(NewRow);
                }
            }
            Table.Row.Reverse();
            int j = 0;
            foreach (var row in Table.Row)
            {
                row.ID = j;
                j++;
            }

            return Table;
        }


        public int TypeTrainingLoad(string NameTypeTrainingLoad)
        {
            List<string> Auditor = new List<string>() {"лекции",
"практическиезанятия",
"проведениегрупповыхконсультацийврамкахдпппк",
"проведениедиагностических,контрольныхработ,тестирование",
"итоговаяаттестация(защитапроектов,предусмотренныхучебнымпланом,обработкарезультатовтестированияидр.,всоответствиисуп)",
"итоговаяаттестация(приемписьменныхзачетныхработ,предусмотренныхучебнымпланом)",
"приёмэкзаменов,предусмотренныхучебнымпланом",
"участиевработеитоговойаттестационнойкомиссии",
"проведениевебинаров,видеоконференцийит.п.длядистанционногообучения"
 };
            List<string> VneAuditor = new List<string>() { "разработкановыхлекционныхматериаловиумк(дляочногообучения)конспектов,дидактическогоматериаладлялекционныхипрактическихзанятий",
"разработкаучебно-методическихматериаловдлядистанционногообучения(длязаочногообучения)",
"разработкадиагностическихматериалов(входной,промежуточной,выходнойдиагностики)",
"проверкавходнойи/иливыходнойдиагностикиуровняпредметнойготовностислушателей,подготовкааналитическойсправки",
"разработкаконтрольныхзаданийкзачету",
"разработкаэкзаменационныхматериалов",
"подготовкаматериаловпрезентационногохарактеравэлектронномвиде",
"организацияисопровождениефорумов,индивидуальныхконсультацийпридистанционномобучении(заочногообучения)",
"организационноеруководствокурсами",
"учебно-методическоеруководство",
"организационноесопровождениедистанционногообучения(заочногообучения)(вт.ч.техническое)",
"обработкарезультатовтестированияпомодулю,подготовкааналитическойсправки",
"промежуточныйконтрольповариативномумодулю(проверказачетныхработ)",
"руководствостажировкойпопрограммамдополнительногопрофессиональногообразованияворганизацияхспроверкойотчетов",
"проверкаконтрольныхизачетныхработ,эссе(втомчислепридистанционныхформахобучения),проектов(толькопридистанционнойформеобучения),предусмотренныхутп",
"проведениеиндивидуальныхконсультацийдляслушателейкурсоввсоответствиисутп(очноеобучение)",
"проведениеиндивидуальныхзанятийдляслушателей,обучающихсяпоиндивидуальномумаршрутуповышенияквалификации",
"разработкадпппкповышенияквалификации,модулейпрограмм",
"руководствокурсовойработой",
"подготовкакитоговойаттестации,вт.ч.руководствовыпускнойработой,включаянаписаниеотзыва",
"рецензированиевыпускныхработ",
"подготовкавидеоматериаловдляпроведениялекций(вт.ч.лекцийвдистанционномрежиме)"
 };



            List<string> Default = new List<string>()
            {
"всегочасов:",
"итогочасовпоутп:",
"внеаудиторнаяработа:",
"аудиторнаяработа:",
"дистанционноеобучение:"



        };

            List<TypeLoadAndCoefficientModel> ListLoad = new List<TypeLoadAndCoefficientModel>();
            ListLoad.AddRange(Auditor.Select(x => new TypeLoadAndCoefficientModel { NameLoad = x, TypeLoad = 1 }).ToList());
            ListLoad.AddRange(VneAuditor.Select(x => new TypeLoadAndCoefficientModel { NameLoad = x, TypeLoad = 2 }).ToList());
            ListLoad.AddRange(Default.Select(x => new TypeLoadAndCoefficientModel { NameLoad = x, TypeLoad = 0 }).ToList());
            DeterminateTypeLoad determinateTypeLoad = new DeterminateTypeLoad(ListLoad);


            /*  float percentAuditor = 0;
             float percentVneAuditor = 0;
             int tip = 0;
             NameTypeTrainingLoad = NameTypeTrainingLoad.Replace(" ", "");
             NameTypeTrainingLoad = NameTypeTrainingLoad.ToLower();
             foreach (var row in Auditor)
             {

                 int kolsovpad = 0;


                 int result = String.Compare(NameTypeTrainingLoad, row);

                 for (int i = 0; i < (NameTypeTrainingLoad.Length < row.Length ? NameTypeTrainingLoad.Length : row.Length); i++)
                 {
                     if (row[i] == NameTypeTrainingLoad[i])
                     {

                         kolsovpad++;
                     }



                 }


                 if (kolsovpad != 0)
                     if (kolsovpad / NameTypeTrainingLoad.Length * 100 > percentAuditor)
                     {
                         percentAuditor = kolsovpad / NameTypeTrainingLoad.Length * 100;

                     }


             }





             foreach (var row in VneAuditor)
             {
                 NameTypeTrainingLoad = NameTypeTrainingLoad.Replace(" ", "");
                 NameTypeTrainingLoad = NameTypeTrainingLoad.ToLower();
                 int kolsovpad = 0;




                 for (int i = 0; i < (NameTypeTrainingLoad.Length < row.Length ? NameTypeTrainingLoad.Length : row.Length); i++)
                 {
                     if (row[i] == NameTypeTrainingLoad[i])
                     {
                         kolsovpad++;
                     }



                 }


                 if (kolsovpad != 0)
                     if (kolsovpad / NameTypeTrainingLoad.Length * 100 > percentVneAuditor)
                     {
                         percentVneAuditor = kolsovpad / NameTypeTrainingLoad.Length * 100;

                     }


             }


             if (percentAuditor < percentVneAuditor)
             {
                 tip = 2;
             }
             else
             {
                 tip = 1;

             }
            */
            return determinateTypeLoad.SearchType(NameTypeTrainingLoad);
        }



    }


}





