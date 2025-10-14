using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Data.SQLite;
using System.IO;
namespace Calculator_.src
{
	internal class SQLiteContext : DbContext{
		internal DbSet<User> Users { get; set; }
		public SQLiteContext() : base()
		{
		}
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
			SQLiteCommand exec = conn.CreateCommand();
			exec.CommandText = "CREATE TABLE IF NOT EXISTS Users(ID INTEGER PRIMARY KEY NOT NULL, Fname TEXT NOT NULL, Lname TEXT NOT NULL, Gender TEXT NOT NULL, DOB TEXT NOT NULL, Username TEXT NOT NULL, CCN TEXT NOT NULL, CCE TEXT NOT NULL, CVC TEXT NOT NULL, IsActiveUser INTEGER NOT NULL, FavouriteColour INTEGER NOT NULL);";
			exec.ExecuteNonQuery();
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
