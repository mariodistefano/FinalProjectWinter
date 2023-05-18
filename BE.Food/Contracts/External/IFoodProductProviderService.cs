using BE.Food.Models;

namespace BE.Food.Contracts.External
{

    public interface IFoodProductProviderService
        {
            public void SendOrder(Basket basket);
            public void CheckMenu(Basket basket);
            public bool CheckIsOpened();

        }
    
}
