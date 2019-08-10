using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class student : System.Web.UI.Page
{
    OdbcConnection conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["mchingis"].ConnectionString);
    DataTable tb = new DataTable();
    string[] rownum = new string[50];
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null || Session["role"] == null)
        {
            Response.Redirect("homepage.aspx");
        }
        else if (Session["role"].ToString() != "student")
        {
            Response.Redirect("unautorized.aspx");
        }
        else
        {
            createtable();
            string userN = Session["username"].ToString();
            string get = " SELECT s.studNo,coursecode,studname,studsurname,pcname,seatallocdate,seatallocslot,subjcode ";
            get += " FROM tblstudent s, tblseatallocation sa ";
            get += " WHERE s.studusername = '" + userN + "' ";
            get += "AND s.studno = sa.studno ";

            OdbcDataAdapter adapt = new OdbcDataAdapter(get, conn);
            DataSet dts = new DataSet();
            adapt.Fill(dts);
            DataTable myTable = dts.Tables[0];

            string usernamf = userN;
            string studnf = "";
            string namef = "";
            string surnamf = "";
            string courseCodef = "";
            string logTimf = DateTime.Now.ToLongTimeString();
            string subjf = "";
            string labf = "";
            string pcNumf = "";
            string slotf = "";
            string datef = "";
            foreach (DataRow dat in myTable.Rows)
            {
                studnf = dat["studNo"].ToString();
                courseCodef = dat["coursecode"].ToString();
                namef = dat["studname"].ToString();
                surnamf = dat["studsurname"].ToString();
                labf = dat["pcname"].ToString().Substring(0, 5);
                pcNumf = dat["pcname"].ToString().Substring(5);
                datef = dat["seatallocdate"].ToString();
                slotf = dat["seatallocslot"].ToString();
                subjf = dat["subjcode"].ToString();

                dr = tb.NewRow();
                dr["Subject"] = subjf;
                dr["Lab"] = labf;
                dr["Pc Number"] = pcNumf;
                dr["Date"] = datef;
                dr["Slot"] = slotf;
                tb.Rows.Add(dr);
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();
            ViewState["table1"] = tb;
            studUsername.Text = usernamf;
            studNo.Text = studnf;
            studName.Text = namef;
            studSurname.Text = surnamf;
            studCoursecod.Text = courseCodef;
            timeLoged.Text = logTimf;
        }
    }
    public void createtable()
    {
        tb.Columns.Add("Subject", typeof(string));
        tb.Columns.Add("Lab", typeof(string));
        tb.Columns.Add("Pc Number", typeof(string));
        tb.Columns.Add("Date", typeof(string));
        tb.Columns.Add("Slot", typeof(string));
        dr = tb.NewRow();
        tb.Rows.Add(dr);
        GridView1.DataSource = tb;
        GridView1.DataBind();
        ViewState["table1"] = tb;

    }

    protected void exit_Click(object sender, EventArgs e)
    {
        Session["role"] = null;
        Response.Redirect("homepage.aspx");
    }
}