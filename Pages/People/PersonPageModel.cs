using Leome.Data;
using Leome.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Leome.Pages.People
{
    public class PersonPageModel : PageModel
    {
        public List<PersonTagsViewModel> SelectedTags;

        public void PopulatePersonTags(Context context,
            Person person)
        {
            var allTags = context.Tags.OrderBy(x => x.TagType).ThenBy(x => x.Title);
            var personsTags = new HashSet<int>(
                person.PersonTags.Select(c => c.TagID));
            SelectedTags = new List<PersonTagsViewModel>();
            foreach (var tag in allTags)
            {
                SelectedTags.Add(new PersonTagsViewModel
                {
                    PersonTagId = tag.ID,
                    PersonTag = new PersonTag { Tag = tag },
                    Assigned = personsTags.Contains(tag.ID)
                });
            }
        }

        public void UpdatePersonTags(Context context,
            string[] selectedTags, Person personToUpdate)
        {
            if (selectedTags == null)
            {
                personToUpdate.PersonTags = new List<PersonTag>();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var personsTags = new HashSet<int>
                (personToUpdate.PersonTags.Select(c => c.TagID));
            foreach (var tags in context.Tags)
            {
                if (selectedTagsHS.Contains(tags.ID.ToString()))
                {
                    if (!personsTags.Contains(tags.ID))
                    {
                        personToUpdate.PersonTags.Add(
                            new PersonTag
                            {
                                PersonID = personToUpdate.ID,
                                TagID = tags.ID
                            });
                    }
                }
                else
                {
                    if (personsTags.Contains(tags.ID))
                    {
                        var tagToRemove
                            = personToUpdate
                                .PersonTags
                                .SingleOrDefault(i => i.TagID == tags.ID);
                        context.Remove(tagToRemove);
                    }
                }
            }
        }
    }
}
