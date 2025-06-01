using Microsoft.AspNetCore.Mvc;
using static Domain.Constant.Constants;

namespace Domain.Common
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult OkResponse<T>(T? data)
        {
            //BaseResponse<T> rs = new(
            //    statusCode: StatusCodeHelper.OK,
            //    code: ResponseCodeConstants.SUCCESS,
            //    data: data,
            //    message: MessagesConstants.SUCCESS
            //);
            //return base.Ok(rs);
            return Ok(new
            {
                statusCode = 200,
                code = ResponseCodeConstants.SUCCESS,
                data = data,
                message = MessagesConstants.SUCCESS
            });
        }
    }
}
