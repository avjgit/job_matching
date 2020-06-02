using Leome.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leome.Pages
{
    public static class GetBest
    {
        public async static Task<IList<Person>> Candidates(Job job, IQueryable<Person> people)
        {
            return await people.Take(3).ToListAsync();
        }
    }
}
