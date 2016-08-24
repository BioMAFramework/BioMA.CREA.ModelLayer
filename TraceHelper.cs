namespace CRA.ModelLayer
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Utility class that defines the trace <see cref="System.Diagnostics.TraceSource">source</see> of the model layer's log messages. Trace name is 'CRA.ModelLayer' (this is the name to be used in the applications configuration files to listen to this trace).
    /// </summary> 
    public static class TraceHelper
    {
        private static readonly TraceSource Source = new TraceSource(Assembly.GetExecutingAssembly().GetName().Name);

        /// <summary>
        /// Add a <see cref="System.Diagnostics.TraceListener">listener</see> to the source
        /// </summary>
        /// <param name="listener"></param>
        public static void AddListener(TraceListener listener)
        {
            Source.Listeners.Add(listener);
            Source.Switch.Level = ~SourceLevels.Off;
        }

        /// <summary>
        /// Trace a log event
        /// </summary>
        /// <param name="eventType"><see cref="System.Diagnostics.TraceEventType">Event</see> type</param>
        /// <param name="id">Trace message id</param>
        /// <param name="message">Message to be logged</param>
        [Conditional("TRACE")]
        public static void TraceEvent(TraceEventType eventType, int id, string message)
        {
            Source.TraceEvent(eventType, id, message);
            Source.Flush();
        }
    }
}

