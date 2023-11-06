/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Tests product.cs
 */

using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        MMABooksContext dbContext;
        Product? p;
        List<Product>? products;

        public void PrintAll(List<Product> products)
        {
            foreach (Product p in products)
            {
                Console.WriteLine(p);
            }
        }

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetByPrimaryKeyTest() // Test 1
        {
            p = dbContext.Products.Find("A4CS");
            Assert.IsNotNull(p);
            Assert.AreEqual(4637, p.OnHandQuantity);
            Console.WriteLine(p);
        }


        [Test]
        public void GetAllTest() // Test 2
        {
            products = dbContext.Products.OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            Assert.AreEqual(3974, products[1].OnHandQuantity);
            PrintAll(products);
        }

                [Test]
        public void GetUsingWhere() // Test 3
        {
            // LS: Get a list of all of the products that have a unit price of 56.50
            products = dbContext.Products.Where(p => p.UnitPrice == 56.5m).OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(7, products.Count);
            Assert.AreEqual(4637, products[0].OnHandQuantity);
            PrintAll(products);
        }

        [Test]
        public void GetWithCalculatedFieldTest() // Test 4
        {
            // LS: Get a list of objects that include the productcode, unitprice, quantity and inventoryvalue.
            var products = dbContext.Products.Select(
            p => new { p.ProductCode, p.UnitPrice, p.OnHandQuantity, Value = p.UnitPrice * p.OnHandQuantity }).
            OrderBy(p => p.ProductCode).ToList();
            Assert.AreEqual(16, products.Count);
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        [Test]
        public void DeleteTest() // Test 5
        {
            // Delete a product that does not have any invoiceslines - to maintain referential integrity.
            p = dbContext.Products.Find("ZLJR");
            if (p != null)
                dbContext.Products.Remove(p);
            dbContext.SaveChanges(); // SaveChanges() is needed after add, update, or delete.
            Assert.IsNull(dbContext.Products.Find("ZLJR"));
        }

        [Test]
        public void CreateTest()
        {
            // Create a new customer object.
            // Add the object to the customers collection.
            // Ask the dbcontext to save changes.
            p = new Product();
            p.ProductCode = "ABCD";
            p.Description = "Erics Guide to C# Success";
            p.UnitPrice = 1.0m;
            p.OnHandQuantity = 1111;
            dbContext.Products.Add(p);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Products.Find("ABCD"));
        }

        [Test]
        public void UpdateTest()
        {
            p = dbContext.Products.Find("ABCD");
            if (p != null)
            {
                p.OnHandQuantity = 1112;
                dbContext.Products.Update(p);
            }
            dbContext.SaveChanges();
            p = dbContext.Products.Find("ABCD");
            if (p != null)
                Assert.AreEqual(1112, p.OnHandQuantity);
        }

    } // end class ProductTests
} // namespace MMABooksEFClasses