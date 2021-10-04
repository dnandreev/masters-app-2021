using System.Windows;

namespace MastersApp2021{
	public partial class App : Application{
		public static Entities.DataBaseNameEntities Context { get; } = new Entities.DataBaseNameEntities();
		public static Entities.User CurrentUser = null;
	}
}