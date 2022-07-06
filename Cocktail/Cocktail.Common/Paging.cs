namespace Cocktail.Common
{
    public class Paging
    {
        public int Rpp { get; set; }
        public int PageNumber { get; set; }

        public Paging(int rpp, int pageNumber)
        {
            this.Rpp = rpp;
            this.PageNumber = pageNumber;
        }
    }
}
