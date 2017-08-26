using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;

namespace JiHuangBaiKeForUWP.Model
{
    class CallExternalContent
    {
        /// <summary>
        /// Redirect user to Windows Store and open the review window for current App
        /// </summary>
        /// <returns>Task</returns>
        public static async Task OpenStoreReviewAsync()
        {
            var pfn = Package.Current.Id.FamilyName;
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?PFN=" + pfn));
        }

        /// <summary>
        /// Open Microsoft Edge and navigate to a url
        /// </summary>
        /// <param name="url">Website Url</param>
        /// <returns>Task</returns>
        public static async Task OpenWebsiteAsync(string url)
        {
            await Launcher.LaunchUriAsync(new Uri(url, UriKind.Absolute));
        }

        /// <summary>
        /// Open Windows Mail
        /// </summary>
        /// <param name="toAddress">To Email Address</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Email Content</param>
        /// <returns></returns>
        public static async Task OpenEmailComposeAsync(string toAddress, string subject, string body)
        {
            var uri = new Uri($"mailto:{toAddress}?subject={subject}&body={body}", UriKind.Absolute);
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
