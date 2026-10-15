using ShoppingBasket;

Console.WriteLine("Shopping Basket");
Console.WriteLine("Enter one item at a time (Apple, Banana, Melon or Lime).");
Console.WriteLine("Press Enter on an empty line to calculate the total.");

var basket = new List<string>();

while (true)
{
    Console.Write("Item: ");
    string? item = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(item))
    {
        break;
    }

    basket.Add(item);
}

try
{
    decimal totalRupees = BasketPriceCalculator.CalculateTotal(basket);
    Console.WriteLine($"Total: ₹{totalRupees:F2}");
}
catch (ArgumentException exception)
{
    Console.Error.WriteLine(exception.Message);
    Environment.ExitCode = 1;
}
