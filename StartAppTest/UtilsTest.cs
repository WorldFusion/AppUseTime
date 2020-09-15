using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StartAppTest
{
    [TestClass]
    public　class UtilsTest
    {
        [TestMethod]
        public void ReadIniFile()
        {
            // 



        }

        [TestMethod]
        public void GetGWBPathTest()
        {
            var gwb = StartApp.Utils.ReadInfo.GetGWBPath();
            if (string.IsNullOrEmpty(gwb))
                throw new Exception("not get GenomicsWorkbench Path");


            System.Diagnostics.Debug.WriteLine(gwb);
        }

        [TestMethod]
        public void InitInfoTest()
        {
            var res = StartApp.Utils.ReadInfo.InitAppInfo();

            if (! string.IsNullOrEmpty(res))
                throw new Exception("Error " + res);


            System.Diagnostics.Debug.WriteLine(res);

        }

    }
}
