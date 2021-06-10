using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Utility;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {

        public CategoryController(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            { 
                return View(category);

            }

            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }


            return View(category);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() //uses stored procedure
        {
         // return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory, null) });

            return Json(new { data = _unitOfWork.Category.GetAll() }); //get all implemented in irepository
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        
        }
        #endregion

    }
}