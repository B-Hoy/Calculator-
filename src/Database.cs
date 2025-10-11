using System.IO;
using System.Data.SQLite;
namespace Calculator_.src
{
    public class Database
    {
        private static readonly SQLiteConnection conn;
        static Database(){
			string DBFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\Calculator²\\userdata.sqlite");
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
		}
        public static SQLiteConnection Hook{
            get{
                return conn;
            }
        }
    }
}
