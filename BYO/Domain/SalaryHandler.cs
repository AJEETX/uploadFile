using BYO.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYO.Domain
{
    public abstract class SalaryRateHandlerBase
    {
        protected SalaryRateHandlerBase _nextHandler;
        public abstract decimal LowerSalary { get; set; }
        public abstract decimal UpperSalary { get; set; }
        public abstract decimal Taxbase { get; set; }
        public abstract decimal TaxRate { get; set; }
        public void SetNextHandler(SalaryRateHandlerBase nextHandler)
        {
            _nextHandler = nextHandler;
        }
        public async Task<OutputModel> CalculateSalary(InputModel input)
        {
            if (input == null || input.AnnualSalary==0) return null;

            if (input.AnnualSalary > LowerSalary && (input.AnnualSalary <= UpperSalary ))
            {
                var grossIncome = input.AnnualSalary / 12;
                var incometax = (Taxbase + (input.AnnualSalary - LowerSalary) * (TaxRate / 100))/12;

                return new OutputModel {
                    Name =input.FirstName+ " "+input.LastName, PayPeriod=input.PaymentStartDate, GrossIncome=Math.Round(grossIncome,0),
                     Incometax=Math.Round(incometax,0), NetIncome=Math.Round(grossIncome-incometax,0), Super =Math.Round( grossIncome*(input.SuperRate/100))
                };
            }
            else if(_nextHandler!=null)

                return await _nextHandler.CalculateSalary(input);
            else
            {
                return  null;
            }
        }
    }
    public class SalaryRateHandler : SalaryRateHandlerBase
    {
        public override decimal LowerSalary { get ; set; }
        public override decimal UpperSalary { get; set; }
        public override decimal Taxbase { get; set; }
        public override decimal TaxRate { get; set; }
    }
    public class SalaryRateHandlers
    {
        public IList<SalaryRateHandler> SalaryRateHandlerList { get; set; }
    }
}
