namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visitor")]
    public partial class Visitor
    {
        public string ID { get; set; }

        [StringLength(50)]
        public string User_name { get; set; }

        [StringLength(10)]
        public string google { get; set; }

        [StringLength(10)]
        public string facebook { get; set; }

        [StringLength(256)]
        public string email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }
    }
}
