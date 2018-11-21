namespace Store.Data.DataDbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chatclient")]
    public partial class chatclient
    {
        public int id { get; set; }

        [StringLength(255)]
        public string ip { get; set; }

        [StringLength(255)]
        public string addweb { get; set; }

        [StringLength(255)]
        public string idchat { get; set; }

        [StringLength(255)]
        public string urlchat { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string sdt { get; set; }

        [StringLength(255)]
        public string value1 { get; set; }

        [StringLength(255)]
        public string value2 { get; set; }

        [StringLength(255)]
        public string value3 { get; set; }

        [StringLength(250)]
        public string value4 { get; set; }
    }
}
