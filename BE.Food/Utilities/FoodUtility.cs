using BE.Food.Commom;
using BE.Food.Models;
using FinalTest.BackEnd.Enum;
using FinalTest.BackEnd.FoodFactories;
using FinalTest.BackEnd.Product;
using FinalTest.BackEnd.ProductProvider;
using Lesson.DesingPaterns.Observer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalTest.BackEnd.Utilities
{
    internal class FoodProviderUtility : BaseEntity
    {       
        internal async static void CreateFakeOrders(List<FoodProvider> FoodProviders)
        {  
            List<Task<bool>> tasks = new List<Task<bool>>();  
            foreach (var Provider in FoodProviders)
            {
                tasks.Add( Task.Run( () => CreateFakeOrder(Provider, Provider.Menu) )); 
            }
             await Task.WhenAll(tasks);
        }
        internal static List<FoodProduct> CreateMenu(MealType mealType)
        {
            return CreateRandomMenu(mealType);  
        }
        internal static List<FoodProduct> CreateRandomMenu(MealType mealType)
        {
            List<FoodProduct> itemTypes = new() { new Coffe(), new Burger(), new Dessert(), new Drink() };

            List<FoodProduct> foodItems = new(); 
           
            List<FoodProduct> Items = new(); 
           
                foreach (var item in itemTypes)
                {
                    if (item.MealOfferType.Contains(MealType.ALL))
                    {
                        Items.Add(item);
                    }
                    else if( (item.MealOfferType.Contains(mealType)))
                    {
                    Items.Add(item);
                     
                    }                
                }    

            for (int i = 0; i < Items.Count; i++)
            {
                int index = random.Next(0, itemTypes.Count - 1);
               
                decimal price = Convert.ToDecimal(random.Next(1, 10));
               // int FoodCode = random.Next(100, 999);
                Type type = itemTypes[index].GetType(); // Type Picker 
                string name = itemTypes[index].DevivedProducts[random.Next(0, itemTypes[index].DevivedProducts.Length - 1)];
                FoodProduct foodtype = (FoodProduct) Activator.CreateInstance(type,price,name); // new Instance from scratch               

                foodItems.Add(foodtype);
            }

            return foodItems;
        }
        internal static async Task<bool> CreateFakeOrder(FoodProvider foodProvider, List<FoodProduct> foodProductOrder)
        {
            Order newOrder = new ();
            try
            {
                foreach (var item in foodProductOrder)
                {
                    FoodProduct foodProduct = foodProvider.Menu.Where(i => i.FoodCode == item.FoodCode).FirstOrDefault();                   
                    foodProduct.Order = newOrder;
                    newOrder.foodItems.Add(foodProduct);                 
                }

                foodProvider.Orders.Enqueue(newOrder);
                await Task.Delay(3000);
                return true;
            }
            catch (Exception ex )
            {
                return false;

                throw;
            }
        }
        internal static List<T> CreateFoodProviders<T>(FoodProviderFactory foodProviderFactory, string[] args) where T : FoodProvider, new()
        {
            T t = new T();
            Type type = t.GetType(); 
            int random = new Random().Next(5);
            List<T> foodProviders = new();
          
            for (int i = 0; i < args.Length; i++)
            {
                var obj =  (T)  Activator.CreateInstance(type, args[i]);
                foodProviders.Add(obj); // 
            }
            foodProviders.Cast<T>().ToList();
            foodProviders.ForEach(fp => foodProviderFactory.Attach(fp)); //  Fill IObservers' list 
            return foodProviders; 
        }
    }  
    
}
