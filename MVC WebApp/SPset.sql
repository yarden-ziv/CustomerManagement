CREATE OR ALTER PROCEDURE GetAllCustomers
AS
BEGIN
    SELECT 
        c.Id,
        c.FullNameHeb,
        c.FullNameEng,
        c.BirthDate,
        c.IdNumber,
        c.CityId,
        ci.Name AS CityName,
        c.BankId,
        c.BranchId,
        c.AccountNumber
    FROM Customers c
    INNER JOIN Cities ci ON ci.Id = c.CityId
    ORDER BY c.FullNameHeb;
END;
