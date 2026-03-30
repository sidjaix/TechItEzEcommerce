using App_Contracts.Common;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.Interfaces;

namespace Order_Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderRepository orderRepository, ResponseDto response, IPublishEndpoint publishEndpoint) : ControllerBase
    {

    }
}
