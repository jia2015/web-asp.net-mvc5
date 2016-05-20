using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restReview.Controllers
{
    public class CuisineController : Controller
    {
        //[Authorize]
        // GET: Cuisine
        public ActionResult Search(string name="mrli")
        {
            var message = Server.HtmlEncode(name);
            return Content(message);
        }
    }
}