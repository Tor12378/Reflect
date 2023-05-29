using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Company : Organization
    {
        private int _privateEmployeeCount = 0;
        public int PublicEmployeeCount { get; set; } = 0;
        public static int PublicAndStaticEmployeeCount { get; set; } = 0;

        public string CompanyType { get; set; }
        private int CompanyRevenue { get; set; }

        public string GetCompanyRevenue()
        {
            return $"Company Revenue: {CompanyRevenue}";
        }

        public static string GetTotalEmployeeCount()
        {
            return $"Total Employee Count: {PublicAndStaticEmployeeCount}";
        }

        public Company(int employeeCount)
        {
            _privateEmployeeCount = employeeCount;
            Console.WriteLine("Invoked constructor with employeeCount parameter");
        }

        public Company()
        {
            Console.WriteLine("Invoked parameterless constructor");
        }

        public Company(string companyType)
        {
            CompanyType = companyType;
        }

        public void HireEmployee(int count)
        {
            _privateEmployeeCount += count;
            PublicEmployeeCount += count;
            PublicAndStaticEmployeeCount += count;
            Console.WriteLine($"Hired {count} employee(s)");
        }
    }
}
