using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTests: IDisposable
  {
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=3306; database=hair_salon2_test;";
    }
    public void Dispose()
    {
      Client.ClearAll();
    }

    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;

      Assert.AreEqual(0, result);
    }

  }
}
