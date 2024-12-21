using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nj.Infrastructure.Models.Entities.Common
{
    public class ResultEntity<T> : ResultBase 
    {
        public T Entity { get; set; }

        public static implicit operator ResultEntity<T>(Task<ResultEntity<int>> v)
        {
            throw new NotImplementedException();
        }
    }
}
