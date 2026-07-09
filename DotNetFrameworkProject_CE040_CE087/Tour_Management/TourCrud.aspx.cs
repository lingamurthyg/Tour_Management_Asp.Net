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
            conn.Open(); // Connection pooling is handled by RDS Proxy via the connection string


        // Ensure connection is disposed if used in a larger scope
            //GridView1.DataBind();


        }

       
    }
}