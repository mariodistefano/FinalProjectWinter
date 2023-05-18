using FinalTest.BackEnd.DesignPattern;
using FinalTest.BackEnd.ProductProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson.DesingPaterns.Observer
{
    public abstract class Subject<T> where T : IObserver
    {
        public string name;

        protected abstract List<T> observers { get; set; } 
        public void Attach(T observer)
        {
            observers.Add(observer);
        }
        public void Detach(T observer)
        {
            observers.Remove(observer);
        }        
        public Subject(string Name)
        {
            observers = new List<T>();

            name = Name;
        }
        public Subject()
        {
            observers = new List<T>();
        }
    }
    public abstract class FoodSubject: Subject<FoodProvider>
    {

        protected override List<FoodProvider> observers { get; set; }

        public void GetMenu()
        {
            foreach (IFoodObserver o in observers)
            {
                // - Quando viene richeisto il menu, Viene notificato a tutti gli Observers che fanno dei calcoli 
                //    in base al numero di ordini e distanza risponderà il provider piu appropriato.

                o.GetMenu();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ResetColor();
        }
        public FoodSubject(string Name) :base(Name)
        {
           
        }
        public FoodSubject()
        {            

        }
    }

}
