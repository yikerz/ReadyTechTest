using CoffeeMachineAPI.Models;

namespace CoffeeMachineAPI.Services.IServices
{
    public interface ICoffeeMachine
    {
        int Stock { get; set; }
        bool IsBrewSuccess();
        void Restock();
    }
}
