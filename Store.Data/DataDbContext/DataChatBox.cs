namespace Store.Data.DataDbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataChatBox : DbContext
    {
        public DataChatBox()
            : base("name=DataChatBox")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<chatagent2> chatagent2 { get; set; }
        public virtual DbSet<chatclient> chatclients { get; set; }
        public virtual DbSet<ChatLine> ChatLines { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chatagent2>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.addweb)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.idchat)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.urlchat)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.value1)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.value2)
                .IsUnicode(false);

            modelBuilder.Entity<chatagent2>()
                .Property(e => e.value3)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.ip)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.addweb)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.idchat)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.urlchat)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.value1)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.value2)
                .IsUnicode(false);

            modelBuilder.Entity<chatclient>()
                .Property(e => e.value3)
                .IsUnicode(false);

            modelBuilder.Entity<Visitor>()
                .Property(e => e.User_name)
                .IsFixedLength();

            modelBuilder.Entity<Visitor>()
                .Property(e => e.google)
                .IsFixedLength();

            modelBuilder.Entity<Visitor>()
                .Property(e => e.facebook)
                .IsFixedLength();
        }
    }
}
