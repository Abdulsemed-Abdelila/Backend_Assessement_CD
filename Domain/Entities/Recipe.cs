using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public int Prep_Time { get; set; }
    }
}