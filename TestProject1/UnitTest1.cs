using Bookify.Domain.Model;
using NUnit.Framework.Internal;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            User user = new User(1,"test","test","test",new List<Book>() { new Book("test",new List<string>() { "Mistery", "Drama"}) });
            var output = user.GetUserPreferences();
            var expected = new List<string>() { "Mistery", "Drama" };
            CollectionAssert.AreEqual(expected, output);
        }
        [TestMethod]
        public void TestMethod2()
        {
            User user = new User(1, "test", "test", "test", new List<Book>() { new Book("test", new List<string>() { "Mistery", "Drama" }), new Book("test2", new List<string>() { "Drama", "Comedy"}) });
            var output = user.GetUserPreferences();
            var expected = new List<string>() { "Drama", "Mistery", "Comedy"};
            CollectionAssert.AreEqual(expected, output);
        }
        [TestMethod]
        public void TestMethod3()
        {
            User user = new User(1, "test", "test", "test", new List<Book>() { new Book("test", new List<string>()) });
            var output = user.GetUserPreferences();
            var expected = new List<string>() ;
            CollectionAssert.AreEqual(expected, output);
        }
    }
}