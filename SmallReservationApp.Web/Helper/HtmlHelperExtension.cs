using Newtonsoft.Json;
using SmallReservationApp.Core;
using System.Web;
using System.Web.Mvc;
public static class HtmlHelperExtensions
{
    public static HtmlString HtmlConvertToJson(this HtmlHelper htmlHelper,
    object model)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };
        return new HtmlString(JsonConvert.SerializeObject(model, settings));
    }

    public static MvcHtmlString BuildDropdownSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, bool ascending, QueryOptions queryOptions)
    {
        var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        return new MvcHtmlString(string.Format("<a class=\"dropdown-item\" href=\"{0}\">{1}</a>",
        urlHelper.Action(actionName,
        new
        {
            SortField = sortField,
            SortOrder = ascending ? SortOrder.ASC : SortOrder.DESC
        }), fieldName));
    }

    public static MvcHtmlString BuildSortableLink(this HtmlHelper htmlHelper, string fieldName, string actionName, string sortField, QueryOptions queryOptions)
    {
        UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        bool isCurrentSortField = queryOptions?.SortField == sortField;
        return new MvcHtmlString(string.Format("<a href=\"{0}\">{1} {2}</a>",
        urlHelper.Action(actionName,
        new
        {
            SortField = sortField,
            SortOrder = (isCurrentSortField
        && queryOptions.SortOrder == SortOrder.ASC)
        ? SortOrder.DESC : SortOrder.ASC
        }),
        fieldName,
        BuildSortIcon(isCurrentSortField, queryOptions)));
    }

    private static string BuildSortIcon(bool isCurrentSortField, QueryOptions queryOptions)
    {
        string sortIcon = "sort";
        if (isCurrentSortField)
        {
            sortIcon += "-by-alphabet";
            if (queryOptions.SortOrder == SortOrder.DESC)
            {
                sortIcon += "-alt";
            }
        }
        return string.Format("<span class=\"{0} {1}{2}\"></span>",
        "glyphicon", "glyphicon-", sortIcon);
    }

    public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
    {
        UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        return new MvcHtmlString(string.Format(
        "<nav>" +
        " <ul class=\"pager\">" +
        " <li class=\"previous {0}\">{1}</li>" +
        " <li class=\"next {2}\">{3}</li>" +
        " </ul>" +
        "</nav>",
        IsPreviousDisabled(queryOptions),
        BuildPreviousLink(urlHelper, queryOptions, actionName),
        IsNextDisabled(queryOptions),
        BuildNextLink(urlHelper, queryOptions, actionName)
        ));
    }

    public static MvcHtmlString BuildPagination(this HtmlHelper htmlHelper, QueryOptions queryOptions, string actionName)
    {
        UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
        string previousDisabled = IsPreviousDisabled(queryOptions);
        string nextDisabled = IsNextDisabled(queryOptions);
        string linkPrevious = BuildPreviousLink(urlHelper, queryOptions, actionName);
        string linkNext = BuildNextLink(urlHelper, queryOptions, actionName);
        string linkPages = BuildPageLinks(urlHelper, queryOptions, actionName);

        string paginationItems = string.Format(
            "<nav>" +
            "<ul class=\"pagination\">" +
            "<li class=\"page-item {0}\">{1}</li>" +
            "{2}" +
            "<li class=\"page-item {3}\">{4}</li>" +
            "</ul>" +
            "</nav>",
            previousDisabled,
            linkPrevious,
            linkPages,
            nextDisabled,
            linkNext);


        return new MvcHtmlString(paginationItems);
    }

    private static string IsPreviousDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == 1)
        ? "disabled" : string.Empty;
    }

    private static string IsNextDisabled(QueryOptions queryOptions)
    {
        return (queryOptions.CurrentPage == queryOptions.TotalPages)
        ? "disabled" : string.Empty;
    }

    private static string BuildPreviousLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
    {
        return string.Format(
             "<a class=\"page-link\" href=\"{0}\" aria-label=\"Previous\">" +
                "<span aria-hidden=\"true\">&laquo;</span>" +
                "<span class=\"sr-only\">Previous</span>" +
             "</a>",
             urlHelper.Action(actionName, new
             {
                 SortOrder = queryOptions.SortOrder,
                 SortField = queryOptions.SortField,
                 CurrentPage = queryOptions.CurrentPage != 1 ? queryOptions.CurrentPage - 1 : 1,
                 PageSize = queryOptions.PageSize
             }));
    }

    private static string BuildNextLink(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
    {
        return string.Format(
            "<a class=\"page-link\" href=\"{0}\" aria-label=\"Next\">" +
                "<span aria-hidden=\"true\">&raquo;</span>" +
                "<span class=\"sr-only\">Next</span>" +
             "</a>",
            urlHelper.Action(actionName, new
            {
                SortOrder = queryOptions.SortOrder,
                SortField = queryOptions.SortField,
                CurrentPage = queryOptions.CurrentPage != queryOptions.TotalPages ? queryOptions.CurrentPage + 1 : queryOptions.CurrentPage,
                PageSize = queryOptions.PageSize
            }));
    }

    private static string BuildPageLinks(UrlHelper urlHelper, QueryOptions queryOptions, string actionName)
    {
        string linkPages = "";
        for (int i = 1; i <= queryOptions.TotalPages; i++)
        {
            string linkPage = "";
            if (queryOptions.CurrentPage == i)
            {
                linkPage = string.Format(
                    "<li class=\"page-item active\">" +
                    "<a class=\"page-link\" href=\"{0}\">{1}</a>" +
                    "</li>",
                    urlHelper.Action(actionName, new
                    {
                        SortOrder = queryOptions.SortOrder,
                        SortField = queryOptions.SortField,
                        CurrentPage = i,
                        PageSize = queryOptions.PageSize
                    }), i);
            }
            else
            {
                linkPage = string.Format(
                "<li class=\"page-item\">" +
                "<a class=\"page-link\" href=\"{0}\">{1}</a>" +
                "</li>",
                urlHelper.Action(actionName, new
                {
                    SortOrder = queryOptions.SortOrder,
                    SortField = queryOptions.SortField,
                    CurrentPage = i,
                    PageSize = queryOptions.PageSize
                }), i);
            }
            linkPages += linkPage;
        }
        return linkPages;
    }
}