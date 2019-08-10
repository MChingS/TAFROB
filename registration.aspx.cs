using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;






public partial class _Default : System.Web.UI.Page
{
    OdbcConnection conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["mchingis"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        warning.Visible = false;
        conn.Open();
        string getnumber = "SELECT MAX(empId) FROM tblemployee";
        OdbcDataAdapter adapt = new OdbcDataAdapter(getnumber, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];

        string num="";
        int maxNum=0;
        foreach (DataRow dat in myTable.Rows)
        {
            num = dat["MAX(empId)"].ToString();
            maxNum = Convert.ToInt32(num)+1;
        }
        conn.Close();
        empid.Text = maxNum.ToString(); ;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        conn.Open();
        string getnumber = "SELECT empUsername FROM tblemployee WHERE empUsername='" + TextBox7.Text.ToUpper() + "'";
        OdbcDataAdapter adapt = new OdbcDataAdapter(getnumber, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];
        if (myTable.Rows.Count > 0)
        {
            warning.Visible = true;
        }
        else
        {
            string jobcode = "";
            if (DropDownList1.SelectedIndex == 0)
            {
                jobcode = "labTe";
            }
            if (DropDownList1.SelectedIndex == 1)
            {
                jobcode = "lectu";
            }
            string insert = "INSERT INTO tblemployee(empid,jobCode,empUsername,empPassword) values(" + Convert.ToInt32(empid.Text)
                + ",'" + jobcode + "','" + TextBox7.Text.ToUpper() + "','" + TextBox13.Text + "')";
            OdbcCommand cmd = new OdbcCommand(insert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("login.aspx");
        }
        conn.Close();

    }
}