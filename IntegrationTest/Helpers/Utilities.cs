using ConsolePresentation;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTest(DataContext db)
        {
            Seeder.SeedData(db);
        }
    }
}
