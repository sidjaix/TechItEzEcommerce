using MediatR;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class CreateRoleCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<CreateRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var hasCreated = await identityRepository.CreateRoleAsync(request.Role);
            if (!hasCreated)
            {
                response.Message = "Role has not created.";
                response.IsSuccess = false;
                return response;
            }

            response.Message = "Role created successfully.";
            return response;
        }
    }
}