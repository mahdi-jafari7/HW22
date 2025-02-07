using _01_Domain._01_Entities;
using _01_Domain._02_Contracts.AppServices;
using EndPoint.MVC.WebFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.MVC.Controllers
{
    //[Authorize]
    public class MyTaskController : Controller
    {
        public IMyTaskAppService _myTaskAppservice;
        private readonly UserManager<User> _userManager;
        public MyTaskController(IMyTaskAppService myTaskAppservice, UserManager<User> userManager)
        {
            _myTaskAppservice = myTaskAppservice;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult TaskList(CancellationToken cancellationToken)
        {
            var userid = _userManager.GetUserId(User);
            _myTaskAppservice.GetAll(int.Parse(userid),cancellationToken);
            return View("TaskList");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(MyTask task, CancellationToken cancellationToken)
        {
            var userid = _userManager.GetUserId(User);
            _myTaskAppservice.Create(task, int.Parse(userid), cancellationToken);
            return RedirectToAction("TaskList");
        }

    }
}
