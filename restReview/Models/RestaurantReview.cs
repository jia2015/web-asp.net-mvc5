using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace restReview.Models
{
    public class RestaurantReview
    {
        public int Id { get; set; }

        [Range(1,10), Required]
        public int Rating { get; set; }

        [Required, StringLength(50)]
        [Display(Name="Comment")]
        public string Body { get; set; }
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

        
    }
}