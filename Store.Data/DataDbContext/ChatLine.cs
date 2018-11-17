namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatLine")]
    public partial class ChatLine
    {
        [Key]
        public int ID_ContentChat { get; set; }

        [StringLength(128)]
        public string Visitor { get; set; }

        [StringLength(128)]
        public string Agent { get; set; }

        public bool? feeling { get; set; }

        public string Comment { get; set; }

        public string Address { get; set; }

        [StringLength(128)]
        public string ID_Customer { get; set; }

        public string Line { get; set; }

        public DateTime? Date { get; set; }

        public bool? Actor { get; set; }
    }
}
