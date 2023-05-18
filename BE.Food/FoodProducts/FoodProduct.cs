using System;
using System.Collections.Generic;
using System.Threading;
using BE.Food.Commom;
using BE.Food.Models;
using FinalTest.BackEnd.Enum;
using FinalTest.BackEnd.ProductProvider;

namespace FinalTest.BackEnd.Product
{
    public  class FoodProductRequest : BaseEntity
    {
        protected string name;
        protected decimal price;
        protected int foodCode;

       
        public int FoodCode { get { return foodCode; } }
        public string Name { get { return name; } }
        public decimal Price { get { return price; } }

        public FoodProductRequest(int FoodCode)
        {
            foodCode = FoodCode;  
        }
        internal FoodProductRequest()
        {
        }

    }
    public abstract class FoodProductOrder : FoodProductRequest
    {
        Order order;
        internal Order Order { get; set; }
        internal bool isReady;
        internal bool inPreparation;
        internal  int PreperationTime { get;  }

        internal FoodProductOrder(Order Order)
        {
            order = Order;
        }
        internal FoodProductOrder()
        {
          
        }
    }
    public abstract class FoodProduct : FoodProductOrder 
    {
        protected string description;
        protected int preperationTime;
       

        public FoodProduct(decimal Price, string Name) :base()
        {
            price = Price;
            name = Name;
            foodCode = random.Next(100,999)  ;
            MealOfferType = new List<MealType>();
        }
        public FoodProduct()
        {
            MealOfferType = new List<MealType>();
        }

        // internal  int PreperationTime { get { return preperationTime; } }
        internal abstract string[] DevivedProducts { get; }
        internal  List<MealType> MealOfferType { get; }

        
        public double GetTime()
        {
            //  Fake time of preparation 
            Thread.Sleep(random.Next(1000, 10000));
            return new TimeSpan(random.Next(60, 3600)).TotalMinutes;
        }
    }
}
