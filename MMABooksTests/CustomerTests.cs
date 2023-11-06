/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Tests Customer.cs
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
    public class CustomerTests
    {
        MMABooksContext dbContext;
        Customer? c;
        List<Customer>? customers;

        public void PrintAll(List<Customer> customers)
        {
            foreach (Customer c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetAllCustomerData()");
            // We call 3 stored procedures (sp) for customers. The data is split over 3 sp's. 
            // dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer1Data()");
            // dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer2Data()");
            // dbContext.Database.ExecuteSqlRaw("call usp_testingResetCustomer3Data()");
        }

        [Test]
        public void GetByPrimaryKeyTest() // Test 1
        {
            c = dbContext.Customers.Find(3);
            Assert.IsNotNull(c);
            Assert.AreEqual("Fayetteville", c.City);
            Console.WriteLine(c);
        }

        [Test]
        public void GetAllTest() // Test 2
        {
            customers = dbContext.Customers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(696, customers.Count);
            Assert.AreEqual("Montvale", customers[1].City);
            PrintAll(customers);
        }

        [Test]
        public void GetUsingWhere() // Test 3
        {
            // LS: Get a list of all of the customers who live in OR.
            customers = dbContext.Customers.Where(c => c.StateCode == "OR").OrderBy(c => c.Name).ToList();
            // customers = dbContext.Customers.Where(c => c.StateCode.StartsWith("OR")).OrderBy(c => c.Name).ToList();
            Assert.AreEqual(5, customers.Count);
            Assert.AreEqual("Grants Pass", customers[0].City);
            PrintAll(customers);
        }

        [Test]
        public void GetWithInvoicesTest() // Test 4
        {
            // LS: Get the customer whose id is 20 and all of the invoices for that customer.
            c = dbContext.Customers.Include("Invoices").Where(c => c.CustomerId == 20).SingleOrDefault();
            Assert.AreEqual("Doraville", c.City);
            // Assert.AreEqual(3, c.Invoices.Count);
            Console.WriteLine(c);
        }

        [Test]
        public void GetWithJoinTest() // Test 5
        {
            // LS: Get a list of objects that include the customer id, name, statecode and statename
            var customers = dbContext.Customers.Join(
               dbContext.States,
               c => c.StateCode,
               s => s.StateCode,
               (c, s) => new { c.CustomerId, c.Name, c.StateCode, s.StateName }).OrderBy(r => r.StateName).ToList();
            Assert.AreEqual(696, customers.Count);
            // LS: I wouldn't normally print here but this lets you see what each object looks like
            foreach (var c in customers)
            {
                Console.WriteLine(c);
            }
        }

        [Test]
        public void DeleteTest() // Test 6
        {
            // Delete a customer who does not have any invoices - to maintain referential integrity.
            c = dbContext.Customers.Find(8);
            if (c != null)
                dbContext.Customers.Remove(c);
            dbContext.SaveChanges(); // SaveChanges() is needed after add, update, or delete.
            Assert.IsNull(dbContext.Customers.Find(8));
        }

        [Test]
        public void CreateTest()
        {
            // Create a new customer object.
            // Add the object to the customers collection.
            // Ask the dbcontext to save changes.
            c = new Customer();
            c.CustomerId = 701;
            c.Name = "John Galt";
            c.Address = "1234 Ayn Rand Way";
            c.City = "Ouray";
            c.StateCode = "CO";
            c.ZipCode = "81427";
            dbContext.Customers.Add(c);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Customers.Find(701));
        }

        [Test]
        public void UpdateTest()
        {
            c = dbContext.Customers.Find(501);
            if (c != null)
            {
                c.City = "San Francisco";
                dbContext.Customers.Update(c);
            }
            dbContext.SaveChanges();
            c = dbContext.Customers.Find(501);
            if (c != null)
                Assert.AreEqual("San Francisco", c.City);
        }

    } // end class CustomerTests
} // namespace MMABooksEFClasses