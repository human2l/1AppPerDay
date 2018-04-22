using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace UTS.ScheduleSystem.Web
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        Controller controller;

        protected void Page_Init(object sender, EventArgs e)
        {
            
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;

        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null && controller == null)
            {

                controller = (Controller)Session["Controller"];

                if(controller.CurrentUser == null)
                {
                    editorPage.Visible = false;
                    approverPage.Visible = false;
                    dataMaintainerPage.Visible = false;
                }
                else
                {
                    switch (controller.CurrentUser.Role)
                    {
                        case Role.A:
                            editorPage.Visible = false;
                            dataMaintainerPage.Visible = false;
                            break;
                        case Role.E:
                            approverPage.Visible = false;
                            dataMaintainerPage.Visible = false;
                            break;
                        case Role.DM:
                            approverPage.Visible = false;
                            editorPage.Visible = false;
                            break;
                        default:
                            editorPage.Visible = false;
                            approverPage.Visible = false;
                            dataMaintainerPage.Visible = false;
                            break;
                    }
                }
                
            }
            else
            {
                editorPage.Visible = false;
                approverPage.Visible = false;
                dataMaintainerPage.Visible = false;
            }

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //if(Session["Controller"] != null)
            //{
            //    controller = (Controller)Session["Controller"];
            //    controller.CurrentUser
            //}
            if(controller != null)
            {
                controller.CurrentUser = null;
            }
        }
    }

}