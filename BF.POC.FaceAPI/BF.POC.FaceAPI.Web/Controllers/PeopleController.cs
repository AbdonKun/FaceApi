using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Controllers
{
    public class PeopleController : BaseController
    {
        private readonly IGroupManager groupManager;
        private readonly IPersonManager personManager;

        public PeopleController(IGroupManager groupManager, IPersonManager personManager)
        {
            this.groupManager = groupManager;
            this.personManager = personManager;
        }

        public ActionResult Index(int? groupId = null)
        {
            var personListViewModel = new PersonListViewModel()
            {
                List = groupId.HasValue ?
                    personManager.GetAllByGroupId(groupId.Value).Select(person => new PersonViewModel(person)) :
                    personManager.GetAll().Select(person => new PersonViewModel(person))
            };

            return View(personListViewModel);
        }

        public ActionResult Add(int? groupId = null)
        {
            var personViewModel = new PersonViewModel()
            {
                GroupList = FillGroupList(groupId)
            };

            return View(personViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Add(PersonViewModel personViewModel, int? groupId = null)
        {
            if (ModelState.IsValid)
            {
                await personManager.AddAsync(personViewModel.ToEntity());
                return RedirectToAction("Index", new { groupId = groupId });
            }

            personViewModel.GroupList = FillGroupList(groupId);

            return View(personViewModel);
        }

        public ActionResult Edit(int id, int? groupId = null)
        {
            var personViewModel = new PersonViewModel(personManager.GetById(id));
            personViewModel.GroupList = FillGroupList(personViewModel.GroupId);

            return View(personViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(PersonViewModel personViewModel, int? groupId = null)
        {
            if (ModelState.IsValid)
            {
                await personManager.UpdateAsync(personViewModel.ToEntity());
                return RedirectToAction("Index", new { groupId = groupId });
            }

            personViewModel.GroupList = FillGroupList(personViewModel.GroupId);

            return View(personViewModel);
        }

        private IEnumerable<SelectListItem> FillGroupList(int? groupId)
        {
            if (groupId.HasValue && groupId.Value > 0)
            {
                var group = groupManager.GetById(groupId.Value);

                return new List<SelectListItem>() {
                    new SelectListItem()
                    {
                        Value = group.Id.ToString(),
                        Text = group.Name,
                        Selected = true,
                    }
                };
            }
            else
            {
                return groupManager.GetAll()
                    .Select(group => new SelectListItem()
                    {
                        Value = group.Id.ToString(),
                        Text = group.Name,
                        Selected = group.Id == groupId,
                    });
            }
        }
    }
}