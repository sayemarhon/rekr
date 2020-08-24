using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DemoSource;
using System.Globalization;
using DemoUtil;

namespace Demo
{
    public class ToDo
    {
        private readonly AccountComparer _comparer;

        public ToDo(AccountComparer comparer)
        {
            _comparer = comparer;
        }

        public IEnumerable<(Account, Person)> MatchPersonToAccount(
            IEnumerable<Group> groups,
            IEnumerable<Account> accounts,            IEnumerable<string> emails)
        {
            var personAccountMatches = new List<(Account, Person)>();
            var sortedAcounts = accounts.ToList();
            sortedAcounts.Sort(_comparer);

            foreach (Group g in groups)
            {
                foreach (Person p in g.People)
                {
                    foreach (EmailAddress email in p.Emails)
                    {
                        var accIndex = sortedAcounts.BinarySearch(new Account() { EmailAddress = email }, _comparer);
                        personAccountMatches.Add((sortedAcounts[accIndex], p));
                    }
                }
            }

            return personAccountMatches;
        }
    }
}
