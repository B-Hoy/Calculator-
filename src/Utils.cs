namespace Calculator_.src
{
	static internal class Utils
	{
		public static void ExitError(string errorMessage, int errorCode){
			Console.Error.WriteLine(errorMessage);
			Environment.Exit(errorCode);
		}
	}
}
