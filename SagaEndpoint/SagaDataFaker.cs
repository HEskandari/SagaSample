using Bogus;

namespace SagaEndpoint
{
    public class SagaDataFaker
    {
        public static AdditionalData AdditionalDataFaker = new Faker<AdditionalData>()
                .RuleFor(c => c.Text, f => f.Lorem.Sentences(2))
                .RuleFor(c => c.Image, f => f.Image.PlaceImgUrl())
                .RuleFor(c => c.BooleanValue, f => f.Random.Bool())
                .RuleFor(c => c.SentDate, f => f.Date.Future())
            ;
    }
}