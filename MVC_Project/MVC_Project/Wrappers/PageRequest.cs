namespace MVC_Project.Wrappers
{
    public class PageRequest<T>
    {
        public string SortDirection { get; set; } = "ASC";

        public string SortColumn { get; set; } = "";

        public int Size { get; set; } = 10;

        public int Page { get; set; } = 1;

        public string FilterString { get; set; }

        public Dictionary<string, string> FilterMap { get; set; }
    }
}
