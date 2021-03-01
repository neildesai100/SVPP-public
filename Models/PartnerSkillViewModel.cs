using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace SVPP.Models
{
    public class PartnerSkillViewModel
    {
        public IPagedList<Partner> Partners { get; set; }
        public SelectList Skills { get; set; }
        public SelectList Preferences { get; set; }
        public string PartnerSkill1 { get; set; }
        public string PartnerSkill2 { get; set; }
        public string PartnerSkill3 { get; set; }
        public string PartnerPreference1 { get; set; }
        public string PartnerPreference2 { get; set; }
        public string PartnerPreference3 { get; set; }
        public string SearchString { get; set; }
        public bool FilterActive { get; set; }
    }
}
