using System;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Exceptions;
using Xunit;

namespace Healthy.Tests.Diets.Domain
{
    public class DomainTests
    {
        [Fact]
        public void Check_if_category_domain_object_return_a_error()
        {
            Assert.Throws<DomainException>(() => new Category(Guid.NewGuid(), ""));
            Assert.Throws<DomainException>(() => new Category(Guid.NewGuid(), @"..........................................................................
                ..........................................................................................................................................
                .........................................................................................................................................."));
        }
    }
}