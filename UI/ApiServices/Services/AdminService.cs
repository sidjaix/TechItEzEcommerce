using ApiServices.Services.IService;

namespace ApiServices.Services;

public class AdminService(IBaseService baseService) : IAdminService
{

}
