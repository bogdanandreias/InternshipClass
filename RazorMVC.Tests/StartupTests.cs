using RazorMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RazorMVC.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertUrlToHerokuString()
        {
            //Assume
            string url = "postgres://wxtoozdfgwmqeg:77330bd1daff0c623e9583fe62ca7e572646d87a521adbb8f3c5f3c68a0b0e26@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/dbid7964iqr5rc";

            //Act
            var herokuConnectionString = Startup.ConvertDatabaseUrlToHerokuString(url);

            //Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=dbid7964iqr5rc;User Id=wxtoozdfgwmqeg;Password=77330bd1daff0c623e9583fe62ca7e572646d87a521adbb8f3c5f3c68a0b0e26;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }

        [Fact]
        public void ShouldThrowExceptionOnCorruptUrl()
        {
            //Assume
            string url = "Server=127.0.0.1;Port=5432;Database=internshipclass;User Id=internshipclassadmin;Password=dhGKIYVccVqs3qV9wWb3;";

            //Act & Assert
            var exception = Assert.Throws<FormatException>(() => Startup.ConvertDatabaseUrlToHerokuString(url));

            Assert.StartsWith("Database URL could not be converted! Check this", exception.Message);

        }
    }
}
