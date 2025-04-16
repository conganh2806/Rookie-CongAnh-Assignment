namespace ECommerce.Application.Responses
{
    public class Pagination<T>
    {
        private int _perPage { get; set; }
        private int _currentPage { get; set; }
        public Pagination(IEnumerable<T> _data, int perPage, int currentPage)
        {
            data = _data;
            _perPage = perPage;
            _currentPage = currentPage;
            Paginate();
        }
        public IEnumerable<T> data { get; set; }
        public MetaData Meta { get; set; }
        private void Paginate()
        {
            Meta = new MetaData();
            Meta.CurrentPage = _currentPage;
            Meta.PerPage = _perPage;
            Meta.Total = data.Count();
            Meta.Path = "http://localhost/";
            Meta.LastPage = CalMaxPage(Meta.Total);

            data = data.Skip(_perPage * (_currentPage - 1)).Take(_perPage);

            Meta.Count = data.Count();
        }

        private int CalMaxPage(int count)
        {
            return (int)Math.Ceiling((double)count / _perPage);
        }
    }

    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public string Path { get; set; } = default!;
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
    }
}
