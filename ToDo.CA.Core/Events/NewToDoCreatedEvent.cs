using MediatR;

namespace ToDo.CA.Core.Events
{
    public class NewToDoCreatedEvent : INotification
    {
        private readonly string toDoName;

        public NewToDoCreatedEvent(string toDoName)
        {
            this.toDoName = toDoName;
        }
    }
}