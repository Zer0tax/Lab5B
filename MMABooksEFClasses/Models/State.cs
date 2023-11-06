/* Author:  Scaffold-DbContext command
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Defines states
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class State
    {
        // Default constructor
        public State()
        {
            Customers = new HashSet<Customer>();
        }

        public string StateCode { get; set; } = null!;
        public string StateName { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; } // ???

        public override string ToString()
        {
            return StateCode + ", " + StateName;
        }

    } // end class Product
} // namespace MMABooksEFClasses.Models
