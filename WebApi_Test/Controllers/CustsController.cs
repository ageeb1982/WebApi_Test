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
using WebApi_Test.Models;

namespace WebApi_Test.Controllers
{
    public static  class db
    {
        private static List<CustX> _cust { set; get; } = new List<CustX>();
        public static  List<CustX> Custs { set { _cust = value; } get { Chk(); return _cust; } }
        public static  void Chk()
        {
            
            if (_cust.Count() == 0)
            {
                _cust.Add(new CustX { Name = "Abdulla", Id = 1, Contry = "orden", Date1 = DateTime.Now.AddDays(15), Tel = "054000", Job = "programer" });
                _cust.Add(new CustX { Name = "Abdualrahman", Id = 2, Contry = "tones", Date1 = DateTime.Now.AddDays(10), Tel = "0535588", Job = "Mobile Devoloper" });
                _cust.Add(new CustX { Name = "alhareth", Id = 3, Contry = "jazaier", Date1 = DateTime.Now.AddDays(0), Tel = "0545454", Job = "Tester" });
                _cust.Add(new CustX { Name = "Mohamed", Id = 4, Contry = "Yeman", Date1 = DateTime.Now.AddDays(20), Tel = "3355", Job = "Web Designer" });
                _cust.Add(new CustX { Name = "Ahmed", Id = 5, Contry = "Moroco", Date1 = DateTime.Now.AddDays(30), Tel = "88541", Job = "Dovloper" });
                _cust.Add(new CustX { Name = "Saleh", Id = 6, Contry = "Kwiet", Date1 = DateTime.Now.AddDays(36), Tel = "7742", Job = "Driver" });
                _cust.Add(new CustX { Name = "Omer", Id = 7, Contry = "UAE", Date1 = DateTime.Now.AddDays(1), Tel = "054222", Job = "Techer" });
                _cust.Add(new CustX { Name = "Osman", Id = 8, Contry = "KSA", Date1 = DateTime.Now.AddDays(12), Tel = "555222", Job = "Since" });
                _cust.Add(new CustX { Name = "Ali", Id = 10, Contry = "Egypt", Date1 = DateTime.Now.AddDays(3), Tel = "778787", Job = "Doctor" });
                _cust.Add(new CustX { Name = "abubBaker", Id = 11, Contry = "Sudan", Date1 = DateTime.Now.AddDays(7), Tel = "55544", Job = "IT" });
            }
        }
    }
    public class CustsController : ApiController
    {
        // private MyDBEntities db = new MyDBEntities();
         
        // GET: api/Custs

        public List<CustX> GetCusts()
        {
            var d = db.Custs.ToList();
            return d;
        }

        // GET: api/Custs/5
        [ResponseType(typeof(CustX))]
        public IHttpActionResult GetCust(long id)
        {
            CustX cust =  db.Custs.FirstOrDefault(x=>x.Id==id);
            if (cust == null)
            {
                return NotFound();
            }

            return Ok(cust);
        }

        // PUT: api/Custs/5
        [ResponseType(typeof(void))]
        public  IHttpActionResult PutCust(long id, CustX cust)
        {
            CustX Cust_curr = db.Custs.FirstOrDefault(x => x.Id == id);
            if (Cust_curr == null) { return NotFound(); }
            Cust_curr = cust;

             

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Custs
        [ResponseType(typeof(CustX))]
        public IHttpActionResult PostCust(CustX cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Custs.Add(cust);
            
            return CreatedAtRoute("DefaultApi", new { id = cust.Id }, cust);
        }

        // DELETE: api/Custs/5
        [ResponseType(typeof(CustX))]
        public  IHttpActionResult DeleteCust(long id)
        {
            CustX cust =  db.Custs.FirstOrDefault(e=>e.Id==id);
            if (cust == null)
            {
                return NotFound();
            }

            db.Custs.Remove(cust);
            

            return Ok(cust);
        }

        protected override void Dispose(bool disposing)
        {
             
            base.Dispose(disposing);
        }

        private bool CustExists(long id)
        {
            return db.Custs.Count(e => e.Id == id) > 0;
        }
    }
}