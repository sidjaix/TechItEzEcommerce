using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var users = await userRepository.GetUsersAsync();
            response.Result = users;
            return response;
        }
    }
}