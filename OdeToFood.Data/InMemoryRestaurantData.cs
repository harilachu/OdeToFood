using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 10,
                    Name = "Restaurant 1",
                    Location = "1, Address 1, pincode : 123456",
                    Cuisine = CuisingType.Indian
                },
                new Restaurant
                {
                    Id = 11,
                    Name = "Restaurant 2",
                    Location = "2, Address 2, pincode : 123456",
                    Cuisine = CuisingType.Mexican
                },
                new Restaurant
                {
                    Id = 12,
                    Name = "Restaurant 3",
                    Location = "3, Address 3, pincode : 123456",
                    Cuisine = CuisingType.Italian
                }
            };
        }

        public Restaurant AddNew(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            this.restaurants.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var deleteRestaurant = this.restaurants.SingleOrDefault(r => r.Id == id);
            if (deleteRestaurant != null)
            {
                this.restaurants.Remove(deleteRestaurant);
            }
            return deleteRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return this.restaurants.Count();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = "")
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = this.restaurants.SingleOrDefault(s => s.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }
    }
}
