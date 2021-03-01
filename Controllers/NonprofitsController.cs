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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using X.PagedList;

namespace SVPP.Controllers
{
    public class NonprofitsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NonprofitsController(DatabaseContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        [Authorize(Policy = "loggedinpolicy")]
        //GET: Nonprofits
        public async Task<IActionResult> Index(string NonprofitSkill1, string NonprofitSkill2, string NonprofitSkill3, 
                                               string NonprofitFocus1, string NonprofitFocus2, string NonprofitFocus3, 
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

            IQueryable<string> skillsQuery1 = from p in _context.Nonprofit
                                              orderby p.Skill_1
                                              select p.Skill_1;

            IQueryable<string> skillsQuery2 = from p in _context.Nonprofit
                                              orderby p.Skill_2
                                              where p.Skill_2 != null
                                              select p.Skill_2;

            IQueryable<string> skillsQuery3 = from p in _context.Nonprofit
                                              orderby p.Skill_3
                                              where p.Skill_3 != null
                                              select p.Skill_3;

            var skillsQueryTemp = skillsQuery1.Union(skillsQuery2);
            var skillsQuery = skillsQuery3.Union(skillsQueryTemp);

            IQueryable<string> focusesQuery1 = from p in _context.Nonprofit
                                               orderby p.Focus_Area1
                                               where p.Focus_Area1 != "None"
                                               select p.Focus_Area1;

            IQueryable<string> focusesQuery2 = from p in _context.Nonprofit
                                               orderby p.Focus_Area2
                                               where p.Focus_Area2 != "None" && p.Focus_Area2 != null
                                               select p.Focus_Area2;

            IQueryable<string> focusesQuery3 = from p in _context.Nonprofit
                                               orderby p.Focus_Area3
                                               where p.Focus_Area3 != "None" && p.Focus_Area3 != null
                                               select p.Focus_Area3;

            var focusesQueryTemp = focusesQuery1.Union(focusesQuery2);
            var focusesQuery = focusesQuery3.Union(focusesQueryTemp);


            var nonprofits = from p in _context.Nonprofit
                             select p;
            var preferredNonprofits = from p in _context.Nonprofit
                                      select p;

            if (FilterActive)
            {
                nonprofits = nonprofits.Where(x => x.Active == true);
                preferredNonprofits = preferredNonprofits.Where(x => x.Active == true);
            }

            ViewBag.OrgSortParm = String.IsNullOrEmpty(SortOrder) ? "org_desc" : "";
            ViewBag.NameSortParm = SortOrder == "name" ? "name_desc" : "name";
            ViewBag.PhoneSortParm = SortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.SkillSortParm = SortOrder == "skill" ? "skill_desc" : "skill";

            switch (SortOrder)
            {
                case "skill_desc":
                    nonprofits = nonprofits.OrderByDescending(s => s.Skill_3);
                    nonprofits = nonprofits.OrderByDescending(s => s.Skill_2);
                    nonprofits = nonprofits.OrderByDescending(s => s.Skill_1);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Skill_3);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Skill_2);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Skill_1);
                    break;
                case "skill":
                    nonprofits = nonprofits.OrderBy(s => s.Skill_3);
                    nonprofits = nonprofits.OrderBy(s => s.Skill_2);
                    nonprofits = nonprofits.OrderBy(s => s.Skill_1);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Skill_3);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Skill_2);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Skill_1);
                    break;
                case "email_desc":
                    nonprofits = nonprofits.OrderByDescending(s => s.Contact_Email);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Contact_Email);
                    break;
                case "email":
                    nonprofits = nonprofits.OrderBy(s => s.Contact_Email);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Contact_Email);
                    break;
                case "name_desc":
                    nonprofits = nonprofits.OrderByDescending(s => s.Contact_Name);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Contact_Name);
                    break;
                case "name":
                    nonprofits = nonprofits.OrderBy(s => s.Contact_Name);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Contact_Name);
                    break;
                case "org_desc":
                    nonprofits = nonprofits.OrderByDescending(s => s.Organization_Name);
                    preferredNonprofits = preferredNonprofits.OrderByDescending(s => s.Organization_Name);
                    break;
                default:
                    nonprofits = nonprofits.OrderBy(s => s.Organization_Name);
                    preferredNonprofits = preferredNonprofits.OrderBy(s => s.Organization_Name);
                    break;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                nonprofits = nonprofits.Where(s => s.Organization_Name.Contains(SearchString));
                preferredNonprofits = preferredNonprofits.Where(s => s.Organization_Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(NonprofitSkill1))
            {
                nonprofits = nonprofits.Where(x => x.Skill_1 == NonprofitSkill1
                                            || x.Skill_2 == NonprofitSkill1
                                            || x.Skill_3 == NonprofitSkill1);
                preferredNonprofits = preferredNonprofits.Where(x => x.Skill_1 == NonprofitSkill1
                                            || x.Skill_2 == NonprofitSkill1
                                            || x.Skill_3 == NonprofitSkill1);
            }

            if (!string.IsNullOrEmpty(NonprofitSkill2))
            {
                nonprofits = nonprofits.Where(x => x.Skill_1 == NonprofitSkill2
                                            || x.Skill_2 == NonprofitSkill2
                                            || x.Skill_3 == NonprofitSkill2);
                preferredNonprofits = preferredNonprofits.Where(x => x.Skill_1 == NonprofitSkill2
                                            || x.Skill_2 == NonprofitSkill2
                                            || x.Skill_3 == NonprofitSkill2);
            }

            if (!string.IsNullOrEmpty(NonprofitSkill3))
            {
                nonprofits = nonprofits.Where(x => x.Skill_1 == NonprofitSkill3
                                            || x.Skill_2 == NonprofitSkill3
                                            || x.Skill_3 == NonprofitSkill3);
                preferredNonprofits = preferredNonprofits.Where(x => x.Skill_1 == NonprofitSkill2
                                            || x.Skill_2 == NonprofitSkill2
                                            || x.Skill_3 == NonprofitSkill2);
            }

            if (!string.IsNullOrEmpty(NonprofitFocus1))
            {
                preferredNonprofits = preferredNonprofits.Where(x => x.Focus_Area1 == NonprofitFocus1
                                                              || x.Focus_Area2 == NonprofitFocus1
                                                              || x.Focus_Area3 == NonprofitFocus1);
            }

            if (!string.IsNullOrEmpty(NonprofitFocus2))
            {
                preferredNonprofits = preferredNonprofits.Where(x => x.Focus_Area1 == NonprofitFocus2
                                                              || x.Focus_Area2 == NonprofitFocus2
                                                              || x.Focus_Area3 == NonprofitFocus2);
            }

            if (!string.IsNullOrEmpty(NonprofitFocus3))
            {
                preferredNonprofits = preferredNonprofits.Where(x => x.Focus_Area1 == NonprofitFocus3
                                                              || x.Focus_Area2 == NonprofitFocus3
                                                              || x.Focus_Area3 == NonprofitFocus3);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (string.IsNullOrEmpty(NonprofitFocus1) && string.IsNullOrEmpty(NonprofitFocus2) && string.IsNullOrEmpty(NonprofitFocus3))
            {
                var nonprofitSkillVM = new NonprofitSkillViewModel
                {
                    Skills = new SelectList(await skillsQuery.Distinct().ToListAsync()),
                    Focus_Areas = new SelectList(await focusesQuery.Distinct().ToListAsync()),
                    Nonprofits = nonprofits.ToPagedList(pageNumber, pageSize)
                };
                return View(nonprofitSkillVM);
            }
            else
            {
                nonprofits = nonprofits.Where(x => x.Focus_Area1 == "None" &&
                                                  (x.Focus_Area2 == "None" || x.Focus_Area2 == null) &&
                                                  (x.Focus_Area3 == "None" || x.Focus_Area3 == null));
                var nonprofitsList = nonprofits.ToList();
                var preferredList = preferredNonprofits.ToList();
                preferredList.AddRange(nonprofitsList);
                var nonprofitSkillVM = new NonprofitSkillViewModel
                {
                    Skills = new SelectList(await skillsQuery.Distinct().ToListAsync()),
                    Focus_Areas = new SelectList(await focusesQuery.Distinct().ToListAsync()),
                    Nonprofits = preferredList.ToPagedList(pageNumber, pageSize)
                };
                return View(nonprofitSkillVM);
            }
        }

        [Authorize(Policy = "loggedinpolicy")]
        // GET: Nonprofits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonprofit = await _context.Nonprofit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nonprofit == null)
            {
                return NotFound();
            }

            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            if (User.IsInRole("Nonprofit")) // if the logged in user is a guest, then authorize looking at details if theirs
            {
                if (currentUserId != nonprofit.OwnerId)
                {
                    return NotFound();
                }
            }
            return View(nonprofit);
        }

        [Authorize(Policy = "adminandnonprofitpolicy")]
        // GET: Nonprofits/Create
        public IActionResult Create()
        {
            var skills = GetAllSkills();
            ViewData["skillsList"] = skills;
            var preferences = GetAllPreferences();
            ViewData["preferences"] = preferences;
            return View();
        }

        [Authorize(Policy = "adminandnonprofitpolicy")]
        // POST: Nonprofits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId,Organization_Name,Focus_Area1,Focus_Area2,Focus_Area3,Date_Founded,Phone_Number,Street_Address,City,State,Zip_Code,Website,Executive_Name,Contact_Name,Contact_Title,Contact_Email,Contact_Phone,Facebook_Id,Twitter_Id,Instagram_Id,Mission,Programs,Challenges,Tax_Status,Tax_Id,Skill_1,Skill_2,Skill_3,Active")] Nonprofit nonprofit)
        {
            nonprofit.OwnerId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (!this.User.IsInRole("Admin"))
            {
                nonprofit.Active = false;
            }
            if (ModelState.IsValid)
            {
                _context.Add(nonprofit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nonprofit);
        }

        [Authorize(Policy = "adminandnonprofitpolicy")]
        // GET: Nonprofits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var skills = GetAllSkills();
            ViewData["skillsList"] = skills;
            var preferences = GetAllPreferences();
            ViewData["preferences"] = preferences;
            if (id == null)
            {
                return NotFound();
            }

            var nonprofit = await _context.Nonprofit.FindAsync(id);
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (nonprofit == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Nonprofit"))
            {
                if (currentUserId != nonprofit.OwnerId)
                {
                    return NotFound();
                }
            }
            return View(nonprofit);
        }

        [Authorize(Policy = "adminandnonprofitpolicy")]
        // POST: Nonprofits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,Organization_Name,Focus_Area1,Focus_Area2,Focus_Area3,Date_Founded,Phone_Number,Street_Address,City,State,Zip_Code,Website,Executive_Name,Contact_Name,Contact_Title,Contact_Email,Contact_Phone,Facebook_Id,Twitter_Id,Instagram_Id,Mission,Programs,Challenges,Tax_Status,Tax_Id,Skill_1,Skill_2,Skill_3,Active")] Nonprofit nonprofit)
        {
            if (id != nonprofit.Id)
            {
                return NotFound();
            }

            var isAdmin = User.IsInRole("Admin");
            var currentUserId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (!isAdmin & currentUserId != nonprofit.OwnerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonprofit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonprofitExists(nonprofit.Id))
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
            return View(nonprofit);
        }

        [Authorize(Policy = "adminonlypolicy")]
        // GET: Nonprofits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonprofit = await _context.Nonprofit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nonprofit == null)
            {
                return NotFound();
            }

            return View(nonprofit);
        }

        // POST: Nonprofits/Delete/5
        [Authorize(Policy = "adminonlypolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nonprofit = await _context.Nonprofit.FindAsync(id);
            _context.Nonprofit.Remove(nonprofit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonprofitExists(int id)
        {
            return _context.Nonprofit.Any(e => e.Id == id);
        }
    }
}
