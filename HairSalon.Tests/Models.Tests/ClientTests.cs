using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests

{
  [TestClass]
  public class ClientTest: IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=3306; database=hair_salon_test;";
    }
    public void Dispose()
    {
      Client.ClearAll();
    }
    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      List<Client> clients = Client.GetAll();
      int result = clients.Count;

      Assert.AreEqual(0, result);
    }
  }
}
