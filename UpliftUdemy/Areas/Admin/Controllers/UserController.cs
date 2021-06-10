using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : AdminController
    {
        
        public UserController(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_unitOfWork.User.GetAll(u=>u.Id != claims.Value));  //retrieves all users other than logged in user !remember GetAll returns an IEnumerable
        }

        public IActionResult Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.User.LockUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _unitOfWork.User.UnlockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}