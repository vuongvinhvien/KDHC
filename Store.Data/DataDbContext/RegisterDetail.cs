namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegisterDetail")]
    public partial class RegisterDetail
    {
        public long Id { get; set; }

        public long RegisterFormId { get; set; }

        public long VehicleId { get; set; }

        public int? Quantlity { get; set; }

        public long TestMethodId { get; set; }

        [StringLength(10)]
        public string Increace { get; set; }

        [StringLength(10)]
        public string Decreace { get; set; }

        public bool? StatusVehicle { get; set; }

        public double? Price { get; set; }
    }
}
