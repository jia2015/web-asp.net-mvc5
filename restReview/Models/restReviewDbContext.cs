using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace restReview.Models
{
    public class restReviewDbContext: DbContext
    {
        public restReviewDbContext()
            : base("name=restaurantDB")
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantReview> Reviews { get; set; }
    }
}