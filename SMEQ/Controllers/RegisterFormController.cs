using SMEQ.Models;
using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace SMEQ.Controllers
{
    public class RegisterFormController : Controller
    {
        // Create DB
        private DataChatBox db = new DataChatBox();
        // GET: RegisterForm
        public ActionResult RegisterForm()
        {
            return View();
        }
        public ActionResult RegisterByCustomer()
        {
            return View();
        }

        #region FormRegister
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
        #endregion

        #region ListRegister

        // GET: Getlist
        public ActionResult ListByCustomer()
        {
            return View();
        }

        public ActionResult ListByCustomerData()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DataChatBox _context = new DataChatBox())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data  

                    var customerData = (
                                            from tempcustomer in _context.RegisterForms
                                            select tempcustomer
                                        );
                    //var d = customerData.Where()
                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.RegisterName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        #endregion

        #region ListRegister

        // GET: Getlist
        public ActionResult ListRegister()
        {
            return View();
        }
        
        public ActionResult RegisterData()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DataChatBox _context = new DataChatBox())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data  
                    
                    var customerData = (    
                                            from tempcustomer in _context.RegisterForms
                                            select tempcustomer
                                        );
                    //var d = customerData.Where()
                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.RegisterName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        //
        [HttpPost]
        public JsonResult DeleteRecord(int? ID)
        {
            using (DataChatBox _context = new DataChatBox())
            {
                var customer = _context.RegisterForms.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _context.RegisterForms.Remove(customer);
                _context.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }
        
        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name");
            return View();
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationName,TaxCode,Name,Position,PhoneNumber,Address,DistrictId,Fax,CreatedDate")] Customer2 customer2)
        {
            if (ModelState.IsValid)
            {
                db.Customer2.Add(customer2);
                db.SaveChanges();
                return RedirectToAction("ShowGrid");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", customer2.DistrictId);
            return View(customer2);
        }

        #endregion

        #region ListRegisterProcessing

        // GET: Getlist
        public ActionResult ListRegisterProcessing()
        {
            return View();
        }
        //
        public ActionResult DataRegisterProcessing()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DataChatBox _context = new DataChatBox())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all
                    var customerData = (from tempcustomer in _context.RegisterForms
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.RegisterName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        //
        [HttpPost]
        public JsonResult Edit(int? ID)
        {
            using (DataChatBox _context = new DataChatBox())
            {
                var customer = _context.RegisterForms.Find(ID);
                if (ID == null)
                    return Json(data: "Not Deleted", behavior: JsonRequestBehavior.AllowGet);
                _context.RegisterForms.Remove(customer);
                _context.SaveChanges();

                return Json(data: "Deleted", behavior: JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region ListRegisterWaitingReturn

        // GET: Getlist
        public ActionResult ListRegisterWait()
        {
            return View();
        }
        //
        public ActionResult DataRegisterWait()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DataChatBox _context = new DataChatBox())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all
                    var customerData = (from tempcustomer in _context.RegisterForms
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.RegisterName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        #endregion

        #region ListRegisterCompleted

        // GET: Getlist
        public ActionResult ListRegisterCompleted()
        {
            return View();
        }
        //
        public ActionResult DataRegisterCompleted()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (DataChatBox _context = new DataChatBox())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all
                    var customerData = (from tempcustomer in _context.RegisterForms
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.RegisterName == searchValue);
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        #endregion
    }
}
