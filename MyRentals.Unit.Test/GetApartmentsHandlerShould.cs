namespace MyRentals.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using MyRentals.Domain.CommandModels;
    using MyRentals.Domain.Repositories;
    using MyRentals.Domain.StorageModels;
    using MyRentals.Service.Handlers;
    using MyRentals.Service.Messages;
    using OneOf.Types;
    using Xunit;

    public class GetApartmentsHandlerShould
    {
        private readonly MockRepository mr;
        private readonly IRequestHandler<GetApartments, IEnumerable<Apartment>> handler;
        private static readonly PropertyFilters NoFilters = new PropertyFilters(new None(), new None(), new None(), false);

        public GetApartmentsHandlerShould()
        {
            this.mr = new MockRepository(MockBehavior.Strict);
            var repo = this.mr.Create<IApartmentRepository>();
            repo.Setup(a => a.GetAll(NoFilters, CancellationToken.None)).ReturnsAsync(new[] {new Apartment(1, "Apartment", "Description", 1, 2, 3, 4, 5, DateTime.UtcNow, 0, false),});
            this.handler = new GetApartmentsHandler(repo.Object);
        }

        [Fact]
        public async Task Run()
        {
            var r = await this.handler.Handle(new GetApartments(NoFilters),
                CancellationToken.None);

            r.Should().NotBeNull();
            this.mr.VerifyAll();
        }
    }
}
