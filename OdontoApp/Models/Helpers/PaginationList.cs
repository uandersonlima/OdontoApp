using System.Collections.Generic;


namespace OdontoApp.Models.Helpers
{
    public class PaginationList<T> : List<T> where T : class
    {
        public Pagination Pagination { get; set; }
    }
}
