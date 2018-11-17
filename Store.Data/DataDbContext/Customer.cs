namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [Key]
        public string ID_Customer { get; set; }

        public DateTime? DateCreate { get; set; }

        [StringLength(50)]
        public string MaSoThue { get; set; }
    }
}
