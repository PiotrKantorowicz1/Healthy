using System;
using System.Collections.Generic;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Suplementation : Entity, ITimestampable
    {
        ISet<DailySupplementation> _dailySupplementations = new HashSet<DailySupplementation>();
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<DailySupplementation> DailySupplementations
        {
            get { return _dailySupplementations; }
            protected set { _dailySupplementations = new HashSet<DailySupplementation>(value); }
        }
    }
}