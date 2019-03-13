using System;
using System.Collections.Generic;
using System.Text;

namespace MeetupSnagger
{
    class SpreadsheetGroupView
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Size { get; set; }
        public string Cadence => "";
        public string Url { get; set; }
        public string Audience => "";
        public string Notes => "";
    }
}
