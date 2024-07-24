using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string Author { get; set; }
    }
}