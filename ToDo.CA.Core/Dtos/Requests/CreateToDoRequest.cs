using MediatR;

namespace ToDo.CA.Core.Dtos.Requests
{
    public class CreateToDoRequest : IRequest<BaseResponseDto<bool>>
    {
        public string Name { get; set; }
    }
}