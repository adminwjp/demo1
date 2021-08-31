
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Zk;
using Xunit;

namespace Utility.XUnit
{
    public class ZookeeperTest
    {
        //[Fact]
        public void Test()
        {
            ZooKeeperHelper zooKeeperHelper = new ZooKeeperHelper();
            var a=zooKeeperHelper.Create("/a/b");
            var b = zooKeeperHelper.GetData("/a/b");
        }
    }
}
