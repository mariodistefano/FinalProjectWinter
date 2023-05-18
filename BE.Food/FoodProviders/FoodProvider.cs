using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using FinalTest.BackEnd.DesignPattern;
using FinalTest.BackEnd.Product;
using Lesson.DesingPaterns.Observer;
using BE.Food.Contracts.External;
using System.Threading;
using BE.Food.Commom;
using BE.Food.Models;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FinalTest.BackEnd.ProductProvider
{
    public class FoodProvider : BaseEntity, IFoodProductProvider
    {   
        
        string name;
        internal int ProviderId;
        bool KitchenIsWorking;
        public string Name { get { return name; } }
        internal Queue<Order> Orders = new();
        internal List<FoodProduct> Menu;
        internal int WaitingTime { get { return Orders.ToList().Sum(i => i.foodItems.Sum(i => i.PreperationTime)); }  }
        protected static int FreeCookingPlate = -1;
        protected double distance;  // in KM
        protected Kitchen cookingPlate;
        protected TimeSpan opening;
        protected TimeSpan closing;
        protected static TimeSpan Now = DateTime.Now.TimeOfDay;
        protected static int TotOrderInQueue = 0;
        protected List<Order> Bag; 
        internal int TempoDiAttesa 
        { 
                  get 
                  {
                    Thread.Sleep(random.Next(1000,10000));
                    return TotOrderInQueue;
                  } 
        }
        internal FoodProvider(string Name)
        {
            name = Name;            
            Orders = new();            
            distance = random.NextDouble();
            cookingPlate = new();
            //TakeOrdersToCook();
            Bag = new(); 
        }
        internal FoodProvider()
        {
            distance = random.NextDouble();
        }
        void PrepareOrder(Order order)
        {
        }        
        void DeliverOrder(Order order) { }
        async Task<Order> CreateOrder(Basket basket)
        {
            Order newOrder = new();
            try
            {
                foreach (var item in basket.foodProductOrder)
                {
                    FoodProductOrder foodProduct = this.Menu.Where(i => i.FoodCode == item.FoodCode).FirstOrDefault();
                    foodProduct.Order = newOrder;
                    newOrder.foodItems.Add(foodProduct);
                }
               
                //await Task.Run(() =>
                //{ 
                   
                //    this.Orders.Enqueue(newOrder);

                //    if (!KitchenIsWorking)
                //        TakeOrdersToCook();
                //});               
                    this.Orders.Dequeue();

                this.Orders.Enqueue(newOrder);

                    if (!KitchenIsWorking)
                        TakeOrdersToCook();
            

                var bag =  await  CompleteOrders(newOrder);
              return bag;
            }
            catch (Exception ex)
            {  
                 return null;   
            }
            
        }
        public void CheckMenu(Basket basket)
        {
            throw new NotImplementedException();
        }       
        public bool CheckIsOpened()
        {

            if (Now > opening && Now < closing)
                return true;
            else
                return false;
        }
        public double GetTime()
        {
            Thread.Sleep(random.Next(1000, 10000));
            return new TimeSpan(random.Next(60, 3600)).TotalMinutes;
        }
        public async Task<Order> PlaceOrder(Basket basket)
        {
           return await CreateOrder(basket);           
        }
        List<FoodProductRequest> IFoodObserver.GetMenu()
        { 
            return  this.Menu.Cast<FoodProductRequest>().ToList();
        } 
       internal async Task TakeOrdersToCook()
        {
            KitchenIsWorking = true; 
            do
            {
                Order nextOrder;
                if(Orders.TryPeek(out nextOrder))
                {
                    do
                    {
                        if (Kitchen.FreePlaces > 0)
                        {
                            cookingPlate.AddFoodToPlate(nextOrder.foodItems.Where(f => f.inPreparation == false).FirstOrDefault());
                        }
                    }
                    while (nextOrder.foodItems.Where(f => f.inPreparation == false).Any());
                    nextOrder.InPreparation = true;
                }
            } while (Orders.Where(f => f.InPreparation == false).Any());
            KitchenIsWorking = false;

        }
       internal async Task<Order> CompleteOrders(Order OrderToComplete)
        {

            cookingPlate.Cook(OrderToComplete);

            while (OrderToComplete.foodItems.Where(f => f.isReady == false).Any())
            {
               // Finchè c'è qualsosa in piastra
            }
            OrderToComplete.isReady = true;
            Orders.TryDequeue(out OrderToComplete);
            return OrderToComplete;
              
        }
    }

}
