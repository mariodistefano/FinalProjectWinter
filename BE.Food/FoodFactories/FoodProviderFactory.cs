using BE.Food.Models;
using FinalTest.BackEnd.DesignPattern;
using FinalTest.BackEnd.FoodFactories;
using FinalTest.BackEnd.ProductProvider;
using FinalTest.BackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FinalTest.BackEnd.FoodServiceFactory
{    
    internal class BreakFastFoodProviderFactory : FoodProviderFactory
    {     
        public BreakFastFoodProviderFactory()
        {
          string[] args = { "McCafé ", "Starbucks", "Bar Centrale", "Bar Duomo", "Bar FS" };
          observers.Cast<BreakfastProvider>(); // Lista vuota  
          FoodProviderUtility.CreateFoodProviders<BreakfastProvider>(this, args); // Fill observers list 
          FoodProviderUtility.CreateFakeOrders(observers);
        }        
        public override IFoodProductProvider CreateProductProvider(double Distance)
        {           
            BreakfastProvider fp = (BreakfastProvider) observers.OrderByDescending(x => x.WaitingTime).First();
            return fp;          
        } 
    }
    internal class PartTimeFoodProviderFactory : FoodProviderFactory
    {  
        public PartTimeFoodProviderFactory()
        {
            string[] args = { "McDonalds", "BugerKing", "Pizzeria centrale", "Shushi Wu" };

            observers.Cast<PartTimeProvider>().ToList();
            FoodProviderUtility.CreateFoodProviders<PartTimeProvider>(this, args); // Fill observers list 
        }       
        public override IFoodProductProvider CreateProductProvider(double Distance)
        {
            PartTimeProvider fp = (PartTimeProvider)observers.OrderByDescending(x => x.WaitingTime).First();
            return fp;
        }
    }   
    internal class FullTimeFoodProviderFactory : FoodProviderFactory
    {      
        public FullTimeFoodProviderFactory() 
        {
            string[] args = { "McDonalds", "BugerKing", "Kebab Centrale", "Ristorante bella Napoli ", "Terrazza Aperol" };

            observers.Cast<FullTimeProvider>().ToList();
            FoodProviderUtility.CreateFoodProviders<FullTimeProvider>(this, args); // Fill observers list 

        }       
        public override IFoodProductProvider CreateProductProvider(double Distance)
        {
            FullTimeProvider fp = (FullTimeProvider)observers.OrderByDescending(x => x.WaitingTime).First();
            return fp;
        }
    }


}