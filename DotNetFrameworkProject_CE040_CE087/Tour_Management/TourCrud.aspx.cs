using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Tour_Management
{
    public partial class TourCrud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                refreshdata();
            }
        }
        public void refreshdata()
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? ConfigurationManager.ConnectionStrings["dbconnection"]?.ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = "select * from Tour";
                SqlCommand com = new SqlCommand(insertQuery, conn);
              // GridView1.DataSource = insertQuery;
               // GridView1.DataBind();
            }
            //sda.Fill(dt);
           // GridView1.DataSource = dt;
            //GridView1.DataBind();


        }

       
    }
}