using Nj.Infrastructure.Models.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nj.Infrastructure.Models
{
    public class TodoItem :BaseEntity
    {
        public string Title { get; set; } 
        public bool IsCompleted { get; set; }
    }
}
