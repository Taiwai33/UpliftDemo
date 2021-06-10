﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace UpliftUdemy.Areas.Admin.Controllers
{
    public class OrderController : AdminController
    {

        public OrderController(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
           
                OrderVM orderVM = new OrderVM()
                {
                    OrderHeader = _unitOfWork.OrderHeader.Get(id),
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id)
                };
            
            return View(orderVM);
        }

        public IActionResult Approve(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusApproved);
            return View(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusRejected);
            return View(nameof(Index));
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll()});
            
        }

        [HttpGet]
        public IActionResult GetAllPendingOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter:o=>o.Status == SD.StatusSubmitted) });
        }

        [HttpGet]
        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }
        #endregion
    }
}