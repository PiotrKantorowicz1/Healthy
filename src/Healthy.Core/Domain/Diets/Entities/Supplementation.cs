using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Supplementation : Entity, ITimestampable
    {
        private ISet<DailySupplementation> _dailySupplementations = new HashSet<DailySupplementation>();
        public Guid UserId { get; protected set; }
        public Interval Interval { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public IEnumerable<DailySupplementation> DailySupplementations
        {
            get => _dailySupplementations;
            protected set => _dailySupplementations = new HashSet<DailySupplementation>(value);
        }

        protected Supplementation()
        {
        }

        public Supplementation(Guid id, Guid userId, Interval interval)
        {
            Id = id;
            UserId = userId;
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetInterval(Interval interval)
        {
            if (interval == null)
            {
                throw new DomainException(ErrorCodes.IntervalNotProvided,
                    "Interval can not be null.");
            }

            Interval = interval;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddDailySupplementation(DailySupplementation dailySupplementation)
        {
            _dailySupplementations.Add(new DailySupplementation(dailySupplementation.Id,
                dailySupplementation.Day));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveDailySupplementation(Guid id)
        {
            var dailySupplementation = GetDailySupplementationOrFail(id);
            _dailySupplementations.Remove(dailySupplementation);
            UpdatedAt = DateTime.UtcNow;
        }

        public DailySupplementation GetDailySupplementationOrFail(Guid id)
        {
            var product = GetDailySupplementation(id);
            if (product.HasNoValue)
            {
                throw new DomainException(ErrorCodes.DailySupplementationNotFound,
                    $"Daily supplementation with id: '{id}' was not found.");
            }

            return product.Value;
        }

        private Maybe<DailySupplementation> GetDailySupplementation(Guid id)
            => DailySupplementations.SingleOrDefault(x => x.Id == id);
    }
}