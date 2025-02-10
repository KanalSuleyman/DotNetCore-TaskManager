using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; protected set; } = default!;
    }
}
