using Leome.Data;
using Leome.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leome.Pages.Jobs
{
    public class JobTagsPageModel : PageModel
    {

        public List<JobTagsViewModel> AssignedCourseDataList;

        public void PopulateAssignedCourseData(Context context,
                                               Job instructor)
        {
            var allCourses = context.Tags;
            var instructorCourses = new HashSet<int>(
                instructor.JobTags.Select(c => c.TagID));
            AssignedCourseDataList = new List<JobTagsViewModel>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new JobTagsViewModel
                {
                    JobTagId = course.ID,
                    JobTag = new JobTag { Tag = course },
                    Assigned = instructorCourses.Contains(course.ID)
                });
            }
        }

        public void UpdateInstructorCourses(Context context,
            string[] selectedCourses, Job instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.JobTags = new List<JobTag>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.JobTags.Select(c => c.TagID));
            foreach (var course in context.Tags)
            {
                if (selectedCoursesHS.Contains(course.ID.ToString()))
                {
                    if (!instructorCourses.Contains(course.ID))
                    {
                        instructorToUpdate.JobTags.Add(
                            new JobTag
                            {
                                JobID = instructorToUpdate.ID,
                                TagID = course.ID
                            });
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.ID))
                    {
                        var courseToRemove
                            = instructorToUpdate
                                .JobTags
                                .SingleOrDefault(i => i.TagID == course.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
