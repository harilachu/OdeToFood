using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }


        public EditModel(IRestaurantData restaurantData,
                        IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            this.Cuisines = this.htmlHelper.GetEnumSelectList<CuisingType>();
            if (restaurantId.HasValue)
            {
                this.Restaurant = this.restaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                this.Restaurant = new Restaurant();
            }
            
            if(this.Restaurant == null)
            {
                this.RedirectToPage("./NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                this.Cuisines = this.htmlHelper.GetEnumSelectList<CuisingType>();
                return Page();
            }

            if (this.Restaurant.Id == 0)
            {
                this.restaurantData.AddNew(this.Restaurant);
            }
            else
            {
                this.restaurantData.Update(this.Restaurant);
            }

            this.restaurantData.Commit();

            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            //return RedirectToPage("./List");
        }
    }
}