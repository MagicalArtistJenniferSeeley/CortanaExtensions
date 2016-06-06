using CortanaExtensions.Enums;

namespace CortanaExtensions
{
    public class ListenStatement
    {
        public ListenStatement(string ListenFor, RequireAppName AppNameLocation)
        {
            this.ListenFor = ListenFor;
            this.AppNameLocation = AppNameLocation;
        }
        public string ListenFor { get; set; }
        public RequireAppName AppNameLocation { get; set; }
    }
}
