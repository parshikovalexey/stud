using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    public class WeightModels
    {
        public string TitleOfItem { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public string UnitOfWeight { get; set; }

        public WeightModels() { }
    }
}