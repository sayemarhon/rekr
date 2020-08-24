using DemoSource;
using System.Collections.Generic;

namespace DemoUtil
{
    public class AccountComparer : IComparer<Account>
    {
        public int Compare(Account a1, Account a2)
        {
            return CompareEmails(a1.EmailAddress, a2.EmailAddress);
        }

        private int CompareEmails(EmailAddress e1, EmailAddress e2)
        {
            return e1.Email != e2.Email ?
                string.Compare(e1.Email, e2.Email) :
                string.Compare(e1.EmailType, e2.EmailType);
        }
    }
}
