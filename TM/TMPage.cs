using System;
using System.Linq;
using System.Collections.Generic;

namespace TM.Page
{
    public class Pages
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int RowIndex { get; set; }
        public int TotalRow { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<dynamic> query { get; set; }
        public Pages() { }
        public Pages(IEnumerable<dynamic> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            this.query = query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
        public List<dynamic> ToList()
        {
            return query.ToList();
        }

        public IEnumerable<T> PagesAnonymous<T>(IEnumerable<T> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            return query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }

        public List<T> ToList<T>(IEnumerable<T> query)
        {
            return PagesAnonymous(query).ToList();
        }

        //public IEnumerable<dynamic> DapperPage(IEnumerable<dynamic> query, int PageNumber, int PageSize)
        //{
        //    this.PageNumber = PageNumber;
        //    this.PageSize = PageSize;
        //    this.TotalRow = query.Count();
        //    this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
        //    return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        //}
        public string getRowIndexStr(Int32 index)
        {
            index = (index + (this.PageNumber - 1) * this.PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public int getRowIndex(int index) { return Convert.ToInt32(getRowIndexStr(index)); }
        //public string PaginationList(int links, string linkPage)
        //{
        //    int start = ((this.PageNumber - links) > 0) ? this.PageNumber - links : 1;
        //    int end = ((this.PageNumber + links) < this.TotalPage) ? this.PageNumber + links : this.TotalPage;
        //    string html = "<div class=\"pagination-container\"><ul class=\"pagination\">";
        //    string href = TM.Url.GetBaseHost() + linkPage;
        //    if (this.PageNumber > 1)
        //    {
        //        //html += "<li><a href=\"?page=1\">&laquo;&laquo;</a></li>";
        //        html += "<li><a href=\"" + ReplacePage(href, this.PageNumber - 1) + "\">&laquo;</a></li>";
        //    }

        //    if (start > 1)
        //    {
        //        html += "<li><a href=\"" + ReplacePage(href, 1) + "\">1</a></li>";
        //        html += "<li class=\"disabled\"><span>...</span></li>";
        //    }

        //    for (int i = start; i <= end; i++)
        //    {
        //        string active = this.PageNumber == i ? "active" : "";
        //        html += "<li class=\"" + active + "\"><a href=\"" + ReplacePage(href, i) + "\">" + i + "</a></li>";
        //    }

        //    if (end < this.TotalPage)
        //    {
        //        html += "<li class=\"disabled\"><span>...</span></li>";
        //        html += "<li><a href=\"" + ReplacePage(href, this.TotalPage) + "\">" + this.TotalPage + "</a></li>";
        //    }

        //    if (this.PageNumber < this.TotalPage)
        //    {
        //        html += "<li><a href=\"" + ReplacePage(href, this.PageNumber + 1) + "\">&raquo;</a></li>";
        //        //html += "<li><a href=\"?page=" + TMDapperPage.TotalPage + "\">&raquo;&raquo;</a></li>";
        //    }
        //    html += "</ul></div>";

        //    return html;
        //}
        private string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
}