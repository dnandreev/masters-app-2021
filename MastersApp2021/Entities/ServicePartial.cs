using System.Windows;

namespace MastersApp2021.Entities{
	public partial class Service{
		public string DiscountText => (Discount == 0 || Discount == null) ? "" : $"* скидка {Discount * 100} %";

		public string TotalCost => (Discount == 0 || Discount == null) ? $"{Cost:N2} рублей за {DurationInSeconds / 60} минут" : $"{CostWithDiscount:N2} рублей за {DurationInSeconds / 60} минут";

		public double CostWithDiscount => (Discount == 0 || Discount == null) ? (double)Cost : ((double)Cost * (1.0 - Discount)).Value;

		public Visibility DiscountVisibility => (Discount == 0 || Discount == null) ? Visibility.Collapsed : Visibility.Visible;

		public string BackColor => (Discount == 0 || Discount == null) ? "#ffffe1" : "#d1ffd1";

		public string AdminControlsVisibility => App.CurrentUser.RoleId == 1 ? "Visible" : "Collapsed";
	}
}