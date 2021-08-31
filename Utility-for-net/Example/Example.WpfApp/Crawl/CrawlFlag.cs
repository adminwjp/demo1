using System;
using System.Collections.Generic;
using System.Text;
using Utility.Wpf.Attributes;

namespace Wpf.Crawl
{
    [MenuGroupAttribute(Config = "config/crawl.json")]
    public enum CrawlFlag
    {
        Task=0,
        FiveOneJobTask =1,
        LaGouTask = 2,
        Category=3
    }
}
