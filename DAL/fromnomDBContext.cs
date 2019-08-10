namespace WebSites1.Model
{
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;




/// <summary>
/// Summary description for fromnomDBContext
/// </summary>
/// 

    public class fromnomDBContext : DbContext
    {

        public virtual DbSet<student> students { get; set; }    //
                                                       // TODO: Add constructor logic here
                                                       //

    }

    public class student
    {
        public int empID { get; set; }
        public string jobCode { get; set; }
        public string empUsername { get; set; }
        public string empPassword { get; set; }
    }
}