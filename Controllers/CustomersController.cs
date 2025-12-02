using CustomerManagement.Data;
using CustomerManagement.Models;
using CustomerManagement.Models.Dto;
using CustomerManagement.Services;
using CustomerManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var customers = await _context.Customers
                .Include(c => c.City)
                .ToListAsync();

            return View(customers);
        }

        // GET: /Customers/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cities = await _cityService.GetCitiesAsync();
            var banks = await _bankService.GetBanksAsync();

            var vm = new CreateCustomerViewModel
            {
                // SORT CITIES
                Cities = cities.OrderBy(c => c.Name).ToList(),

                // SORT BANKS
                Banks = banks.OrderBy(b => b.Description).ToList(),

                // Branches empty until bank is selected
                Branches = new List<BranchDto>()
            };

            return View(vm);
        }


        // POST: /Customers/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel vm)
        {
            foreach (var item in ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"MODEL ERROR: Key = {item.Key}, Error = {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
            {
                // Reload dropdowns because they are not part of the POSTed model
                vm.Cities = await _cityService.GetCitiesAsync();
                vm.Banks = await _bankService.GetBanksAsync();
                vm.Branches = await _bankService.GetBranchesAsync(vm.BankId);

                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

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
                BranchId = vm.BranchId.Value,
                AccountNumber = vm.AccountNumber
            };

            // Save to DB
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            TempData["Success"] = "הלקוח נוצר בהצלחה!";
            return RedirectToAction("List");
        }

        public async Task<IActionResult> GetBranches(int bankCode)
        {
            var branches = await _bankService.GetBranchesAsync(bankCode);
            return Json(branches);
        }

    }
}
