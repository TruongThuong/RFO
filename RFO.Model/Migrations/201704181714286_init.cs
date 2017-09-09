namespace RFO.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommonInfo",
                c => new
                    {
                        CommonInfoId = c.Int(nullable: false, identity: true),
                        CommonInfoCode = c.Int(nullable: false),
                        Name = c.String(),
                        BriefDescription = c.String(),
                        Description = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.CommonInfoId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BriefDescription = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MenuId = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        Price = c.Long(nullable: false),
                        BriefDescription = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPopular = c.Boolean(nullable: false),
                        IsBestSeller = c.Boolean(nullable: false),
                        Description = c.String(storeType: "ntext"),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TableId = c.Int(nullable: false),
                        OrderStateId = c.Int(nullable: false),
                        DeliveryNote = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.OrderState", t => t.OrderStateId, cascadeDelete: true)
                .ForeignKey("dbo.Table", t => t.TableId, cascadeDelete: true)
                .Index(t => t.TableId)
                .Index(t => t.OrderStateId);
            
            CreateTable(
                "dbo.OrderState",
                c => new
                    {
                        OrderStateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BriefDescription = c.String(),
                    })
                .PrimaryKey(t => t.OrderStateId);
            
            CreateTable(
                "dbo.Table",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        BriefDescription = c.String(),
                        NumSeat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId)
                .ForeignKey("dbo.Region", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BriefDescription = c.String(),
                    })
                .PrimaryKey(t => t.RegionId);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        ProductImageId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageFile = c.String(),
                        IsPresent = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Promotion",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        ImageFile = c.String(),
                        Title = c.String(),
                        BriefDescription = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsPopular = c.Boolean(nullable: false),
                        Description = c.String(storeType: "ntext"),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BriefDescription = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        RoleId = c.Int(nullable: false),
                        Password = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        FullName = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        AvatarFile = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ProductImage", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Table", "RegionId", "dbo.Region");
            DropForeignKey("dbo.Order", "TableId", "dbo.Table");
            DropForeignKey("dbo.Order", "OrderStateId", "dbo.OrderState");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Product", "MenuId", "dbo.Menu");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.ProductImage", new[] { "ProductId" });
            DropIndex("dbo.Table", new[] { "RegionId" });
            DropIndex("dbo.Order", new[] { "OrderStateId" });
            DropIndex("dbo.Order", new[] { "TableId" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.Product", new[] { "MenuId" });
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Promotion");
            DropTable("dbo.ProductImage");
            DropTable("dbo.Region");
            DropTable("dbo.Table");
            DropTable("dbo.OrderState");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Product");
            DropTable("dbo.Menu");
            DropTable("dbo.CommonInfo");
        }
    }
}
