namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer2
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string LocationName { get; set; }

        [StringLength(255)]
        public string TaxCode { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Position { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public int? DistrictId { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual District District { get; set; }
    }
}
