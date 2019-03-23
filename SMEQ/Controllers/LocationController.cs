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
    public class LocationController : Controller
    {
        private DataChatBox db = new DataChatBox();
        #region ListRegister

        // GET: Getlist
        public ActionResult ListDistrict()
        {
            return View();
        }

        public ActionResult DataDistrict()
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
                    var customerData = (from tempcustomer in _context.Districts
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.Type == searchValue || m.Name == searchValue || m.LatiLongTude == searchValue);
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

        #region ListRegister

        // GET: Getlist
        public ActionResult ListProvince()
        {
            return View();
        }

        public ActionResult DataProvince()
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
                    var customerData = (from tempcustomer in _context.Provinces
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.Type == searchValue || m.Name == searchValue);
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
        
        #endregion
    }
}