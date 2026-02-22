RESTORE FILELISTONLY 
FROM DISK = N'/var/opt/mssql/backup/AdventureWorks2019.bak';

RESTORE DATABASE AdventureWorks2019
FROM DISK = N'/var/opt/mssql/data/AdventureWorks2019.bak'
WITH MOVE 'AdventureWorks2017' 
     TO '/var/opt/mssql/data/AdventureWorks2019.mdf',
     MOVE 'AdventureWorks2017_Log' 
     TO '/var/opt/mssql/log/AdventureWorks2019.ldf',
     REPLACE;
SELECT name FROM sys.databases;

use AdventureWorks

SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_SCHEMA, TABLE_NAME;

--  https://www.dbdiagrams.com/sql-server/sql-server-adventureworks-diagram/
--  https://www.dbdiagrams.com/online-diagrams/adventureworks/
--HumanResources
    select * from HumanResources.Department
    select * from HumanResources.Employee
    select * from HumanResources.EmployeeDepartmentHistory
    select * from HumanResources.EmployeePayHistory
    select * from HumanResources.JobCandidate
    select * from HumanResources.Shift
-- Person
    select * from Person.BusinessEntity
    select * from Person.Person 
    select * from Person.[Password]
    select * from Person.EmailAddress
    select * from Person.Address
    select * from Person.AddressType
    select * from Person.BusinessEntityAddress
    select * from Person.ContactType 
    select * from Person.BusinessEntityContact where BusinessEntityID=1492
    select * from Person.PersonPhone
    select * from Person.PhoneNumberType
    select * from Person.CountryRegion
    select * from Person.StateProvince
-- Production
    select * from production.ProductCategory
    select * from production.ProductSubcategory
    select * from production.Product
    select * from production.ProductModel
    select * from production.ProductDescription
    select * from production.ProductModelProductDescriptionCulture
    select * from production.Illustration
    select * from production.ProductModelIllustration
    select * from production.ProductCostHistory
    select * from production.ProductInventory
    select * from production.ProductListPriceHistory
    select * from production.ScrapReason
    select * from production.TransactionHistory
    select * from production.TransactionHistoryArchive
    select * from production.UnitMeasure
    select * from production.WorkOrder
    select * from production.WorkOrderRouting
    select * from productions.BillOfMaterials
    select * from production.Culture
    select * from production.Document
    select * from production.[Location]
--Sales
    select * from Sales.Customer
    select * from Sales.SalesOrderDetail
    select * from Sales.SalesOrderHeader
    select * from Sales.SalesPerson
    select * from Sales.SalesReason
    select * from Sales.SalesOrderHeaderSalesReason
    select * from Sales.SalesTaxRate
    select * from Sales.SalesTerritory
    select * from Sales.SalesTerritoryHistory
    select * from Sales.PersonCreditCard
    select * from Sales.Store
    select * from Sales.CountryRegionCurrency
    select * from Sales.Currency
    select * from Sales.CurrencyRate
    select * from Sales.CreditCard
    select * from Sales.ShoppingCartItem
    select * from Sales.SpecialOfferProduct
    select * from Sales.SpecialOffer
--Purchasing
    Select * from Purchasing.ProductVendor
    Select * from Purchasing.PurchaseOrderDetail
    Select * from Purchasing.PurchaseOrderHeader
    Select * from Purchasing.Vendor

    




    


