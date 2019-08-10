using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class seatAllocation : System.Web.UI.Page
{
    OdbcConnection conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["mchingis"].ConnectionString);

    Label[] labName = new Label[100];
    Label[] LabCap = new Label[100];
    Label[] labStruct = new Label[100];
    CheckBox[] labCheck = new CheckBox[100];
    CheckBox[] pcName = new CheckBox[800];
    int labTot = 0;
    DateTime tod = DateTime.Today;

    public static List<string> studNoList = new List<string>();
    public static List<string> pcStatusList = new List<string>();
    public static List<string> pcNamesList = new List<string>();
    public static List<string> upDatePCStat = new List<string>();
    public static List<string> pcNameUpdate = new List<string>();
    string usernamF = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null || Session["role"] == null)
        {
            Response.Redirect("homepage.aspx");
        }
        else if (Session["role"].ToString() != "lectu")
        {
            Response.Redirect("unautorized.aspx");
        }
        else
        {
            usernamF = Session["username"].ToString();
            studUsername.Text = usernamF;
            warning.Visible = false;
            createLabs();

            if (slotDate.SelectedDate < DateTime.Today)
            {
                slotDate.SelectedDate = DateTime.Today;
            }
        }
    }
    public void createLabs()
    {
        conn.Open();

        string colect = "SELECT * FROM tblvenue WHERE venDesc = 'lab' AND venStatus=1";
        OdbcDataAdapter adapt = new OdbcDataAdapter(colect, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];

        string labN;
        DataRow temp;
        int i = 0;
        foreach (DataRow dat in myTable.Rows)
        {
            temp = dat;
            labN = temp["venCode"].ToString();
            labName[i] = new Label();
            labName[i].Width = 100;
            labName[i].Text = labN;

            int numm = Convert.ToInt32(temp["venNumSeats"]);
            LabCap[i] = new Label();
            LabCap[i].Width = 100;
            LabCap[i].Text = numm.ToString();

            string stru = temp["venStructure"].ToString();

            labStruct[i] = new Label();
            labStruct[i].Width = 100;
            labStruct[i].Text = stru;

            labCheck[i] = new CheckBox();
            labCheck[i].Width = 100;
            labCheck[i].ID = labN;

            labControler.Controls.Add(labName[i]);
            labControler.Controls.Add(LabCap[i]);
            labControler.Controls.Add(labStruct[i]);
            labControler.Controls.Add(labCheck[i]);

            ViewState["AddedControl"] = true;
            labTot++;
            i++;
        }

        colect = "SELECT * FROM tblcourse";
        adapt = new OdbcDataAdapter(colect, conn);
        dts = new DataSet();
        adapt.Fill(dts);
        myTable = dts.Tables[0];

        i = 0;
        if (courseList.Items.Count < 2)
        {
            foreach (DataRow dat in myTable.Rows)
            {
                temp = dat;
                labN = temp["courseCode"].ToString();
                courseList.Items.Add(labN);
            }
        }
        conn.Close();

    }
    protected void courseList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        labControler.Visible = false;
        nextToPc.Visible = false;
        Panel2.Visible = false;
        seatName.Visible = false;
        //numStudeUpdates.Visible = false;
        if (courseList.SelectedIndex != 0)
        {
            conn.Open();
            string code = courseList.SelectedItem.ToString();
            String colect = "SELECT * FROM tblsubject WHERE courseCode ='" + code + "' AND subPractical_Theory='Practical'; ";
            OdbcDataAdapter adapt = new OdbcDataAdapter(colect, conn);
            DataSet dts = new DataSet();
            adapt.Fill(dts);
            DataTable myTable = dts.Tables[0];
            subjectList.Items.Clear();
            if (myTable.Rows.Count < 1)
            {
                subjectList.Items.Add("No subjects available");
            }
            else
            {
                foreach (DataRow dat in myTable.Rows)
                {
                    DataRow temp = dat;
                    string labN = temp["subjCode"].ToString();
                    subjectList.Items.Add(labN);
                }
            }
            conn.Close();
        }
    
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel2.Visible = false;
        string check = "SELECT subjCode,subjVenSlot,subjVenDate";
        check +=" FROM tblsubj_venue";
        check += " WHERE subjCode = '"+subjectList.SelectedItem.ToString()+"'";
        check +=" AND subjVenSlot = '"+slots.SelectedItem.ToString()+"'";
        check += " AND subjVenDate = '"+slotDate.SelectedDate.ToShortDateString()+"'";
        OdbcDataAdapter adapt = new OdbcDataAdapter(check, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];
        if (myTable.Rows.Count > 0)
        {
            //ScriptManager.RegisterStartupScript(this,
            //  this.GetType(),
            // "script",
            // "confirm('The seatallocation already exist');", true);
            warning.Visible = true;
            warning.Text = "The seatallocation already exist";
        }
        else
        {
            numStudeUpdates.Visible = false;
            seatName.Visible = false;
            if (slotDate.SelectedDate < DateTime.Today)
            {
                //ScriptManager.RegisterStartupScript(this,
                // this.GetType(),
                //"script",
                //"confirm('Date selected is invalid todays date will be selected');", true);
                warning.Visible = true;
                warning.Text = "Date selected is invalid todays date will be selected";
                slotDate.SelectedDate = DateTime.Today;
            }
            else
            {
                labControler.Visible = true;
                nextToPc.Visible = true;
                Panel1.Visible = true;
            }
        }
    }
    protected void nextToPc_Click(object sender, EventArgs e)
    {
        bool checkDupi = false;
        string check = "SELECT subjCode,subjVenSlot,subjVenDate";
        check += " FROM tblsubj_venue";
        check += " WHERE subjCode = '" + subjectList.SelectedItem.ToString() + "'";
        check += " AND subjVenSlot = '" + slots.SelectedItem.ToString() + "'";
        check += " AND subjVenDate = '" + slotDate.SelectedDate.ToShortDateString() + "'";
        OdbcDataAdapter adapt = new OdbcDataAdapter(check, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];
        if (myTable.Rows.Count > 0)
        {
            //ScriptManager.RegisterStartupScript(this,
            //  this.GetType(),
            // "script",
            // "confirm('The seatallocation already exist');", true);
            warning.Visible = true;
            warning.Text = "The seatallocation already exist";
        }
        if (checkDupi == false)
        {
            createSeats();
            Panel2.Visible = true;
            numStudeUpdates.Visible = true;
            seatName.Visible = true;
        }
        else
        {
            Panel2.Visible = false;
        }
        
        var loaded = numStudeUpdates.Text.Split(' ');
        if (loaded.Length > 0)
        {
            string allocated = loaded[0];
            string tot = loaded[5];
            //Response.Write(allocated + "\t" + tot);

            if (allocated == tot)
            {
                saveToDB.Enabled = true;
            }
            else { saveToDB.Enabled = false; }
        }
    }
    public void createSeats()
    {
        pcNamesList.Clear();
        if (subjectList.SelectedIndex!=0)
        pcControlerBy4.Visible = true;
        int sum;
        ////// populate the list box 
        string getStud = "SELECT * from tblsubj_stud where subjCode='" + subjectList.SelectedItem.ToString() + "'";
        conn.Open();
        string code = courseList.SelectedItem.ToString();
        OdbcDataAdapter adapt = new OdbcDataAdapter(getStud, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];

        studNoList.Clear();
        foreach (DataRow data in myTable.Rows)
        {
            DataRow temp = data;
            studNoList.Add(temp["studNo"].ToString());
        }
        conn.Close();

        sum = studNoList.Count;
        
        int labChecked = 0;
        String labNames = "";
        int heightLa = 1205;
        seatName.Text = "Seat Allocation For " + subjectList.SelectedItem.ToString();

        for (int l = 0; l < labTot; l++)
        {
            if (labCheck[l].Checked == true)
            {
                labNames = labName[l].Text;
                labChecked++;
                string cap = LabCap[l].Text;
                int capInt = Convert.ToInt32(cap);
                //adding pc to pc controller
                int by4 = 4;
                int by3 = 0;
                int by6 = 0;
          for (int x = 1; x < capInt + 1; x++)
           {
                    string student;

                    if (studNoList.Count > 0)
                    {
                        student = studNoList[0].ToString();
                    }
                    else
                    {
                        break;
                        // or use 00000 to show no one is seated
                        student = "000000000";
                    }
                    //////////////////////////////////////
                    if (labStruct[l].Text == "4X4")
                    {
                        bool removePc = false;
                        if (x == 1)
                        {
                            Label xxx = new Label();
                            pcControlerBy4.Controls.Add(xxx);
                            xxx.Text = labNames;
                            xxx.Width = heightLa;

                            xxx.Height = 30;
                            xxx.Font.Bold = true;
                            xxx.Font.Size = 15;
                        }
                        if (by4 == 8)
                        {
                            Label lll = new Label();
                            pcControlerBy4.Controls.Add(lll);
                            by4 = 0;
                            lll.Width = 110;
                            heightLa = 995;
                        }
                        pcControlerBy4.Width = 990;
                        by4++;
                        String pcNameInuse = "";
                        pcName[x] = new CheckBox();
                        pcName[x].Width = 110;
                        // get pc sattus
                        if (x < 10)
                        {
                            pcName[x].Text = labNames + "PC0" + x.ToString();
                        }
                        else
                        {
                            pcName[x].Text = labNames + "PC" + x.ToString();
                        }
                        pcNameInuse = pcName[x].Text;
                        // pcNamesList.Add(pcName[x].Text.ToString());
                        getStud = "SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcBrocken";
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " PcBrocken";
                            }
                            pcName[x].Checked = true;
                            removePc = false;
                        }
                        else
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " " + student;
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " " + student;
                            }
                            //remove student number after adding it
                            removePc = true;
                        }
                        // check if pc is in use
                        getStud = "SELECT pcName,seatAllocDate,seatallocSlot FROM tblSeatallocation WHERE pcName='" + pcNameInuse
                         + "' AND seatAllocDate = '" + slotDate.SelectedDate.ToShortDateString() + "' AND seatallocSlot='" + slots.SelectedItem.ToString() + "'";
                        //"SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcIsInUse";
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " PcIsInUse";
                            }
                            pcName[x].Checked = true;
                            removePc = false;
                        }
                        //check the spacing
                        if (spacingType.SelectedIndex == 0)
                        {
                            if (x % 2 == 0)
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy4.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy4.Controls.Add(pcName[x]);
                            }
                            
                        }
                        if (spacingType.SelectedIndex == 1)
                        {
                            if (x % 2 == 0)
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy4.Controls.Add(pcName[x]);
                            }
                            else
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy4.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                        }
                        if (spacingType.SelectedIndex < 0)
                        {
                            pcNamesList.Add(labName[l].Text + pcName[x].Text);
                            pcName[x].TextAlign = TextAlign.Left;
                            pcControlerBy4.Controls.Add(pcName[x]);
                            if (removePc == true)
                            {
                                if (studNoList.Count > 0)
                                {
                                    studNoList.RemoveAt(0);
                                }
                            }
                        }

                        if (x == capInt)
                        {
                            Label lll = new Label();
                            lll.Width = 1205;
                            lll.Height = 15;
                            pcControlerBy4.Controls.Add(lll);
                        }
                    }
                    //////////////////////////////////////
                    if (labStruct[l].Text == "3X5X3")
                    {
                        bool removePc = false;
                        heightLa = 1285;
                        if (x == 1)
                        {
                            Label xxx = new Label();
                            pcControlerBy3.Controls.Add(xxx);
                            xxx.Text = labNames;
                            xxx.Width = heightLa;
                            xxx.Height = 30;
                            xxx.Font.Bold = true;
                            xxx.Font.Size = 15;
                        }
                        if (by3 == 3 || by3 == 8)
                        {
                            Label xxx = new Label();
                            pcControlerBy3.Controls.Add(xxx);
                            if (by3 == 8)
                            {
                                by3 = -3;
                            }
                            xxx.Width = 35;
                        }
                        by3++;
                        pcControlerBy3.Width = 1290;

                        pcName[x] = new CheckBox();
                        pcName[x].Width = 110;
                        string pcNameInuse = "";
                        //pcName[x].Text = labNames + "PC" + x.ToString();
                        if (x < 10)
                        {
                            pcName[x].Text = labNames + "PC0" + x.ToString();
                        }
                        else
                        {
                            pcName[x].Text = labNames + "PC" + x.ToString();
                        }
                        pcNameInuse = pcName[x].Text;
                        getStud = "SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcBrocken";
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " PcBrocken";
                            }
                            removePc = false;
                            pcName[x].Checked = true;
                        }
                        else
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " " + student;
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " " + student;
                            }
                            //remove student number after adding it
                            removePc = true;
                        }
                        // check if pc is in use
                        getStud = "SELECT pcName,seatAllocDate,seatallocSlot FROM tblSeatallocation WHERE pcName='" + pcNameInuse
                         + "' AND seatAllocDate = '" + slotDate.SelectedDate.ToShortDateString() + "' AND seatallocSlot='" + slots.SelectedItem.ToString() + "'";
                        //"SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcIsInUse";
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " PcIsInUse";
                            }
                            pcName[x].Checked = true;
                            removePc = false;
                        }
                        if (spacingType.SelectedIndex == 0)
                        {
                            if (x % 2 == 0)
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy3.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy3.Controls.Add(pcName[x]);
                            }

                        }
                        if (spacingType.SelectedIndex == 1)
                        {
                            if (x % 2 == 0)
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy3.Controls.Add(pcName[x]);
                            }
                            else
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy3.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                        }
                        if (spacingType.SelectedIndex < 0)
                        {
                            pcNamesList.Add(labName[l].Text + pcName[x].Text);
                            pcName[x].TextAlign = TextAlign.Left;
                            pcControlerBy3.Controls.Add(pcName[x]);
                            if (removePc == true)
                            {
                                if (studNoList.Count > 0)
                                {
                                    studNoList.RemoveAt(0);
                                }
                            }
                        }


                        if (x == capInt)
                        {
                            Label lll = new Label();
                            lll.Width = 1205;
                            lll.Height = 15;
                            pcControlerBy3.Controls.Add(lll);
                        }

                        Panel2.Visible = true;
                    }
                    /////////////////////////////////
                    if (labStruct[l].Text == "5X5")
                    {
                        bool removePc = false;

                        heightLa = 1260;
                        if (x == 1)
                        {
                            Label xxx = new Label();
                            pcControlerBy5.Controls.Add(xxx);
                            xxx.Text = labNames;

                            xxx.Width = heightLa;
                            xxx.Height = 30;
                            xxx.Font.Bold = true;
                            xxx.Font.Size = 15;
                        }
                        if (by6 == 5)
                        {
                            Label xxx = new Label();
                            pcControlerBy5.Controls.Add(xxx);
                            if (by6 == 5)
                            {
                                by6 = -5;
                            }
                            xxx.Width = 100;
                        }
                        by6++;
                        pcControlerBy5.Width = 1260;
                        string pcNameInuse;
                        pcName[x] = new CheckBox();
                        pcName[x].Width = 110;
                        if (x < 10)
                        {
                            pcName[x].Text = labNames+"PC0" + x.ToString();
                        }
                        else
                        {
                            pcName[x].Text = labNames + "PC" + x.ToString();
                        }
                        pcNameInuse = pcName[x].Text;
                        getStud = "SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcBrocken";
                            }
                            else
                            {
                                pcName[x].Text = "PC"  + x.ToString() + " PcBrocken";
                            }
                            removePc = false;
                            pcName[x].Checked = true;
                           
                        }
                        else
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " " + student;
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " " + student;
                            }
                            //remove student number after adding it
                            removePc = true;
                        }
                        // check if pc is in use
                        getStud = "SELECT pcName,seatAllocDate,seatallocSlot FROM tblSeatallocation WHERE pcName='" + pcNameInuse
                         + "' AND seatAllocDate = '" + slotDate.SelectedDate.ToShortDateString() + "' AND seatallocSlot='" + slots.SelectedItem.ToString() + "'";
                        //"SELECT pcName FROM tblPc where pcName='" + pcName[x].Text.ToString() + "' and pcstatus='0'";
                        adapt = new OdbcDataAdapter(getStud, conn);
                        dts = new DataSet();
                        adapt.Fill(dts);
                        myTable = dts.Tables[0];
                        if (myTable.Rows.Count > 0)
                        {
                            if (x < 10)
                            {
                                pcName[x].Text = "PC0" + x.ToString() + " PcIsInUse";
                            }
                            else
                            {
                                pcName[x].Text = "PC" + x.ToString() + " PcIsInUse";
                            }
                            pcName[x].Checked = true;
                            removePc = false;
                        }
                        //check the spacing
                        if (spacingType.SelectedIndex == 0)
                        {
                            if (x % 2 == 0)
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy5.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                            else
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy5.Controls.Add(pcName[x]);
                            }

                        }
                        if (spacingType.SelectedIndex == 1)
                        {
                            if (x % 2 == 0)
                            {
                                if (x < 10)
                                {
                                    pcName[x].Text = "PC0" + x.ToString() + " 000000000";
                                }
                                else
                                {
                                    pcName[x].Text = "PC" + x.ToString() + " 000000000";
                                }
                                pcName[x].Checked = true;
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy5.Controls.Add(pcName[x]);
                            }
                            else
                            {
                                pcNamesList.Add(labName[l].Text + pcName[x].Text);
                                pcName[x].TextAlign = TextAlign.Left;
                                pcControlerBy5.Controls.Add(pcName[x]);
                                if (removePc == true)
                                {
                                    if (studNoList.Count > 0)
                                    {
                                        studNoList.RemoveAt(0);
                                    }
                                }
                            }
                        }
                        if (spacingType.SelectedIndex < 0)
                        {
                            pcNamesList.Add(labName[l].Text + pcName[x].Text);
                            pcName[x].TextAlign = TextAlign.Left;
                            pcControlerBy5.Controls.Add(pcName[x]);
                            if (removePc == true)
                            {
                                if (studNoList.Count > 0)
                                {
                                    studNoList.RemoveAt(0);
                                }
                            }
                        }

                        if (x == capInt)
                        {
                            Label lll = new Label();
                            lll.Width = 1255;
                            lll.Height = 15;
                            pcControlerBy5.Controls.Add(lll);
                        }
                        Panel2.Visible = true;
                    }
                    pcName[x].Enabled = false;
                }
            }
        }
        int used = 0;
        used = sum - studNoList.Count;
        numStudeUpdates.Text = used + " students allocated out of " + sum;
    }
    protected void exit_Click(object sender, EventArgs e)
    {
        Session["role"] = null;
        Response.Redirect("homepage.aspx");
    }
    protected void subjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        subjectList.AutoPostBack = true;
        conn.Open();
        string nuStud = "SELECT count(studNo) FROM tblsubj_stud where subjCode='" + subjectList.SelectedItem.ToString() + "';";
        OdbcDataAdapter adapt = new OdbcDataAdapter(nuStud, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];

        foreach (DataRow dat in myTable.Rows)
        {
            DataRow temp = dat;
            String labN = temp["count(studNo)"].ToString();
            numOfStud.Text = labN;
        }
        conn.Close();
        nextToLab.Visible = true;
    }
    protected void saveToDB_Click(object sender, EventArgs e)
    {
        createSeats();
        conn.Open();
        // insert to tbl tblsubj_venue 
        for (int l = 0; l < labTot; l++)
        {
            if (labCheck[l].Checked == true)
            {
                string labNames = labName[l].Text;
                string addStud = "INSERT INTO tblsubj_venue (subjCode,venCode,subjvenSlot,subjvenDate,empid) values('"
                  + subjectList.SelectedItem.ToString() + "','" + labNames + "','" + slots.SelectedItem.ToString() + "','"
                  + slotDate.SelectedDate.ToShortDateString() + "','00000')";
                OdbcCommand cmd = new OdbcCommand(addStud, conn);
                cmd.ExecuteNonQuery();
            }
        }

        for (int i = 0; i < pcNamesList.Count; i++)
        {
            string namPc = pcNamesList[i].Substring(0,9);
            string studNoo = pcNamesList[i].Substring(10);
            int outPut;

            //if (int.TryParse(studNoo, out outPut))
            //{
            //    warning.Text = " put numbers only";
            //    warning.Visible = true;
            //    warning.Enabled = true;
            //}

            //if (studNoo != "000000000" || studNoo != "PcBrocken" || studNoo!= "PcIsInUse")
                if (int.TryParse(studNoo, out outPut) && studNoo != "000000000")
                {
                string selec = "SELECT * FROM tblseatallocation WHERE studNo='" + studNoo + "' AND pcName='" + namPc + "' AND subjCode='"
                    + subjectList.SelectedItem.ToString() + "' AND seatAllocDate='" + slotDate.SelectedDate.ToShortDateString()
                    + "'AND seatAllocSlot='" + slots.SelectedItem.ToString() + "'";
                OdbcDataAdapter adapt = new OdbcDataAdapter(selec, conn);
                DataSet dts = new DataSet();
                adapt.Fill(dts);
                DataTable myTable = dts.Tables[0];
                if (myTable.Rows.Count > 0)
                {

                }
                else
                {
                    //insert into seatallocation
                    string addStud = "INSERT INTO tblseatallocation (studNo,pcName,subjCode,seatAllocDate,seatAllocSlot) values('"
                          + studNoo + "','" + namPc + "','" + subjectList.SelectedItem.ToString() + "','"
                          + slotDate.SelectedDate.ToShortDateString() + "','" + slots.SelectedItem.ToString() + "')";
                    OdbcCommand cmd = new OdbcCommand(addStud, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        conn.Close();
        saveToDB.Enabled = false;
        pcNamesList.Clear();
        Response.Redirect("complexLectu.aspx");
    }
    protected void slots_SelectedIndexChanged(object sender, EventArgs e)
    {
        labControler.Visible = false;
    }


    protected void slotDate_SelectionChanged(object sender, EventArgs e)
    {
        if (slotDate.SelectedDate < DateTime.Today)
        {
            warning.Visible = true;
            warning.Text = "Date selected is invalid todays date will be selected";
            slotDate.SelectedDate = DateTime.Today;
        }
        checkdDup();
    }
    public void checkdDup()
    {
        string check = "SELECT subjCode,subjVenSlot,subjVenDate";
        check += " FROM tblsubj_venue";               
        check += " WHERE subjCode = '" + subjectList.SelectedItem.ToString() + "'";
        check += " AND subjVenSlot = '" + slots.SelectedItem.ToString() + "'";
        check += " AND subjVenDate = '" + slotDate.SelectedDate.ToShortDateString() + "'";
        OdbcDataAdapter adapt = new OdbcDataAdapter(check, conn);
        DataSet dts = new DataSet();
        adapt.Fill(dts);
        DataTable myTable = dts.Tables[0];
        if (myTable.Rows.Count > 0)
        {
            //ScriptManager.RegisterStartupScript(this,
            //  this.GetType(),
            // "script",
            // "confirm('The seatallocation already exist');", true);
            warning.Visible = true;
            warning.Text = "The seatallocation already exist";
            nextToPc.Visible = false;
            saveToDB.Enabled = false;
            pcControlerBy3.Visible = false;
            pcControlerBy4.Visible = false;
            pcControlerBy5.Visible = false;
            numStudeUpdates.Visible = false;
            Panel2.Visible = false;
            Panel1.Visible = false;
            labControler.Visible = false;
        }
    }
    protected void spacingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //createSeats();
        Panel2.Visible = false;
        seatName.Visible = false;
    }
}