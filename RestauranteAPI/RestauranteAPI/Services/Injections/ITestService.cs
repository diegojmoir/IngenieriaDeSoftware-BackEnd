namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    using System.Collections.Generic;


    public interface ITestService
    {
         string GetTestDocuments();
         List<Test> GetTestDataFromFireBase();
    }
}
