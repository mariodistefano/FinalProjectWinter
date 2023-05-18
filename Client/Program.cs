using BE.Food.Models;
using Middleware;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {

        static double distance = 2.0; // 2 KM
        static char input;
        static Services foodServices;
        static FoodUI foodUI;
        static Action<string, Order> feedback ;
        static async Task Main(string[] args)
        {


                
           
            do
            {
                Console.Clear();
               // FoodUI.getAllThresds();


                do
                {

                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("SMART OFFICE OPERATIONS");
                    Console.WriteLine("SELECT : ");
                    Console.WriteLine("   1.Food");
                    Console.WriteLine("   2.Tranlation");
                    Console.WriteLine("   Q. for quit");

                    input = char.ToUpper(Console.ReadKey().KeyChar);                   


                } while (input != 'Q' && input != '1' && input != '2');

                if (input == 'Q')
                    return;

                int inputNumber = CharUnicodeInfo.GetDecimalDigitValue(input);
                Console.Clear();
               
                switch (inputNumber)
                { 

                    case 1:  
                        foodServices = new FoodServices();
                        foodUI = new((FoodServices)foodServices, distance);
                        feedback = foodUI.Notify; 
                        foodUI.Start(ref input);

                       
                        do
                            {

                            switch (input)
                            {

                                case 'B':
                                    foodUI.ShowBasket(ref input);//
                                    break;
                                case 'R':
                                    foodUI.ShowMenu(ref input);
                                    break;
                                case 'S':
                                    foodUI.SendOrder(feedback); //   procedere con il lavoro quindi non aspettare nulla. 
                                    input = 'E';
                                    //Thread.Sleep(2000);
                                    break;
                                case 'E':
                                    input = 'Q';
                                    break;
                                case 'Q':
                                    return;
                                    break;
                                default:
                                    foodUI.ShowMenu(ref input);
                                    break;
                            }
                            Console.ResetColor();

                        } while (input != 'Q');                       
                        break;
                    case 2:
                        break;

                }
                Console.ResetColor();


                if (input == 'E')
                    return;


            } while (input != 'E') ;

         } 
       


    } 
}

