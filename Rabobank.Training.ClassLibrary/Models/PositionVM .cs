using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary.Models
{
    public class PositionVM
    {
        public string Code { get; set; }
        public string Name { get; set; }       
        public decimal Value { get; set; }
        public List<MandateVM> Mandates { get; set; }
    }
}
