using Leome.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Leome.Pages
{
    public class Match
    {
        public Person Person { get; set; }
        public IList<PersonTag> MatchedTags { get; set; }
        public IList<PersonTag> OtherTags { get; set; }
        public int Score { get; set; }
    }

    public static class GetBest
    {
        public async static Task<IList<Match>> Candidates(Job job, IQueryable<Person> people)
        {
            var matches = new List<Match>();

            foreach (var person in people)
            {
                var match = new Match()
                {
                    Person = person,
                    MatchedTags = new List<PersonTag>(),
                    OtherTags = new List<PersonTag>(),
                    Score = 0
                };

                foreach (var personTag in person.PersonTags)
                {
                    var jobTag = job.JobTags.FirstOrDefault(x => x.TagID == personTag.TagID);

                    if (jobTag != null)
                    {
                        match.Score += 1; // for now, use same even weights; todo: use jobTag.Weight, also jobTag.SkillLevel
                        match.MatchedTags.Add(personTag);
                    }
                    else
                    {
                        match.OtherTags.Add(personTag);
                    }
                }
                matches.Add(match);
            }

            const int topPersonsCount = 3;
            
            var personsRating = matches.OrderByDescending(x => x.Score);
            var maxScore = personsRating.First().Score;
            var equalToMax = personsRating.Where(x => x.Score == maxScore);

            if (equalToMax.Count() > topPersonsCount)
            {
                return equalToMax.ToList();
            }
            return personsRating.Take(topPersonsCount).ToList();
        }
    }
}
