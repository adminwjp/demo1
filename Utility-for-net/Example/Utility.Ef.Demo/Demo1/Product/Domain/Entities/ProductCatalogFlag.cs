using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Entities
{
   public enum ProductCatagoryFlag
    {
        None=0x0,
        Nav=0x1,
        ChildrenNav=0x2,
        BottomNav=0x3,
        MobileNav=0x4,
        BottomNavLink=0x5
    }
}
