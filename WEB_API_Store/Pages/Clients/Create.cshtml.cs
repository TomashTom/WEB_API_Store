using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WEB_API_Store.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.age = Request.Form["age"];
            clientInfo.comment = Request.Form["comment"];


            if (clientInfo.name.Length == 0 || clientInfo.age.Length == 0 || clientInfo.comment.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save the new client into the database

            try
            {
                String connectionString = "Data Source=DESKTOP-1FTST8H\\MSSQLSERVER01;Initial Catalog=mypeople;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " + "(name, age, comment) VALUES " +
                        "(@name, @age, @comment);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@age", clientInfo.age);
                        command.Parameters.AddWithValue("@comment", clientInfo.comment);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.age = "";
            clientInfo.comment = "";
            successMessage = "New Client Added Correctly!";

            Response.Redirect("/Clients/Index");
        }
          

    }
    
}
