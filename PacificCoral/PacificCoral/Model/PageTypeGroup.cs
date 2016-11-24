using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificCoral.Model
{
    public class PageTypeGroup : List<CustomerModel>
    {
        public string Title { get; set; }
       
        public PageTypeGroup(string title)
        {
            Title = title;
        }

        public static IList<PageTypeGroup> All { private set; get; }
    }
   
}
