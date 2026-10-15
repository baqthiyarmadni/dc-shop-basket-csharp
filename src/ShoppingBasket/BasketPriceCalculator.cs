namespace ShoppingBasket;

public static class BasketPriceCalculator
{
    private const decimal ApplePrice = 35m;
    private const decimal BananaPrice = 20m;
    private const decimal MelonPrice = 50m;
    private const decimal LimePrice = 15m;

    public static int CalculateTotal(IEnumerable<string> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        int apples = 0;
        int bananas = 0;
        int melons = 0;
        int limes = 0;

        foreach (string? item in items)
        {
            if (item is null)
            {
                throw new ArgumentException("Basket items cannot be null.", nameof(items));
            }

            switch (item.Trim().ToLower())
            {
                case "apple":
                    apples++;
                    break;
                case "banana":
                    bananas++;
                    break;
                case "melon":
                    melons++;
                    break;
                case "lime":
                    limes++;
                    break;
                default:
                    throw new ArgumentException($"Unknown item: '{item}'.", nameof(items));
            }
        }

        decimal appleTotal = apples * ApplePrice;
        decimal bananaTotal = bananas * BananaPrice;

        // Buy one, get one free.
        decimal melonTotal = ((melons + 1) / 2) * MelonPrice;

        // Three limes for the price of two.
        decimal limeTotal = (limes / 3 * 2 * LimePrice)
                          + (limes % 3 * LimePrice);

        decimal totalRupees = appleTotal + bananaTotal
                            + melonTotal + limeTotal;
        return (int)totalRupees;
    }
}
