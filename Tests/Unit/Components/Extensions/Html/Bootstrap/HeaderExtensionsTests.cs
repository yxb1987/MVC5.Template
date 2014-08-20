﻿using MvcTemplate.Components.Extensions.Html;
using MvcTemplate.Components.Mvc;
using MvcTemplate.Resources;
using MvcTemplate.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcTemplate.Tests.Unit.Components.Extensions.Html
{
    [TestFixture]
    public class HeaderExtensionsTests
    {
        private HtmlHelper html;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            LocalizationManager.Provider = new LanguageProviderMock().Provider;
            html = HtmlHelperFactory.CreateHtmlHelper();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            LocalizationManager.Provider = null;
        }

        #region Extension method: ProfileLink(this HtmlHelper html)

        [Test]
        public void ProfileLink_FormsProfileLink()
        {
            String actual = html.ProfileLink().ToString();
            String expected = String.Format("<a href=\"{0}\"><i class=\"fa fa-user\"></i><span>{1}</span></a>",
                new UrlHelper(html.ViewContext.RequestContext).Action("Edit", new { controller = "Profile", area = String.Empty }),
                ResourceProvider.GetActionTitle("Profile"));

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Extension method: LanguageLink(this HtmlHelper html)

        [Test]
        public void LanguageLink_OnSingleLanguageReturnsEmptyHtml()
        {
            List<Language> languages = new List<Language>() { new Language() };
            LocalizationManager.Provider = new LanguageProviderMock().Provider;
            LocalizationManager.Provider.Languages.Returns(languages);

            String actual = html.LanguageLink().ToString();
            String expected = String.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LanguageLink_FormsLanguageLink()
        {
            RouteValueDictionary routeValues = html.ViewContext.RequestContext.RouteData.Values;
            String controller = routeValues["controller"].ToString();
            String action = routeValues["action"].ToString();
            String area = routeValues["area"].ToString();

            String actual = html.LanguageLink().ToString();
            String expected = String.Format("<a class=\"dropdown-toggle\" data-toggle=\"dropdown\">"
                + "<i class=\"fa fa-flag\"></i>{0}<span class=\"caret\"></span></a>"
                + "<ul class=\"dropdown-menu\" role=\"menu\"><li>"
                + "<a href=\"{1}\"><img src=\"/Images/Flags/en.gif\" alt=\"\" />English</a></li><li>"
                + "<a href=\"{2}\"><img src=\"/Images/Flags/lt.gif\" alt=\"\" />Lietuvių</a></li></ul>",
                ResourceProvider.GetActionTitle("Language"),
                HttpUtility.HtmlEncode(new UrlHelper(html.ViewContext.RequestContext).Action(action, new { language = "en", controller = controller, area = area, Param1 = "Value1" })),
                HttpUtility.HtmlEncode(new UrlHelper(html.ViewContext.RequestContext).Action(action, new { language = "lt", controller = controller, area = area, Param1 = "Value1" })));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LanguageLink_FormsLanguageLinkOnSpecificDomain()
        {
            html.ViewContext.HttpContext.Request.ApplicationPath.Returns("/TestDomain");
            RouteValueDictionary routeValues = html.ViewContext.RequestContext.RouteData.Values;
            String controller = routeValues["controller"].ToString();
            String action = routeValues["action"].ToString();
            String area = routeValues["area"].ToString();

            String actual = html.LanguageLink().ToString();
            String expected = String.Format("<a class=\"dropdown-toggle\" data-toggle=\"dropdown\">"
                + "<i class=\"fa fa-flag\"></i>{0}<span class=\"caret\"></span></a>"
                + "<ul class=\"dropdown-menu\" role=\"menu\"><li>"
                + "<a href=\"{1}\"><img src=\"/TestDomain/Images/Flags/en.gif\" alt=\"\" />English</a></li><li>"
                + "<a href=\"{2}\"><img src=\"/TestDomain/Images/Flags/lt.gif\" alt=\"\" />Lietuvių</a></li></ul>",
                ResourceProvider.GetActionTitle("Language"),
                HttpUtility.HtmlEncode(new UrlHelper(html.ViewContext.RequestContext).Action(action, new { language = "en", controller = controller, area = area, Param1 = "Value1" })),
                HttpUtility.HtmlEncode(new UrlHelper(html.ViewContext.RequestContext).Action(action, new { language = "lt", controller = controller, area = area, Param1 = "Value1" })));

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Extension method: LogoutLink(this HtmlHelper html)

        [Test]
        public void LogoutLink_FormsLogoutLink()
        {
            String actual = html.LogoutLink().ToString();
            String expected = String.Format("<a href=\"{0}\"><i class=\"fa fa-share\"></i><span>{1}</span></a>",
                new UrlHelper(html.ViewContext.RequestContext).Action("Logout", new { controller = "Auth", area = String.Empty }),
                ResourceProvider.GetActionTitle("Logout"));

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}