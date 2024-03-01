using CoffeeMachineAPI.Services;

namespace CoffeeMachineAPI.Test
{
    public class CoffeeMachineServiceTest
    {
        private readonly CoffeeMachine _machine;
        public CoffeeMachineServiceTest()
        {
            // Arrange
            _machine = new CoffeeMachine();
        }
        [Fact]
        public void New_coffee_machine_stock_should_return_four()
        {
            // Act
            int stock = _machine.Stock;
            // Assert
            Assert.Equal(4, stock);
        }
        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        [InlineData(4, 0)]
        public void Successful_brew_coffee_if_brew_less_than_five_times_return_correct_remain_stock(int brewTime, int remainStock)
        {
            // Act
            for (int i = 0; i < brewTime; i++)
            {
                bool isBrewSuccess = _machine.IsBrewSuccess();
                Assert.True(isBrewSuccess);
            }
            // Assert
            Assert.Equal(remainStock, _machine.Stock);
        }
        [Fact]
        public void Restock_after_brew_coffee_five_times()
        {
            // Arrange
            bool isBrewSuccess = true;
            // Act
            for (int i = 0; i < 5; i++)
            {
                isBrewSuccess = _machine.IsBrewSuccess();
            }
            // Assert
            Assert.False(isBrewSuccess);
            Assert.Equal(4, _machine.Stock);
        }
    }
}