using System;
using System.Xml;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VerismoTest.BLL;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace VerismoTest.Website
{

    public partial class Default : PageBaseClass
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Peter\Desktop\VerismoTest-masterPeter\Website\App_Data\Database.mdf';Connect Timeout=30";
        List<string> Names = new List<string>();
        List<string> DueDates = new List<string>();
        List<string> Descriptions = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRows();
                LoadAllValues();
                GetAllTextBoxValues();
            }
        }

        private void SetInitialRows()
        {
            int rowIndex = 0;
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            dr = dt.NewRow();
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            GridView1.DataSource = dt;
            GridView1.DataBind();

            int count = 0;
            string stmt = string.Format("SELECT COUNT(*) FROM DataEntry");
            using (SqlConnection thisConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdCount = new SqlCommand(stmt, thisConnection))
                {
                    thisConnection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                }
            }
            if (count > 1)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                for (int i = 1; i < count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Column1"] = string.Empty;
                    drCurrentRow["Column2"] = string.Empty;
                    drCurrentRow["Column3"] = string.Empty;

                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);

                }

                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;


                //Rebind the Grid with the current data
                GridView1.DataSource = dtCurrentTable;
                GridView1.DataBind();

            }
        }

        void LoadAllValues()
        {
            int count = 0, count2 = 0;
            List<string> values = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DataEntry", con);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                //putting values into a list of strings
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        values.Add(item.ToString());
                    }
                }
                //int count = 0;
                foreach (GridViewRow grv in GridView1.Rows)
                {

                    TextBox txtbox1 = grv.FindControl("TextBox1") as TextBox;
                    TextBox txtbox2 = grv.FindControl("TextBox2") as TextBox;
                    TextBox txtbox3 = grv.FindControl("TextBox3") as TextBox;

                    //Check if your textbox was found
                    if (values.Count > count)
                    {
                        if (txtbox1 != null)
                        {
                            //Get your value here                        
                            txtbox1.Text = values[count];
                            count2++;
                            Debug.WriteLine(count2);
                            count++;
                        }
                        if (txtbox2 != null)
                        {
                            //Get your value here
                            txtbox2.Text = values[count];
                            count2++;
                            Debug.WriteLine(count2);
                            count++;
                        }
                        if (txtbox3 != null)
                        {
                            //Get your value here
                            txtbox3.Text = values[count];
                            count2++;
                            Debug.WriteLine(count2);
                            count++;
                        }
                    }
                }
            }
            Debug.WriteLine(count);
        }

        private void GetAllTextBoxValues()
        {
            Names.Clear();
            DueDates.Clear();
            Descriptions.Clear();
            foreach (GridViewRow item in GridView1.Rows)
            {
                TextBox nm = item.FindControl("TextBox1") as TextBox;
                Names.Add(nm.Text);
                Debug.WriteLine(nm.Text);
                TextBox dd = item.FindControl("TextBox2") as TextBox;
                DueDates.Add(dd.Text);
                TextBox de = item.FindControl("TextBox3") as TextBox;
                Descriptions.Add(de.Text);
            }
            foreach (string item in Names)
            {
                Debug.WriteLine("Name: " + item);
            }
        }

        void PopulateRemaingingBoxes()
        { 
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<string> values = new List<string>();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM DataEntry", con);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                //putting values into a list of strings
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        values.Add(item.ToString());
                    }
                }
                int cntr = 0;
                foreach (GridViewRow item in GridView1.Rows)
                {
                    TextBox txtbox1 = item.FindControl("TextBox1") as TextBox;
                    TextBox txtbox2 = item.FindControl("TextBox2") as TextBox;
                    TextBox txtbox3 = item.FindControl("TextBox3") as TextBox;

                    //Check if your textbox was found
                    if (cntr < Names.Count)
                    {
                        if (txtbox1 != null)
                        {
                            //Get your value here                        
                            txtbox1.Text = Names[cntr];
                        }
                        if (txtbox2 != null)
                        {
                            //Get your value here
                            txtbox2.Text = DueDates[cntr];
                        }
                        if (txtbox3 != null)
                        {
                            //Get your value here
                            txtbox3.Text = Descriptions[cntr];
                        }
                        cntr++;

                    }
                }
            }
        }

        void PopulateGridview()
        {
            List<string> values = new List<string>();
            DataTable dtbl = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM DataEntry", con);
                sqlDa.Fill(dtbl);

                SqlCommand cmd = new SqlCommand("SELECT * FROM DataEntry", con);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                //putting values into a list of strings
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        values.Add(item.ToString());
                    }
                }
            }
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                int count = 0;
                foreach (GridViewRow grv in GridView1.Rows)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM DataEntry", con);

                    TextBox txtbox1 = grv.FindControl("TextBox1") as TextBox;
                    TextBox txtbox2 = grv.FindControl("TextBox2") as TextBox;
                    TextBox txtbox3 = grv.FindControl("TextBox3") as TextBox;

                    //Check if your textbox was found
                    if (txtbox1 != null)
                    {
                        //Get your value here                        
                        txtbox1.Text = values[count];
                        count++;
                    }
                    if (txtbox2 != null)
                    {
                        //Get your value here
                        txtbox2.Text = values[count];
                        count++;
                    }
                    if (txtbox3 != null)
                    {
                        //Get your value here
                        txtbox3.Text = values[count];
                        count++;
                    }
                }
            }
        }

        private void AddNewRowToGrid()
        {
           
        }

        //private void SetPreviousData()
        //{
        //    List<TextBox> boxArr = new List<TextBox>();
        //    int rowIndex = 0;
        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dt = (DataTable)ViewState["CurrentTable"];
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 1; i < dt.Rows.Count; i++)
        //            {
        //                TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
        //                TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
        //                TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

        //                box1.Text = dt.Rows[i]["Column1"].ToString();
        //                box2.Text = dt.Rows[i]["Column2"].ToString();
        //                box3.Text = dt.Rows[i]["Column3"].ToString();

        //                rowIndex++;
        //            }
        //        }
        //    }
        //}

        protected void Save_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You have saved the data');", true);
            GetAllTextBoxValues();

            //remove old data
            SqlConnection sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();

            string sql = @"DELETE FROM DataEntry;";
            SqlCommand cmd1 = new SqlCommand(sql, sqlCon);
            cmd1.ExecuteNonQuery();
            sqlCon.Close();
            //end of - remove old data

            //add current data to database
            Button button = (Button)sender;
            string buttonID = button.ID;
            if (buttonID == "SaveData")
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    foreach (GridViewRow grv in GridView1.Rows)
                    {
                        con.Open();

                        TextBox nm = (TextBox)grv.FindControl("TextBox1");
                        TextBox dd = (TextBox)grv.FindControl("TextBox2");
                        TextBox de = (TextBox)grv.FindControl("TextBox3");

                        string query = "INSERT INTO DataEntry (Name,DueDate,Description) VALUES (@TextBox1,@TextBox2,@TextBox3)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@TextBox1", (grv.FindControl("TextBox1") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@TextBox2", (grv.FindControl("TextBox2") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@TextBox3", (grv.FindControl("TextBox3") as TextBox).Text.Trim());

                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }

        }

        protected void AddRow_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddRow"))
            {
                GetAllTextBoxValues();
                Names.Add("");
                DueDates.Add("");
                Descriptions.Add("");

                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            //extract the TextBox values
                            TextBox box1 = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                            TextBox box2 = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                            TextBox box3 = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("TextBox3");

                            drCurrentRow = dtCurrentTable.NewRow();
                            drCurrentRow["Column1"] = box1.Text;
                            drCurrentRow["Column2"] = box2.Text;
                            drCurrentRow["Column3"] = box3.Text;

                            rowIndex++;
                        }

                        //add new row to DataTable
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        //Store the current data to ViewState
                        ViewState["CurrentTable"] = dtCurrentTable;

                        //Rebind the Grid with the current data
                        GridView1.DataSource = dtCurrentTable;
                        GridView1.DataBind();
                    }
                    if (dtCurrentTable.Rows.Count == 0)
                    {
                        SetInitialRows();
                    }
                }
                else {
                    SetInitialRows();
                }
                PopulateRemaingingBoxes();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GetAllTextBoxValues();

            Names.RemoveAt(e.RowIndex);
            DueDates.RemoveAt(e.RowIndex);
            Descriptions.RemoveAt(e.RowIndex);

            DataTable dt = (DataTable)ViewState["CurrentTable"];

            dt.Rows.RemoveAt(e.RowIndex);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            PopulateRemaingingBoxes();
        }
    }
}