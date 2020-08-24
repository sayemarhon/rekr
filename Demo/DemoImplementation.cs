using DemoSource;
using DemoTarget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoImplementation
{
    public static class DemoLib
    {
        /// <summary>
        /// I'm not sure what's this question about, but I'll try to cover it broadly in hopes of touching the clue.
        /// Technically this function can be called always. Because Concat changes nulls to empty strings, this should always produce an output, albeit sometimes an empty one.
        /// In real project it should be checked of course if this null handling is a desired behavour here. 
        /// If the models should always have not null data, some exception throw could be more appropriate.
        /// 
        /// This method could be useful if we wanted to have an easy-to-iterate collection of users with their emails.
        /// We need to remember though, that in case some user doesn't have any email, he won't be included in the output.
        /// Also, this collection would have some redundant data compared to input collection, since user name would be included several times in case of user having multiple emails.
        /// This might be important if for some reason users have a lot of emails attached to their accounts.
        /// </summary>
        /// <param name="people"></param>
        /// <returns>A collection of objects PersonWithEmail</returns>
        public static IEnumerable<PersonWithEmail> Flatten(IEnumerable<Person> people)
        {
            var peopleWithEmails = new List<PersonWithEmail>();

            foreach (Person p in people)
            {
                foreach (EmailAddress email in p.Emails)
                {
                    peopleWithEmails.Add(new PersonWithEmail
                    {
                        SanitizedNameWithId = string.Concat(SanitizeNameString(p.Name), p.Id),
                        FormattedEmail = string.Concat(email.Email, email.EmailType)
                    });
                }                
            }

            return peopleWithEmails;
        }

        public static IEnumerable<IEnumerable<string>> OnlyBigCollections(List<IEnumerable<string>> toFilter)
        {
            Func<IEnumerable<string>, bool> predicate = list => list != null && list.Skip(5).Any();
            return toFilter.Where(predicate);
        }


        private static string SanitizeNameString(string name)
        {
            if (name is null)
            {
                return null;
            }

            return new string(name.Where(c => char.IsLetterOrDigit(c)).ToArray());
        }
    }
}
    
