using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InventorySystem.Pages.Saim
{
	public class CreateSaimModel : PageModel
	{
		public StockInfo stockInfo = new StockInfo();
		public string errorMessage = "";
		public String successMessage = "";

		public void OnGet()
		{
		}
		public void OnPost()
		{
			stockInfo.item = Request.Form["item"];
			stockInfo.storeid = Request.Form["storeid"];
			stockInfo.supplier = Request.Form["supplier"];
			stockInfo.amount = Request.Form["amount"];

			if (stockInfo.item.Length == 0 || stockInfo.storeid.Length == 0 || stockInfo.supplier.Length == 0 || stockInfo.amount.Length == 0)
			{
				errorMessage = "All the fields are required";
				return;
			}

			try
			{
				String connectionString = "Server=tcp:inventory1650706805.database.windows.net,1433;Initial Catalog=Inventory_1650706805;Persist Security Info=False;User ID=pharanwit;Password=Ffd12e8f;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSET INTO stocks " +
								 "(item, storeid, supplier, amount) VALUES " +
								 "(@item, @storeid, @supplier, @amount);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@item", stockInfo.item);
						command.Parameters.AddWithValue("@storeid", stockInfo.storeid);
						command.Parameters.AddWithValue("@supplier", stockInfo.supplier);
						command.Parameters.AddWithValue("@amount", stockInfo.amount);

						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			stockInfo.item = "";
			stockInfo.storeid = "";
			stockInfo.supplier = "";
			stockInfo.amount = "";
			successMessage = "New Item Added Correctly";

			Response.Redirect("/Rangsit/IndexSaim");

		}
	}
}
