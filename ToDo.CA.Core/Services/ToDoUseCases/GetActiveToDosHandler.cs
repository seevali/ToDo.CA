using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.CA.Core.Dtos;
using ToDo.CA.Core.Dtos.Requests;
using ToDo.CA.Core.Interfaces;

namespace ToDo.CA.Core.Services.ToDoUseCases
{
    public class GetActiveToDosHandler : IRequestHandler<GetActiveToDosRequest, BaseResponseDto<List<ToDoDto>>>
    {
        private readonly IRepository<Models.ToDo> repository;
        private readonly ILogger<GetActiveToDosHandler> logger;

        public GetActiveToDosHandler(IRepository<Models.ToDo> repository, ILogger<GetActiveToDosHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<BaseResponseDto<List<ToDoDto>>> Handle(GetActiveToDosRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<ToDoDto>> response = new BaseResponseDto<List<ToDoDto>>();

            try
            {
                List<ToDoDto> toDos = (await repository.GetWhereAsync(m => m.IsCompleted == false)).Select(m => new ToDoDto
                {
                    Name = m.Name,
                    IsCompleted = m.IsCompleted
                }).ToList();

                response.Data = toDos;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while getting ToDos.");
            }

            return response;
        }
    }
}