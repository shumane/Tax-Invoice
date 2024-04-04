namespace MAM_Invoice
{
    public class ShoppingBasket
    {
        private List<Item> items;

        public ShoppingBasket() => items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public decimal CalculateTotalTax()
        {
            return items.Sum(item => item.CalculateTax());
        }

        public decimal CalculateTotalPrice()
        {
            return items.Sum(item => item.CalculateTotalPrice());
        }

        public void GenerateReceipt()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Sales Taxes: {CalculateTotalTax().ToString("0.00")}");
            Console.WriteLine($"Total: {CalculateTotalPrice().ToString("0.00")}");
        }
    }
}
