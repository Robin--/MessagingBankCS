using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingBankCS.Entity
{
    class LoanResponse
    {
        public double InterestRate { get; set; }
        public long Ssn { get; set; }

        public LoanResponse(double interestRate, long ssn)
        {
            InterestRate = interestRate;
            Ssn = ssn;
        }
    }
}
