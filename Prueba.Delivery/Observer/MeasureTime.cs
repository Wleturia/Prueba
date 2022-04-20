using Microsoft.AspNetCore.Http;
using Prueba.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Prueba.Delivery.Middleware
{
    public class MeasureTime : IObserver<KeyValuePair<string, object>>
    {
        private readonly Log _log;
        private const string StartTimestampKey = "MeasureTime_StartTimestamp";

        public MeasureTime(Log log)
        {
            _log = log;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (value.Key.Equals("Microsoft.AspNetCore.Hosting.BeginRequest"))
            {
                var httpContext = (HttpContext)value.Value.GetType().GetProperty("httpContext").GetValue(value.Value);
                httpContext.Items[StartTimestampKey] = (long)value.Value.GetType().GetProperty("timestamp").GetValue(value.Value);
            }
            else if (value.Key.Equals("Microsoft.AspNetCore.Hosting.EndRequest"))
            {
                var httpContext = (HttpContext)value.Value.GetType().GetProperty("httpContext").GetValue(value.Value);
                var endTimestamp = (long)value.Value.GetType().GetProperty("timestamp").GetValue(value.Value);
                var startTimestamp = (long)httpContext.Items[StartTimestampKey];

                var duration = new TimeSpan((long)((endTimestamp - startTimestamp) * TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency));
                _log.Message($"Request ended for {httpContext.Request.Path} in {duration.TotalMilliseconds} ms");
            }
        }
    }
}
