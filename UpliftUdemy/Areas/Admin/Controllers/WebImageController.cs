using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using UpliftUdemy.DataAccess.Data;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    public class WebImageController : AdminController
    {
        private readonly ApplicationDbContext _db;
        public WebImageController(IUnitOfWork unitOfWork, ApplicationDbContext db)  //normally unit of work can be used, this is showing how to use appdb ONLY
           : base(unitOfWork)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            WebImages imgFromObj = new WebImages();
            if (id == null)
            {
            }
            else
            {
                imgFromObj = _db.WebImages.FirstOrDefault(m => m.Id == id);
                if (imgFromObj == null)
                {
                    return NotFound();

                }
            }
            return View(imgFromObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id,WebImages imgObj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using(var fileStream1 = files[0].OpenReadStream())
                    {
                        using (var memoryStream1 = new MemoryStream())
                        {
                            fileStream1.CopyTo(memoryStream1);
                            p1 = memoryStream1.ToArray();
                        }
                    }
                    imgObj.Picture = p1;
                }
                if (imgObj.Id == 0) //check either imgObj or id from parameters
                {
                    _db.WebImages.Add(imgObj);
                }
                else
                {
                    var imageFromDb = _db.WebImages.Where(c => c.Id == id).FirstOrDefault();

                    imageFromDb.Name = imgObj.Name;
                    if (files.Count > 0)
                    {
                        imageFromDb.Picture = imgObj.Picture;                 
                    
                    }
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return View(imgObj);
        }

        #region

        [HttpGet]
        public IActionResult GetAll() //uses stored procedure
        {
            // return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory, null) });

            return Json(new { data = _db.WebImages.ToList() }); //get all implemented in irepository
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _db.WebImages.Find(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.WebImages.Remove(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}