using System;
using System.Collections.Generic;
using MediatR;

namespace Healthy.Core.Domain.BaseClasses
{
    public class AggregateRoot : IAggregateRoot
    {
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        int? _requestedHashCode;

        public Guid Id { get; protected set; }

        protected AggregateRoot()
        {
            Id = Guid.NewGuid();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default(Guid);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AggregateRoot))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            AggregateRoot item = (AggregateRoot)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; 

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(AggregateRoot left, AggregateRoot right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot left, AggregateRoot right)
        {
            return !(left == right);
        }
    }
}
