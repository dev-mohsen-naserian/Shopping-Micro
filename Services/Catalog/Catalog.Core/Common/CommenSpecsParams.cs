using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Common;

public class CommenSpecsParams
{
    private int _pageSize = 10;
    private const int MaxpageSize = 70;
    public int PageSize 
    {
        get => _pageSize;
        set => _pageSize = value > MaxpageSize ? MaxpageSize : value;
    }
    public int PageIndex { get; set; } = 1;
    public string Sort { get; set; }
    public string Search { get; set; }
}
