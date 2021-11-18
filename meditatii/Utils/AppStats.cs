using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace meditatii.web.Utils
{
    public class AppStats
    {
        private static AppStats _current;
        private static readonly object _lock = new object();

        public static AppStats Current
        {
            get
            {
                lock (_lock)
                {
                    if (_current == null) throw new InvalidOperationException("AppStats uninitialized");
                    return _current;
                }
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (_current != null) throw new InvalidOperationException("Current AppStats can only be set once.");

                if (_current == null)
                {
                    lock (_lock)
                    {
                        if (_current == null) _current = value;
                    }
                }
            }
        }


        public int TotalTeacher { get; set; }
        public int TotalUser { get; set; }
        public int TotalMeetingMinutes { get; set; }
    }
}