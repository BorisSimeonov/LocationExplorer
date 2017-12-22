namespace LocationExplorer.Web.Test.Infrastructure.TagHelpers
{
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Moq;
    using Service.Infrastructure;
    using Web.Infrastructure.TagHelpers;
    using Xunit;

    public class PagingTagHelperTests
    {
        [Fact]
        public void PagingTagHelper_Works()
        {
            // arrange
            var urlHelperFactory = new Mock<IUrlHelperFactory>(MockBehavior.Strict);
            var tagName = "div";
            var pageAction = "action";
            var pageClass = "pageClass";
            var pageClassNormal = "normal";
            var pageClassSelected = "active";
            var viewContext = new ViewContext();

            var tagHelper = new PagingTagHelper(urlHelperFactory.Object);
            tagHelper.PageModel = new PagingInfo { TotalItems = 20, ItemsPerPage = 10, CurrentPage = 1 };
            tagHelper.PageAction = pageAction;
            tagHelper.PageClass = pageClass;
            tagHelper.PageClassNormal = pageClassNormal;
            tagHelper.PageClassSelected = pageClassSelected;
            tagHelper.PageClassesEnabled = true;
            tagHelper.ViewContext = viewContext;

            var tagHelperAttributeList = new TagHelperAttributeList();
            var tagHelperContext = new TagHelperContext(tagName, tagHelperAttributeList, new Dictionary<object, object>(), "unique-id");
            var tagHelperOutput = new TagHelperOutput(tagName, tagHelperAttributeList, (b, encoder) => null);
            var urlHelperMock = new Mock<IUrlHelper>();
            
            urlHelperFactory.Setup(s => s.GetUrlHelper(viewContext)).Returns(urlHelperMock.Object);

            // act
            tagHelper.Process(tagHelperContext, tagHelperOutput);

            // assert
            var expected =
                "<a class=\"&#x61;&#x63;&#x74;&#x69;&#x76;&#x65;&#x20;&#x70;&#x61;&#x67;&#x65;&#x43;&#x6C;&#x61;&#x73;&#x73;\" href=\"\">&#x31;</a><a class=\"&#x6E;&#x6F;&#x72;&#x6D;&#x61;&#x6C;&#x20;&#x70;&#x61;&#x67;&#x65;&#x43;&#x6C;&#x61;&#x73;&#x73;\" href=\"\">&#x32;</a>";
            var content = tagHelperOutput.Content.GetContent(HtmlEncoder.Create());
            Assert.True(content == expected);
        }
    }
}
