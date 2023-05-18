
using BE.Food.Models;
using FinalTest.BackEnd.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Middleware
{
    public class FoodUI
    {
        FoodServices foodServices;
        IReadOnlyList<FoodProductRequest> menu;
       
        double distance = 2.0;  // 2 Km
        public FoodUI(FoodServices FoodServices, double Distance)
        {
            foodServices = FoodServices;
        }
        public void ShowStartOptions(ref char input)
        {
            do
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine(" HOW IT WORKS: ");
                Console.WriteLine("   Choose Type of meal");
                Console.WriteLine("   It returns to you providers nearby");
                Console.WriteLine("   Return menu from the best Product provider ");
                Console.WriteLine("   Choose Items on menu");
                Console.WriteLine("   Add Items to Bastet");
                Console.WriteLine("   Show Items on the Basket");
                Console.WriteLine("   Send Order");
                Console.WriteLine();
                Console.WriteLine("   Q. for quit | Y to proceed ");
                input = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

            } while (input == 'Q' && input == 'Y');

            if (input == 'Q')
                return;
            else
                foodServices.Start(ref input);
        }
        public void ShowMenuTypes(ref char input)
        {
            int inputNumber;

            do
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("Choose Type of meal:");
                Console.WriteLine("     1.BreakFast:");
                Console.WriteLine("     2.Lunch:");
               // Console.WriteLine("     3.Snack:");
                Console.WriteLine("     3.Dinner:");
                Console.WriteLine();
                Console.WriteLine("   Q. for quit ");
                input = char.ToUpper(Console.ReadKey().KeyChar);
                if (input == 'Q')
                {
                    return;
                }
                inputNumber = CharUnicodeInfo.GetDecimalDigitValue(input);

            } while (inputNumber != 1 && inputNumber != 2 && inputNumber != 3);

            
            foodServices.CreateProviderFactory(ref input);
        }
        public void ShowMenu(ref char input)// Show Menu with all items 
        { 

            string MenuInput = null;
            Console.Clear();
            getAllThresds();

            menu = foodServices.GetMenu();
           
            do
            {
                Console.Clear();

                do
                {

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Menu from {foodServices.FoodProvider.Name}:".ToUpper());
                    Console.ResetColor();

                    Console.WriteLine("");                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"SELECT ITEM FROM MENU ");
                    Console.ResetColor();
                    Console.WriteLine("-------------------------------");

                    foreach (var item in menu)
                    {

                        Console.Write($"{item.FoodCode}");
                        Console.Write(" | ");
                        Console.Write(item.Name);
                        Console.Write(" | ");
                        Console.Write($"$.{item.Price}");
                        Console.WriteLine("");

                    }
                Console.WriteLine("-------------------------------");

                ShowTotPrice();

                Console.WriteLine("Type item number an press Enter");
                Console.WriteLine("Q. to quit | B. basket | S. Send order ");
                Console.Write("INSERT CHOICE:  ");

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                    MenuInput = Console.ReadLine();
                 Console.ResetColor();

                    if (MenuInput.Length == 1)
                    {
                        input = char.Parse(MenuInput.ToUpper());
                        return;
                    }

                 Console.WriteLine("-------------------------------");

                } while (AddToBasket(MenuInput));
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Q. to quit | B. basket | S. Send order ");

            } while (true);
            Console.ResetColor();

        }
        public void ShowAmountBasket()
        {
            decimal tot = foodServices.Basket.foodProductOrder.Sum(i => i.Price);

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Totale ordini nel carrello ", tot);
            Console.ResetColor();
        }
        public void ShowBasket(ref char input)
        {
            Console.Clear();

            var items = foodServices.Basket.foodProductOrder
           .GroupBy(e => e.FoodCode)
           .ToDictionary(fp =>
           fp.Key,
           fp => fp.ToArray());

            string Values = string.Empty;
            decimal Price;
            string Name;
            int Amount;

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("MY BASKET\n\n");
            Console.ResetColor();
            Console.WriteLine("-------------------------------");

            foreach (int item in items.Keys)
            {

                 Price = items[item].Sum(i=> i.Price);
                 Amount = items[item].Count();
                 Name = items[item].FirstOrDefault().Name;

               // Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{Name.ToUpper()}");
                Console.ResetColor();
                //Console.WriteLine($"    CODE: {item} ");
                Console.WriteLine($"    PIECES: {Amount} ");
                Console.WriteLine($"    PRICE AMOUNT: $.{Price} ");
                Console.WriteLine("-------------------------------");
            }
            do
            {
               
                Console.WriteLine();
                Console.WriteLine();
                ShowTotPrice();

                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine(" R. Return to Menu ");
                input = char.ToUpper(Console.ReadKey().KeyChar);



            } while (input != 'R');
            return;

        }
        public bool AddToBasket(string menuItem)
        {            

            int inputNumber;
            int.TryParse(menuItem, out inputNumber);
            
            FoodProductRequest item = menu.Where(i => i.FoodCode == inputNumber).FirstOrDefault();
            if (item is not null)
            {
                foodServices.AddToBasket(item);
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Inserire un elemento valido.");
                Console.Beep();
                Thread.Sleep(2000); 
                Console.ResetColor();
                Console.Clear();
                return true;

            }
        }
        public void ShowOrders()
        {
        }
        public void Start(ref char input)
        {
            foodServices.Start(ref input);
            ShowMenuTypes(ref input);
            //  foodServices.CreateProviderFactory(ref input);

            if (input == 'Q')
                return;

            //  ShowMenuTypes(ref input);

            if (input == 'Q')
                return;

            foodServices.GetProvider(distance);

        }
        public async Task<char> SendOrder(Action<string,Order> FeedBack)
        {
            Console.Clear();

            await Task.Run(async () =>
            {                          
                Console.WriteLine("Sending Order... ");
                await Task.Delay(2000);

                 OrderResponse response = await foodServices.SendOrder();
                if (response.Order is not null)
                {
                  
                    Console.WriteLine("\n\n\n");
                    FeedBack("You Order is Here!! ", response.Order);
                  

                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("There's was an error with your Order. Please, Try again!");
                    Console.WriteLine(response.Error);
                    Console.WriteLine(response.Error);
                    Console.WriteLine();
                    Console.ResetColor();
                }
              
            });

            return 'Q'; 
        }
        public void ShowTotPrice()
        {
            decimal tot = foodServices.Basket.foodProductOrder.Sum(i => i.Price);

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"PRICE AMOUNT to PAY : $. {tot} ");
            Console.WriteLine();
            Console.ResetColor();


        } 
        public  void Notify(string msg, Order order)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(msg);
            Console.ResetColor();
            foreach (var item in order.foodItems)
            {
                Console.WriteLine($"    {item.Name}");
            }

           // getAllThresds();

            Thread.Sleep(2000); 
        }
        public static void getAllThresds()
        {
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("ThreadPool PendingWorkItemCount:");
            Console.Write(ThreadPool.PendingWorkItemCount);
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("ThreadPool ThreadCount :");
            Console.Write(ThreadPool.ThreadCount);
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("ThreadPool CompletedWorkItemCount  :");
            Console.Write(ThreadPool.CompletedWorkItemCount);
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");


        }


    }
}
