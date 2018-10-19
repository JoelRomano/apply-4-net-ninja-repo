using ninja.model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ninja.Controllers
{
    public class InvoiceDetailController : Controller
    {
        // GET: InvoiceDetail
        public ActionResult _InvoiceDetailPartialPage(Invoice item, int? index)
        {
            ViewBag.Index = index;
            return PartialView(item);
        }
    }
}