using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiHuangBaiKeForUWP.Model
{
    public class PageStackItem
    {
        /// <summary>
        /// 页面类型
        /// </summary>
        public Type SourcePageType { get; set; }

        /// <summary>
        /// 页面参数
        /// </summary>
        public object Parameter { get; set; }
    }
}
