using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BDServicioWebAPI.Models;

namespace BDServicioWebAPI.Controllers
{
    [Authorize]
    public class FacturasController : ApiController
    {
        private BDServicioEntities1 db = new BDServicioEntities1();

        // GET: api/Facturas
        public IQueryable<Factura> GetFactura()
        {
            return db.Factura;
        }

        // GET: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public IHttpActionResult GetFactura(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFactura(int id, Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.IdFactura)
            {
                return BadRequest();
            }

            // TODO: Figure out why this thing is not modifying the nested Client
            //db.Cliente.Attach(factura.Cliente);
            //db.Entry(factura.Cliente).State = EntityState.Modified;

            // db.Entry(factura).State = EntityState.Detached;
            // var facturaOriginal = db.Factura.First(f => f.IdFactura == id);

            //facturaOriginal.Cliente.IdCliente = factura.Cliente.IdCliente;
            //facturaOriginal.Cliente.Nombre = factura.Cliente.Nombre;
            //facturaOriginal.Cliente.Apellido = factura.Cliente.Apellido;
            //facturaOriginal.Cliente.Telefono = factura.Cliente.Telefono;
            //facturaOriginal.Cliente.Tipo = factura.Cliente.Tipo;
            //facturaOriginal.Cliente.Estado = factura.Cliente.Estado;

            //facturaOriginal.Numero = factura.Numero;
            //facturaOriginal.Fecha = factura.Fecha;
            //facturaOriginal.IdZonaCliente = factura.IdZonaCliente;
            //facturaOriginal.Total = factura.Total;

            //db.Entry(facturaOriginal.Cliente).State = EntityState.Modified;
            //db.Entry(facturaOriginal).State = EntityState.Modified;

            //Console.Write(db.ChangeTracker.Entries());
            db.Entry(factura).State = EntityState.Modified;
            //db.Entry(factura.Cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        [ResponseType(typeof(Factura))]
        public IHttpActionResult PostFactura(Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factura.Add(factura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = factura.IdFactura }, factura);
        }

        // DELETE: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public IHttpActionResult DeleteFactura(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.Factura.Remove(factura);
            db.SaveChanges();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacturaExists(int id)
        {
            return db.Factura.Count(e => e.IdFactura == id) > 0;
        }
    }
}