using System.Collections.Generic;
using System.Linq;
using DemoSource;
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

        /// <summary>
        /// I didn't use the provided emails collection, since I assumed that we want to assign addresses to all accounts, not just those from the list
        /// And I don't see how using this list would be of a help with the solution below
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="accounts"></param>
        /// <param name="emails"></param>
        /// <returns>Collection of Accounts with matched person to each</returns>
        public IEnumerable<(Account, Person)> MatchPersonToAccount(
            IEnumerable<Group> groups,
            IEnumerable<Account> accounts,
            IEnumerable<string> emails)
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
