using FinalTest.BackEnd.DesignPattern;
using FinalTest.BackEnd.FoodServiceFactory;
using FinalTest.BackEnd.ProductProvider;
using Lesson.DesingPaterns.Observer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalTest.BackEnd.FoodFactories
{
    public class AbstractFoodProviderFactory :  IAbstractFoodServiceFactory
    {       
        public AbstractFoodProviderFactory()
        {
           
        }
        public IFoodProviderFactory CreateProviderFactory(ref char input)
        {
            var inputNumber = CharUnicodeInfo.GetDecimalDigitValue(input);

            switch (inputNumber)
            {
                case 1:
                    return new BreakFastFoodProviderFactory();

                case 2:
                    return new PartTimeFoodProviderFactory();

                case 3:
                    return new FullTimeFoodProviderFactory();

                default:
                    return null;
            }
        }        
    }
    public abstract class FoodProviderFactory: FoodSubject, IFoodProviderFactory
    { 
        public abstract IFoodProductProvider CreateProductProvider(double Distance);  

        public FoodProviderFactory()
        {
        }
    }
}
    