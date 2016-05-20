using restReview.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restReview.Controllers
{
    public class ReviewsController : Controller
    {
        private restReviewDbContext db = new restReviewDbContext();
        
        // GET: Reviews
        public ActionResult Index([Bind(Prefix = "id")] int? restaurantId)
        {
            var rest = db.Restaurants.Find(restaurantId);
            if (rest != null)
            {
                return View(rest);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create(int? restaurantId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RestaurantReview newReview)
        {           
            if (ModelState.IsValid)
            {
                db.Reviews.Add(newReview);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = newReview.RestaurantId });
            }
            return View(newReview);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Reviews.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude="ReviewerName")] RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
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
