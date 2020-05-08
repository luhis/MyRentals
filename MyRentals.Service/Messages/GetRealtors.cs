﻿namespace MyRentals.Service.Messages
{
    using System.Collections.Generic;
    using MediatR;
    using MyRentals.Domain.StorageModels;

    public class GetRealtors : IRequest<IEnumerable<Realtor>>
    {

    }
}