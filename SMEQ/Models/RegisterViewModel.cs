using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMEQ.Models
{
    // model for registerform
    public class RegisterFormViewModel
    {
        //public long id { get; set; }

        public long? Code { get; set; }

        //public long CustomerId { get; set; }

        [StringLength(255)]
        public string RegisterName { get; set; }

        [StringLength(10)]
        public string Testing { get; set; }

        [StringLength(10)]
        public string Calibration { get; set; }

        public DateTime? ReceivedDate { get; set; }

        [StringLength(10)]
        public string ReceivedBy { get; set; }

        public DateTime? EffectTime { get; set; }

        public double? TotalPrice { get; set; }

        [StringLength(128)]
        public string LocationBy { get; set; }

        [StringLength(10)]
        public string Languge { get; set; }

        public DateTime? ExpectedDate { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public long ProcessingStatusId { get; set; }


        ////////
       // public long RegisterId { get; set; }

        public long VehicleId { get; set; }

        public int? Quantlity { get; set; }

        //public long TestMethodId { get; set; }

        [StringLength(10)]
        public string Increace { get; set; }

        [StringLength(10)]
        public string Decreace { get; set; }

        public bool? StatusVehicle { get; set; }

        public double? Price { get; set; }
    }
}