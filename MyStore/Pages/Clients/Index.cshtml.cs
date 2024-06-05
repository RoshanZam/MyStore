using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-SRGU7RJ\\SQLEXPRESS;Initial Catalog=MYSTORE;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "select * from clients";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = reader.GetInt32(0).ToString();
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.createdAt = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo); 
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String createdAt;
    }
}
