using MediatR;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class DeleteRoleCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<DeleteRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var isDeleted = await identityRepository.DeleteRoleAsync(request.RoleId);
            if (!isDeleted)
            {
                response.IsSuccess = false;
                response.Message = "Role has not been deleted.";
                return response;
            }
            response.Result = true;
            response.Message = "Role deleted successfully.";
            return response;
        }
    }
}