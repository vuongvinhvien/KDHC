namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle")]
    public partial class Vehicle
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Scope { get; set; }

        [StringLength(50)]
        public string Accuracy { get; set; }

        public double? UnitPrice { get; set; }
    }
}
