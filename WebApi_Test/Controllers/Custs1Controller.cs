using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;
using WebApi_Test.Models;

using System.Xml;
using System.Xml.Schema;

namespace WebApi_Test.Controllers
{
    public class CustResult:IXmlSerializable
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Tel { set; get; }
        
        public Nullable<DateTime> Date { set; get; }
        public string Job { get; set; }
        public string Contry { get; set; }

        public static implicit operator CustResult(Cust cust)
        {
            CustResult cuu = new CustResult()
            {
                Id = cust.Id,
                Name = cust.Name,
                Contry = cust.Contry,
                Date = cust.Date1,
                Job = cust.Job,
                Tel = cust.Tel

            };
            return cuu;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Id = Convert.ToInt64(reader.ReadElementContentAsString(nameof(Id), ""));
            Name = reader.ReadElementContentAsString(nameof(Name), "");
            Contry = reader.ReadElementContentAsString(nameof(Contry), "");
            Date = Convert.ToDateTime(reader.ReadElementContentAsString(nameof(Date), ""));
            Job = reader.ReadElementContentAsString(nameof(Job), "");
            Tel = reader.ReadElementContentAsString(nameof(Tel), "");

        }

        public void WriteXml(XmlWriter writer)
        {


            writer.WriteAttributeString(nameof(Id), Convert.ToString(Id));
            writer.WriteAttributeString(nameof(Name), Convert.ToString(Name));
            writer.WriteAttributeString(nameof(Contry), Convert.ToString(Contry));
            writer.WriteAttributeString(nameof(Date), Convert.ToString(Date));
            writer.WriteAttributeString(nameof(Job), Convert.ToString(Job));
            writer.WriteAttributeString(nameof(Tel), Convert.ToString(Tel));



        }
    }
    public class Custs1Controller : ApiController
    {
        private MyDBEntities1 db = new MyDBEntities1();

        // GET: api/Custs1
        public List<Cust> GetCusts()
        {
            var dd= db.Custs.Select(x => new CustResult { Id = x.Id, Name = x.Name, Tel = x.Tel, Date = x.Date1,Contry=x.Contry,Job=x.Job }).ToList();
            List<Cust> ww = new List<Cust>();
            ww=db.Custs.AsEnumerable().Select(x => (Cust)x).ToList();
            return ww;
        }

 //public List<CustResult> GetCusts()
 //       {
 //           var dd= db.Custs.Select(x => new CustResult { Id = x.Id, Name = x.Name, Tel = x.Tel, Date = x.Date1,Contry=x.Contry,Job=x.Job }).ToList();
 //           List<CustResult> ww = new List<CustResult>();
 //           ww=db.Custs.AsEnumerable().Select(x => (CustResult)x).ToList();
 //           return ww;
 //       }





        // GET: api/Custs1/5
        [ResponseType(typeof(CustResult))]
        public async Task<IHttpActionResult> GetCust(long id)
        {
            Cust cust = await db.Custs.FindAsync(id);

            //CustResult custW = cust;
            if (cust == null)
            {
                return NotFound();
            }

            return Ok(cust);
        }

        // PUT: api/Custs1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCust(long id, Cust cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cust.Id)
            {
                return BadRequest();
            }

            db.Entry(cust).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        public HttpResponseMessage PostCust(Cust cust)
        {
            HttpResponseMessage response = Request.CreateResponse<Cust>(cust);
            //  response.RequestMessage.Headers.Add(name: "state", value: "---------- ok");
            //return response;

            Cust cc = new Cust();

            if (cust!=null)
            {
                if(!db.Custs.Any(x=>x.Name.ToLower()==cust.Name.ToLower()))
                {
                    cc.Name = cust.Name;
                    cc.Contry = cust.Contry;
                    cc.Tel = cust.Tel;
                    
                    



                    db.Custs.Add(cc);
                    db.Entry(cc).State = EntityState.Added;
                    db.SaveChanges();
                    response.Headers.Location = new Uri("api/Cust1/" + cust.Id + "?type=json");
                    response.StatusCode = HttpStatusCode.Created;
                }
                else
                {
                    //response.StatusCode = HttpStatusCode.SeeOther;
                    response.RequestMessage.CreateErrorResponse(HttpStatusCode.SeeOther, "اسم المستخدم مكرر حاول مرة اخرى");
                }
            }
            else
            {
                response.RequestMessage.CreateErrorResponse(HttpStatusCode.NoContent, "لم تقم بإدخال بيانات");

            }

            return response;
        }



        //// POST: api/Custs1
        //[ResponseType(typeof(Cust))]
        //public async Task<IHttpActionResult> PostCust(Cust cust)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Custs.Add(cust);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = cust.Id }, cust);
        //}

        // DELETE: api/Custs1/5
        [ResponseType(typeof(Cust))]
        public async Task<IHttpActionResult> DeleteCust(long id)
        {
            Cust cust = await db.Custs.FindAsync(id);
            if (cust == null)
            {
                return NotFound();
            }

            db.Custs.Remove(cust);
            await db.SaveChangesAsync();

            return Ok(cust);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustExists(long id)
        {
            return db.Custs.Count(e => e.Id == id) > 0;
        }
    }
}