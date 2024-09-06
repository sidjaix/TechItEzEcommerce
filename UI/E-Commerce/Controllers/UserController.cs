using ApiServices.Models.User;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class UserController(IUserService userService) : Controller
    {
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            UserPageViewModel user = await GetUserPageInfo(userService);
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress(AddressViewModel addressInfo)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await userService.CreateAddress(addressInfo);
                if (isCreated)
                {
                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Address created successfully";
                }
                else
                {
                    TempData["MessageType"] = "error";
                    TempData["Message"] = "Address has not created";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> DeleteAddress(int addressId)
        {
            if (addressId > 0)
            {
                var isDeleted = await userService.DeleteAddressAsync(addressId);
                if (isDeleted)
                {
                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Address deleted successfully";
                }
                else
                {
                    TempData["MessageType"] = "error";
                    TempData["Message"] = "Address has not deleted";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserProfile(UserViewModel userDetail)
        {
            if (ModelState.IsValid)
            {
                var response = await userService.UpdateUserAsync(userDetail);
                if (response != null && response.IsSuccess)
                {
                    TempData["Message"] = response.Message;
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = response.Message;
                    TempData["MessageType"] = "error";
                }
            }
            else
            {
                TempData["Message"] = "Invalid data";
                TempData["MessageType"] = "error";

                // If model state is invalid, return the partial view with the model (including validation errors)
                return PartialView("_PersonalInfo", userDetail);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<UserPageViewModel> GetUserPageInfo(IUserService userService)
        {
            var user = new UserPageViewModel();
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            user.PersonalInfo = await userService.GetUserAsync(userId);
            user.Addresses = await userService.GetUserAddresses(userId);
            user.Address.UserId = userId;
            return user;
        }
    }
}