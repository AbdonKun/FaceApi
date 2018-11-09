using BF.POC.FaceAPI.Domain.Contracts;
using BF.POC.FaceAPI.Domain.Exceptions;
using BF.POC.FaceAPI.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BF.POC.FaceAPI.Web.Controllers
{
    public class GroupsController : BaseController
    {
        private readonly IGroupManager groupManager;

        public GroupsController(IGroupManager groupManager)
        {
            this.groupManager = groupManager;
        }

        public ActionResult Index()
        {
            var groupListViewModel = new GroupListViewModel()
            {
                List = groupManager.GetAll().Select(group => new GroupViewModel(group))
            };

            return View(groupListViewModel);
        }

        public ActionResult Search(int id)
        {
            var groupViewModel = new GroupViewModel(groupManager.GetById(id));

            return View(groupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> DoSearch(int id)
        {
            try
            {
                var image = Convert.FromBase64String(System.Web.HttpContext.Current.Request.Form["IMAGE"]);

                var response = await groupManager.SearchCandidatesAsync(id, image);

                return Json(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public ActionResult Add()
        {
            var groupViewModel = new GroupViewModel();

            return View(groupViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Add(GroupViewModel groupViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await groupManager.AddAsync(groupViewModel.ToEntity());
                    return RedirectToAction("Index");
                }

                return View(groupViewModel);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(groupViewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            var groupViewModel = new GroupViewModel(groupManager.GetById(id));

            return View(groupViewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(GroupViewModel groupViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await groupManager.UpdateAsync(groupViewModel.ToEntity());
                    return RedirectToAction("Index");
                }

                return View(groupViewModel);
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(groupViewModel);
            }
        }
    }
}