﻿namespace Iris.Controllers
{
    public class SendRecord
    {
        public int TimeId { get; set; }

        public int SentCount { get; set; }
        public int SentSize { get; set; }

        public int FirstSentSize { get; set; }
        
        public int AckedSize { get; set; }
        
        public int ResentSize { get; set; }

        public SendRecord(int timeId)
        {
            TimeId = timeId;
        }   
    }
}
