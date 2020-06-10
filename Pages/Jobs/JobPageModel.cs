using Leome.Data;
using Leome.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Leome.Pages.Jobs
{
    public class JobPageModel : PageModel
    {

        public List<JobTagsViewModel> SelectedTags;

        public SelectList CompaniesNamesSelectList { get; set; }

        public void PopulateCompaniesDropDownList(
            Context _context,
            object selectedCompany = null)
        {
            var companiesQuery = from d in _context.Companies
                                 orderby d.CompanyName
                                 select d;

            CompaniesNamesSelectList = new SelectList(companiesQuery.AsNoTracking(),
                        "ID", "CompanyName", selectedCompany);
        }

        public void PopulateJobTags(Context context,
                                               Job job)
        {
            var allTags = context.Tags.OrderBy(x => x.TagType).ThenBy(x => x.Title);
            var jobsTags = new HashSet<int>(
                job.JobTags.Select(c => c.TagID));
            SelectedTags = new List<JobTagsViewModel>();
            foreach (var tag in allTags)
            {
                SelectedTags.Add(new JobTagsViewModel
                {
                    JobTagId = tag.ID,
                    JobTag = new JobTag { Tag = tag },
                    Assigned = jobsTags.Contains(tag.ID)
                });
            }
        }

        public void UpdateJobTags(Context context,
            string[] selectedTags, Job jobToUpdate)
        {
            if (selectedTags == null)
            {
                jobToUpdate.JobTags = new List<JobTag>();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var jobsTags = new HashSet<int>
                (jobToUpdate.JobTags.Select(c => c.TagID));
            foreach (var tag in context.Tags)
            {
                if (selectedTagsHS.Contains(tag.ID.ToString()))
                {
                    if (!jobsTags.Contains(tag.ID))
                    {
                        jobToUpdate.JobTags.Add(
                            new JobTag
                            {
                                JobID = jobToUpdate.ID,
                                TagID = tag.ID
                            });
                    }
                }
                else
                {
                    if (jobsTags.Contains(tag.ID))
                    {
                        var jobToRemove
                            = jobToUpdate
                                .JobTags
                                .SingleOrDefault(i => i.TagID == tag.ID);
                        context.Remove(jobToRemove);
                    }
                }
            }
        }
    }
}
