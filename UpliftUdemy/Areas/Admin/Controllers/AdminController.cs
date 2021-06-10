using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {

        protected readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}