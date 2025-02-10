using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.ValueObjects
{
    public class DueDateRange
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

        public DueDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date cannot be before the start date.");

            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
