using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNA_X_Men.Models
{
    public class DNA
    {
        public int Id { get; set; } 
        public List<string> DnaSequence { get; set; }
        public bool IsMutant { get; set; }
    }
}