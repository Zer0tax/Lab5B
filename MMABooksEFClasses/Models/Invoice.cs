/* Author:  Scaffold-DbContext command
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: Defines invoices
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Invoice
    {
        // Default constructor
        public Invoice()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        // Properties
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal SalesTax { get; set; }
        public decimal Shipping { get; set; }
        public decimal InvoiceTotal { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }

        public override string ToString()
        {
            return InvoiceId + ", " + CustomerId + ", " + InvoiceDate + ", " + ProductTotal + ", " + SalesTax + ", " + Shipping + ", " + InvoiceTotal;
        }
    } // end class Invoice
} // end namespace MMABooksEFClasses.Models
