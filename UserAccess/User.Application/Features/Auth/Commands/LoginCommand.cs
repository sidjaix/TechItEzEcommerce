using System.ComponentModel;
using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginResponseModel>
{
	[DefaultValue("admin@tie.com")]
	public string UserName { get; set; } = string.Empty;
	[DefaultValue("Admin@123")]
	public string Password { get; set; } = string.Empty;
}