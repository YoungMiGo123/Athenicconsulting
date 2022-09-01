namespace AthenicConsulting.Office.Office.ViewModels
{
    public class DashboardViewModel
    {
        public bool ShouldDisplayMessage => !string.IsNullOrEmpty(Message);
        public string Message { get; set; } 
    }
}
