using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace IssueReproduction.DefaultValueIgnored
{
    public class DummyModel
    {
        public DummyModel(int dummyProperty1, int dummyProperty2)
        {
            if (dummyProperty2 != 1)
            {
                throw new Exception("Expected value: 1, Received value: " + dummyProperty2);
            }
        }

        public int DummyProperty1 { get; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate), DefaultValue(1)]
        public int DummyProperty2 { get; }
    }
}
