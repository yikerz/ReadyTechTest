using CoffeeMachineAPI.Services.IServices;

namespace CoffeeMachineAPI.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
