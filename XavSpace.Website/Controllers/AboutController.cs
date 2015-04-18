using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using XavSpace.Website.Filters;

namespace XavSpace.Website.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }

        // GET: About/Contact
        public ActionResult Contact()
        {
            return View();
        }

        [RestrictAccessTo(UserTypes="moderator")]
        public string Test()
        {
            return "This is a test";
        }

        public FileResult ConceptNote()
        {
            return File("~/Content/PDF/ConceptNote.pdf", "application/pdf", "Concept Note - XavSpace.pdf");
        }
    }
}