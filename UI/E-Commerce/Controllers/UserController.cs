using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class UserController() : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // [HttpGet]
        // public async Task<ActionResult> UpdateUser()
        // {
        //     var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        //     var response = await userService.GetUserAsync(userId);
        //     if (response != null && response.IsSuccess)
        //     {
        //         var user = JsonConvert.DeserializeObject<UserViewModel>(Convert.ToString(response.Result));
        //         return View("_PersonalInfo", user);
        //     }
        //     TempData["Message"] = response.Message;
        //     TempData["MessageType"] = "error";
        //     return View(nameof(Index));
        // }
    }
}