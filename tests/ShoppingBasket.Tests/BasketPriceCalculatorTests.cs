using ShoppingBasket;
using Xunit;

namespace ShoppingBasket.Tests;

public class BasketPriceCalculatorTests
{
    [Fact]
    public void EmptyBasket_CostsNothing()
    {
        Assert.Equal(0, BasketPriceCalculator.CalculateTotal(Array.Empty<string>()));
    }

    [Theory]
    [InlineData("Apple", 35)]
    [InlineData("Banana", 20)]
    [InlineData("Melon", 50)]
    [InlineData("Lime", 15)]
    public void SingleItem_UsesCorrectPrice(string item, int expected)
    {
        Assert.Equal(expected, BasketPriceCalculator.CalculateTotal(new[] { item }));
    }

    [Fact]
    public void RepeatedApplesAndBananas_AreChargedForEveryItem()
    {
        string[] basket = { "Apple", "Apple", "Banana" };

        Assert.Equal(90, BasketPriceCalculator.CalculateTotal(basket));
    }

    [Theory]
    [InlineData(1, 50)]
    [InlineData(2, 50)]
    [InlineData(3, 100)]
    [InlineData(4, 100)]
    [InlineData(5, 150)]
    public void Melons_ApplyBuyOneGetOneFree(int melonCount, int expected)
    {
        string[] basket = Enumerable.Repeat("Melon", melonCount).ToArray();

        Assert.Equal(expected, BasketPriceCalculator.CalculateTotal(basket));
    }

    [Theory]
    [InlineData(1, 15)]
    [InlineData(2, 30)]
    [InlineData(3, 30)]
    [InlineData(4, 45)]
    [InlineData(5, 60)]
    [InlineData(6, 60)]
    [InlineData(7, 75)]
    public void Limes_ApplyThreeForThePriceOfTwo(int limeCount, int expected)
    {
        string[] basket = Enumerable.Repeat("Lime", limeCount).ToArray();

        Assert.Equal(expected, BasketPriceCalculator.CalculateTotal(basket));
    }

    [Fact]
    public void MixedBasket_AppliesEachOfferIndependently()
    {
        string[] basket = { "Apple", "Banana", "Melon", "Melon", "Lime", "Lime", "Lime" };

        Assert.Equal(135, BasketPriceCalculator.CalculateTotal(basket));
    }

    [Fact]
    public void ItemNames_AreCaseInsensitiveAndTrimmed()
    {
        string[] basket = { " apple ", "BANANA" };

        Assert.Equal(55, BasketPriceCalculator.CalculateTotal(basket));
    }

    [Fact]
    public void NullBasket_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => BasketPriceCalculator.CalculateTotal(null!));
    }

    [Fact]
    public void NullItem_ThrowsArgumentException()
    {
        string[] basket = { "Apple", null! };

        Assert.Throws<ArgumentException>(
            () => BasketPriceCalculator.CalculateTotal(basket));
    }

    [Theory]
    [InlineData("Orange")]
    [InlineData("")]
    [InlineData("   ")]
    public void UnsupportedItem_ThrowsArgumentException(string item)
    {
        Assert.Throws<ArgumentException>(
            () => BasketPriceCalculator.CalculateTotal(new[] { item }));
    }
}
