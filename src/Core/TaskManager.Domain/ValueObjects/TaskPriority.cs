using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.ValueObjects
{
    public class TaskPriority
    {
        public PriorityLevel PriorityLevel { get; private set; }

        // Parameterless constructor foe Ef core
        public TaskPriority()
        {
        }

        // Constructor with parameters
        public TaskPriority(PriorityLevel priority)
        {
            PriorityLevel = priority;
        }

        // Business logic: Escalate priority
        public void EscalatePriority()
        {
            PriorityLevel = PriorityLevel switch
            {
                PriorityLevel.Low => PriorityLevel.Medium,
                PriorityLevel.Medium => PriorityLevel.High,
                _ => PriorityLevel
            };
        }

        public override string ToString()
        {
            return PriorityLevel.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is TaskPriority other)
            {
                return PriorityLevel == other.PriorityLevel;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return PriorityLevel.GetHashCode();
        }
    }
}
