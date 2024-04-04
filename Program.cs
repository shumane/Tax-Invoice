using MAM_Invoice;

class Program
{
    public static void Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Manual Input");
            Console.WriteLine("2. Predefined Input");
            Console.WriteLine("3. Quit");

            int option;

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        ManualInput();
                        break;
                    case 2:
                        PredefinedInput();
                        break;
                    case 3:
                        isRunning = false;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose 1, 2 or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }

        //if (option == 1)
        //{
        //    ManualInput();
        //}
        //else if (option == 2)
        //{
        //    PredefinedInput();
        //}
        //else
        //{
        //    Console.WriteLine("Invalid option. Please choose 1 or 2.");
        //}
    }

    static void ManualInput()
    {
        // Manual input
        Console.WriteLine("Enter the number of shopping baskets (should be three):");
        int numBaskets = int.Parse(Console.ReadLine());

        List<ShoppingBasket> baskets = new List<ShoppingBasket>();

        for (int i = 1; i <= numBaskets; i++)
        {
            Console.WriteLine($"Input {i}:");
            Console.WriteLine("Enter items (format: quantity item_name at price):");

            var basket = new ShoppingBasket();
            string input;
            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    string[] parts = input.Split(" at ");
                    string[] itemInfo = parts[0].Split(' ');
                    int quantity = int.Parse(itemInfo[0]);
                    string itemName = string.Join(" ", itemInfo[1..]);
                    decimal price = decimal.Parse(parts[1]);
                    bool isImported = itemName.Contains("imported", StringComparison.OrdinalIgnoreCase);
                    bool isExempt = itemName.Contains("book", StringComparison.OrdinalIgnoreCase)
                        || itemName.Contains("chocolate", StringComparison.OrdinalIgnoreCase)
                        || itemName.Contains("pill", StringComparison.OrdinalIgnoreCase);

                    var itemModel = new ItemModel
                    {
                        Name = itemName,
                        Price = price,
                        IsImported = isImported,
                        IsExemptFromBasicTax = isExempt
                    };

                    var item = new Item(itemModel);
                    basket.AddItem(item);
                }
            } while (!string.IsNullOrWhiteSpace(input));

            baskets.Add(basket);
        }

        // Display receipts for all baskets
        for (int i = 0; i < baskets.Count; i++)
        {
            Console.WriteLine($"Output {i + 1}:");
            baskets[i].GenerateReceipt();
            Console.WriteLine();
        }
    }

    static void PredefinedInput()
    {
        // Predefined input and output
        var basket1 = new ShoppingBasket();
        basket1.AddItem(new Item(new ItemModel { Name = "1 Book", Price = 12.49m, IsImported = false, IsExemptFromBasicTax = true }));
        basket1.AddItem(new Item(new ItemModel { Name = "1 Music CD", Price = 14.99m, IsImported = false, IsExemptFromBasicTax = false }));
        basket1.AddItem(new Item(new ItemModel { Name = "1 Chocolate bar", Price = 0.85m, IsImported = false, IsExemptFromBasicTax = true }));
        Console.WriteLine("Output 1:");
        basket1.GenerateReceipt();
        Console.WriteLine();

        var basket2 = new ShoppingBasket();
        basket2.AddItem(new Item(new ItemModel { Name = "1 Imported box of chocolates", Price = 10.00m, IsImported = true, IsExemptFromBasicTax = true }));
        basket2.AddItem(new Item(new ItemModel { Name = "1 Imported bottle of perfume", Price = 47.50m, IsImported = true, IsExemptFromBasicTax = false }));
        Console.WriteLine("Output 2:");
        basket2.GenerateReceipt();
        Console.WriteLine();

        var basket3 = new ShoppingBasket();
        basket3.AddItem(new Item(new ItemModel { Name = "1 Imported bottle of perfume", Price = 27.99m, IsImported = true, IsExemptFromBasicTax = false }));
        basket3.AddItem(new Item(new ItemModel { Name = "1 Bottle of perfume", Price = 18.99m, IsImported = false, IsExemptFromBasicTax = false }));
        basket3.AddItem(new Item(new ItemModel { Name = "1 Packet of headache pills", Price = 9.75m, IsImported = false, IsExemptFromBasicTax = true }));
        basket3.AddItem(new Item(new ItemModel { Name = "1 Box of imported chocolates", Price = 11.25m, IsImported = true, IsExemptFromBasicTax = true }));
        Console.WriteLine("Output 3:");
        basket3.GenerateReceipt();
    }
}
