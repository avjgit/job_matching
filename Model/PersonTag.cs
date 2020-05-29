using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leome.Model
{
    public enum SkillLevel
    {
        OneStar, TwoStars, ThreeStars, FourStars, FiveStars
    }

    public class PersonTag
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int TagID { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public int Weight { get; set; } // also person can set preferences

        public Person Person { get; set; }
        public Tag Tag { get; set; }
    }

}
