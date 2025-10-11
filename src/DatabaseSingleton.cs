namespace Calculator_.src
{
	internal class DatabaseSingleton
	{
		private readonly DatabaseSingleton self = new();
		private static readonly Database db = new();
		static DatabaseSingleton(){

		}
		private DatabaseSingleton(){

		}
	}
}
