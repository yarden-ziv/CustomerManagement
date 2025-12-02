using CustomerManagement.Models;
using CustomerManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;



namespace CustomerManagement.ViewModels
{
    public class CreateCustomerViewModel
    {
        // --- Hebrew Full Name ---
        [Required(ErrorMessage = "יש להזין שם מלא בעברית")]
        [StringLength(20, ErrorMessage = "השם בעברית חייב להכיל עד 20 תווים")]
        [RegularExpression(@"^[א-ת\s'\-]+$", ErrorMessage = "השם בעברית יכול להכיל אותיות עבריות בלבד")]
        [Display(Name = "שם מלא בעברית")]
        public string FullNameHeb { get; set; }

        // --- English Full Name ---
        [Required(ErrorMessage = "יש להזין שם מלא באנגלית")]
        [StringLength(15, ErrorMessage = "השם באנגלית חייב להכיל עד 15 תווים")]
        [RegularExpression(@"^[A-Za-z\s'\-]+$", ErrorMessage = "השם באנגלית יכול להכיל אותיות באנגלית בלבד")]
        [Display(Name = "שם מלא באנגלית")]
        public string FullNameEng { get; set; }

        // --- Birth Date ---
        [Required(ErrorMessage = "יש להזין תאריך לידה")]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך לידה")]
        public DateTime? BirthDate { get; set; }

        // --- ID Number ---
        [Required(ErrorMessage = "יש להזין תעודת זהות")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "תעודת זהות חייבת להכיל 9 ספרות")]
        [Display(Name = "מספר תעודת זהות")]
        public string IdNumber { get; set; }

        // --- City ---
        [Required(ErrorMessage = "יש לבחור עיר")]
        [Display(Name = "עיר")]
        public int CityId { get; set; }
        [ValidateNever]
        public List<City> Cities { get; set; }

        // --- Bank ---
        [Required(ErrorMessage = "יש לבחור בנק")]
        [Display(Name = "בנק")]
        public int BankId { get; set; }
        [ValidateNever]
        public List<BankDto> Banks { get; set; }

        // --- Branch ---
        [Required(ErrorMessage = "יש לבחור סניף")]
        [Display(Name = "סניף")]
        public int? BranchId { get; set; }
        [ValidateNever]
        public List<BranchDto> Branches { get; set; }

        // --- Account Number ---
        [Required(ErrorMessage = "יש להזין מספר חשבון")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "מספר חשבון עד 10 ספרות, ללא תווים מיוחדים")]
        [Display(Name = "מספר חשבון בנק")]
        public string AccountNumber { get; set; }
    }
}