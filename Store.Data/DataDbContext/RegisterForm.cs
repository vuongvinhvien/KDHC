namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegisterForm")]
    public partial class RegisterForm
    {
        public long id { get; set; }

        public long? Code { get; set; }

        public long? CustomerId { get; set; }

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

        public DateTime? CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public long ProcessingStatusId { get; set; }

        public virtual ProcessingStatu ProcessingStatu { get; set; }
    }
}
