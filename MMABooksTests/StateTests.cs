/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Tests state.cs
 */

using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using MMABooksEFClasses.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
// using MMABooksEFClasses.MarisModels;
using MMABooksEFClasses.Models;

namespace MMABooksTests
{
    [TestFixture]
    public class StateTests
    {
        // ignore this warning about making dbContext nullable.
        // if you add the ?, you'll get a warning wherever you use dbContext
        MMABooksContext dbContext;
        State? s;
        List<State>? states;

        public void PrintAll(List<State> states)
        {
            foreach (State s in states)
            {
                Console.WriteLine(s);
            }
        }

        [SetUp]
        public void Setup()
        {
            dbContext = new MMABooksContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetStateData()");
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetByPrimaryKeyTest() // Test 1 from video
        {
            s = dbContext.States.Find("OR");
            Assert.IsNotNull(s);
            if(s != null )
                Assert.AreEqual("Oregon", s.StateName);
            Console.WriteLine(s);
        }

        [Test]
        public void GetAllTest()  // Test 2 from video
        {
            states = dbContext.States.OrderBy(s => s.StateName).ToList();
            Assert.AreEqual(53, states.Count);
            Assert.AreEqual("Alabama", states[0].StateName);
            PrintAll(states);
        }

        [Test]
        public void GetUsingWhere() // Test 3 from video
        {
            states = dbContext.States.Where(s => s.StateName.StartsWith("A")).OrderBy(s => s.StateName).ToList();
            Assert.AreEqual(4, states.Count);
            Assert.AreEqual("Alabama", states[0].StateName);
            PrintAll(states);
        }

        [Test]
        public void GetWithCustomersTest() // Test 4 from video
        {
            s = dbContext.States.Include("Customers").Where(s => s.StateCode == "OR").SingleOrDefault();
            Assert.IsNotNull(s);
            if (s != null)
            {
                Assert.AreEqual("Oregon", s.StateName);
                Assert.AreEqual(5, s.Customers.Count);
            }
            Console.WriteLine(s);
        }

        [Test]
        public void DeleteTest() // Test 5 from video.
        {
            // We should delete a state without customers to maintain referential integrity.
            s = dbContext.States.Find("HI");
            dbContext.States.Remove(s);
            dbContext.SaveChanges(); // We should save after we insert, update, or delete.
            Assert.IsNull(dbContext.States.Find("HI"));
        }

        [Test]
        public void CreateTest() // Test 6
        {
            // See video 3 ~5:00
            // Create a new state object.
            // Add the object to the states collection.
            // Ask the dbcontext to save changes.
            s = new State ();
            s.StateCode = "GI";
            s.StateName = "Greater Idaho";
            dbContext.States.Add(s);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.States.Find("GI"));
        }

        [Test]
        public void UpdateTest() // Test 7
        {
            s = dbContext.States.Find("OR");
            s.StateName = "Oregun"; // I changed my data and sps to make OR = Oregon, already.
            dbContext.States.Update(s);
            dbContext.SaveChanges();
            s = dbContext.States.Find("OR");
            if (s != null) 
                Assert.AreEqual("Oregun", s.StateName);
        }

      } // end class StateTests
} // namespace MMABooksEFClasses