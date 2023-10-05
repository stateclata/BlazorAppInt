using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class AutosuggestResult
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public AutosuggestResult(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
