using BE.Food.Commom;
using FinalTest.BackEnd.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BE.Food.Models
{
    public class Kitchen: BaseEntity
    {
        const int maxCookingPlaces = 5; // postazioni disponibili  
        static FoodProductOrder[] plates;
        List<Task> foodsToCook = new();
        public static int FreePlaces
        {
            get
            {
                return plates.Where(i => i == null).Count();
            }
            private set
            {
            }
        }
        public  void AddFoodToPlate(FoodProductOrder foodItem)
        {
            try
            {
                var elemet = Array.Find(plates, i => i == null);
                int index = Array.IndexOf(plates, elemet);
                plates[index] = foodItem;
                plates[index].inPreparation = true;
            }
            catch 
            {

                throw; 
            }
        }
        public  void RemoveFoodFromPlate(FoodProductOrder foodItem)
        {           
            int index = Array.IndexOf(plates,foodItem);
            plates[index] = null;
        }
        public Kitchen()
        {
           
            plates = new FoodProductOrder[maxCookingPlaces];
           
        }
       internal async Task Cook(Order order)
        {
            try
            {
                var item = plates
                    .Where(i => i.Order.OrderId == order.OrderId)
                    .FirstOrDefault();

                item.Order.foodItems.ForEach(i => Task.WaitAll(Task.Run( async () =>
                {
                    await Task.Delay(random.Next(15000, 20000));
                    i.isReady = true;
                    this.RemoveFoodFromPlate(i);
                })));
            }
            catch
            {

                throw ;

            }


        }
    }
}
