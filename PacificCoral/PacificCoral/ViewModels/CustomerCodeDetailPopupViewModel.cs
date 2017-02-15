using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
using PacificCoral.Model;

namespace PacificCoral.ViewModels
{
    class CustomerCodeDetailPopupViewModel : BasePageViewModel
    {

        private string _contactName = "";
        private string _contactPosition = "";
        private string _address = "";
        private string _cityStateZip = "";
        private string _telephone = "";
        private string _email = "";


        public CustomerCodeDetailPopupViewModel()
        {
            loadData();
        }
        private void loadData()
        {
            if (Globals.PopupParameter == null) return;
            CustomerCodes cc = Globals.PopupParameter as CustomerCodes;
            ContactName = cc.ContactName;
            ContactPosition = cc.ContactPosition;
            Address = cc.Address;
            CityStateZip = ((cc.City == null) ? "" : cc.City) + ", " +
                ((cc.State == null) ? "" : cc.State) + " " +
                ((cc.Zip == null) ? "" : cc.Zip);
            Telephone = cc.Telephone;
            Email = cc.Email;
        }
        public string ContactName
        {
            get
            {
                return _contactName;
            }

            set
            {
                SetProperty<string>(ref _contactName, value);
            }
        }

        public string ContactPosition
        {
            get
            {
                return _contactPosition;
            }

            set
            {
                SetProperty<string>(ref _contactPosition, value);
            }
        }

        public string CityStateZip
        {
            get
            {
                return _cityStateZip;
            }

            set
            {
                SetProperty<string>(ref _cityStateZip, value);
            }
        }

        public string Telephone
        {
            get
            {
                return _telephone;
            }

            set
            {
                SetProperty<string>(ref _telephone, value);
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                SetProperty<string>(ref _email, value);
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }
    }
}
