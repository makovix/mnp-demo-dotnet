using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace mnp_demo_dotnet
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //query the contacts, companies table in the DB and populate the list on the website

                DataTable dt = this.GetContactsData();
                             
                StringBuilder contactList = new StringBuilder();

                contactList.Append("</tr>");

                int i=1;
                string rowClass = "";

                foreach (DataRow row in dt.Rows)
                {

                    //determine the row coloring for even and odd rows
                    if (i%2 == 0)
                    {
                        rowClass = "table-td-even-contactlist";
                    }
                    else
                    {
                        rowClass = "table-td-odd-contactlist";
                    }

                    contactList.Append("<tr>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append("<a href=\"/contact?id=" + row["c_id"] + "\">");
                    contactList.Append(row["c_name"]);
                    contactList.Append("</a>");
                    contactList.Append("<br>");
                    contactList.Append(row["c_title"]);
                    contactList.Append("</td>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append(row["co_company"]);
                    contactList.Append("</td>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append(row["c_phone"]);
                    contactList.Append("</td>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append(row["c_address"]);
                    contactList.Append("</td>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append(row["c_email"]);
                    contactList.Append("</td>");

                    contactList.Append("<td class=\"" + rowClass + "\">");
                    contactList.Append(row["c_lastcontacted"]);
                    contactList.Append("</td>");

                    contactList.Append("</tr>");

                    i++;
                }


                plhContactList.Controls.Add(new Literal { Text = contactList.ToString() });
            }
        }

        private DataTable GetContactsData()
        {
            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;
            string strQuery = "SELECT c.c_id, c.c_name, c.c_title, co.co_company, c.c_phone, c.c_address, c.c_email, c.c_lastcontacted FROM dbo.contacts c, dbo.companies co WHERE c.co_id=co.co_id";

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strQuery))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            con.Close();
                            return dt;
                        }
                    }
                }
                
            }
        }

        protected void btnNewContact_Click(object sender, EventArgs e)
        {
            //route to new contact form
            Response.Redirect("/contact.aspx?new=true");
        }
    }
}