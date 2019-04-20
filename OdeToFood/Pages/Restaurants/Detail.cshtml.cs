using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        [TempData]
        public string Message { get; set; }

        public Restaurant Restaurant { get; set; }

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            this.Restaurant = new Restaurant();
            this.Restaurant.Id = restaurantId;

            var restaurantRetrieved = this.restaurantData.GetRestaurantById(restaurantId);

            if(restaurantRetrieved==null)
            {
                return RedirectToPage("./NotFound");
            }

            this.Restaurant.Name = restaurantRetrieved.Name;
            this.Restaurant.Cuisine = restaurantRetrieved.Cuisine;
            this.Restaurant.Location = restaurantRetrieved.Location;
            return Page();
        }
    }
}