using System;
using NServiceBus;

namespace SagaEndpoint
{
    public class MySagaData : ContainSagaData
    {
        public MySagaData()
        {
            ShouldStart = true;
        }

        public bool ShouldStart { get; set; }
        public AdditionalData MoreData { get; set; }
        public Guid SagaID { get; set; }
    }

    public class AdditionalData
    {
        public string Text {  get;  set; }
        public DateTime SentDate { get; set; }
        public bool BooleanValue { get; set; }
        public string Image { get; set; }
    }
}