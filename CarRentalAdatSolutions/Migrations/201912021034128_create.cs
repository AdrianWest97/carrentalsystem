namespace CarRentalAdatSolutions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.String(),
                        StrLine1 = c.String(),
                        StrLine2 = c.String(),
                        Country = c.String(),
                        ZIP = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        IsDealer = c.Boolean(nullable: false),
                        ProfilePhoto = c.Binary(),
                        About = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.String(nullable: false, maxLength: 128),
                        VehicleId = c.Int(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                        PickUpDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        PickUpLocationAddress = c.String(nullable: false),
                        ReturnLocationAddress = c.String(nullable: false),
                        created = c.DateTime(nullable: false),
                        BillingAddress_Id = c.Int(),
                        Reciept_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Billings", t => t.BillingAddress_Id)
                .ForeignKey("dbo.Reciepts", t => t.Reciept_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.VehicleId)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.Reciept_Id);
            
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        State = c.String(),
                        StrLine1 = c.String(nullable: false),
                        StrLine2 = c.String(),
                        Country = c.String(nullable: false),
                        ZIP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reciepts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChargeAmount = c.Double(nullable: false),
                        TAX = c.Double(nullable: false),
                        TotalCharges = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        make = c.String(nullable: false, maxLength: 20),
                        model = c.String(nullable: false),
                        year = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 200),
                        RegNumber = c.String(nullable: false, maxLength: 10),
                        RentalPrice = c.String(nullable: false, maxLength: 7),
                        FuelType = c.String(),
                        Added = c.DateTime(nullable: false),
                        isBooked = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                        airCondition = c.Boolean(nullable: false),
                        PowerSteering = c.Boolean(nullable: false),
                        CDPlayer = c.Boolean(nullable: false),
                        AirBag = c.Boolean(nullable: false),
                        BreakAssist = c.Boolean(nullable: false),
                        Speed = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Dealer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Dealer_Id)
                .Index(t => t.Dealer_Id);
            
            CreateTable(
                "dbo.VehicleImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.Binary(),
                        Vehicle_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id, cascadeDelete: true)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        Reviews = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Created = c.DateTime(nullable: false),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.WatchLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.String(nullable: false, maxLength: 128),
                        vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.vehicle_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.vehicle_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.WatchLists", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WatchLists", "vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Reviews", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleImages", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "Dealer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "Reciept_Id", "dbo.Reciepts");
            DropForeignKey("dbo.Bookings", "BillingAddress_Id", "dbo.Billings");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.WatchLists", new[] { "vehicle_Id" });
            DropIndex("dbo.WatchLists", new[] { "userId" });
            DropIndex("dbo.Reviews", new[] { "VehicleId" });
            DropIndex("dbo.VehicleImages", new[] { "Vehicle_Id" });
            DropIndex("dbo.Vehicles", new[] { "Dealer_Id" });
            DropIndex("dbo.Bookings", new[] { "Reciept_Id" });
            DropIndex("dbo.Bookings", new[] { "BillingAddress_Id" });
            DropIndex("dbo.Bookings", new[] { "VehicleId" });
            DropIndex("dbo.Bookings", new[] { "userId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.WatchLists");
            DropTable("dbo.Reviews");
            DropTable("dbo.VehicleImages");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Reciepts");
            DropTable("dbo.Billings");
            DropTable("dbo.Bookings");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
