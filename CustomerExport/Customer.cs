namespace CustomerManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullNameHeb { get; set; }
        public string FullNameEng { get; set; }

        public DateTime BirthDate { get; set; }

        public string IdNumber { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int BankId { get; set; }
        public int BranchId { get; set; }

        public string AccountNumber { get; set; }
    }
}
