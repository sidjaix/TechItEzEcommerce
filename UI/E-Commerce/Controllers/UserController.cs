using ApiServices.Models.User;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IWishlistService wishlistService;

        public UserController(IUserService userService, IOrderService orderService, IWishlistService wishlistService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.wishlistService = wishlistService;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            UserPageViewModel user = await GetUserPageInfo(userService);
            return View(user);
        }

        public async Task<ActionResult> GetAddress(int addressId)
        {
            var address = await userService.GetAddressAsync(addressId);
            if (address == null)
            {
                TempData["MessageType"] = "warning";
                TempData["Message"] = "Address not found";
            }
            return Json(address);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAddress(AddressViewModel addressInfo)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await userService.SaveAddressAsync(addressInfo);
                if (isSuccess)
                {
                    TempData["MessageType"] = "success";
                    TempData["Message"] = "Address saved successfully";
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

        public async Task<ActionResult> GetOrderDetails(int orderId)
        {
            var order = await orderService.GetOrderDetail(orderId);
            return PartialView("_OrderDetails", order);
        }

        private async Task<UserPageViewModel> GetUserPageInfo(IUserService userService)
        {
            var user = new UserPageViewModel();
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            user.PersonalInfo = await userService.GetUserAsync(userId);
            user.Addresses = await userService.GetUserAddresses(userId);
            user.Orders = await orderService.GetOrdersAsync();
            user.Wishlist = await wishlistService.GetWishlistItemsAsync(userId);
            user.Address.UserId = userId;
            return user;
        }
    }
}