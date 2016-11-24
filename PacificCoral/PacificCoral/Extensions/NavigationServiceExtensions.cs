using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;

namespace PacificCoral.Extensions
{
    /// <summary>
    ///     Extends Prism's navigation class.
    /// </summary>
    internal static class NavigationServiceExtensions
    {
        /// <summary>
        ///     Allows to use strongly typed navigation instead of strings.
        /// </summary>
        /// <typeparam name="T">Content page.</typeparam>
        /// <param name="navigationService">Navigation service.</param>
        /// <returns></returns>
        public static Task NavigateAsync<T>(this INavigationService navigationService, NavigationParameters navigationParameters = null) where T : ContentPage
        {
            return navigationService.NavigateAsync(typeof(T).Name, navigationParameters);
        }
    }
}
