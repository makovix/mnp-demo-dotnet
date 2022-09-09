using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Net;
using System.Xml.Linq;
using System.Text;

namespace mnp_demo_dotnet
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //upon pageload, determine if the user is here to create a new contact, or update an existing one. Same form is used for both events
                string contactId = Request.QueryString["id"];
                string newContact = Request.QueryString["new"];

                setCompanyList();

                //view and edit existing contact
                if (contactId != null)
                {
                    //load data for existing user
                    setContactsData(int.Parse(contactId));
                }

                //create a new contact
                if (newContact == "true")
                {
                    //ensure all fields are reset for new user
                    txtName.Text = "";
                    txtJobTitle.Text = "";
                    txtPhone.Text = "";
                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtComments.Text = "";
                    txtCID.Text = "-1"; // set new contact flag
                    txtLastDate.Text = "";
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string cName = txtName.Text;
            string cTitle = txtJobTitle.Text;
            string cPhone = txtPhone.Text;
            string cAddress = txtAddress.Text;
            string cEmail = txtEmail.Text;
            DateTime cLastContacted = DateTime.Parse(txtLastDate.Text);
            string cComments = txtComments.Text;
            int cId = int.Parse(txtCID.Text);
            string coId = ddlCompany.SelectedValue;

            //upon submission, determine if this is for a new or existing user
            // cId == -1 is a flag that is set in a hidden field to indicate that the user is new. If cId is any other integer, it will be an existing user
            if (cId == -1)
            {
                insertContactData(cName, cTitle, cPhone, cAddress, cEmail, cLastContacted, cComments, coId);
            } 
            else
            {
                updateContactsData(cName, cTitle, cPhone, cAddress, cEmail, cLastContacted, cComments, cId, coId);
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/");
        }

        private void setContactsData(int contactID)
        {

            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;
            string strQuery = "SELECT c.c_id, c.c_name, c.c_title, co.co_company, co.co_id, c.c_phone, c.c_address, c.c_email, c.c_lastcontacted, c.c_comments FROM dbo.contacts c, dbo.companies co WHERE c.co_id=co.co_id AND c.c_id=" + contactID;

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

                            //extract user contact values from query and set fields with these values
                            foreach (DataRow row in dt.Rows)
                            {

                                txtName.Text = row["c_name"].ToString();
                                txtJobTitle.Text = row["c_title"].ToString();
                                txtPhone.Text = row["c_phone"].ToString();
                                txtAddress.Text = row["c_address"].ToString();
                                txtEmail.Text = row["c_email"].ToString();
                                txtComments.Text = row["c_comments"].ToString();
                                txtCID.Text = row["c_id"].ToString();
                                
                                DateTime lastContacted = DateTime.Parse(row["c_lastcontacted"].ToString());

                                txtLastDate.Text = lastContacted.ToShortDateString();
                                ddlCompany.SelectedValue = row["co_id"].ToString();
                            }
                            con.Close();
                        }
                    }
                }

            }
        }
        private void updateContactsData(string cName, string cTitle, string cPhone, string cAddress, string cEmail, DateTime cLastContacted, string cComments, int cId, string coId)
        {

            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;

            litResponseMsg.Text = "";

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.contacts SET c_name = @con_name, co_id = @comp_id, c_title = @conn_title, c_phone = @conn_phone, c_address = @con_address, c_email = @con_email, c_lastcontacted = @conn_lastcontacted, c_comments = @con_comments WHERE c_id = @con_id", con))
                {

                    try
                    {
                        //add values submitted from form as query parameters
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@con_name", cName);
                        cmd.Parameters.AddWithValue("@conn_title", cTitle);
                        cmd.Parameters.AddWithValue("@conn_phone", cPhone);
                        cmd.Parameters.AddWithValue("@con_address", cAddress);
                        cmd.Parameters.AddWithValue("@con_email", cEmail);
                        cmd.Parameters.AddWithValue("@conn_lastcontacted", cLastContacted);
                        cmd.Parameters.AddWithValue("@con_comments", cComments);
                        cmd.Parameters.AddWithValue("@con_id", cId);
                        cmd.Parameters.AddWithValue("@comp_id", coId);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected == 1)
                        {
                            litResponseMsg.Text = "<font color=\"green\">Contact information has been successfully updated!</font> <a href=\"/\">Back to contact list</a>";
                        } else
                        {
                            litResponseMsg.Text = "<font color=\"red\">Contact information could not be updated</font> <a href=\"/\">Back to contact list</a>";
                        }
                    }
                    catch (Exception e)
                    {
                        litResponseMsg.Text = "<font color=\"red\">There was an error, contact information could not be updated</font> <a href=\"/\">Back to contact list</a>";
                    } 
                    finally
                    {
                        con.Close();
                    }

                    
                }
            }

        }


        private void insertContactData(string cName, string cTitle, string cPhone, string cAddress, string cEmail, DateTime cLastContacted, string cComments, string coId)
        {

            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;

            litResponseMsg.Text = "";

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.contacts (c_id, c_name, c_title, c_phone, c_address, c_email, c_lastcontacted, c_comments, co_id) VALUES (@con_id, @con_name, @conn_title, @conn_phone, @con_address, @con_email, @conn_lastcontacted, @con_comments, @comp_id)", con))
                {

                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@con_name", cName);
                        cmd.Parameters.AddWithValue("@conn_title", cTitle);
                        cmd.Parameters.AddWithValue("@conn_phone", cPhone);
                        cmd.Parameters.AddWithValue("@con_address", cAddress);
                        cmd.Parameters.AddWithValue("@con_email", cEmail);
                        cmd.Parameters.AddWithValue("@conn_lastcontacted", cLastContacted);
                        cmd.Parameters.AddWithValue("@con_comments", cComments);
                        cmd.Parameters.AddWithValue("@con_id", getNextCID());
                        cmd.Parameters.AddWithValue("@comp_id", coId);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected == 1)
                        {
                            litResponseMsg.Text = "<font color=\"green\">Contact information has been successfully added!</font> <a href=\"/\">Back to contact list</a>";
                        }
                        else
                        {
                            litResponseMsg.Text = "<font color=\"red\">Contact information could not be added</font> <a href=\"/\">Back to contact list</a>";
                        }
                    }
                    catch (Exception e)
                    {
                        litResponseMsg.Text = "<font color=\"red\">There was an error, contact information could not be added</font> <a href=\"/\">Back to contact list</a>";
                    }
                    finally
                    {
                        con.Close();
                    }


                }
            }

        }


        private int getNextCID()
        {
            //I missed the opportunity to set AUTO INCREMENT on the contact ID field in the database, there my workaround was to 
            //find the max contact ID and add 1 to it

            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;
            string strQuery = "select (max(c_id)+1) c_id from dbo.contacts";

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
                            int nextCID = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                nextCID = int.Parse(row["c_id"].ToString());
                            }

                            return nextCID;
                            
                        }
                    }
                }

            }
        }

       
        private void setCompanyList()
        {
            // Get list of all companies in the company table in the DB and populate the dropdownlist with these values
            // I couldn't remember the quick way to do this, so I simply iterated though the data

            string strConn = ConfigurationManager.ConnectionStrings["mnp-demo-dotnet_dbConnectionString"].ConnectionString;
            string strQuery = "select co_id, co_company from dbo.companies";

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

                            foreach (DataRow row in dt.Rows)
                            {
                                ddlCompany.Items.Add(new ListItem(row["co_company"].ToString(), row["co_id"].ToString()));
                            }


                        }
                    }
                }

            }
        }

    }
}