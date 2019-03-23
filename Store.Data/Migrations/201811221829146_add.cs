namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        ID_Customer = c.String(maxLength: 128),
                        Avartar = c.String(maxLength: 256),
                        IsMain = c.Boolean(),
                        status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.chatagent2",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ip = c.String(maxLength: 255, unicode: false),
                        addweb = c.String(maxLength: 255, unicode: false),
                        idchat = c.String(maxLength: 255, unicode: false),
                        urlchat = c.String(maxLength: 255, unicode: false),
                        name = c.String(maxLength: 255),
                        email = c.String(maxLength: 255),
                        sdt = c.String(maxLength: 255, unicode: false),
                        value1 = c.String(maxLength: 255, unicode: false),
                        value2 = c.String(maxLength: 255, unicode: false),
                        value3 = c.String(maxLength: 255, unicode: false),
                        value4 = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.chatclient",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ip = c.String(maxLength: 255, unicode: false),
                        addweb = c.String(maxLength: 255, unicode: false),
                        idchat = c.String(maxLength: 255, unicode: false),
                        urlchat = c.String(maxLength: 255, unicode: false),
                        name = c.String(maxLength: 255),
                        email = c.String(maxLength: 255, unicode: false),
                        sdt = c.String(maxLength: 255, unicode: false),
                        value1 = c.String(maxLength: 255, unicode: false),
                        value2 = c.String(maxLength: 255, unicode: false),
                        value3 = c.String(maxLength: 255, unicode: false),
                        value4 = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ChatLine",
                c => new
                    {
                        ID_ContentChat = c.Int(nullable: false, identity: true),
                        Visitor = c.String(maxLength: 128),
                        Agent = c.String(maxLength: 128),
                        feeling = c.Boolean(),
                        Comment = c.String(),
                        Address = c.String(),
                        ID_Customer = c.String(maxLength: 128),
                        Line = c.String(),
                        Date = c.DateTime(),
                        Actor = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID_ContentChat);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(nullable: false, maxLength: 100),
                        CommonName = c.String(maxLength: 100),
                        FormalName = c.String(maxLength: 100),
                        CountryType = c.String(maxLength: 100),
                        CountrySubType = c.String(maxLength: 100),
                        Sovereignty = c.String(maxLength: 100),
                        Capital = c.String(maxLength: 100),
                        CurrencyCode = c.String(maxLength: 100),
                        CurrencyName = c.String(maxLength: 100),
                        TelephoneCode = c.String(maxLength: 100),
                        CountryCode3 = c.String(maxLength: 100),
                        CountryNumber = c.String(maxLength: 100),
                        InternetCountryCode = c.String(maxLength: 100),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        Flags = c.String(maxLength: 50),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Type = c.String(maxLength: 20),
                        TelephoneCode = c.Int(),
                        ZipCode = c.String(maxLength: 20),
                        CountryId = c.Int(nullable: false),
                        CountryCode = c.String(maxLength: 2),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        ProvinceId = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Customer2",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LocationName = c.String(maxLength: 255),
                        TaxCode = c.String(maxLength: 255, unicode: false),
                        Name = c.String(maxLength: 255),
                        Position = c.String(maxLength: 255),
                        PhoneNumber = c.String(maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 255),
                        DistrictId = c.Int(),
                        Fax = c.String(maxLength: 50, unicode: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Ward",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(maxLength: 50),
                        LatiLongTude = c.String(maxLength: 50),
                        DistrictId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        IsPublished = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID_Customer = c.String(nullable: false, maxLength: 128),
                        DateCreate = c.DateTime(),
                        MaSoThue = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_Customer);
            
            CreateTable(
                "dbo.ProcessingStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RegisterForm",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        Code = c.Long(),
                        CustomerId = c.Long(),
                        RegisterName = c.String(maxLength: 255),
                        Testing = c.String(maxLength: 10),
                        Calibration = c.String(maxLength: 10),
                        ReceivedDate = c.DateTime(),
                        ReceivedBy = c.String(maxLength: 10),
                        EffectTime = c.DateTime(),
                        TotalPrice = c.Double(),
                        LocationBy = c.String(maxLength: 128),
                        Languge = c.String(maxLength: 10),
                        ExpectedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        ProcessingStatusId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ProcessingStatus", t => t.ProcessingStatusId)
                .Index(t => t.ProcessingStatusId);
            
            CreateTable(
                "dbo.RegisterDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegisterFormId = c.Long(nullable: false),
                        VehicleId = c.Long(nullable: false),
                        Quantlity = c.Int(),
                        TestMethodId = c.Long(nullable: false),
                        Increace = c.String(maxLength: 10),
                        Decreace = c.String(maxLength: 10),
                        StatusVehicle = c.Boolean(),
                        Price = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        ID_Setting = c.String(nullable: false, maxLength: 128),
                        ID_Customer = c.String(maxLength: 128),
                        EmailSendScript = c.String(maxLength: 256),
                        Sound = c.Boolean(),
                        ShareFileAgent = c.Boolean(),
                        ShareFileVisitor = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID_Setting);
            
            CreateTable(
                "dbo.TestMethod",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Scope = c.String(maxLength: 50),
                        Accuracy = c.String(maxLength: 50),
                        UnitPrice = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visitor",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        User_name = c.String(maxLength: 50, fixedLength: true),
                        google = c.String(maxLength: 10, fixedLength: true),
                        facebook = c.String(maxLength: 10, fixedLength: true),
                        email = c.String(maxLength: 256),
                        Phone = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisterForm", "ProcessingStatusId", "dbo.ProcessingStatus");
            DropForeignKey("dbo.District", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Ward", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Customer2", "DistrictId", "dbo.District");
            DropForeignKey("dbo.Province", "CountryId", "dbo.Country");
            DropIndex("dbo.RegisterForm", new[] { "ProcessingStatusId" });
            DropIndex("dbo.Ward", new[] { "DistrictId" });
            DropIndex("dbo.Customer2", new[] { "DistrictId" });
            DropIndex("dbo.District", new[] { "ProvinceId" });
            DropIndex("dbo.Province", new[] { "CountryId" });
            DropTable("dbo.Visitor");
            DropTable("dbo.Vehicle");
            DropTable("dbo.TestMethod");
            DropTable("dbo.Setting");
            DropTable("dbo.RegisterDetail");
            DropTable("dbo.RegisterForm");
            DropTable("dbo.ProcessingStatus");
            DropTable("dbo.Customer");
            DropTable("dbo.Ward");
            DropTable("dbo.Customer2");
            DropTable("dbo.District");
            DropTable("dbo.Province");
            DropTable("dbo.Country");
            DropTable("dbo.ChatLine");
            DropTable("dbo.chatclient");
            DropTable("dbo.chatagent2");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
        }
    }
}
