using AutoMapper;
using SMEQ.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Store.Data.DataDbContext;
using Store.Data.Repositories;
using Store.Sevices.Services;
using Store.Web.Infrastructure.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static Store.Web.Infrastructure.ExtensionMethod.CustomAuthorize;

namespace SMEQ.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager roleManager;
        private readonly IAccountResponsitory _Account;
        private readonly IAccountSevices _AccountServices;
        private readonly ICustomerSevices _Customer;
        private readonly IMapper _Mapper;
        private readonly ISettingSevice _Settingservices;
        private readonly IChatLineSevices _ChatLineServices;
        private readonly IVisitorSevices _VisitorSevices;
        public HomeController(IAccountResponsitory Account, IMapper Mapper, ICustomerSevices Customer, IAccountSevices AccountServices, ISettingSevice Settingservices, IVisitorSevices VisitorSevices, IChatLineSevices ChatLineServices)
        {
            _Account = Account;
            _Mapper = Mapper;
            _Customer = Customer;
            _Settingservices = Settingservices;
            _AccountServices = AccountServices;
            _VisitorSevices = VisitorSevices;
            _ChatLineServices = ChatLineServices;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { roleManager = value; }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
      
             
            var user = User.Identity.GetUserId();
            ViewBag.IDRoom = _Customer.GetIDCustomerByUser(user);
            ViewBag.UserName = User.Identity.GetUserName();
            ViewBag.AgentID = user;
            ViewBag.email = _Account.GetById(user).Email;


            return View(_Settingservices.GetSettingByDashBoard(ViewBag.IDRoom));




        }
        public string GetWebClientIp()
        {
            string userIP = "Without access to user IP";

            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN accelerated after the IP 
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;
        }
        [AllowAnonymous]
        [AllowIframeFromUri]
        public ActionResult chatbox(string id)
        {


            ViewBag.ID = id;
            ViewBag.IP = GetWebClientIp();
            //  var    Setting = _Settingservices.GetSettingByDashBoard(id);
            try
            {
                ViewBag.Urlweb = Request.UrlReferrer.Authority.ToString();
            }
            catch { ViewBag.Urlweb = ""; }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Admin()
        {
            ViewBag.IDRoom = Session["DashBoard"];
            ViewBag.UserName = Session["UserName"];
            return View();
        }

        public ActionResult History()
        {
            ViewBag.IDRoom = Session["DashBoard"];
            ViewBag.UserName = Session["UserName"];
            var user = User.Identity.GetUserId();
            var IDDashBoard = _Customer.GetIDCustomerByUser(user);
            var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDDashBoard));
            Model.History = _ChatLineServices.GetChatLineByUser(user).GroupBy(f => f.Visitor).Select(d => new ContentChatHepperHistory
            {
                Visitor = d.Key,
                Agent = d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).Count() > 0 ? d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).OrderByDescending(x => x.Date).FirstOrDefault().Agent : null,
                sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Line,

                Date = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Date
            }).ToList();

            //Model.History = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ContentChatHepperHistory>>);
            Model.ListAgent = _Account.GetAll().Where(x => x.ID_Customer == IDDashBoard);
            foreach (var item in Model.History)
            {
                if (!string.IsNullOrEmpty(item.Agent))
                {
                    item.AgentName = _Account.GetById(item.Agent).UserName;
                }
                item.VisitorName = string.IsNullOrEmpty(_VisitorSevices.GetByID(item.Visitor).User_name) ? string.Empty : _VisitorSevices.GetByID(item.Visitor).User_name;
            }
            ViewBag.TypeAccount = _AccountServices.isMain(user);
            return View(Model);
        }
        [HttpAjaxRequest]
        public ActionResult Agent()
        {
            var user = User.Identity.GetUserId();
            var IDcustomer = _Customer.GetIDCustomerByUser(user);
            var Resutl = _Account.GetAll().Where(x => x.ID_Customer == IDcustomer && x.status == true);
            ViewBag.TypeAccount = _AccountServices.isMain(user);
            ViewBag.Time = _ChatLineServices.getlasttimeonline(user).First().ToString();
            ViewBag.CoutConver = _ChatLineServices.GetChatLineByUser(user).GroupBy(x => new { x.Visitor, x.Agent }).Where(x => x.Key.Agent == user).Count();
            return View(Resutl);
        }
        [HttpAjaxRequest]
        public ActionResult Report()
        {
            ViewBag.IDRoom = Session["DashBoard"];
            ViewBag.UserName = Session["UserName"];
            return View();
        }
        [HttpAjaxRequest]
        public ActionResult Setting()
        {

            var user = User.Identity.GetUserId();
            var IDDashBoard = _Customer.GetIDCustomerByUser(user);
            var model = _Settingservices.GetSettingByDashBoard(IDDashBoard);
            ViewBag.IDRoom = IDDashBoard;
            return View(model);
        }
        [HttpPost]
        [HttpAjaxRequest]
        public JsonResult RegisterAgent(string strAgent)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            RegisterViewModel model = serializer.Deserialize<RegisterViewModel>(strAgent);
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            user.ID_Customer = _Customer.GetIDCustomerByUser(User.Identity.GetUserId());
            user.IsMain = false;
            user.status = true;
            var result = UserManager.Create(user, model.Password);
            bool status = false;
            string message = "Lỗi";
            if (result.Succeeded)
            {
                var idagent = UserManager.FindByEmail(model.Email).Id;
                status = true;
                string roleName = "User";
                // Check to see if Role Exists, if not create it
                if (!RoleManager.RoleExists(roleName))
                {
                    RoleManager.Create(new IdentityRole(roleName));
                }
                UserManager.AddToRole(user.Id, roleName);
                message = "Thành công";
            }

            return Json(new
            {
                status = status,
                message = message
            });
        }
        [HttpAjaxRequest]
        [HttpPost]
        public JsonResult UpdateAgent(string strAgentedit)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            EditRegisterViewModel model = serializer.Deserialize<EditRegisterViewModel>(strAgentedit);
            var UserResult = UserManager.FindByEmail(model.Email);
            UserResult.Avartar = model.Avartar;
            UserResult.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
            var result = UserManager.Update(UserResult);
            bool status = false;
            string message = "Lỗi";
            if (result.Succeeded)
            {
                status = true;
                message = "Thành công";
            }

            return Json(new
            {
                status = status,
                message = message
            });
        }

        [HttpGet]
        public JsonResult GetDetailAgent(string idAgent)
        {

            var model = _Mapper.Map<AspNetUser, DetailAgentViewModel>(_Account.GetById(idAgent));
            model.sumconver = _ChatLineServices.GetChatLineByUser(idAgent).GroupBy(x => new { x.Visitor, x.Agent }).Where(x => x.Key.Agent == idAgent).Count();
            model.lastonline = _ChatLineServices.getlasttimeonline(idAgent).FirstOrDefault();
            {
                var typeaccout = "Agent";
                if (model.IsMain == true)
                {
                    typeaccout = "Administrator";

                }
                return Json(new
                {
                    data = model,
                    typeaccout = typeaccout,
                    status = true
                }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public JsonResult DeleteAgent(string idAgent)
        {
            if (UserManager.FindById(idAgent).IsMain == true)
            {
                return Json(new
                {
                    status = false,
                    message = "Không thể xóa Admin"
                });
            }
            else
            {
                var Current = UserManager.FindById(idAgent) ;
                Current.status = false;
                UserManager.Update(Current);
                return Json(new
                {
                    status = true,

                });
            }

        }
        [HttpAjaxRequest]
        [HttpGet]
        public JsonResult LoadAgent()
        {
            var user = User.Identity.GetUserId();
            var IDcustomer = _Customer.GetIDCustomerByUser(user);
            var Resutl = _Account.GetAll().Where(x => x.ID_Customer == IDcustomer && x.status == true);
            return Json(new
            {
                data = Resutl,
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpAjaxRequest]
        public JsonResult UploadFile(HttpPostedFileWrapper ImageFile)
        {


            var file = ImageFile;

            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/UploadedImageAvartar/" + file.FileName));
                return Json(new { LinkFile = file.FileName, stt = true }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { LinkFile = file.FileName, stt = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpAjaxRequest]
        [AllowAnonymous]
        public JsonResult UploadFileVisitor(HttpPostedFileWrapper ImageFile)
        {


            var file = ImageFile;

            if (file != null)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extention = Path.GetExtension(file.FileName);
                var filenamewithoutextension = Path.GetFileNameWithoutExtension(file.FileName);
                file.SaveAs(Server.MapPath("/UploadedImageAvartar2/" + file.FileName));


                return Json(new { LinkFile = "<a target='_blank' href = '/UploadedImageAvartar2/" + file.FileName + "'>" + file.FileName + " </a>", stt = true }, JsonRequestBehavior.AllowGet);


            }
            return Json(new { stt = false }, JsonRequestBehavior.AllowGet);
        }
        private bool RemoveFileFromServer(string path)
        {
            var fullPath = Request.MapPath(path);
            if (!System.IO.File.Exists(fullPath)) return false;

            try //Maybe error could happen like Access denied or Presses Already User used
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }

        [HttpGet]
        public JsonResult CustomDateHistory(DateTime? startDate, DateTime? endDate)
        {
            if (endDate != null)
            {
                if (startDate > endDate || startDate == null)
                    return Json(new
                    {
                        status = false
                    }, JsonRequestBehavior.AllowGet);
                var user = User.Identity.GetUserId();
                var IDCustomer = _Customer.GetIDCustomerByUser(user);
                var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDCustomer));
                Model.History = _ChatLineServices.GetChatLineByUser(user).Where(x => x.Date <= endDate && x.Date >= startDate).GroupBy(abc => abc.Visitor).Select(d => new ContentChatHepperHistory
                {
                    Visitor = d.Key,
                    Agent = d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).Count() > 0 ? d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).OrderByDescending(x => x.Date).FirstOrDefault().Agent : null,
                    sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Line,

                    Date = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Date
                }).ToList();

                //Model.History = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ContentChatHepperHistory>>);

                foreach (var item in Model.History)
                {
                    if (!string.IsNullOrEmpty(item.Agent))
                    {
                        item.AgentName = _Account.GetById(item.Agent).UserName;
                    }
                    item.VisitorName = string.IsNullOrEmpty(_VisitorSevices.GetByID(item.Visitor).User_name) ? string.Empty : _VisitorSevices.GetByID(item.Visitor).User_name;
                }

                return Json(new
                {
                    data = Model,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var user = User.Identity.GetUserId();
                var IDCustomer = _Customer.GetIDCustomerByUser(user);
                var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDCustomer));
                Model.History = _ChatLineServices.GetChatLineByUser(user).Where(x => x.Date >= startDate).GroupBy(abc => abc.Visitor).Select(d => new ContentChatHepperHistory
                {
                    Visitor = d.Key,
                    Agent = d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).Count() > 0 ? d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).OrderByDescending(x => x.Date).FirstOrDefault().Agent : null,
                    sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Line,

                    Date = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Date
                }).ToList();

                //Model.History = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ContentChatHepperHistory>>);

                foreach (var item in Model.History)
                {
                    if (!string.IsNullOrEmpty(item.Agent))
                    {
                        item.AgentName = _Account.GetById(item.Agent).UserName;
                    }
                    item.VisitorName = string.IsNullOrEmpty(_VisitorSevices.GetByID(item.Visitor).User_name) ? string.Empty : _VisitorSevices.GetByID(item.Visitor).User_name;
                }

                return Json(new
                {
                    data = Model,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpAjaxRequest]
        [HttpGet]
        public JsonResult FitterByAgent(string IdAgent)
        {

            var user = User.Identity.GetUserId();
            var IDCustomer = _Customer.GetIDCustomerByUser(user);
            var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDCustomer));
            Model.History = _ChatLineServices.GetChatLineByUser(user).Where(x => x.Agent == IdAgent).GroupBy(abc => abc.Visitor).Select(d => new ContentChatHepperHistory
            {
                Visitor = d.Key,
                Agent = d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).Count() > 0 ? d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).OrderByDescending(x => x.Date).FirstOrDefault().Agent : null,
                sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Line,

                Date = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Date
            }).ToList();

            //Model.History = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ContentChatHepperHistory>>);

            foreach (var item in Model.History)
            {
                if (!string.IsNullOrEmpty(item.Agent))
                {
                    item.AgentName = _Account.GetById(item.Agent).UserName;
                }
                item.VisitorName = string.IsNullOrEmpty(_VisitorSevices.GetByID(item.Visitor).User_name) ? string.Empty : _VisitorSevices.GetByID(item.Visitor).User_name;
            }
            return Json(new
            {
                data = Model,
                status = true
            }, JsonRequestBehavior.AllowGet);

        }
        //[HttpAjaxRequest]
        //[HttpGet]
        //public JsonResult FitterByFeeling(bool Like)
        //{
        //    var user = User.Identity.GetUserId();
        //    var IDCustomer = _Customer.GetIDCustomerByUser(user);
        //    var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDCustomer));
        //    Model.History = _ChatLineServices.GetChatLineByUser(user).Where(x => x.Agent == IdAgent).GroupBy(abc => new { abc.Visitor, abc.Agent }).Select(d => new ContentChatHepperHistory
        //    {
        //        Visitor = d.Key.Visitor,
        //        Agent = d.Select(gg => gg.Agent).FirstOrDefault(),
        //        sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderByDescending(s => s.Date).FirstOrDefault().Line,
        //        AgentName = _Account.GetById(d.Key.Agent).UserName
        //    });
        //    foreach (var item in Model.History)
        //    {
        //        if (item.Agent != null)
        //            item.AgentName = _Account.GetById(item.Agent).UserName;
        //        var Name_visitor = _VisitorSevices.GetByID(item.Visitor).User_name;
        //        item.Visitor = Name_visitor != null ? item.Visitor = Name_visitor : item.Visitor;
        //    }

        //    return Json(new
        //    {
        //        data = Model,
        //        status = true
        //    }, JsonRequestBehavior.AllowGet);            
        //}
        [HttpAjaxRequest]
        [HttpGet]
        public JsonResult LoadConvert(string IDvisitor)
        {

            var user = User.Identity.GetUserId();
            var model = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ChatLineViewModel>>(_ChatLineServices.GetByVisitorAgent(IDvisitor, user));
            foreach (var item in model)
            {

                item.NameAgent = (item.Actor == false ?
                 (item.Agent == null ? "" : _Account.GetById(item.Agent).UserName) :
                    (_VisitorSevices.GetByID(item.Visitor).User_name == null ? _VisitorSevices.GetByID(item.Visitor).ID : _VisitorSevices.GetByID(item.Visitor).User_name));


            }

            return Json(new
            {
                data = model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FitterMutilt(string mess = "", string visitor = "", string email = "", string phone = "")
        {


            var user = User.Identity.GetUserId();
            var IDCustomer = _Customer.GetIDCustomerByUser(user);
            var Model = _Mapper.Map<Customer, HistoryViewModel>(_Customer.GetDashBoardByID(IDCustomer));
            Model.History = _ChatLineServices.FitterMutilt(IDCustomer, mess, visitor, email, phone).GroupBy(abc =>  abc.Visitor).Select(d => new ContentChatHepperHistory
            {
                Visitor = d.Key,
                Agent = d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).Count() > 0 ? d.Select(gg => new { gg.Agent, gg.Date }).Where(x => !string.IsNullOrEmpty(x.Agent)).OrderByDescending(x => x.Date).FirstOrDefault().Agent : null,
                sumary = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Line,

                Date = d.Select(gg => new { gg.Line, gg.Date }).OrderBy(s => s.Date).FirstOrDefault().Date
            }).ToList();

            //Model.History = _Mapper.Map<IEnumerable<ChatLine>, IEnumerable<ContentChatHepperHistory>>);

            foreach (var item in Model.History)
            {
                if (!string.IsNullOrEmpty(item.Agent))
                {
                    item.AgentName = _Account.GetById(item.Agent).UserName;
                }
                item.VisitorName = string.IsNullOrEmpty(_VisitorSevices.GetByID(item.Visitor).User_name) ? string.Empty : _VisitorSevices.GetByID(item.Visitor).User_name;
            }


            return Json(new
            {
                data = Model,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [HttpAjaxRequest]
        public JsonResult SettingUpdate(string emailsend, bool sound, bool sharefileAgent, bool sharefileVisitor)
        {
            var user = User.Identity.GetUserId();
            var IDCustomer = _Customer.GetIDCustomerByUser(user);
            try
            {
                var setting = _Settingservices.EditSetting(IDCustomer, emailsend, sound, sharefileAgent, sharefileVisitor);
                return Json(new
                {
                    date = setting,
                    status = true

                });
            }
            catch
            {
                return Json(new
                {
                    status = false

                });
            }

        }
    }

}