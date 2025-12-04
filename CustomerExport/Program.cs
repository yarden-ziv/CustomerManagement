using System.Xml.Serialization;
using CustomerManagement.Data;
using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;

// 1. Build DbContextOptions with a direct connection string
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CustomerManagementDb;Trusted_Connection=True;")
    .Options;

// 2. Create the DbContext
using var context = new AppDbContext(options);

// 3. Read customers (include City if you want that in the XML)
var customers = context.Customers
    .Include(c => c.City)
    .ToList();

// 4. Serialize to XML
var serializer = new XmlSerializer(typeof(List<Customer>));
var projectPath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
var filePath = Path.Combine(projectPath, "customers.xml");


using var writer = new StreamWriter(filePath);
serializer.Serialize(writer, customers);

Console.WriteLine($"XML export complete! Saved to: {filePath}");