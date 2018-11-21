using SMEQ.Models;
using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMEQ.Controllers
{
    public class RegisterFormController : Controller
    {
        // Create DB
        private DataChatBox db = new DataChatBox();
        // GET: RegisterForm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterFormAll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterFormAll(RegisterFormViewModel viewModel)
        {
            var registerForm = new RegisterForm()
            {
                Code=viewModel.Code,
                //CustomerId = viewModel.CustomerId,
                RegisterName = viewModel.RegisterName,
                Testing = viewModel.Testing,
                Calibration = viewModel.Calibration,
                ReceivedDate = viewModel.ReceivedDate,
                ReceivedBy = viewModel.ReceivedBy,
                EffectTime = viewModel.EffectTime,
                TotalPrice = viewModel.TotalPrice,
                LocationBy = viewModel.LocationBy,
                Languge = viewModel.Languge,
                ExpectedDate = viewModel.ExpectedDate,
                CreatedDate = viewModel.CreatedDate,
                CreatedBy = viewModel.CreatedBy,
                ProcessingStatusId = viewModel.ProcessingStatusId,
                


            };
            var registerDetail = new RegisterDetail()
            {
                VehicleId = viewModel.VehicleId,
                Quantlity = viewModel.Quantlity,
                //TestMethodId = viewModel.TestMethodId,
                Increace = viewModel.Increace,
                Decreace = viewModel.Decreace,
                StatusVehicle = viewModel.StatusVehicle,
                Price = viewModel.Price,

            };
            db.RegisterForms.Add(registerForm);
            db.RegisterDetails.Add(registerDetail);
            db.SaveChanges();

            return View();
        }
    }
}