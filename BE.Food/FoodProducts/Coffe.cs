using System.Collections.Generic;
using FinalTest.BackEnd.Enum;
using static FinalTest.BackEnd.Product.FoodProduct;

namespace FinalTest.BackEnd.Product
{
    class Coffe : FoodProduct
    {
        internal override string[] DevivedProducts {get { return new string[] { "Caffe normale", "Caffe Macchiato", "Cappuccino", "Latte Macchiato", "Marrochino" }; }  }
        public Coffe(decimal Price, string Name) : base(Price, Name)
        {
            MealOfferType.Add(MealType.BREAKFAST);
            preperationTime = 10000; 
        }
        
        public Coffe()
        {
            MealOfferType.Add(MealType.BREAKFAST);
            preperationTime = 10000;

        }
    }
}
