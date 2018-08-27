using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerismoTest.BLL;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace VerismoTest.Website
{
    public partial class Default : PageBaseClass
    {

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Peter\Desktop\VerismoTest-master - kopia\Website\App_Data\Database.mdf';Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM DataEntry", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvData.DataSource = dtbl;
                gvData.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                gvData.DataSource = dtbl;
                gvData.DataBind();
                gvData.Rows[0].Cells.Clear();
                gvData.Rows[0].Cells.Add(new TableCell());
                gvData.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                gvData.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void gvData_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void gvData_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            PopulateGridview();
        }

        protected void gvData_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE DataEntry SET Name=@Name,DueDate=@DueDate,Description=@Description WHERE Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Name", (gvData.Rows[e.RowIndex].FindControl("txtName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@DueDate", (gvData.Rows[e.RowIndex].FindControl("txtDueDate") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Description", (gvData.Rows[e.RowIndex].FindControl("txtDescription") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvData.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvData.EditIndex = -1;
                    PopulateGridview();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM DataEntry WHERE Id = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvData.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        // Jag ville skapa dynamiska textlådor som skapas med "Add Row" knappen för att sen spara allt med 
        // "Save" knappen. Lyckades inte få detta att funka som jag ville så struntade i "Add Row" knappen då 
        // "Save" knappen har samma funktion.

        protected void Save_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {


                sqlCon.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * from DataEntry", sqlCon);
                DataTable table = new DataTable();
                string query = "INSERT INTO DataEntry (Name,DueDate,Description) VALUES (@Name,@DueDate,@Description)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                adapter.Fill(table);
                sqlCmd.Parameters.AddWithValue("@Name", (gvData.FooterRow.FindControl("txtNameFooter") as TextBox).Text.Trim());
                sqlCmd.Parameters.AddWithValue("@DueDate", (gvData.FooterRow.FindControl("txtDueDateFooter") as TextBox).Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Description", (gvData.FooterRow.FindControl("txtDescriptionFooter") as TextBox).Text.Trim());
                sqlCmd.ExecuteNonQuery();
                PopulateGridview();
                sqlCon.Close();

            }
        }
    }
}