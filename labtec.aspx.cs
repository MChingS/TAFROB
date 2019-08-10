using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Configuration;
using System.Data;

public partial class labtec : System.Web.UI.Page
{
    OdbcConnection conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["mchingis"].ConnectionString);
    string[] labCap = new string[50];
    string[] labCod = new string[50];
    string[] labStruc = new string[50];
    List<string> checkedBox = new List<string>();
    CheckBox[] pc = new CheckBox[150];
    int labMax=0;
    public static List<string> pcNamesList = new List<string>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null || Session["role"] == null)
        {
            Response.Redirect("homepage.aspx");
        }
        else if (Session["role"].ToString() != "labTe")
        {
            Response.Redirect("unautorized.aspx");
        }
        else
        {
            config.Text = configList.SelectedItem.ToString();
            conn.Open();
            string get = "SELECT * FROM tblvenue where venDesc='lab'";
            OdbcDataAdapter adapt = new OdbcDataAdapter(get, conn);
            DataSet dts = new DataSet();
            adapt.Fill(dts);
            DataTable myTable = dts.Tables[0];

            int v = 0;
            foreach (DataRow ll in myTable.Rows)
            {
                labCod[v] = ll["venCode"].ToString();
                if (labList.Items.Count < 5)
                {
                    labList.Items.Add(labCod[v]);
                }
                v++;
            }
            if (pcListFromDB.Items.Count < 1)
            {
                get = "SELECT pcName FROM tblpc WHERE venCode = '" + labList.SelectedItem.ToString() + "'";
                adapt = new OdbcDataAdapter(get, conn);
                dts = new DataSet();
                adapt.Fill(dts);
                myTable = dts.Tables[0];
                pcListFromDB.Items.Clear();
                foreach (DataRow ll in myTable.Rows)
                {
                    string pcName1 = ll["pcName"].ToString();
                    pcListFromDB.Items.Add(pcName1);
                }
            }
            labName.Visible = false;
            conn.Close();
        }
    }
    protected void configList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(configList.SelectedIndex == 0)
        {
            pcDet.Visible = false;
            pcListFromDB.Visible = false;
        }
        else
        {
            pcDet.Visible = true;
            pcListFromDB.Visible = true;
        }
      
        config.Text = configList.SelectedItem.ToString();
    }
    protected void update_Click(object sender, EventArgs e)
    {
        int stat = DropDownList3.SelectedIndex;
        string ven = labList.SelectedItem.ToString();
        conn.Open();
        if (configList.SelectedIndex == 0)
        {
            string upt = "UPDATE tblvenue SET venStatus =" + stat + " WHERE venCode ='" + ven + "'";
            OdbcDataAdapter adapt = new OdbcDataAdapter(upt, conn);
            DataSet dtss = new DataSet();
            adapt.Fill(dtss);
            labName.Text = ven + " status updated";
        }
        else
        {
            int pcStatus = DropDownList3.SelectedIndex;
            string pcNam = pcListFromDB.SelectedItem.ToString();
            string update = "UPDATE tblpc SET pcStatus=" + pcStatus + " WHERE pcName = '" + pcNam + "'";
            OdbcDataAdapter adapt =  new OdbcDataAdapter(update, conn);
            DataSet dtss = new DataSet();
            adapt.Fill(dtss);
            labName.Text = pcNam+" status updated";
        }


        labName.Visible = true;
       conn.Close();
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void labList_SelectedIndexChanged(object sender, EventArgs e)
    {
        conn.Open();
        pcListFromDB.Items.Clear();
        string get = "SELECT pcName FROM tblpc WHERE venCode = '" + labList.SelectedItem.ToString() + "'";
        OdbcDataAdapter adapt = new OdbcDataAdapter(get, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];
        pcListFromDB.Items.Clear();
        foreach (DataRow ll in myTable.Rows)
        {
            string pcName1 = ll["pcName"].ToString();
            pcListFromDB.Items.Add(pcName1);
        }
        conn.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["role"] = null;
        Response.Redirect("homepage.aspx");
    }
}