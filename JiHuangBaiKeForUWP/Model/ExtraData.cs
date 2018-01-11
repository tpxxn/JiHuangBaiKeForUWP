using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiHuangBaiKeForUWP.Model
{
    internal class ViewExtraData
    {
        public string Classify { get; set; }
        public string Picture { get; set; }
        public double ScrollViewerVerticalOffset { get; set; }
        public List<string> ExpandedList { get; set; }
    }

    internal class SearchExtraData
    {
        public string SourcePath { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }
    }
}
