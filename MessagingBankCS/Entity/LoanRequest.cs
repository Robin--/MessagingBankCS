using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingBankCS.Entity
{
    class LoanRequest
    {
        public long Ssn { get; set; }
        public int CreditScore { get; set; }
        public double LoanAmount { get; set; }
        public int LoanDuration { get; set; }

        public LoanRequest(long ssn, int creditScore, double loanAmount, int loanDuration)
        {
            Ssn = ssn;
            CreditScore = creditScore;
            LoanAmount = loanAmount;
            LoanDuration = loanDuration;
        }
    }
}
