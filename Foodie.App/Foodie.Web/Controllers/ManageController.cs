using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Foodie.Web.Models;
using Foodie.Web.Services;
using Foodie.Web.Repositories;
using System.Collections.Generic;

namespace Foodie.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService userService;

        public ManageController()
        {
            this.userService = new UserService(new UserRepository());
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.userService = new UserService(new UserRepository());
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = await this.userService.GetUserById(userId);

            return View(user);
        }

        // POST: /Manage/Index
        [HttpPost] 
        public async Task<ActionResult> Index(ApplicationUser user) 
        {
            string userId = User.Identity.GetUserId();
            user.Id = userId;
            ApplicationUser updatedUser =  await this.userService.UpdateUser(user);
            return View(updatedUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}