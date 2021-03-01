using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SVPP.Data;
using SVPP.Models;
using SVPP.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace SVPP.Controllers
{
    public class PartnersController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PartnersController(DatabaseContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin, Partner, Nonprofit, Guest")]
        // GET: Partners
        public async Task<IActionResult> Index(string PartnerSkill1, string PartnerSkill2, string PartnerSkill3, 
                                               string PartnerPreference1, string PartnerPreference2, string PartnerPreference3,
                                               string SearchString, bool FilterActive, string SortOrder, string CurrentFilter, int? page)
        {

            ViewBag.CurrentSort = SortOrder;

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            IQueryable<string> skillsQuery1 = from p in _context.Partner
                                             orderby p.Skill_1
                                             select p.Skill_1;

            IQueryable<string> skillsQuery2 = from p in _context.Partner
                                              orderby p.Skill_2
                                              where p.Skill_2 != null
                                              select p.Skill_2;

            IQueryable<string> skillsQuery3 = from p in _context.Partner
                                              orderby p.Skill_3
                                              where p.Skill_3 != null
                                              select p.Skill_3;

            var skillsQueryTemp = skillsQuery1.Union(skillsQuery2);
            var skillsQuery = skillsQuery3.Union(skillsQueryTemp);

            IQueryable<string> preferencesQuery1 = from p in _context.Partner
                                                    orderby p.Preference_1
                                                    where p.Preference_1 != "None"
                                                    select p.Preference_1;

            IQueryable<string> preferencesQuery2 = from p in _context.Partner
                                                   orderby p.Preference_2
                                                   where p.Preference_2 != "None" && p.Preference_2 != null
                                                   select p.Preference_2;

            IQueryable<string> preferencesQuery3 = from p in _context.Partner
                                                   orderby p.Preference_3
                                                   where p.Preference_3 != "None" && p.Preference_3 != null
                                                   select p.Preference_3;

            var preferencesQueryTemp = preferencesQuery1.Union(preferencesQuery2);
            var preferencesQuery = preferencesQuery3.Union(preferencesQueryTemp);

            var partners = from p in _context.Partner
                           select p;
            var preferredPartners = from p in _context.Partner
                                    select p;

            if (FilterActive)
            {
                partners = partners.Where(x => x.Active == true);
                preferredPartners = preferredPartners.Where(x => x.Active == true);
            }

            ViewBag.LastSortParm = String.IsNullOrEmpty(SortOrder) ? "last_desc" : "";
            ViewBag.FirstSortParm = SortOrder == "first" ? "first_desc" : "first";
            ViewBag.PhoneSortParm = SortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.EmailSortParm = SortOrder == "email" ? "email_desc" : "email";
            ViewBag.SkillSortParm = SortOrder == "skill" ? "skill_desc" : "skill";

            switch (SortOrder)
            {
                case "first_desc":
                    partners = partners.OrderByDescending(s => s.First_Name);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.First_Name);
                    break;
                case "first":
                    partners = partners.OrderBy(s => s.First_Name);
                    preferredPartners = preferredPartners.OrderBy(s => s.First_Name);
                    break;
                case "phone_desc":
                    partners = partners.OrderByDescending(s => s.Phone_Number);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Phone_Number);
                    break;
                case "phone":
                    partners = partners.OrderBy(s => s.Phone_Number);
                    preferredPartners = preferredPartners.OrderBy(s => s.Phone_Number);
                    break;
                case "email_desc":
                    partners = partners.OrderByDescending(s => s.Email);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Email);
                    break;
                case "email":
                    partners = partners.OrderBy(s => s.Email);
                    preferredPartners = preferredPartners.OrderBy(s => s.Email);
                    break;
                case "skill_desc":
                    partners = partners.OrderByDescending(s => s.Skill_3);
                    partners = partners.OrderByDescending(s => s.Skill_2);
                    partners = partners.OrderByDescending(s => s.Skill_1);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Skill_3);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Skill_2);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Skill_1);
                    break;
                case "skill":
                    partners = partners.OrderBy(s => s.Skill_3);
                    partners = partners.OrderBy(s => s.Skill_2);
                    partners = partners.OrderBy(s => s.Skill_1);
                    preferredPartners = preferredPartners.OrderBy(s => s.Skill_3);
                    preferredPartners = preferredPartners.OrderBy(s => s.Skill_2);
                    preferredPartners = preferredPartners.OrderBy(s => s.Skill_1);
                    break;
                case "last_desc":
                    partners = partners.OrderByDescending(s => s.Last_Name);
                    preferredPartners = preferredPartners.OrderByDescending(s => s.Last_Name);
                    break;
                default:
                    partners = partners.OrderBy(s => s.Last_Name);
                    preferredPartners = preferredPartners.OrderBy(s => s.Last_Name);
                    break;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                partners = partners.Where(s => s.Last_Name.Contains(SearchString));
                preferredPartners = preferredPartners.Where(s => s.Last_Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(PartnerSkill1))
            {
                partners = partners.Where(x => x.Skill_1 == PartnerSkill1 
                                            || x.Skill_2 == PartnerSkill1 
                                            || x.Skill_3 == PartnerSkill1);
                preferredPartners = preferredPartners.Where(x => x.Skill_1 == PartnerSkill1
                                            || x.Skill_2 == PartnerSkill1
                                            || x.Skill_3 == PartnerSkill1);
            }

            if (!string.IsNullOrEmpty(PartnerSkill2))
            {
                partners = partners.Where(x => x.Skill_1 == PartnerSkill2
                                            || x.Skill_2 == PartnerSkill2
                                            || x.Skill_3 == PartnerSkill2);
                preferredPartners = preferredPartners.Where(x => x.Skill_1 == PartnerSkill2
                                            || x.Skill_2 == PartnerSkill2
                                            || x.Skill_3 == PartnerSkill2);
            }

            if (!string.IsNullOrEmpty(PartnerSkill3))
            {
                partners = partners.Where(x => x.Skill_1 == PartnerSkill3
                                            || x.Skill_2 == PartnerSkill3
                                            || x.Skill_3 == PartnerSkill3);
                preferredPartners = preferredPartners.Where(x => x.Skill_1 == PartnerSkill3
                                            || x.Skill_2 == PartnerSkill3
                                            || x.Skill_3 == PartnerSkill3);
            }

            if (!string.IsNullOrEmpty(PartnerPreference1))
            {
                preferredPartners = preferredPartners.Where(x => x.Preference_1 == PartnerPreference1
                                                              || x.Preference_2 == PartnerPreference1
                                                              || x.Preference_3 == PartnerPreference1);
            }

            if (!string.IsNullOrEmpty(PartnerPreference2))
            {
                preferredPartners = preferredPartners.Where(x => x.Preference_1 == PartnerPreference2
                                                              || x.Preference_2 == PartnerPreference2
                                                              || x.Preference_3 == PartnerPreference2);
            }

            if (!string.IsNullOrEmpty(PartnerPreference3))
            {
                preferredPartners = preferredPartners.Where(x => x.Preference_1 == PartnerPreference3
                                                              || x.Preference_2 == PartnerPreference3
                                                              || x.Preference_3 == PartnerPreference3);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (string.IsNullOrEmpty(PartnerPreference1) && string.IsNullOrEmpty(PartnerPreference2) && string.IsNullOrEmpty(PartnerPreference3))
            {
                var partnerSkillVM = new PartnerSkillViewModel
                {
                    Skills = new SelectList(await skillsQuery.Distinct().ToListAsync()),
                    Preferences = new SelectList(await preferencesQuery.Distinct().ToListAsync()),
                    Partners = partners.ToPagedList(pageNumber, pageSize)
                };
                return View(partnerSkillVM);
            }
            else
            {
                partners = partners.Where(x => x.Preference_1 == "None" &&
                                               (x.Preference_2 == "None" || x.Preference_2 == null) &&
                                               (x.Preference_3 == "None" || x.Preference_3 == null));
                var partnersList = partners.ToList();
                var preferredList = preferredPartners.ToList();
                preferredList.AddRange(partnersList);
                var partnerSkillVM = new PartnerSkillViewModel
                {
                    Skills = new SelectList(await skillsQuery.Distinct().ToListAsync()),
                    Preferences = new SelectList(await preferencesQuery.Distinct().ToListAsync()),
                    Partners = preferredList.ToPagedList(pageNumber, pageSize)
                };
                return View(partnerSkillVM);
            }
        }


        [Authorize(Policy = "adminpartnerguestpolicy")]
        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            if (User.IsInRole("Guest")) // if the logged in user is a guest, then authorize looking at details if theirs
            {
                if (currentUserId != partner.OwnerId)
                {
                    return NotFound();
                }
            }

            return View(partner);
        }

        private IEnumerable<string> GetAllSkills()
        {
            return new List<String>
            {
                "Facilitation",
                "Finance",
                "Fundraising",
                "Human Resources",
                "Leadership",
                "Marketing",
                "Nonprofit Management",
                "Sales",
                "Strategy",
                "Technology/Web",
                "Other"
            };
        }

        private IEnumerable<String> GetAllPreferences()
        {
            return new List<string>
            {
                "Education",
                "Environment",
                "Arts & Culture",
                "Food Insecurity",
                "Health & Wellness",
                "Youth Engagement",
                "Workforce Development",
                "Community Development",
                "Housing",
                "Advocacy & Social Justice",
                "Social Services",
                "None"
            };
        }

        [Authorize(Policy = "adminpartnerguestpolicy")]
        // GET: Partners/Create
        public IActionResult Create()
        {
            var skills = GetAllSkills();
            ViewData["skillsList"] = skills;
            var preferences = GetAllPreferences();
            ViewData["preferences"] = preferences;
            return View();
        }

        [Authorize(Policy = "adminpartnerguestpolicy")]
        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId,First_Name,Last_Name,Phone_Number,Street_Address,City,State,Zip_Code,Email,Twitter_Id,Linkedin_Id,Employer,Job_Title,Bio,Skill_1,Skill_2,Skill_3,Preference_1,Preference_2,Preference_3,Active")] Partner partner)
        {
            partner.OwnerId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (!this.User.IsInRole("Admin"))
            {
                partner.Active = false;
            }
            if (ModelState.IsValid)
            {
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        [Authorize(Policy = "adminpartnerguestpolicy")]
        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var skills = GetAllSkills();
            var preferences = GetAllPreferences();
            ViewData["skillsList"] = skills;
            ViewData["preferences"] = preferences;
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (partner == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Partner") | User.IsInRole("Guest")) // if the logged in user is a partner or guest, then authorize looking at details 
            {
                if (currentUserId != partner.OwnerId)
                {
                    return NotFound();
                }
            }
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "adminpartnerguestpolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,First_Name,Last_Name,Phone_Number,Street_Address,City,State,Zip_Code,Email,Twitter_Id,Linkedin_Id,Employer,Job_Title,Bio,Skill_1,Skill_2,Skill_3,Preference_1,Preference_2,Preference_3,Active")] Partner partner)
        {
            if (id != partner.Id)
            {
                return NotFound();
            }

            var isAdmin = User.IsInRole("Admin");
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (!isAdmin & currentUserId != partner.OwnerId) // if not admin(aka are a partner) and id doesn't match, error
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        [Authorize(Policy = "adminonlypolicy")]
        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [Authorize(Policy = "adminonlypolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partner.FindAsync(id);
            _context.Partner.Remove(partner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.Partner.Any(e => e.Id == id);
        }
    }
}
