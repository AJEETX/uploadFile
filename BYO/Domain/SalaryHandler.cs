using BYO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Domain
{
    public interface SalaryRate
    {
        decimal LowerSalary { get; set; }
        decimal UpperSalary { get; set; }
        decimal Taxbase { get; set; }
        decimal TaxRate { get; set; }
    }
    public abstract class SalaryHandler : SalaryRate
    {
        protected SalaryHandler _nextHandler;
        public abstract decimal LowerSalary { get; set; }
        public abstract decimal UpperSalary { get; set; }
        public abstract decimal Taxbase { get; set; }
        public abstract decimal TaxRate { get; set; }
        public void SetNextHandler(SalaryHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
        public OutputModel CalculateSalary(InputModel input)
        {
            if (input.AnnualSalary > LowerSalary && input.AnnualSalary <= UpperSalary)
            {
                var grossIncome = input.AnnualSalary / 12;
                var incometax = (Taxbase + (input.AnnualSalary - LowerSalary) * (TaxRate / 100))/12;

                return new OutputModel { Name=input.FirstName+ " "+input.LastName,
                     PayPeriod=input.PaymentStartDate, GrossIncome= grossIncome,
                     Incometax=incometax, NetIncome= grossIncome-incometax,Super= grossIncome*(9/100)
                };
            }
            else
                return _nextHandler.CalculateSalary(input);
        }
    }
    public class Salary : SalaryHandler
    {
        public override decimal LowerSalary { get ; set; }
        public override decimal UpperSalary { get; set; }
        public override decimal Taxbase { get; set; }
        public override decimal TaxRate { get; set; }
    }
    public class SalaryRates
    {
        public List<Salary> SalaryRate { get; set; }
    }
}
