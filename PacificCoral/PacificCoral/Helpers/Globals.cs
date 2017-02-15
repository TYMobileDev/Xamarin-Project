using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacificCoral.Model;

namespace PacificCoral
{
    public static class Globals
    {
        private static string currentOpco = string.Empty;
        private static CustomerCodes popupParameter;

        public static string CurrentOpco
        {
            get
            {
                return currentOpco;
            }

            set
            {
                currentOpco = value;
                Helpers.Settings.LastOPCO = value;
            }
        }

        public static CustomerCodes PopupParameter
        {
            get
            {
                return popupParameter;
            }

            set
            {
                popupParameter = value;
            }
        }
    }
    #region enumerations

    public enum enumRefreshTableStatus
    {
        NotRefreshing,
        Begin,
        End
    }
    public enum enumOrderDirection
    {
        Ascending,
        Descending
    }

    #endregion
}
