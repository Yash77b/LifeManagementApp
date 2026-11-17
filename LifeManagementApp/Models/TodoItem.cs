namespace LifeManagementApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int Priority { get; set; } = 3;
        public DateTime DueDate { get; set; } = DateTime.Today;
        public bool IsDone { get; set; }
    }
}