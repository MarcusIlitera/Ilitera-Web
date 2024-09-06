using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Administration;

namespace Ilitera.Net
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            EO.Web.Runtime.AddLicense(
                "0luX+vYd8qLm8s7NsGqltLPLrneEjrHLn1mXpLHLu5rb6LEf+KncwbP/8Z7c" +
                "2voQ9luX+vYd8qLm8s7NsGqltLPLrneEjrHLn1mXpLHLu5rb6LEf+KncwbP4" +
                "9KXr7eEM5p6ZpAcQ8azg8//ooWqossHNn2i1kZvLn1mXpLHLn3XY6PXL87Ln" +
                "6c7Nwprj6f8P4KuZpAcQ8azg8//ooWqossHNn2i1kZvLn1mXpLHLn3XY6PXL" +
                "87Ln6c7Nwprj8PMM4qSZpAcQ8azg8//ooWqossHNn2i1kZvLn1mXpLHLn3XY" +
                "6PXL87Ln6c7NwIO43OYb66jY6PYdoVnt6QMe6KjlwbPcsGenprHavUaBpLHL" +
                "n1mXpLHn4J3bpAUk7560ptUU4KXm67PL9Z7p9/oa7XaZtcLZr1uXs8+4iVmX" +
                "pLHLn1mXwPIP41nr/QEQvFvK9P0U863c9rPL9Z7p9/oa7XaZtcLZr1uXs8+4" +
                "iVmXpLHLn1mXwPIP41nr/QEQvFvE5QQW5J286PofoVnt6QMe6KjlwbPcsGen" +
                "prHavUaBpLHLn1mXpLHn4J3bpAUk7560ptgd6J2ZpAcQ8azg8//ooWqossHN" +
                "n2i1kZvLn1mXpLHLn3XY6PXL87Ln6c7Nwqjj8wP76Jzi6QPNn6/c9gQU7qe0" +
                "psLcrWmZpMDpjEOXpLHLn1mXpM0M452X+Aob5HaZ1wEQ66W67PYO6p7pprEh" +
                "5Kvq7QAZvFuotb/boVmmwp61n1mXpLHLn1mz5fUPn63w9PbooX7b7QUa8VuX" +
                "+vYd8qLm8s7NsGqltLPLrneEjrHLn1mXpLHLu5rb6LEf+KncwbP07Jre6esa" +
                "7qaZpAcQ8azg8//ooWqossHNn2i1kZvLn1mXpLHLn3XY6PXL87Ln6c7Nw6ju" +
                "8v0a4J3c9rPL9Z7p9/oa7XaZtcLZr1uXs8+4iVmXpLHLn1mXwPIP41nr/QEQ" +
                "vFu98AAM857pprEh5Kvq7QAZvFuotb/boVmmwp61n1mXpLHLn1mz5fUPn63w" +
                "9PbooYzj7fUQoVnt6QMe6KjlwbPcsGenprHavUaBpLHLn1mXpLHn4J3bpAUk" +
                "7560ptcX+Kjs+LPL9Z7p9/oa7XaZtcLZr1uXs8+4iVmXpLHLn1mXwPIP41nr" +
                "/QEQvFu86Pof4Jvj6d0M4Z7jprEh5Kvq7QAZvFuotb/boVmmwp61n1mXpLHL" +
                "n1mz5fUPn63w9PbooYXg9wXt7rGZpAcQ8azg8//ooWqossHNn2i1kZvLn1mX" +
                "pLHLn3XY6PXL87Ln6c7Nwqjk5gDt7rGZpAcQ8azg8//ooWqossHNn2i1kZvL" +
                "n1mXpLHLn3XY6PXL87Ln6c7Nwprn+PQT4FuX+vYd8qLm8s7NsGqltLPLrneE" +
                "jrHLn1mXpLHLu5rb6LEf+KncwbP/7qjj2PoboVnt6QMe6KjlwbPcsGenprHa" +
                "vUaBpLHLn1mXpLHn4J3bpAUk7560puMM86Ll67PL9Z7p9/oa7XaZtcLZr1uX" +
                "s8+4iVmXpLHLn1mXwPIP41nr/QEQvFvK8PoP5KuZpAcQ8azg8//ooWqossHN" +
                "n2i1kZvLn1mXwMAM66Xm+8+4iVmXpLHn7qvb6QP07Z/mpPUM8560psbasGym" +
                "tsHcs1uX+vYd8qLm8s7NsGqZpMDpjEOXpLHLu6zg6/8M867p6c/3sbG8z/gg" +
                "wZ/dx+YM9rHZ7eId6qW4wc7nrqzg6/8M867p6c+4iXWm8PoO5Kfq6c+4iXXj" +
                "7fQQ7azcwp61n1mXpM0X6Jzc8gQQyJ21usLisHGouMTesnWm8PoO5Kfq6doP" +
                "vUaBpLHLn3Xj7fQQ7azc6c/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4iVmX" +
                "pLHLn1mXwPIP41nr/QEQvFvE6f8goVnt6QMe6KjlwbPcsGenprHavUaBpLHL" +
                "n1mXpLHn4J3bpAUk7560puQX6J3c0fYZ9FuX+vYd8qLm8s7NsGqltLPLrneE" +
                "jrHLn1mXpLHLu5rb6LEf+KncwbP/4JvK+AMU7w==");



        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception err = Server.GetLastError();


            Session["ExceptionType"] = err.InnerException.Message.ToString();
            Session["Source"] = err.InnerException.TargetSite.DeclaringType.FullName; //err.InnerException.Source.ToString();
            Session["TargetSite"] = err.InnerException.TargetSite.ToString();
            Session["Message"] = err.InnerException.StackTrace.ToString();


            //if (err.InnerException.StackTrace.ToString().ToUpper().IndexOf(" POOL") > 0)  //se for erro de pool, dar um recycle
            //{

            //    using (Microsoft.Web.Administration.ServerManager iisManager = new Microsoft.Web.Administration.ServerManager())
            //    {
            //        Microsoft.Web.Administration.SiteCollection sites = iisManager.Sites;
            //        foreach (Microsoft.Web.Administration.Site site in sites)
            //        {
            //            if (site.Name == System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName())
            //            {
            //                iisManager.ApplicationPools[site.Applications["/"].ApplicationPoolName].Recycle();
            //                break;
            //            }
            //        }
            //    }

            //}

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
