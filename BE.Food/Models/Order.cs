using BE.Food.Commom;
using FinalTest.BackEnd.Product;
using FinalTest.BackEnd.ProductProvider;
using System.Collections.Generic;
using static FinalTest.BackEnd.Product.FoodProduct;

namespace BE.Food.Models
{
    public class Order : BaseEntity
    {
        int orderId;
        public bool isReady;
        public int OrderId { get { return orderId; } }
        public readonly List<FoodProductOrder> foodItems;
        internal bool InPreparation;
        internal FoodProvider foodProvider;
        public FoodProvider FoodProvider { get { return foodProvider; } }

        public Order()
        {
            orderId = random.Next(100, 999);
            foodItems = new();
           // foodItems = basket.foodProductOrder;
        }
    }
}
