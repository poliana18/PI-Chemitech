using Chemitech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chemitech.ViewModel
{
    public class GerarGrafico
    {
        public IList<Bombona> Bombona { get; set; }
        public List<Descarte> Descarte { get; set; }
        
    }
}