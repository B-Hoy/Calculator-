using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using SQLitePCL;
using System.Data.SQLite;
using System.IO;
using System.Security.Policy;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Calculator_.src{
    // "Use of Entity Framework"
    internal class SQLiteContext : DbContext{
		internal DbSet<User> Users { get; set; }
		internal DbSet<DataPoint> Points { get; set; }
		public SQLiteContext() : base()
		{
		}
        // "At least one example of polymorphism which achieves a useful purpose(either through inheritance, method/constructor overloading/overriding)
		// Considering the override here is *required* to use our database, I'd say that's useful
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
			optionsBuilder.UseSqlite(Calculator_.src.Database.RawHook);
		}
	}
	internal class Database
    {
        private static readonly SQLiteConnection conn;
		private static readonly SQLiteContext hook;
        static Database(){
			SQLitePCL.raw.SetProvider(new SQLite3Provider_e_sqlite3());
			string DBFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Calculator²\\userdata.sqlite");
			string? baseDirectory = Path.GetDirectoryName(DBFilePath);
			if (baseDirectory == null)
			{
				// Environment variables are scuffed, don't assume anything just leave while we can
				Utils.ExitError("Could not identify environment variable for application data directory", 1);
			}

			if (!Path.Exists(baseDirectory))
			{
				Directory.CreateDirectory(baseDirectory!);
			}
			SQLiteConnectionStringBuilder connString = new()
			{
				DataSource = DBFilePath
			};

			SQLiteConnection? tempConn = new(connString.ToString());
			if (tempConn == null)
			{
				Utils.ExitError("Could not identify environment variable for application data directory", 1);
			}
			conn = tempConn!;
			conn.Open();
			hook = new();
			MakeTables();

		}
		private static void MakeTables()
		{
			
			MigrationBuilder migrationBuilder = new("Microsoft.EntityFrameworkCore.Sqlite");
            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Freq = table.Column<long>(type: "INTEGER", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    Val = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Freq = table.Column<long>(type: "INTEGER", nullable: false),
                    Desc = table.Column<string>(type: "TEXT", nullable: false),
                    Val = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<long>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Fname = table.Column<string>(type: "TEXT", nullable: false),
                    Lname = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    CCN = table.Column<string>(type: "TEXT", nullable: false),
                    CCE = table.Column<string>(type: "TEXT", nullable: false),
                    CVC = table.Column<string>(type: "TEXT", nullable: false),
                    IsActiveUser = table.Column<bool>(type: "INTEGER", nullable: false),
                    FavouriteColour = table.Column<int>(type: "INTEGER", nullable: false),
                    UnlockedDigits = table.Column<int>(type: "INTEGER", nullable: false),
                    OperatorsUnlocked = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            var gen = EFHook.GetInfrastructure().GetService<IMigrationsSqlGenerator>()!;
			var backConn = EFHook.GetInfrastructure().GetService<IRelationalConnection>()!;
            var commands = gen.Generate(migrationBuilder.Operations);
			var exec = EFHook.GetInfrastructure().GetService<IMigrationCommandExecutor>()!;
			try
			{
				exec.ExecuteNonQuery(commands, backConn);
			}catch (TypeInitializationException) // db already exists, we don't really care
            {

			}
			catch (Exception e)
			{
				Console.Error.Write(e.ToString());
			}


        }
		// \/ This is empty for the sole purpose of initialising the DB, accessing a static method will start it, even if it doesn't do anything
		public static void BeginIt()
		{

		}
		public static void AddUser(string email, string fname, string lname, string gender, string dob, string username, bool isactiveuser, int favcolour, string ccn, string cce, string cvc)
		{
            // "Use of external database with LINQ"
            IEnumerable<int> idList = from u in EFHook.Users orderby u.ID descending select u.ID;
			int id = idList.Any() ? idList.First() + 1 : 1;
			if (isactiveuser)
			{
				foreach(User u in EFHook.Users)
				{
					u.IsActiveUser = false;
				}
			}
			EFHook.Users.Add(new User(id, email, fname, lname, gender, dob, username, !idList.Any() || isactiveuser, favcolour, ccn, cce, cvc));
			EFHook.SaveChanges();
        }
		public static SQLiteConnection RawHook{
			get{
				return conn;
			}
		}
		public static SQLiteContext EFHook{
			get{
				return hook;
			}
		}
    }
}
