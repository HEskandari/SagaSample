using System;
using Bogus;

namespace Messages
{
    public class DataFaker
    {
        public static SagaExecutionData ExecutionDataFaker = new Faker<SagaExecutionData>()
                .RuleFor(c => c.WaitForSeconds, f => f.Date.Timespan(TimeSpan.FromSeconds(60)))
                .RuleFor(c => c.ProductName, f => f.Commerce.ProductName())
                .RuleFor(c => c.Color, f => f.Commerce.Color())
                .RuleFor(c => c.Category, f => f.Commerce.Random.ListItem(f.Commerce.Categories(10)))
                .RuleFor(c => c.Color, f => f.Commerce.Color())
            ;
    }
}