using Newtonsoft.Json;
using ninja.model.Entity;
using ninja.model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceManager manager;
        private InvoiceManager GetManager() { if (manager == null) manager = new InvoiceManager(); return manager; }
        
        // GET: Invoice
        public ActionResult Index()
        {
            return View(this.GetManager().GetAll());
        }

        public ActionResult Detail(long id) {
            Invoice item = this.GetManager().GetById(id);

            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoice obj) {

            if (obj.Type == null || !Enum.IsDefined(typeof(Invoice.Types), obj.Type)) {
                ModelState.AddModelError("", "Invoice Type is not correct!");
                return View();
            }
            
            try
            {
                this.GetManager().Insert(obj);
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Error creating Invoice! Please try again later.");
                return View();
            }

            return View("Index", this.GetManager().GetAll());

        }

        public ActionResult Edit(long id)
        {
            Invoice item = this.GetManager().GetById(id);

            return View(item);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice item)
        {
            this.GetManager().UpdateDetail(item.Id, item.GetDetail());

            return View("Index", this.GetManager().GetAll());
        }

        public ActionResult Delete(long id) {
            Invoice item = this.GetManager().GetById(id);

            return View(item);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Invoice obj)
        {
            this.GetManager().Delete(obj.Id);

            return View("Index", this.GetManager().GetAll());
        }
    }
}