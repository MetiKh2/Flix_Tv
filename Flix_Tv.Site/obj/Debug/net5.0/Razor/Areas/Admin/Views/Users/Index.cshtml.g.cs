#pragma checksum "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6415d9f88950f78f202552ee317aa8972ebd45fc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Users_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Users/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\_ViewImports.cshtml"
using Flix_Tv.Site;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\_ViewImports.cshtml"
using Flix_Tv.Site.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6415d9f88950f78f202552ee317aa8972ebd45fc", @"/Areas/Admin/Views/Users/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6278c106961752d4bd55c62fce5083d8e5130d21", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Users_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<List<Flix_Tv.Application.DTOs.User.Admin.UsersListDto>,int,int>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("main__title-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- main title -->\r\n<div class=\"col-12\">\r\n    <div class=\"main__title\">\r\n        <h2>کاربران</h2>\r\n\r\n        <span class=\"main__title-stat\">تعداد ");
#nullable restore
#line 12 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                        Write(Model.Item3);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>

        <div class=""main__title-wrap"">
            <a class=""btn btn-info ml-4"" href=""/Admin/CreateUser"">افزودن کاربر جدید</a>
            <!-- filter sort -->
            <div class=""filter"" id=""filter__sort"">
                <span class=""filter__item-label"">مرتب بر اساس :</span>

");
#nullable restore
#line 20 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                 if (ViewBag.sort == "date")
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""filter__item-btn dropdown-toggle"" role=""navigation"" id=""filter-sort"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                        <input type=""button"" value=""تاریخ ایجاد شده"">
                        <span></span>
                    </div>
");
#nullable restore
#line 26 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 27 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                 if (ViewBag.sort == "activate")
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""filter__item-btn dropdown-toggle"" role=""navigation"" id=""filter-sort"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                        <input type=""button"" value=""وضعیت"">
                        <span></span>
                    </div>
");
#nullable restore
#line 33 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                <ul class=""filter__item-menu dropdown-menu scrollbar-dropdown"" aria-labelledby=""filter-sort"">
                    <li type=""sort"" data-value=""تاریخ ایجاد شده"" data=""date"">تاریخ ایجاد شده</li>
                    <li type=""sort"" data-value=""پلن اشتراک"" data=""plan"">پلن اشتراک</li>
                    <li type=""sort"" data-value=""وضعیت"" data=""activate"">وضعیت</li>
                </ul>
");
            WriteLiteral("            </div>\r\n            <!-- end filter sort -->\r\n            <!-- search -->\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6415d9f88950f78f202552ee317aa8972ebd45fc7502", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\" name=\"sort\"");
                BeginWriteAttribute("value", " value=\"", 2286, "\"", 2307, 1);
#nullable restore
#line 49 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 2294, ViewBag.sort, 2294, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("/>\r\n                <input name=\"filter\" id=\"input-filter\" type=\"text\"");
                BeginWriteAttribute("value", " value=\"", 2378, "\"", 2401, 1);
#nullable restore
#line 50 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 2386, ViewBag.filter, 2386, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""دنبال کاربر . ."">
                <button type=""submit"">
                    <svg width=""18"" height=""18"" viewBox=""0 0 18 18"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><circle cx=""8.25998"" cy=""8.25995"" r=""7.48191"" stroke=""#2F80ED"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></circle><path d=""M13.4637 13.8523L16.3971 16.778"" stroke=""#2F80ED"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg>
                </button>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            <!-- end search -->
        </div>
    </div>
</div>
<!-- end main title -->
<!-- users -->
<div class=""col-12"">
    <div class=""main__table-wrap"">
        <table class=""main__table"">
            <thead>
                <tr>
                    
                    <th>اطلاعات پایه</th>
                    <th>نام کاربری</th>
                    <th>پلن اشتراک</th>
                    <th>نظرات</th>
                    <th>امتیازات</th>
                    <th>وضعیت</th>
                    <th>تاریخ ایجاد شده</th>
                    <th>اقدامات</th>
                </tr>
            </thead>

            <tbody>
");
#nullable restore
#line 79 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                 foreach (var item in Model.Item1)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n\r\n                        <td>\r\n                            <div class=\"main__user\">\r\n                                <div class=\"main__avatar\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6415d9f88950f78f202552ee317aa8972ebd45fc11563", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3863, "~/Users/ThumbImages/", 3863, 20, true);
#nullable restore
#line 86 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
AddHtmlAttributeValue("", 3883, item.Avatar, 3883, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </div>\r\n                                <div class=\"main__meta\">\r\n                                    <h3>");
#nullable restore
#line 89 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                   Write(item.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                    <span>");
#nullable restore
#line 90 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                     Write(item.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                        <td>\r\n                            <div class=\"main__table-text\">");
#nullable restore
#line 95 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                                     Write(item.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                        </td>
                        <td>
                            <div class=""main__table-text"">حرفه ای</div>
                        </td>
                        <td>
                            <div class=""main__table-text"">13</div>
                        </td>
                        <td>
                            <div class=""main__table-text"">1</div>
                        </td>
                        <td>
");
#nullable restore
#line 107 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                             if (item.IsActive)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"main__table-text main__table-text--green\">فعال</div>");
#nullable restore
#line 109 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                                                                                }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"main__table-text main__table-text--red\"> غیر فعال</div>");
#nullable restore
#line 112 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                                                                                   }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                        <td>\r\n                            <div class=\"main__table-text\">");
#nullable restore
#line 115 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                                     Write(item.CreateDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        </td>\r\n                        <td>\r\n                            <div class=\"main__table-btns\">\r\n");
            WriteLiteral("                                <a");
            BeginWriteAttribute("href", " href=\"", 5977, "\"", 6008, 2);
            WriteAttributeValue("", 5984, "/Admin/EditUser/", 5984, 16, true);
#nullable restore
#line 122 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 6000, item.Id, 6000, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""main__table-btn main__table-btn--edit"">
                                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M22,7.24a1,1,0,0,0-.29-.71L17.47,2.29A1,1,0,0,0,16.76,2a1,1,0,0,0-.71.29L13.22,5.12h0L2.29,16.05a1,1,0,0,0-.29.71V21a1,1,0,0,0,1,1H7.24A1,1,0,0,0,8,21.71L18.87,10.78h0L21.71,8a1.19,1.19,0,0,0,.22-.33,1,1,0,0,0,0-.24.7.7,0,0,0,0-.14ZM6.83,20H4V17.17l9.93-9.93,2.83,2.83ZM18.17,8.66,15.34,5.83l1.42-1.41,2.82,2.82Z""></path></svg>
                                </a>
                                <button");
            BeginWriteAttribute("onclick", " onclick=\"", 6569, "\"", 6599, 3);
            WriteAttributeValue("", 6579, "DeleteUser(", 6579, 11, true);
#nullable restore
#line 125 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 6590, item.Id, 6590, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6598, ")", 6598, 1, true);
            EndWriteAttribute();
            WriteLiteral(@" class=""main__table-btn main__table-btn--delete open-modal"">
                                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M10,18a1,1,0,0,0,1-1V11a1,1,0,0,0-2,0v6A1,1,0,0,0,10,18ZM20,6H16V5a3,3,0,0,0-3-3H11A3,3,0,0,0,8,5V6H4A1,1,0,0,0,4,8H5V19a3,3,0,0,0,3,3h8a3,3,0,0,0,3-3V8h1a1,1,0,0,0,0-2ZM10,5a1,1,0,0,1,1-1h2a1,1,0,0,1,1,1V6H10Zm7,14a1,1,0,0,1-1,1H8a1,1,0,0,1-1-1V8H17Zm-3-1a1,1,0,0,0,1-1V11a1,1,0,0,0-2,0v6A1,1,0,0,0,14,18Z""></path></svg>
                                </button>
                            </div>
                        </td>
                    </tr>
");
#nullable restore
#line 131 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </tbody>
        </table>
    </div>
</div>
<!-- end users -->
<!-- paginator -->
<div class=""col-12"">
    <div class=""paginator"">
        <span class=""paginator__pages"">10 از 7,545</span>

        <ul class=""paginator__paginator"">
");
#nullable restore
#line 144 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
             if (ViewBag.pageId > 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 7600, "\"", 7688, 6);
            WriteAttributeValue("", 7607, "/Admin/Users?filter=", 7607, 20, true);
#nullable restore
#line 147 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 7627, ViewBag.filter, 7627, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7642, "&sort=", 7642, 6, true);
#nullable restore
#line 147 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 7648, ViewBag.sort, 7648, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7661, "&pageId=", 7661, 8, true);
#nullable restore
#line 147 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 7669, ViewBag.pageId-1, 7669, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <svg width=""14"" height=""11"" viewBox=""0 0 14 11"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M13.1992 5.3645L0.75 5.3645"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M8.17822 0.602051L13.1993 5.36417L8.17822 10.1271"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg>
                    </a>
                </li>
");
#nullable restore
#line 151 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
            }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"active\"><a href=\"#\">");
#nullable restore
#line 158 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                      Write(ViewBag.pageId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 159 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
             if (ViewBag.pageId <= Model.Item2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 8841, "\"", 8929, 6);
            WriteAttributeValue("", 8848, "/Admin/Users?filter=", 8848, 20, true);
#nullable restore
#line 162 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 8868, ViewBag.filter, 8868, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8883, "&sort=", 8883, 6, true);
#nullable restore
#line 162 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 8889, ViewBag.sort, 8889, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8902, "&pageId=", 8902, 8, true);
#nullable restore
#line 162 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
WriteAttributeValue("", 8910, ViewBag.pageId+1, 8910, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <svg width=""14"" height=""11"" viewBox=""0 0 14 11"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M0.75 5.36475L13.1992 5.36475"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M5.771 10.1271L0.749878 5.36496L5.771 0.602051"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg>

                    </a>
                </li>
");
#nullable restore
#line 167 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </div>\r\n</div>\r\n<!-- end paginator -->\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

    <script>
        function DeleteUser(Id) {

            $.ajax({
                type: ""POST"",
                url: ""/Admin/DeleteUser/"" + Id,
                dataType: ""json"",
                success: function (response) {
                    location.reload();
                   
                }
                //}, complete: function (res) {
                //    $('.main__table tbody').html('');
                //    for (var i in res) {
                //        $('.main__table tbody').append(`  <tr>

                //        <td>
                //            <div class=""main__user"">
                //                <div class=""main__avatar"">
                //                    <img src=""~/Users/ThumbImages/${res[i].avatar}"" alt="""">
                //                </div>
                //                <div class=""main__meta"">
                //                    <h3>${res[i].fullName}</h3>
                //                    <span>${res[i].email}</span>
    ");
                WriteLiteral(@"            //                </div>
                //            </div>
                //        </td>
                //        <td>
                //            <div class=""main__table-text"">${res[i].userName}</div>
                //        </td>
                //        <td>
                //            <div class=""main__table-text"">حرفه ای</div>
                //        </td>
                //        <td>
                //            <div class=""main__table-text"">13</div>
                //        </td>
                //        <td>
                //            <div class=""main__table-text"">1</div>
                //        </td>
                //        <td>
                          
                //                <div class=""main__table-text main__table-text--green"">${res[i].isActive}</div>}
                           
                //        </td>
                //        <td>
                //            <div class=""main__table-text"">${res[i].createDate}</div>");
                WriteLiteral(@"
                //        </td>
                //        <td>
                //            <div class=""main__table-btns"">
                //                <a href=""#modal-status"" class=""main__table-btn main__table-btn--banned open-modal"">
                //                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M12,13a1.49,1.49,0,0,0-1,2.61V17a1,1,0,0,0,2,0V15.61A1.49,1.49,0,0,0,12,13Zm5-4V7A5,5,0,0,0,7,7V9a3,3,0,0,0-3,3v7a3,3,0,0,0,3,3H17a3,3,0,0,0,3-3V12A3,3,0,0,0,17,9ZM9,7a3,3,0,0,1,6,0V9H9Zm9,12a1,1,0,0,1-1,1H7a1,1,0,0,1-1-1V12a1,1,0,0,1,1-1H17a1,1,0,0,1,1,1Z""></path></svg>
                //                </a>
                //                <a href=""/Admin/EditUser/${res[i].id}"" class=""main__table-btn main__table-btn--edit"">
                //                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M22,7.24a1,1,0,0,0-.29-.71L17.47,2.29A1,1,0,0,0,16.76,2a1,1,0,0,0-.71.29L13.22,5.12h0L2.29,16.05a1,1,0,0,0-.29.71V21a1,1,0,0,0,1");
                WriteLiteral(@",1H7.24A1,1,0,0,0,8,21.71L18.87,10.78h0L21.71,8a1.19,1.19,0,0,0,.22-.33,1,1,0,0,0,0-.24.7.7,0,0,0,0-.14ZM6.83,20H4V17.17l9.93-9.93,2.83,2.83ZM18.17,8.66,15.34,5.83l1.42-1.41,2.82,2.82Z""></path></svg>
                //                </a>
                //                <button onclick=""DeleteUser(${res[i].id})"" class=""main__table-btn main__table-btn--delete open-modal"">
                //                    <svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M10,18a1,1,0,0,0,1-1V11a1,1,0,0,0-2,0v6A1,1,0,0,0,10,18ZM20,6H16V5a3,3,0,0,0-3-3H11A3,3,0,0,0,8,5V6H4A1,1,0,0,0,4,8H5V19a3,3,0,0,0,3,3h8a3,3,0,0,0,3-3V8h1a1,1,0,0,0,0-2ZM10,5a1,1,0,0,1,1-1h2a1,1,0,0,1,1,1V6H10Zm7,14a1,1,0,0,1-1,1H8a1,1,0,0,1-1-1V8H17Zm-3-1a1,1,0,0,0,1-1V11a1,1,0,0,0-2,0v6A1,1,0,0,0,14,18Z""></path></svg>
                //                </button>
                //            </div>
                //        </td>
                //    </tr>`);
                //    }
                //}
            });
    ");
                WriteLiteral(@"    }
    
         
        $('li[type=sort]').click(function (e) {
            var filterText = $('#input-filter').val();
            var sortText = $(this).attr('data');
            location.href = `users?filter=${filterText}&sort=${sortText}&pageId=`+");
#nullable restore
#line 245 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Areas\Admin\Views\Users\Index.cshtml"
                                                                             Write(ViewBag.pageId);

#line default
#line hidden
#nullable disable
                WriteLiteral(";\r\n        });\r\n\r\n    </script>\r\n\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<List<Flix_Tv.Application.DTOs.User.Admin.UsersListDto>,int,int>> Html { get; private set; }
    }
}
#pragma warning restore 1591
