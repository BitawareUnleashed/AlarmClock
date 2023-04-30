using System.Globalization;
using System.Timers;

namespace DateTimeComponent.Models
{
    public class SystemWatch : IDisposable
    {
        /// <summary>
        /// Event occurs every second changed.
        /// </summary>
        public event EventHandler<DateTime>? SecondChangedEvent;
        /// <summary>
        /// Occurs for separator.
        /// </summary>
        public event EventHandler<string>? Separator;

        #region FILEDS
        /// <summary>
        /// The main timer able to beat the time
        /// </summary>
        private System.Timers.Timer? aTimer;

        /// <summary>
        /// The starting date.
        /// </summary>
        private DateTime StartingDateSeconds = DateTime.Now;
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="SystemWatch"/> class.
        /// </summary>
        /// <param name="interval">The interval in milliseconds.</param>
        public SystemWatch(int interval = 10)
        {
            // Create a timer and set a two second interval.
            if (aTimer == null)
            {
                aTimer = new System.Timers.Timer();
                aTimer.Interval = interval;

                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed -= ATimer_Elapsed!;
                aTimer.Elapsed += ATimer_Elapsed!;

                // Have the timer fire repeated events (true is the default)
                aTimer.AutoReset = true;

                // Start the timer
                aTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the ATimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeCheck(e.SignalTime);
        }

        #region Methods

        /// <summary>
        /// Times the check.
        /// </summary>
        /// <param name="e">The e.</param>
        private void TimeCheck(DateTime e)
        {
            // Useful for separator event
            Separator?.Invoke(this, e.Second % 2 == 0 ? " " : ":");

            // Seconds
            if ((e - StartingDateSeconds).Seconds > 0)
            {
                StartingDateSeconds = e;
                SecondChangedEvent?.Invoke(this, e);
            }
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // If already disposed then exit
            if (_disposed) { return; }
            // Dispose managed state (managed objects).
            if (disposing) { aTimer!.Dispose(); }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
