using FinalTest.BackEnd.Product;
using System.Collections.Generic;

namespace Lesson.DesingPaterns.Observer
{
    public interface IObserver
    {
       // public void Update();
    }
    public interface IFoodObserver : IObserver
    {
        public List<FoodProductRequest> GetMenu();       
    }

}
