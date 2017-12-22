namespace LocationExplorer.Web.Test.Infrastructure.Filters
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Web.Infrastructure.Filters;
    using Xunit;

    public class ValidateModelStateAttributeTest
    {
        [Fact]
        public void When_ModelState_Invalid_Set_Result_Model()
        {
            // arrange
            var attribute = new ValidateModelStateAttribute();
            var controller = new TestController();
            var viewModelId = 13;
            var model = new { Id = viewModelId };
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                ActionDescriptor = new ActionDescriptor(),
                RouteData = new RouteData()
            };

            var context = new ActionExecutingContext(
                actionContext, 
                new List<IFilterMetadata>(new List<IFilterMetadata>()), 
                new Dictionary<string, object>
                {
                    { "model", model }
                }, 
                controller);

            context.ModelState.AddModelError(string.Empty, "Message");

            // act
            attribute.OnActionExecuting(context);

            // assert
            Assert.True(context.Result != null);
            var resultModel = (context.Result as ViewResult)?.Model;
            Assert.True(Convert.ToInt32(resultModel?.GetType().GetProperty("Id").GetValue(resultModel)) == viewModelId);
        }

        [Fact]
        public void When_ModelState_Invalid_Model_Not_Exist_Set_Result_Null()
        {
            // arrange
            var attribute = new ValidateModelStateAttribute();
            var controller = new TestController();
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                ActionDescriptor = new ActionDescriptor(),
                RouteData = new RouteData()
            };

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(new List<IFilterMetadata>()),
                new Dictionary<string, object>(),
                controller);

            context.ModelState.AddModelError(string.Empty, "Message");

            // act
            attribute.OnActionExecuting(context);

            // assert
            Assert.Null(context.Result);
        }

        [Fact]
        public void When_ModelState_Invalid_And_Controller_Not_Found_Set_Result_Null()
        {
            // arrange
            var attribute = new ValidateModelStateAttribute();
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                ActionDescriptor = new ActionDescriptor(),
                RouteData = new RouteData()
            };

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(new List<IFilterMetadata>()),
                new Dictionary<string, object>{ { "model", new { Id = 13, Name = " Some name" } } },
                null);

            context.ModelState.AddModelError(string.Empty, "Message");

            // act
            attribute.OnActionExecuting(context);

            // assert
            Assert.Null(context.Result);
        }

        [Fact]
        public void When_ModelState_Is_Valid_Set_Result_Null()
        {
            // arrange
            var attribute = new ValidateModelStateAttribute();
            var controller = new TestController();
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                ActionDescriptor = new ActionDescriptor(),
                RouteData = new RouteData()
            };

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(new List<IFilterMetadata>()),
                new Dictionary<string, object> { { "model", new { Id = 13, Name = " Some name" } } },
                controller);

            // act
            attribute.OnActionExecuting(context);

            // assert
            Assert.Null(context.Result);
        }

        private class TestController : Controller
        {

        }
    }
}
