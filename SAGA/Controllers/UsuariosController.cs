using SAGA.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace SAGA.Controllers
{
    public class UsuariosController : Controller
    {
        private sagaEntities db = new sagaEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.NivelSuscripcion).Include(u => u.Provincia);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdNivelSuscripcion = new SelectList(db.NivelSuscripcion, "Id", "Nombre");
            ViewBag.IdProvincia = new SelectList(db.Provincia, "Id", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApellidoyNombre,DNI,FechaNacimiento,Sexo,Direccion,IdProvincia,Pais,Email,NombreUsuario,Pass,IdFide,IdLichess,UrlFoto,EMV,IdNivelSuscripcion")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.EMV = false;
                usuario.IdNivelSuscripcion = 1;

                string hashedPassword = HashPassword(usuario.Pass);
                usuario.Pass = hashedPassword;

                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNivelSuscripcion = new SelectList(db.NivelSuscripcion, "Id", "Nombre", usuario.IdNivelSuscripcion);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "Id", "Nombre", usuario.IdProvincia);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNivelSuscripcion = new SelectList(db.NivelSuscripcion, "Id", "Nombre", usuario.IdNivelSuscripcion);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "Id", "Nombre", usuario.IdProvincia);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApellidoyNombre,DNI,FechaNacimiento,Sexo,Direccion,IdProvincia,Pais,Email,NombreUsuario,Pass,IdFide,IdLichess,UrlFoto,EMV,IdNivelSuscripcion")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNivelSuscripcion = new SelectList(db.NivelSuscripcion, "Id", "Nombre", usuario.IdNivelSuscripcion);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "Id", "Nombre", usuario.IdProvincia);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //metodos adicionales
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
