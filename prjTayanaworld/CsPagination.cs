using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace prjTayanaworld
{
    public class CsPagination
    {
        #region "自訂分頁語法產生Function "
        /// <summary>
        ///自訂分頁語法產生Function 
        /// </summary>
        /// <param name="CurrentPage">目前第幾頁</param>
        /// <param name="PageCount">一頁有幾筆</param>
        /// <param name="PrimaryKey">PrimaryKey</param>
        /// <param name="SelectField">要回傳的欄位</param>
        /// <param name="JoinString">要查詢的資料表或是Join字串</param>
        /// <param name="whereString">要查詢的條件與排序方式</param>
        /// <returns>回傳分頁SQL語法</returns>
        /// <remarks></remarks>
        static public string CustomPageSQL(int CurrentPage, int PageCount, string PrimaryKey, string SelectField, string JoinString, string whereString)
        {

            int TopCount = (CurrentPage - 1) * PageCount;
            string TempSqlString = "SELECT top ##PageCount## ##SelectField## FROM  ##JoinString## where ##PrimaryKey## Not IN(SELECT top ##TopCount##  ##PrimaryKey## FROM  ##JoinString## where 1=1 ##whereString##) ##whereString##";
            string strPageCount = PageCount.ToString();
            string strTopCount = TopCount.ToString();
            TempSqlString = TempSqlString.Replace("##PageCount##", strPageCount);
            TempSqlString = TempSqlString.Replace("##SelectField##", SelectField);
            TempSqlString = TempSqlString.Replace("##JoinString##", JoinString);
            TempSqlString = TempSqlString.Replace("##PrimaryKey##", PrimaryKey);
            TempSqlString = TempSqlString.Replace("##TopCount##", strTopCount);
            if (whereString != "")
            {
                whereString = "and " + whereString;
            }

            TempSqlString = TempSqlString.Replace("##whereString##", whereString);
            return TempSqlString;
        }

        #endregion

        #region "取得自訂分頁的總筆數的SQL語法"
        /// <summary>
        /// 取得自訂分頁的總筆數的SQL語法
        /// </summary>
        /// <param name="PrimaryKey">PrimaryKey</param>
        /// <param name="JoinString">要查詢的資料表或是Join字串</param>
        /// <param name="whereString">要查詢的條件與排序方式</param>
        /// <returns>取得自訂分頁的總筆數的SQL語法</returns>
        /// <remarks></remarks>
        static public string CustomPageRecordCount(string PrimaryKey, string JoinString, string whereString)
        {
            string TempSqlString = "SELECT   COUNT(##PrimaryKey##) AS Number FROM ##JoinString## WHERE 1=1 ##whereString##";
            TempSqlString = TempSqlString.Replace("##JoinString##", JoinString);
            TempSqlString = TempSqlString.Replace("##PrimaryKey##", PrimaryKey);
            if (whereString != "")
            {
                whereString = "and " + whereString;
            }
            TempSqlString = TempSqlString.Replace("##whereString##", whereString);
            return TempSqlString;
        }

        #endregion

        #region "產生分頁控制項"
        /// <summary>
        /// 產生分頁控制項
        /// </summary>
        /// <param name="page">目前第幾頁</param>
        /// <param name="totalitems">共有幾筆</param>
        /// <param name="limit">一頁幾筆</param>
        /// <param name="adjacents">不知道，傳2~5都OK</param>
        /// <param name="targetpage">連結文字，例:pagination.aspx?foo=bar</param>
        /// <returns></returns>
        public static string getPaginationString(int page, int totalitems, int limit, int adjacents, string targetpage)
        {
            //defaults

            targetpage = targetpage.IndexOf('?') != -1 ? targetpage + "&" : targetpage + "?";
            string margin = "";
            string padding = "";
            //other vars
            int prev = page - 1;
            //previous page is page - 1
            int nextPage = page + 1;
            //nextPage page is page + 1
            Double value = Convert.ToDouble(totalitems / limit);
            int lastpage = Convert.ToInt16(Math.Ceiling(value));
            //lastpage is = total items / items per page, rounded up.
            int lpm1 = lastpage - 1;
            //last page minus 1
            int counter = 0;
            // Now we apply our rules and draw the pagination object. 
            // We're actually saving the code to a variable in case we want to draw it more than once.

            StringBuilder paginationBuilder = new StringBuilder();
            if (lastpage > 1)
            {

                paginationBuilder.Append("<div class=\"pagination\"");
                if (!string.IsNullOrEmpty(margin) | !string.IsNullOrEmpty(padding))
                {
                    paginationBuilder.Append(" style=\"");
                    if (!string.IsNullOrEmpty(margin))
                    {
                        paginationBuilder.Append("margin: margin");
                    }
                    if (!string.IsNullOrEmpty(padding))
                    {
                        paginationBuilder.Append("padding: padding");
                    }
                    paginationBuilder.Append("\"");
                }
                paginationBuilder.Append(">");

                //previous button
                paginationBuilder.Append(page > 1 ? string.Format("<a href=\"{0}page={1}\">上一頁</a>", targetpage, prev) : "<span class=\"disabled\">上一頁</span>");
                //pages 
                if (lastpage < 7 + (adjacents * 2))
                {
                    //not enough pages to bother breaking it up

                    for (counter = 1; counter <= lastpage; counter++)
                    {

                        paginationBuilder.Append(counter == page ? string.Format("<span class=\"current\">{0}</span>", counter) : string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, counter));
                    }
                }
                else if (lastpage >= 7 + (adjacents * 2))
                {
                    //enough pages to hide some
                    //close to beginning only hide later pages
                    if (page < 1 + (adjacents * 3))
                    {
                        for (counter = 1; counter <= (4 + (adjacents * 2)) - 1; counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format("<span class=\"current\">{0}</span>", counter) : string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, counter));
                        }
                        paginationBuilder.Append("...");
                        paginationBuilder.Append(string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, lpm1));
                        paginationBuilder.Append(string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, lastpage));
                    }
                    //in middle hide some front and some back
                    else if (lastpage - (adjacents * 2) > page & page > (adjacents * 2))
                    {
                        paginationBuilder.Append(string.Format("<a href=\"{0}page=1\">1</a>", targetpage));
                        paginationBuilder.Append(string.Format("<a href=\"{0}page=2\">2</a>", targetpage));
                        paginationBuilder.Append("...");
                        for (counter = (page - adjacents); counter <= (page + adjacents); counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format("<span class=\"current\">{0}</span>", counter) : string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, counter));
                        }
                        paginationBuilder.Append("...");
                        paginationBuilder.Append(string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, lpm1));
                        paginationBuilder.Append(string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, lastpage));
                    }
                    else
                    {
                        //close to end only hide early pages
                        paginationBuilder.Append(string.Format("<a href=\"{0}page=1\">1</a>", targetpage));
                        paginationBuilder.Append(string.Format("<a href=\"{0}page=2\">2</a>", targetpage));
                        paginationBuilder.Append("...");
                        for (counter = (lastpage - (1 + (adjacents * 3))); counter <= lastpage; counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format("<span class=\"current\">{0}</span>", counter) : string.Format("<a href=\"{0}page={1}\">{1}</a>", targetpage, counter));
                        }
                    }
                }
                //nextPage button
                paginationBuilder.Append(page < counter - 1 ? string.Format("<a href=\"{0}page={1}\">下一頁</a>", targetpage, nextPage) : "<span class=\"disabled\">下一頁</span>");
                paginationBuilder.Append("</div>\r\n");
            }
            return paginationBuilder.ToString();
        }

        #endregion
    }
}