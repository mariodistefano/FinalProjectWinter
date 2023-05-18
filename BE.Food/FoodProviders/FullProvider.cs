using BE.Food.Contracts.Internal;
using FinalTest.BackEnd.DesignPattern;
using System;

namespace FinalTest.BackEnd.ProductProvider
{
    class FullTimeProvider : FoodProvider, IBreakfast, ILunch, ISnack, IDinner
    {
        public FullTimeProvider(string Name) : base(Name)
        {
            opening = new TimeSpan(07);
            closing = new TimeSpan(24);
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.DINNER);

        }
        public FullTimeProvider()
        {
            opening = new TimeSpan(07);
            closing = new TimeSpan(24);
            Menu = Utilities.FoodProviderUtility.CreateMenu(Enum.MealType.DINNER);
        }
    }
}
