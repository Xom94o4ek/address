using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace address.Migrations
{
    public partial class AddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });
            
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    RegionsRegionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_Areas_Regions_RegionsRegionId",
                        column: x => x.RegionsRegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    LocalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    AreasAreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.LocalityId);
                    table.ForeignKey(
                        name: "FK_Localities_Areas_AreasAreaId",
                        column: x => x.AreasAreaId,
                        principalTable: "Areas",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalityId = table.Column<int>(type: "int", nullable: false),
                    LocalitiesLocalityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_Localities_LocalitiesLocalityId",
                        column: x => x.LocalitiesLocalityId,
                        principalTable: "Localities",
                        principalColumn: "LocalityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    StreetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    DistrictsDistrictId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.StreetId);
                    table.ForeignKey(
                        name: "FK_Streets_Districts_DistrictsDistrictId",
                        column: x => x.DistrictsDistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Floors = table.Column<int>(type: "int", nullable: false),
                    Entrances = table.Column<int>(type: "int", nullable: false),
                    Flats = table.Column<int>(type: "int", nullable: false),
                    StreetId = table.Column<int>(type: "int", nullable: false),
                    StreetsStreetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_Houses_Streets_StreetsStreetId",
                        column: x => x.StreetsStreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_RegionsRegionId",
                table: "Areas",
                column: "RegionsRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_LocalitiesLocalityId",
                table: "Districts",
                column: "LocalitiesLocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_StreetsStreetId",
                table: "Houses",
                column: "StreetsStreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_AreasAreaId",
                table: "Localities",
                column: "AreasAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_DistrictsDistrictId",
                table: "Streets",
                column: "DistrictsDistrictId");
            migrationBuilder.Sql(
                @"
                    INSERT INTO Users (Name, Password, 'Group', RegistrationDate, Email) VALUES ('TestUser', 'testuser', 0, '27.04.2021 0:00:00', 'TestUser@mail.ru');
                    INSERT INTO Users (Name, Password, 'Group', RegistrationDate, Email) VALUES ('TestAdmin', 'testadmin', 1, '27.04.2021 0:00:00', 'TestAdmin@mail.ru');
                    INSERT INTO Users (Name, Password, 'Group', RegistrationDate, Email) VALUES ('TestOwner', 'testowner', 2, '27.04.2021 0:00:00', 'TestOwner@mail.ru');
                    INSERT INTO Regions (RegionName) VALUES ('Республика Татарстан');
                    INSERT INTO Regions (RegionName) VALUES ('Волгоградская область');
                    INSERT INTO Areas (AreaName, RegionId) VALUES ('Казанский городской округ',1);
                    INSERT INTO Areas (AreaName, RegionId) VALUES ('Балтасинский район',1);
                    INSERT INTO Areas (AreaName, RegionId) VALUES ('Волгоградский городской округ',2);
                    INSERT INTO Areas (AreaName, RegionId) VALUES ('Алексеевский район',2);
                    INSERT INTO Localities (LocalityName, AreaId) VALUES ('Казань',1);
                    INSERT INTO Localities (LocalityName, AreaId) VALUES ('Карадуванское поселение',2);
                    INSERT INTO Localities (LocalityName, AreaId) VALUES ('пгт Балтаси',2);
                    INSERT INTO Localities (LocalityName, AreaId) VALUES ('Волгоград',3);
                    INSERT INTO Districts (DistrictName, LocalityId) VALUES ('Ново-Савиновский район',1);
                    INSERT INTO Districts (DistrictName, LocalityId) VALUES ('деревня Карадуван',2);
                    INSERT INTO Districts (DistrictName, LocalityId) VALUES ('Советский район',1);
                    INSERT INTO Streets (StreetName, DistrictId) VALUES ('ул. Чистопольская',1);
                    INSERT INTO Streets (StreetName, DistrictId) VALUES ('ул. Родник',2);
                    INSERT INTO Streets (StreetName, DistrictId) VALUES ('ул. Чуйкова',1);
                    INSERT INTO Houses (HouseNum, 'Index',Floors,Entrances,Flats,StreetId) VALUES ('11',420066,9,2,0,1);
                    INSERT INTO Houses (HouseNum, 'Index',Floors,Entrances,Flats,StreetId) VALUES ('1',422261,1,1,1,2);
                    INSERT INTO Houses (HouseNum, 'Index',Floors,Entrances,Flats,StreetId) VALUES ('9',420094,9,6,216,3);
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
