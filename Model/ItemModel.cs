namespace MAM_Invoice
{
    public class ItemModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsImported { get; set; }
        public bool IsExemptFromBasicTax { get; set; }
    }
}
