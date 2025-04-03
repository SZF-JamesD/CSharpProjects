using Ex0701.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex0701.Services
{
    internal class FilterService
    {

        private List<Customer> customers;

        public FilterService(List<Customer> customers)
        {
            this.customers = customers;
        }

        public IEnumerable<Customer> FilterCustomers(Func<Customer, bool> filterCriteria, bool useQuerySyntax)
        {
            if (useQuerySyntax)
            {
                return from customer in customers
                       where filterCriteria(customer)
                       select customer;
            }
            else
            {
                return customers.Where(filterCriteria);
            }
        }

        public IEnumerable<Customer> FilterAndSortCustomers(Func<Customer, bool> filterCriteria, bool useQuerySyntax, Func<Customer, object> sortCriteria, bool sortDescending)
        {
            var filteredCustomers = FilterCustomers(filterCriteria, useQuerySyntax);

            return sortDescending ? filteredCustomers.OrderByDescending(sortCriteria) : filteredCustomers.OrderBy(sortCriteria);
        }


    }
}
