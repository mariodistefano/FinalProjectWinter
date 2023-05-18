using BE.Food.Models;
using FinalTest.BackEnd.FoodFactories;
using FinalTest.BackEnd.FoodServiceFactory;
using FinalTest.BackEnd.Product;
using FinalTest.BackEnd.ProductProvider;
using Lesson.DesingPaterns.Observer;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTest.BackEnd.DesignPattern
{

    public interface IAbstractFoodServiceFactory
    {
        public IFoodProviderFactory CreateProviderFactory(ref char input);
    }
    public interface IFoodProviderFactory 
    {
        public IFoodProductProvider CreateProductProvider(double Distance);
    }
    public interface IFoodProductProvider : IFoodObserver
    {      
        public bool CheckIsOpened();
        public Task<OrderResponse> PlaceOrder(Basket basket);
       
    }  
    
}
