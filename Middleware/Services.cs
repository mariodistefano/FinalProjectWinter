using BE.Food.Contracts.External;
using BE.Food.Models;
using FinalTest.BackEnd.DesignPattern;
using FinalTest.BackEnd.Product;
using FinalTest.BackEnd.ProductProvider;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FinalTest.BackEnd.Product.FoodProduct;

namespace Middleware
{

    public class Services
    {
        protected Basket basket;
        public Basket Basket { get { return basket; } }
       
        public Services()
        {
            basket = new(); 
        }
    }
    public class FoodServices : Services
    {    

         static IAbstractFoodServiceFactory ServiceFactory;
         static IFoodProviderFactory ProviderFactory;
         static IFoodProductProvider ProductProvider;
         public FoodProvider FoodProvider { get { return (FoodProvider)ProductProvider; } }

        public FoodServices()
        {
                     
        }
        public void Start(ref char input)
        {  
            ServiceFactory = ClientServicesFactory.CreateServiceFactory(ref input);
           // ProviderFactory = ServiceFactory.CreateProviderFactory(ref input);
        }
        public void CreateProviderFactory(ref char input)
        {
            ProviderFactory = ServiceFactory.CreateProviderFactory(ref input);           
        }
        public void GetProvider(double distance)
        {
            ProductProvider = ProviderFactory.CreateProductProvider(distance);           
        }
        public List<FoodProductRequest> GetMenu()
        {
            return ProductProvider.GetMenu();  
        }
        public void AddToBasket(FoodProductRequest MenuItem)
        {           
            basket.foodProductOrder.Add(MenuItem); 
        }
        public void ShowBasket()
        { 
        }
        public async Task<OrderResponse> SendOrder()
        {
            await Task.Delay(1000);

            if (basket.foodProductOrder.Count == 0 )
            {  
                return null;    
            }           
            OrderResponse ResOrder =  await  ProductProvider.PlaceOrder(basket); 
            return ResOrder;
        } 
        
    }
}
