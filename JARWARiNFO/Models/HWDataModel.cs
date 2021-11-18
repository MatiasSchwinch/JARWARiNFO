using System.Collections.Generic;

namespace JARWARiNFO.Models
{
    public class HWDataModel
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public List<Sensor> Sensors { get; set; }
    }

    public class Sensor
    {
        public string Type { get; set; }
        public List<SensorValue> Values { get; set; }
    }

    public class SensorValue
    {
        public string Name { get; set; }
        public float? Value { get; set; }
    }
}
