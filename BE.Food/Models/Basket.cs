using FinalTest.BackEnd.Product;
using System.Collections.Generic;
using static FinalTest.BackEnd.Product.FoodProduct;

namespace BE.Food.Models
{
    public class Basket
    {
        public List<FoodProductRequest> foodProductOrder;
        public Basket()
        {
            foodProductOrder = new();
        }
    }
}
