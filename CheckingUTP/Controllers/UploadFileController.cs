using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CheckingUTP.Models;

using CheckingUTP.Models.ModelsUTP;
using CheckingUTP.Models.DataBase;

namespace CheckingUTP.Controllers
{
    public class UploadFileController : Controller
    {
        //ApplicationContext _context;

        //public UploadFileController(ApplicationContext context)
        //{
        //    _context = context;

        //}

        Context db;
        public UploadFileController(Context context)
        {
            db = context;
        }


        public IActionResult UploadFile()
        {
            List<ListUTP> UTP = db.UTPs.Select(x => new ListUTP { Name = x.Name, Hour = x.Hour, Utp_id = x.Utp_id }).ToList();

            return View("UploadFile", UTP);
        }
        public async Task<IActionResult> AddFile(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {
                // путь к папке Files
                string path = "/wwwroot/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(Directory.GetCurrentDirectory() + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                //   _context.Files.Add(file);
            }
            // _context.SaveChanges();

            return RedirectToAction("CheckExcel", "CheckUTPExcel");
        }
    }
}