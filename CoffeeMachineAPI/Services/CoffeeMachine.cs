using CoffeeMachineAPI.Services.IServices;

namespace CoffeeMachineAPI.Services
{
    public class CoffeeMachine : ICoffeeMachine
    {
        public int Stock { get; set; } = 4;

        public bool IsBrewSuccess()
        {
            // Check if the stock is empty
            if (Stock == 0)
            {
                // If the stock is empty, restock and return false
                Restock();
                return false;
            }
            // If the stock is not empty, decrement the stock by 1 and return true
            Stock -= 1;
            return true;
        }
        public void Restock()
        {
            // Set the stock level back to 4
            Stock = 4;
        }
    }
}
