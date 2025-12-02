using CustomerManagement.Data;
using CustomerManagement.Models;
using CustomerManagement.Models.Dto;
using CustomerManagement.Services;
using CustomerManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagement.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CityService _cityService;
        private readonly BankService _bankService;
        private readonly AppDbContext _context;

        public CustomersController(
            CityService cityService,
            BankService bankService,
            AppDbContext context)
        {
            _cityService = cityService;
            _bankService = bankService;
            _context = context;
        }

        // GET: /Customers/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateCustomerViewModel
            {
                Cities = await _cityService.GetCitiesAsync(),
                Banks = await _bankService.GetBanksAsync(),
                Branches = new List<BranchDto>() // empty until bank is selected
            };

            return View(vm);
        }

        // POST: /Customers/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns because they are not part of the POSTed model
                vm.Cities = await _cityService.GetCitiesAsync();
                vm.Banks = await _bankService.GetBanksAsync();
                vm.Branches = await _bankService.GetBranchesAsync(vm.BankId);

                return View(vm);
            }

            // Map ViewModel → Entity
            var customer = new Customer
            {
                FullNameHeb = vm.FullNameHeb,
                FullNameEng = vm.FullNameEng,
                BirthDate = vm.BirthDate.Value,
                IdNumber = vm.IdNumber,
                CityId = vm.CityId,
                BankId = vm.BankId,
                BranchId = vm.BranchId,
                AccountNumber = vm.AccountNumber
            };

            // Save to DB
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            TempData["Success"] = "הלקוח נוצר בהצלחה!";
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> GetBranches(int bankCode)
        {
            var branches = await _bankService.GetBranchesAsync(bankCode);
            return Json(branches);
        }

    }
}
