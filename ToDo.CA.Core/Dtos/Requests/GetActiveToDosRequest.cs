using System.Collections.Generic;
using MediatR;

namespace ToDo.CA.Core.Dtos.Requests
{
    public class GetActiveToDosRequest : IRequest<BaseResponseDto<List<ToDoDto>>>
    {
        
    }
}