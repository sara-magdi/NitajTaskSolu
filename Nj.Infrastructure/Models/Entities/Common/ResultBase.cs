using Nj.Infrastructure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nj.Infrastructure.Models.Entities.Common
{
    public abstract class ResultBase
    {
        public ResultBase()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
        public string Details { get; set; }
        public StatusEnum Status { get; set; }
    }
}
