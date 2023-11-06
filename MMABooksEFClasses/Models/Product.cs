/* Author:  Scaffold-DbContext command
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Defines products
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Product
    {
        // Default constructor
        public Product()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        // Properties
        public string ProductCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int OnHandQuantity { get; set; }

        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }

        public override string ToString()
        {
            return ProductCode + ", " + Description + ", " + UnitPrice + ", " + OnHandQuantity;
        }

    } // end class Product
} // namespace MMABooksEFClasses.Models
