using ApiServices.Models;
using ApiServices.Models.User;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AccountController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var login = new LoginViewModel();
                return View(login);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            ResponseDto responseDto = await _authService.LoginAsync(login);
            if (responseDto != null && responseDto.IsSuccess)
            {
                var loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(login);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var register = new RegisterViewModel();
                return View(register);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            ResponseDto result = await _authService.RegisterAsync(register);

            if (result != null && result.IsSuccess)
            {
                return RedirectToAction(nameof(Login));
            }
            return View(register);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction(nameof(Login));
        }

        private async Task SignInUser(LoginResponseModel loginResponse)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = jwtTokenHandler.ReadJwtToken(loginResponse.Token);

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            foreach (var role in jwtToken.Claims)
            {
                if (role.Type == ClaimTypes.Role)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Value));
                }
            }
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
        }
    }
}
