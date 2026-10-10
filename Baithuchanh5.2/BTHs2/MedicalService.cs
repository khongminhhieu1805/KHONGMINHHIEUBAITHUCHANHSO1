
namespace BTHs2
{
    public class MedicalService
    {
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public MedicalService(string name, decimal price, string category)
        {
            ServiceName = name;
            Price = price;
            Category = category;
        }

        public override string ToString()
        {
            return $"{ServiceName} - {Price:N0} đ";
        }
    }
}