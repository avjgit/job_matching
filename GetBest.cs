using Leome.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Leome.Pages
{
    public static class GetBest
    {
        public async static Task<IList<Person>> Candidates(Job job, IQueryable<Person> people)
        {
            var personsWithScores = new List<(Person person, int score)>();

            foreach (var person in people)
            {
                int personsScore = 0;
                foreach (var personTag in person.PersonTags)
                {
                    var jobTag = job.JobTags.FirstOrDefault(x => x.TagID == personTag.TagID);
                    if (jobTag != null)
                    {
                        personsScore += 1; // for now, use same even weights; todo: use jobTag.Weight, also jobTag.SkillLevel
                    }
                }
                personsWithScores.Add((person, personsScore));
            }

            const int topPersonsCount = 3;
            
            var personsRating = personsWithScores.OrderByDescending(x => x.score);
            var maxScore = personsRating.First().score;
            var equalToMax = personsRating.Where(x => x.score == maxScore);

            if (equalToMax.Count() > topPersonsCount)
            {
                return equalToMax.Select(x => x.person).ToList();
            }
            return personsRating.Take(topPersonsCount).Select(x => x.person).ToList();
        }
    }
}
