using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.AppServices;
using EndPoint.MVC.WebFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace EndPoint.MVC.Controllers
{
    [Authorize]
    public class MyTaskController : Controller
    {
        public IMyTaskAppService _myTaskAppservice;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<SiteSetting> _siteSetting;
        public MyTaskController(IMyTaskAppService myTaskAppservice, UserManager<User> userManager,
            IOptions<SiteSetting> siteSetting)
        {
            _myTaskAppservice = myTaskAppservice;
            _userManager = userManager;
            _siteSetting = siteSetting;
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> TaskList(CancellationToken cancellationToken)
        {
            var userid = _userManager.GetUserId(User);
            var model = await _myTaskAppservice.GetAll(int.Parse(userid), cancellationToken);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(MyTask task, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var maxIncomplete = await _myTaskAppservice.GetIncompleteTaskCount(int.Parse(userId), cancellationToken);


            if (maxIncomplete >= _siteSetting.Value.MaxInCompleteTasks)
            {
                ModelState.AddModelError("", $"شما نمی‌توانید بیش از {_siteSetting.Value.MaxInCompleteTasks} تسک ناتمام داشته باشید. خودتو جمع و جور کن!");
                return View(task);
            }


            await _myTaskAppservice.Create(task, int.Parse(userId), cancellationToken);

            return RedirectToAction("TaskList");
        }


        [HttpPost]
        public async Task<IActionResult> CompleteTask(int taskid, CancellationToken cancellationToken)
        {

            var userId = int.Parse(_userManager.GetUserId(User));

            var task = await _myTaskAppservice.Get(userId, cancellationToken);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = true;
            await _myTaskAppservice.MarkAsCompleted(taskid, cancellationToken);

            return RedirectToAction("TaskList");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int taskid, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_userManager.GetUserId(User));

            var task = await _myTaskAppservice.Get(taskid, cancellationToken);
            if (task == null)
            {
                return NotFound();
            }

            await _myTaskAppservice.Delete(taskid, cancellationToken);

            return RedirectToAction("TaskList");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int taskid, CancellationToken cancellationToken)
        {
            var task = await _myTaskAppservice.Get(taskid, cancellationToken);
            await _myTaskAppservice.Update(task, cancellationToken);
            return RedirectToAction("TaskList");

        }
    }

}
