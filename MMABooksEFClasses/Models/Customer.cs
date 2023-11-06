/* Author:  Scaffold-DbContext command
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Defines customers
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Customer
    {
        // Default constructor
        public Customer()
        {
            Invoices = new HashSet<Invoice>(); // No idea what this does.
        }

        // Properties
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StateCode { get; set; } = null!; // FK to State table. This USED to be called "State".
        public string ZipCode { get; set; } = null!;

        public virtual State State { get; set; } = null!; // Allows for foreign key relationships. This USED to be called StateNavigation.
        public virtual ICollection<Invoice> Invoices { get; set; } // Allows us to see collection of invoices?.

        public override string ToString()
        {
            return CustomerId + ", " + Name + ", " + Address + ", " + City + ", " + StateCode + ", " + ZipCode;
        }
    } // end class Customer
} // end namespace MMABooksEFClasses.Models
