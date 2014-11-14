using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessagingBankCS.Control
{
    class Controller
    {
        public string CalculateLoanResponse(string loanRequestJSON)
        {
            // Deserialize JSON loan request to object
            Entity.LoanRequest loanRequest = JsonConvert.DeserializeObject<Entity.LoanRequest>(loanRequestJSON);
            // Get interest rate
            double interestRate = calculateInterestRate(loanRequest.CreditScore);
            // Construct loan response object
            Entity.LoanResponse loanResponse = new Entity.LoanResponse(interestRate, loanRequest.Ssn);
            // Serialize loan response to JSON
            return JsonConvert.SerializeObject(loanResponse);
        }

        private double calculateInterestRate(int creditScore)
        {
            double result = 0.0;
            if (creditScore <= 800 && creditScore >= 600)
            {
                result = 10.0;
            } else if (creditScore < 600 && creditScore >= 400)
            {
                result = 15.0;
            }
            else if (creditScore < 400 && creditScore >= 200)
            {
                result = 20.0;
            }
            else if (creditScore < 200 && creditScore >= 100)
            {
                result = 30.0;
            }
            else if (creditScore < 100)
            {
                result = 35.0;
            }
            return result;
        }
    }
}
