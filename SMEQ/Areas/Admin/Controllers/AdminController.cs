using Store.Sevices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Store.Data.DataDbContext;
using SMEQ.Areas.Admin.Models;
using Store.Web.Infrastructure.ExtensionMethod;

namespace SMEQ.Areas.Admin.Controllers
{
 
    [CustomAuthorize(Roles = "Superusers")]
    public class AdminController : Controller
    {
        private readonly IAccountSevices _AccountServices;
        private readonly IMapper _Maper;
        public AdminController( IAccountSevices AccountServices, IMapper Maper)
        {
      
            _AccountServices = AccountServices;
            _Maper = Maper;
        }
        

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccountList()
        {
            var model = _Maper.Map<IEnumerable<AspNetUser>,IEnumerable<ListAccoutViewModel>>(_AccountServices.GetAllMainAccount());

            return View(model);
        }
        [HttpGet]
        public ActionResult DetailAccount(string id = "error")
        {

            if (id.Equals("error")) return RedirectToAction("AccountList");
            return View(_AccountServices.GetByID(id));
        }
        [HttpGet]
   
        public ActionResult AccountEdit(string id = "error")
        {

           
            if (id.Equals("error")) return RedirectToAction("AccountList");
            return View(_AccountServices.GetByID(id));
        }

        [HttpPost, ActionName("AccountEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult AccountEdit(AspNetUser model)
        {
            _AccountServices.UpdateAccount(model);
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public ActionResult DeleteAccount(string id = "error")
        {

            if (id.Equals("error")) return RedirectToAction("AccountList");
            return View(_AccountServices.GetByID(id));
        }

        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountConfirm(string id ="error")
        {
            if (id.Equals("error")) return RedirectToAction("AccountList");
            _AccountServices.DeleteAccount(id);
            return RedirectToAction("AccountList");
        }

    }
}