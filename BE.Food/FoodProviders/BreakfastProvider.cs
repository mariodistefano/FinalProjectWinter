using BE.Food.Contracts.Internal;
using FinalTest.BackEnd.DesignPattern;
using System;

namespace FinalTest.BackEnd.ProductProvider
{
    class BreakfastProvider : FoodProvider, IBreakfast
    {  
        public BreakfastProvider(string Name) :base(Name) 
        {
            opening = new TimeSpan(07);
            closing = new TimeSpan(12);
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.BREAKFAST);
        }
        public BreakfastProvider()
        {
            opening = new TimeSpan(07);
            closing = new TimeSpan(12);
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.BREAKFAST);
        }
    } 

}
    