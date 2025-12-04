using System.Xml.Serialization;
using CustomerManagement.Models;

var projectPath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
var filePath = Path.Combine(projectPath, "customers.xml");

// 1. Make sure the file exists
if (!File.Exists(filePath))
{
    Console.WriteLine("Error: customers.xml not found in the project folder.");
    return;
}

// 2. Prepare serializer
var serializer = new XmlSerializer(typeof(List<Customer>));

// 3. Read and deserialize
List<Customer> customers;

using (var reader = new StreamReader(filePath))
{
    customers = (List<Customer>)serializer.Deserialize(reader);
}

// 4. Print customers
Console.WriteLine("Imported customers from XML:\n");

foreach (var c in customers)
{
    Console.WriteLine($"שם בעברית: {c.FullNameHeb}");
    Console.WriteLine($"שם באנגלית: {c.FullNameEng}");
    Console.WriteLine($"תאריך לידה: {c.BirthDate:yyyy-MM-dd}");
    Console.WriteLine($"תעודת זהות: {c.IdNumber}");
    Console.WriteLine($"עיר (ID): {c.CityId}");
    Console.WriteLine($"מספר חשבון: {c.AccountNumber}");
    Console.WriteLine("--------------------------------");
}

Console.WriteLine("\nXML import complete.");