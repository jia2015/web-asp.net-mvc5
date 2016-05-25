using restReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using restReview.Services;
using restReview.Data;

namespace restReview.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mail;
        private IMessageBoardRepository _repo;

        restReviewDbContext db = new restReviewDbContext();

        public HomeController(IMailService mail, IMessageBoardRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }
      
        public ActionResult Autocomplete(string term)
        {
            var model =
                db.Restaurants
                    .Where(r => r.Name.StartsWith(term))
                    .Take(10)
                    // ojbect with label property
                    .Select(r => new
                    {
                        label = r.Name
                    });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = db.Restaurants.OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    .Select(r => new RestaurantListViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        City = r.City,
                        Country = r.Country,
                        CountOfReviews = r.Reviews.Count()
                    }).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            //var model = from r in db.Restaurants
            //            orderby r.Reviews.Count() descending
            //            select new RestaurantListViewModel { 
            //                Id = r.Id,
            //                Name = r.Name,
            //                City = r.City,
            //                Country = r.Country,
            //                CountOfReviews = r.Reviews.Count()
            //            };
            return View(model);
        }

        public ActionResult About()
        {
            
            var model = new AboutModel();
            model.Name = "mr. li";
            model.Location = "new york city, NY";

            return View(model);
        }

        //[Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website: {3}{0}Comment:{4}",
                            Environment.NewLine,
                            model.Name,
                            model.Email,
                            model.Website,
                            model.Comment);

            if (_mail.SendMail("noreply@yourdomain.com",
                    "foo@yourdomain.com",
                    "Website Contact",
                    msg))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        public ActionResult MessageBoard()
        {
            var topics = _repo.GetTopics()
                        .OrderByDescending(t => t.Created)
                        .Take(25)
                        .ToList();
            return View(topics);
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}