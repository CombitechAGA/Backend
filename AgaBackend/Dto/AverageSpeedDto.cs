using System;

namespace AgaBackend.Dto
{
    public class AverageSpeedDto
    {
        public string CarId { get; set; }
        public double AverageSpeed { get; set; }
        public DateTime Date { get; set; }
    }
}