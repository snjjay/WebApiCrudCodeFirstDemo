using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiCrudCodeFirstDemo.Data;
using WebApiCrudCodeFirstDemo.Models;


namespace WebApiCrudCodeFirstDemo.Repository
{
    public class ClientDetailsRepo : IDisposable
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public ClientDetail ValidateClient(string clientId, string clientSecret)
        {
            return dbContext.ClientDetails.FirstOrDefault(user => user.ClientId == clientId && user.ClientSecret == clientSecret);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}