using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using Moq;
namespace ContactFormTest
{
    internal class SrsBsnsDbContextMock
    {
        public static SrsBsnsChallengeDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<SrsBsnsChallengeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new SrsBsnsChallengeDbContext(options);

            return context;
        }
    }
}
