#pragma checksum "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ceca8f9db69dc1ccc68a4d7ca211a4c95c722670"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Serial_ShowEpisodeComments), @"mvc.1.0.view", @"/Views/Serial/ShowEpisodeComments.cshtml")]
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
#line 1 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\_ViewImports.cshtml"
using Flix_Tv.Site;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\_ViewImports.cshtml"
using Flix_Tv.Site.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ceca8f9db69dc1ccc68a4d7ca211a4c95c722670", @"/Views/Serial/ShowEpisodeComments.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6278c106961752d4bd55c62fce5083d8e5130d21", @"/Views/_ViewImports.cshtml")]
    public class Views_Serial_ShowEpisodeComments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<List<Flix_Tv.Application.DTOs.SerialComment.Site.ShowSerialCommentDto>, int>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("reviews__avatar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
  Layout = null;
    List<Flix_Tv.Application.DTOs.SerialComment.Site.ShowSerialCommentDto> subComments = ViewBag.subComments;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
 foreach (var item in Model.Item1)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li class=\"reviews__item\"");
            BeginWriteAttribute("id", " id=\"", 295, "\"", 322, 2);
            WriteAttributeValue("", 300, "reviews__item-", 300, 14, true);
#nullable restore
#line 9 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
WriteAttributeValue("", 314, item.Id, 314, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n        <div class=\"reviews__autor\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ceca8f9db69dc1ccc68a4d7ca211a4c95c7226704918", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 410, "~/Users/ThumbImages/", 410, 20, true);
#nullable restore
#line 11 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
AddHtmlAttributeValue("", 430, item.UserImage, 430, 15, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            <span class=\"reviews__name\">");
#nullable restore
#line 12 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                   Write(item.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            <span class=\"reviews__time\">");
#nullable restore
#line 13 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                   Write(item.CreateDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("   توسط ");
#nullable restore
#line 13 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                                           Write(item.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 14 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
             if (item.Rate != null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <span class=""reviews__rating""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M22,9.67A1,1,0,0,0,21.14,9l-5.69-.83L12.9,3a1,1,0,0,0-1.8,0L8.55,8.16,2.86,9a1,1,0,0,0-.81.68,1,1,0,0,0,.25,1l4.13,4-1,5.68A1,1,0,0,0,6.9,21.44L12,18.77l5.1,2.67a.93.93,0,0,0,.46.12,1,1,0,0,0,.59-.19,1,1,0,0,0,.4-1l-1-5.68,4.13-4A1,1,0,0,0,22,9.67Zm-6.15,4a1,1,0,0,0-.29.88l.72,4.2-3.76-2a1.06,1.06,0,0,0-.94,0l-3.76,2,.72-4.2a1,1,0,0,0-.29-.88l-3-3,4.21-.61a1,1,0,0,0,.76-.55L12,5.7l1.88,3.82a1,1,0,0,0,.76.55l4.21.61Z""></path></svg> ");
#nullable restore
#line 16 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 Write(item.Rate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 17 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <p style=\"border-bottom: 1px solid rgba(47,128,237,0.1); padding-bottom: 30px;\" class=\"comments__text\">");
#nullable restore
#line 20 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                                                                                          Write(item.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
        <div class=""comments__actions"">
            <div class=""comments__rate"">
                <button type=""button""><svg width=""22"" height=""22"" viewBox=""0 0 22 22"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M11 7.3273V14.6537"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M14.6667 10.9905H7.33333"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M15.6857 1H6.31429C3.04762 1 1 3.31208 1 6.58516V15.4148C1 18.6879 3.0381 21 6.31429 21H15.6857C18.9619 21 21 18.6879 21 15.4148V6.58516C21 3.31208 18.9619 1 15.6857 1Z"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg> 12</button>

                <button type=""button"">7 <svg width=""22"" height=""22"" viewBox=""0 0 22 22"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M14.6667 10.9905H7.33333"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path fill-rule=""evenodd"" clip-rule");
            WriteLiteral(@"=""evenodd"" d=""M15.6857 1H6.31429C3.04762 1 1 3.31208 1 6.58516V15.4148C1 18.6879 3.0381 21 6.31429 21H15.6857C18.9619 21 21 18.6879 21 15.4148V6.58516C21 3.31208 18.9619 1 15.6857 1Z"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg></button>
            </div>

            <button type=""button""");
            BeginWriteAttribute("onclick", " onclick=\"", 2733, "\"", 2765, 3);
            WriteAttributeValue("", 2743, "replyComment(", 2743, 13, true);
#nullable restore
#line 28 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
WriteAttributeValue("", 2756, item.Id, 2756, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2764, ")", 2764, 1, true);
            EndWriteAttribute();
            WriteLiteral(@"><svg xmlns=""http://www.w3.org/2000/svg"" width=""512"" height=""512"" viewBox=""0 0 512 512""><polyline points=""400 160 464 224 400 288"" style=""fill:none;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px""></polyline><path d=""M448,224H154C95.24,224,48,273.33,48,332v20"" style=""fill:none;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px""></path></svg><span>پاسخ</span></button>
            <button type=""button""><svg xmlns=""http://www.w3.org/2000/svg"" width=""512"" height=""512"" viewBox=""0 0 512 512""><polyline points=""320 120 368 168 320 216"" style=""fill:none;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px""></polyline><path d=""M352,168H144a80.24,80.24,0,0,0-80,80v16"" style=""fill:none;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px""></path><polyline points=""192 392 144 344 192 296"" style=""fill:none;stroke-linecap:round;stroke-linejoin:round;stroke-width:32px""></polyline><path d=""M160,344H368a80.24,80.24,0,0,0,80-80V248"" style=""fill:none;stroke-linecap:round;stroke-linejoin:ro");
            WriteLiteral("und;stroke-width:32px\"></path></svg><span>نقل قول</span></button>\r\n        </div>\r\n    </li>\r\n");
            WriteLiteral("    <li>\r\n\r\n    </li>\r\n");
#nullable restore
#line 36 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
     foreach (var sub in subComments.Where(p => p.ParentId == item.Id))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"reviews__item\"");
            BeginWriteAttribute("id", " id=\"", 4022, "\"", 4048, 2);
            WriteAttributeValue("", 4027, "reviews__item-", 4027, 14, true);
#nullable restore
#line 38 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
WriteAttributeValue("", 4041, sub.Id, 4041, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"reviews__autor\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ceca8f9db69dc1ccc68a4d7ca211a4c95c72267013638", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4144, "~/Users/ThumbImages/", 4144, 20, true);
#nullable restore
#line 40 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
AddHtmlAttributeValue("", 4164, sub.UserImage, 4164, 14, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <span class=\"reviews__name\">");
#nullable restore
#line 41 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                       Write(sub.Subject);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                <span class=\"reviews__time\">");
#nullable restore
#line 42 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                       Write(sub.CreateDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("   توسط ");
#nullable restore
#line 42 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                                              Write(sub.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 43 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                 if (sub.Rate != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <span class=""reviews__rating""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24""><path d=""M22,9.67A1,1,0,0,0,21.14,9l-5.69-.83L12.9,3a1,1,0,0,0-1.8,0L8.55,8.16,2.86,9a1,1,0,0,0-.81.68,1,1,0,0,0,.25,1l4.13,4-1,5.68A1,1,0,0,0,6.9,21.44L12,18.77l5.1,2.67a.93.93,0,0,0,.46.12,1,1,0,0,0,.59-.19,1,1,0,0,0,.4-1l-1-5.68,4.13-4A1,1,0,0,0,22,9.67Zm-6.15,4a1,1,0,0,0-.29.88l.72,4.2-3.76-2a1.06,1.06,0,0,0-.94,0l-3.76,2,.72-4.2a1,1,0,0,0-.29-.88l-3-3,4.21-.61a1,1,0,0,0,.76-.55L12,5.7l1.88,3.82a1,1,0,0,0,.76.55l4.21.61Z""></path></svg> ");
#nullable restore
#line 45 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     Write(item.Rate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 46 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <p style=\"border-bottom: 1px solid rgba(47,128,237,0.1); padding-bottom: 30px;\" class=\"comments__text\">\r\n                <span>");
#nullable restore
#line 50 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                 Write(item.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                ");
#nullable restore
#line 51 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
           Write(sub.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </p>
            <div class=""comments__actions"">
                <div class=""comments__rate"">
                    <button type=""button""><svg width=""22"" height=""22"" viewBox=""0 0 22 22"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M11 7.3273V14.6537"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M14.6667 10.9905H7.33333"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M15.6857 1H6.31429C3.04762 1 1 3.31208 1 6.58516V15.4148C1 18.6879 3.0381 21 6.31429 21H15.6857C18.9619 21 21 18.6879 21 15.4148V6.58516C21 3.31208 18.9619 1 15.6857 1Z"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg> 12</button>

                    <button type=""button"">7 <svg width=""22"" height=""22"" viewBox=""0 0 22 22"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M14.6667 10.9905H7.33333"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path");
            WriteLiteral(@" fill-rule=""evenodd"" clip-rule=""evenodd"" d=""M15.6857 1H6.31429C3.04762 1 1 3.31208 1 6.58516V15.4148C1 18.6879 3.0381 21 6.31429 21H15.6857C18.9619 21 21 18.6879 21 15.4148V6.58516C21 3.31208 18.9619 1 15.6857 1Z"" stroke-width=""1.5"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg></button>
                </div>

            </div>
        </li>
");
#nullable restore
#line 62 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
     
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"catalog__paginator-wrap catalog__paginator-wrap--comments\">\r\n");
            WriteLiteral("\r\n    <ul class=\"catalog__paginator\">\r\n\r\n");
#nullable restore
#line 69 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
         if (ViewBag.pageId > 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>\r\n                <a");
            BeginWriteAttribute("onclick", " onclick=\"", 6847, "\"", 6889, 3);
            WriteAttributeValue("", 6857, "pageComment(", 6857, 12, true);
#nullable restore
#line 72 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
WriteAttributeValue("", 6869, ViewBag.pageId-1, 6869, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6888, ")", 6888, 1, true);
            EndWriteAttribute();
            WriteLiteral(@">
                    <svg width=""14"" height=""11"" viewBox=""0 0 14 11"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M13.1992 5.3645L0.75 5.3645"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M8.17822 0.602051L13.1993 5.36417L8.17822 10.1271"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg>
                </a>
            </li>
");
#nullable restore
#line 76 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li class=\"active\"><a href=\"#\">");
#nullable restore
#line 77 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
                                  Write(ViewBag.pageId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 78 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
         if (ViewBag.pageId <= Model.Item2)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>\r\n                <a");
            BeginWriteAttribute("onclick", " onclick=\"", 7469, "\"", 7511, 3);
            WriteAttributeValue("", 7479, "pageComment(", 7479, 12, true);
#nullable restore
#line 81 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
WriteAttributeValue("", 7491, ViewBag.pageId+1, 7491, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7510, ")", 7510, 1, true);
            EndWriteAttribute();
            WriteLiteral(@">
                    <svg width=""14"" height=""11"" viewBox=""0 0 14 11"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""><path d=""M0.75 5.36475L13.1992 5.36475"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path><path d=""M5.771 10.1271L0.749878 5.36496L5.771 0.602051"" stroke-width=""1.2"" stroke-linecap=""round"" stroke-linejoin=""round""></path></svg>

                </a>
            </li>
");
#nullable restore
#line 86 "D:\Flix_Tv\Flix_Tv\Flix_Tv.Site\Views\Serial\ShowEpisodeComments.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<List<Flix_Tv.Application.DTOs.SerialComment.Site.ShowSerialCommentDto>, int>> Html { get; private set; }
    }
}
#pragma warning restore 1591
