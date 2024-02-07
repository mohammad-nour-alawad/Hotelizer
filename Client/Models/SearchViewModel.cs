using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class SearchViewModel
    {
        public string label { get; set; }
        public string category { get; set; }

        public SearchViewModel(string a, string t)
        {
            label = a;
            category = t;
        }
    }
}
