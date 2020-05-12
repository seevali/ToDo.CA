namespace ToDo.CA.Core.Models
{
    public class ToDo : EntityBase
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}