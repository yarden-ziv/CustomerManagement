using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.Models
{
    public partial class CustomerListItem
    {
        [NotMapped]
        public string BankName { get; set; }  // Not part of SQL results
    }
}