namespace TaskManagmentSystemAPI.Models.Dto
{
    public class TaskCreateDTO        //Data Transfer Objects - exchanging information between different layers of an application
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate = DateTime.Now;

    }
}
