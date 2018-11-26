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
using System.Xml.Serialization;
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
            return db.Custs.ToList();
        }

        // GET: api/Custs1/5
        [ResponseType(typeof(Cust))]
        public async Task<IHttpActionResult> GetCust(long id)
        {
            Cust cust = await db.Custs.FindAsync(id);

            CustResult custW = cust;
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

        // POST: api/Custs1
        [ResponseType(typeof(Cust))]
        public async Task<IHttpActionResult> PostCust(Cust cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Custs.Add(cust);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cust.Id }, cust);
        }

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