﻿using System;

namespace Healthy.Contracts.Events.Users
{
    public class ProviderChanged
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }

        public ProviderChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
