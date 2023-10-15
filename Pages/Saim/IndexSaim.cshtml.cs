using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace InventorySystem.Pages.Saim
{
	public class IndexSaimModel : PageModel
	{
		public List<StockInfo> listStocks = new List<StockInfo>();

		public void OnGet()
		{
			try
			{
				String connectionString = "Server=tcp:inventory1650706805.database.windows.net,1433;Initial Catalog=Inventory_1650706805;Persist Security Info=False;User ID=pharanwit;Password=Ffd12e8f;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM stocks WHERE storeid=2";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								StockInfo stokInfo = new StockInfo();
								stokInfo.itemid = "" + reader.GetInt32(0);
								stokInfo.item = reader.GetString(1);
								stokInfo.storeid = reader.GetString(2);
								stokInfo.supplier = reader.GetString(3);
								stokInfo.amount = reader.GetString(4);
								stokInfo.create_at = reader.GetDateTime(5).ToString();

								listStocks.Add(stokInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
	}

	public class StockInfo
	{

		public String itemid;
		public String item;
		public String storeid;
		public String supplier;
		public String amount;
		public String create_at;
	}
}

