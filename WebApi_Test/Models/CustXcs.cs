using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApi_Test.Models
{
    /// <summary>
    /// datacontract
    /// للتحكم في المخرجات ايش اللي تخرج وايش اللي ما تخرج
    /// 
    /// العناصر المراد اخراجها نضيف لها الخاصية
    /// DataMember
    /// </summary>
  
    [DataContract]
        public class CustX
    {
        [DataMember]
        public long Id { set; get; }
        [DataMember]
        public string  Name { set; get; }
        [DataMember]
        public string Tel { get; set; }
        [DataMember]
        public string Job { get; set; }
        [DataMember]
        public string Contry { get; set; }
        [DataMember]
        public Nullable<System.DateTime> Date1 { get; set; }
        public string Note { get; set; }
        public Nullable<long> User_Add { get; set; }
        public Nullable<System.DateTime> Date_Add { get; set; }
        public Nullable<long> User_Edit { get; set; }
        public Nullable<System.DateTime> Date_Edit { get; set; }
        public string Discriminator { get; set; }
        public Nullable<long> Cust_Srch_Id { get; set; }
        public string hijry { get; set; }
        public double Digit { get; set; }
    }
}