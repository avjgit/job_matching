using Leome.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leome.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            // todo: better data (City, etc)
            context.Database.EnsureCreated();

            if (context.People.Any()) return;   // DB has been seeded

            var random = new Random();
            var careerLevels = Enum.GetValues(typeof(CareerLevel));
            var skillLevels = Enum.GetValues(typeof(SkillLevel));
            var weights = Enum.GetValues(typeof(TagWeight));

            context.People.AddRange(new Person[]
            {
                new Person{ FirstMidName = "Liana", LastName = "Leer" },
                new Person{ FirstMidName = "Thomas", LastName = "Tjaden" },
                new Person{ FirstMidName = "Friedrich", LastName = "Müller" },
                new Person{ FirstMidName = "Diana", LastName = "Detering" },
                new Person{ FirstMidName = "Haie", LastName = "Westhus" },
                new Person{ FirstMidName = "Stanislaus", LastName = "Katczinsky" },
                new Person{ FirstMidName = "Franz", LastName = "Kemmerich" },
                new Person{ FirstMidName = "Karina", LastName = "Kantorek" },
                new Person{ FirstMidName = "Albert", LastName = "Kropppremière classe." },
                new Person{ FirstMidName = "Helene", LastName = "Himmelstoss" },
                new Person{ FirstMidName = "Josef", LastName = "Hamacher" },
                new Person{ FirstMidName = "Brigite", LastName = "Bertinck" },
                new Person{ FirstMidName = "Paul", LastName = "Bäumer" }
            });
            context.SaveChanges();

            var tags = new List<Tag>();

            var industries = new List<string> { "E-commerce", "Logistics", "Automotive", "Retail", "IT", "Real Estate", "Medical Devices", "Food Service", "Manufacturer's Representatives", "Pharmaceutical", "Other" };
            industries.ForEach(x => tags.Add(new Tag { TagType = TagType.Industry, Title = x }));

            var languages = new List<string> { "German", "English", "Spanish", "Italian", "French", "Russian" };
            languages.ForEach(x => tags.Add(new Tag { TagType = TagType.Language, Title = x }));

            var skills = new List<string>
            {
                "Kaltakquise,Research,Cold Calling,Cold acquisition,Hunting",
                "Recurring Business,Warm Calling,Warm Leads,Farming",
                "Key Accounting,Großkunden Betreuun",
                "Kundenberatung,Kundenbetreuung,Customer Care",
                "VertriebsAußendienst,Terminieren,Kundepräsentationen,Kundentermine,Research neuer Kunden",
                "Innendienst,Backoffice,Calling & Support",
                "Lead Generation,Lead Generierung",
                "Change Management",
                "Sales Strategie,Vertriebsstrategien",
                "Business Development,Sales Development",
                "Marketing",
                "SalesForce",
                "CRM",
                "Controlling",
                "Angebotserstellung",
                "Reporting",
                "Kundenbindungskonzepte",
                "Terminkoordinatio",
                "Beratungsgespräche,Verkaufsgespräche,Sales Pitc",
                "Business Strategie",
                "Anfragenbearbeitung",
                "Angebotsbearbeitung",
                "Marketresearch,Marktanalyse,Wettbewerbsanalyse",
                "Verkaufsoptimierung",
                "Analyse und Bewertung von Kundenbedürfnissen",
                "Verhandlungen",
                "Sales Coaching",
                "Teamführung,Teamlead"
            };

            foreach (var skill in skills)
            {
                var split = skill.Split(',');

                tags.Add(new Tag
                {
                    TagType = TagType.Skill,
                    Title = split[0],
                    SynonymsCSV = split.Length == 1 ? null : String.Join(",", split.Skip(1))
                });
            }
            context.Tags.AddRange(tags);
            context.SaveChanges();

            var personTags = new List<PersonTag>();

            foreach (var p in context.People)
            {
                for (int i = 0; i < 8; i++)
                {
                    var randomTag = context.Tags.OrderBy(o => Guid.NewGuid()).First();
                    personTags.Add(new PersonTag
                    {
                        PersonID = p.ID,
                        Person = p,
                        TagID = randomTag.ID,
                        Tag = randomTag,
                        SkillLevel = (SkillLevel)skillLevels.GetValue(random.Next(skillLevels.Length)),
                        Weight = (TagWeight)weights.GetValue(random.Next(weights.Length))
                    });
                }
            };
            context.PersonTags.AddRange(personTags);
            context.SaveChanges();

            context.Companies.AddRange(new Company[]
            {
                new Company{ CompanyName = "Audi" },
                new Company{ CompanyName = "BASF" },
                new Company{ CompanyName = "Continental" } //todo: get from wiki?
            });
            context.SaveChanges();

            var prefix = new string[]{ "Rockstar", "Amazing", "Ninja", "Urgent", "Immediate", "Hiring" };

            var jobs = new List<Job>();
            foreach (var company in context.Companies.Take(3))
            {
                var careerLevel = (CareerLevel)careerLevels.GetValue(random.Next(careerLevels.Length));

                jobs.Add(new Job
                {
                    CompanyID = company.ID,
                    Company = company,
                    CareerLevel = careerLevel,
                    Title = $"{prefix[random.Next(skillLevels.Length)-1]} Sales {careerLevel}",
                    Description = "Lorem ipsum vitae habitasse neque posuere conubia ligula ultricies, curabitur nullam vitae erat scelerisque feugiat ligula, adipiscing aliquam pulvinar dictumst aliquet mollis felis torquent magna rhoncus per faucibus aliquet tortor et ultrices nec semper cubilia.",
                });
            };
            context.Jobs.AddRange(jobs);
            context.SaveChanges();

            var jobTags = new List<JobTag>();
            foreach (var j in context.Jobs)
            {
                for (int i = 0; i < 8; i++)
                {
                    var randomTag = context.Tags.OrderBy(o => Guid.NewGuid()).First();

                    jobTags.Add(new JobTag
                    {
                        JobID = j.ID,
                        Job = j,
                        TagID = randomTag.ID,
                        Tag = randomTag,
                        SkillLevel = (SkillLevel)skillLevels.GetValue(random.Next(skillLevels.Length)),
                        Weight = (TagWeight)weights.GetValue(random.Next(weights.Length))
                    });
                }
            };
            context.JobTags.AddRange(jobTags);
            context.SaveChanges();
        }
    }
}
