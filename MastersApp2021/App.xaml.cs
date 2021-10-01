using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MastersApp2021{
	public partial class App : Application{
		public static Entities.DataBaseNameEntities Context{get;} = new Entities.DataBaseNameEntities();
	}
}