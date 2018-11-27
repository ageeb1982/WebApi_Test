using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WebApi_Test.Controllers;

namespace WebApi_Test.Models
{
    public class CustMetaData
    {
        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cust> Custs1 { get; set; }
        [XmlIgnore]
        public virtual Cust Cust1 { get; set; }
    }
    /// <summary>
    /// IxmlSerializable 
    /// حتى لا يصدر خطأ إذا كان مربوط مع قاعدة بيانات
    /// </summary>
    //[MetadataType(typeof(CustMetaData))]
    public partial class Cust : IXmlSerializable
    {
        //إذا مربوط مع جدول
        public XmlSchema GetSchema()
        {
            return null;
        }


        /// <summary>
        /// في حالة القراءة من 
        /// xml
        /// 
        /// </summary>
        /// <param name="reader">الملف المقروء</param>
        public void ReadXml(XmlReader reader)
        {




            Id = Convert.ToInt64(GetData(reader,nameof(Id)));
            Name =   GetData(reader,nameof(Name));
            Contry = GetData(reader, nameof(Contry));
            Date1 = Convert.ToDateTime(GetData(reader, nameof(Date1)));
            Job = GetData(reader, nameof(Job));
            Tel = GetData(reader, nameof(Tel));

        }

        public void WriteXml(XmlWriter writer)
        {


            writer.WriteAttributeString(nameof(Id), Convert.ToString(Id));
            writer.WriteAttributeString(nameof(Name), Convert.ToString(Name));
            writer.WriteAttributeString(nameof(Contry), Convert.ToString(Contry));
            writer.WriteAttributeString(nameof(Date1), Convert.ToString(Date1));
            writer.WriteAttributeString(nameof(Job), Convert.ToString(Job));
            writer.WriteAttributeString(nameof(Tel), Convert.ToString(Tel));



        }

        public string GetData(XmlReader data, string locName)
        {
            try
            {
                var dd = data.GetAttribute(locName);
               // var D = data.ReadElementContentAsString(locName, "");

                if(!data.IsEmptyElement)
                {
                   // var ddd=data.ReadElementContentAsString();
                }
                return dd;
            }
            catch (Exception e)
            {
                if (locName == "Id") { return "0"; }
                var err = e.Message;
            }

            return "";
        }


        public static implicit operator Cust(CustResult cust)
        {

            Cust MyCust = new Cust()
            {
                Id = cust.Id,
                Name = cust.Name,
                Contry = cust.Contry,
                Date1 = cust.Date,
                Job = cust.Job,
                Tel = cust.Tel
            };


            return MyCust;
        }

    }
}