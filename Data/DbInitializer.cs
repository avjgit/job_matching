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
            context.Database.EnsureCreated();

            if (context.People.Any()) return;   // DB has been seeded

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

            var  random = new Random();
            var skillLevels = Enum.GetValues(typeof(SkillLevel));
            var weights = Enum.GetValues(typeof(TagWeight));
            foreach (var p in context.People)
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
            };

            context.PersonTags.AddRange(personTags);
            context.SaveChanges();
        }
    }
}
