using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.CA.Core.Dtos;
using ToDo.CA.Core.Dtos.Requests;
using ToDo.CA.Core.Events;
using ToDo.CA.Core.Interfaces;

namespace ToDo.CA.Core.Services.ToDoUseCases
{
    public class CreateToDoHandler : IRequestHandler<CreateToDoRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<Models.ToDo> repository;
        private readonly ILogger<CreateToDoHandler> logger;
        private readonly IMediator mediator;

        public CreateToDoHandler(IRepository<Models.ToDo> repository, ILogger<CreateToDoHandler> logger, IMediator mediator)
        {
            this.repository = repository;
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task<BaseResponseDto<bool>> Handle(CreateToDoRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();

            try
            {
                var toDo = new Models.ToDo
                {
                    Name = request.Name,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                };

                await repository.CreateAsync(toDo);

                response.Data = true;

                await mediator.Publish(new NewToDoCreatedEvent(toDoName: toDo.Name));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the ToDo.");
            }

            return response;
        }
    }
}