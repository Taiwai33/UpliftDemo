using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    public class FrequencyController : AdminController
    {
        public FrequencyController(IUnitOfWork unitOfWork)
             : base(unitOfWork)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id) //? denotes optional parameter
        {
            Frequency frequency = new Frequency();
            if (id == null) //validation
            {
                return View(frequency);            
            }
            frequency = _unitOfWork.Frequency.Get(id.GetValueOrDefault());  //need to use getvalueordefault because int is an optional parameter
            if (frequency == null)  //validation
            {
                return NotFound();
            }
            
            return View(frequency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Frequency frequency) //? denotes optional parameter
        {
            if (ModelState.IsValid) //validate model (aka. frequency, current model) is valid
            {
                if (frequency.Id == 0)   //if id does not exist insert, else update
                {
                    _unitOfWork.Frequency.Add(frequency);
                }
                else {
                    _unitOfWork.Frequency.Update(frequency);
                }
                _unitOfWork.Save();
              return RedirectToAction(nameof(Index));
            }


            return View(frequency);
        }



        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Frequency.GetAll() }); //get all implemented in irepository
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Frequency.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Frequency.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}