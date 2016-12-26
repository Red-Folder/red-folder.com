using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.TagHelpers
{
    [HtmlTargetElement("rfc-pager")]
    public class RFCPagerTagHelper: TagHelper
    {
        [HtmlAttributeName("page-no")]
        public int PageNo { get; set; }

        [HtmlAttributeName("pages")]
        public int Pages { get; set; }

        [HtmlAttributeName("on-first-page")]
        public string OnFirstPage { get; set; }

        [HtmlAttributeName("on-previous-page")]
        public string OnPreviousPage { get; set; }

        [HtmlAttributeName("on-next-page")]
        public string OnNextPage { get; set; }

        [HtmlAttributeName("on-last-page")]
        public string OnLastPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var firstCssClasses = PageNo == 1 ? "btn btn-default btn-xs disabled" : "btn btn-default btn-xs selectable";
            var previousCssClasses = PageNo == 1 ? "btn btn-default btn-xs disabled" : "btn btn-default btn-xs selectable";
            var nextCssClasses = PageNo == Pages ? "btn btn-default btn-xs disabled" : "btn btn-default btn-xs selectable";
            var lastCssClasses = PageNo == Pages ? "btn btn-default btn-xs disabled" : "btn btn-default btn-xs selectable";

            var firstOnClick = PageNo == 1 ? "" : $@"onclick = ""{OnFirstPage}""";
            var previousOnClick = PageNo == 1 ? "" : $@"onclick = ""{OnPreviousPage}""";
            var nextOnClick = PageNo == Pages ? "" : $@"onclick = ""{OnNextPage}""";
            var lastOnClick = PageNo == Pages ? "" : $@"onclick = ""{OnLastPage}""";

            var htmlSB = new StringBuilder();
            htmlSB.Append($@"
                <div class=""row pager"">
                    <div class=""{firstCssClasses}"" {firstOnClick}>
                        First
                    </div>

                    <div class=""{previousCssClasses}"" {previousOnClick}>
                        Previous
                    </div>

                    <div class=""page-no"">
                        {PageNo} of {Pages}
                    </div>

                    <div class=""{nextCssClasses}"" {nextOnClick}>
                        Next
                    </div>

                    <div class=""{lastCssClasses}"" {lastOnClick}>
                        Last
                    </div>
                </div>
            ");
            output.Content.SetHtmlContent(htmlSB.ToString());
        }
    }
}
