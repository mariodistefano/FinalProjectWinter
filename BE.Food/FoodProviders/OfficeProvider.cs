using BE.Food.Contracts.Internal;
using FinalTest.BackEnd.DesignPattern;
using System;

namespace FinalTest.BackEnd.ProductProvider
{
    class PartTimeProvider : FoodProvider, IBreakfast, ILunch, ISnack
    {
        public PartTimeProvider(string Name) : base(Name)
        {
            opening = new TimeSpan(07); //  07:00 AM
            closing = new TimeSpan(14); //  14:00 PM
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.BREAKFAST);
        }
        public PartTimeProvider()
        {
            opening = new TimeSpan(07);
            closing = new TimeSpan(14);
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.BREAKFAST);
        }
    }
}
