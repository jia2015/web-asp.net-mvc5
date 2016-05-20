﻿using restReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace restReview.Controllers
{
    public class HomeController : Controller
    {
        restReviewDbContext db = new restReviewDbContext();

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

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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