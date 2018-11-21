using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMEQ.Models
{

    public class ListCustomerViewModel
    {
        [Display(Name = "ID khách hàng")]
        public long Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Mã số thuế")]
        public string LocationName { get; set; }

        [StringLength(255)]
        [Display(Name = "Email")]
        public string TaxCode { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên người đại diện")]
        public string Name { get; set; }

        [StringLength(255)]
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Quận")]
        public int? DistrictId { get; set; }

        [StringLength(50)]
        [Display(Name = "Số Fax")]
        public string Fax { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }
    }
}