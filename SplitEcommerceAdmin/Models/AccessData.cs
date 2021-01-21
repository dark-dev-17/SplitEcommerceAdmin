using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitEcommerceAdmin.Models
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessDataSession : ActionFilterAttribute
    {
        private AccessCtrl AccessCtrl;
        public int[] IdAction { get; set; }

        public AccessDataSession()
        {
            AccessCtrl = new AccessCtrl(new Splittel("Server=68.71.58.18;Port=3306;Database=fibremex_b2b_test;Uid=fibremex_mxgod;Pwd=9Xfs@YBGAG*S;Allow Zero Datetime=True;Convert Zero Datetime=True;Persist Security Info=True"));

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.IsAvailable && filterContext.HttpContext.Session.GetInt32("user_id_permiss") != null)
            {
                if (IdAction.Length > 0)
                {

                    bool autorize = AccessCtrl.ValidAction(1, (int)filterContext.HttpContext.Session.GetInt32("user_id_permiss"));

                    if (!autorize)
                    {
                        filterContext.Result = new BadRequestObjectResult("Sin permisos");
                        return;
                    }
                    AccessCtrl.Terminar();
                }
            }
            else
            {
                filterContext.Result = new BadRequestObjectResult("Por favor inicia sesión");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }

    #region AccessMultipleView
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessMultipleView : ActionFilterAttribute
    {
        private AccessCtrl AccessCtrl;
        public int[] IdAction { get; set; }

        public AccessMultipleView()
        {
            AccessCtrl = new AccessCtrl(new Splittel("Server=68.71.58.18;Port=3306;Database=fibremex_b2b_test;Uid=fibremex_mxgod;Pwd=9Xfs@YBGAG*S;Allow Zero Datetime=True;Convert Zero Datetime=True;Persist Security Info=True"));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.IsAvailable && filterContext.HttpContext.Session.GetInt32("user_id_permiss") != null)
            {
                if (IdAction.Length > 0)
                {
                    bool autorize = AccessCtrl.ValidAction(1, (int)filterContext.HttpContext.Session.GetInt32("user_id_permiss"));

                    if (!autorize)
                    {
                        filterContext.Result = new ViewResult
                        {
                            ViewName = "../ErrorPages/NoAccess",
                        };
                        return;
                    }
                    AccessCtrl.Terminar();
                }
            }
            else
            {
                var isHtps = filterContext.HttpContext.Request.IsHttps;
                var Host = filterContext.HttpContext.Request.Host;
                var Path = filterContext.HttpContext.Request.Path;
                string url = string.Format("{0}//{1}{2}", (isHtps ? "https:" : "http:"), Host, Path);
                filterContext.HttpContext.Session.SetString("url_next", url);
                filterContext.Result = new RedirectResult("~/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
    #endregion

    #region AccessView
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessView : ActionFilterAttribute
    {
        private AccessCtrl AccessCtrl;

        public AccessView()
        {
            //AccessCtrl = new AccessCtrl(new Splittel("Server=68.71.58.18;Port=3306;Database=fibremex_b2b_test;Uid=fibremex_mxgod;Pwd=9Xfs@YBGAG*S;Allow Zero Datetime=True;Convert Zero Datetime=True;Persist Security Info=True"));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.IsAvailable && filterContext.HttpContext.Session.GetInt32("user_id_permiss") != null)
            {

            }
            else
            {
                filterContext.Result = new RedirectResult("~/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
    #endregion
}
