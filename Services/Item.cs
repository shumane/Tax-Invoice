namespace MAM_Invoice
{
    public class Item
    {
        private ItemModel _itemModel;

        public Item(ItemModel itemModel) => _itemModel = itemModel;

        public decimal CalculateTax()
        {
            decimal basicTaxRate = _itemModel.IsExemptFromBasicTax ? 0 : 0.1m;
            decimal importTaxRate = _itemModel.IsImported ? 0.05m : 0;
            decimal totalTax = _itemModel.Price * (basicTaxRate + importTaxRate);
            return Math.Ceiling(totalTax / 0.05m) * 0.05m;
        }

        public decimal CalculateTotalPrice()
        {
            return _itemModel.Price + CalculateTax();
        }

        public override string ToString()
        {
            return $"{_itemModel.Name}: {CalculateTotalPrice().ToString("0.00")}";
        }
    }
}
