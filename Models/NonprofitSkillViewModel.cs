using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace SVPP.Models
{
    public class NonprofitSkillViewModel
    {
        public IPagedList<Nonprofit> Nonprofits { get; set; }
        public SelectList Skills { get; set; }
        public SelectList Focus_Areas { get; set; }
        public string NonprofitSkill1 { get; set; }
        public string NonprofitSkill2 { get; set; }
        public string NonprofitSkill3 { get; set; }
        public string NonprofitFocus1 { get; set; }
        public string NonprofitFocus2 { get; set; }
        public string NonprofitFocus3 { get; set; }
        public string SearchString { get; set; }
        public bool FilterActive { get; set; }
    }
}
