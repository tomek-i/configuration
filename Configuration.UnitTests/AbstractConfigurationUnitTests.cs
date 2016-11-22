using Moq;
using NUnit.Framework;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TIConfiguration.UnitTests
{



    [TestFixture]
    public class ConfigurationExtensionsUnitTests
    {
        

    }

    [TestFixture]
    public class ConfigurationManagerUnitTests
    {
        
    }

    [TestFixture]
    public class AbstractConfigurationUnitTests
    {
        private class ConcreteConfiguration : ConfigurationBase
        {
            
        }

        [Test]
        public void Name_ConcreteClass_HasConcreteClassName()
        {

           // ConfigurationManager.Test();
            //ARRANGE
            IConfiguration config = new ConcreteConfiguration();
            
            //ACT
            string result = config.Name;
            
            //ASSERT
            StringAssert.Contains("ConcreteConfiguration", result);
        }

        [Test]
        public void Name_BasedOnType_SetsTheNameBasedOnType()
        {
           
            //ARRANGE
            Mock<ConfigurationBase> mock = new Mock<ConfigurationBase>();
            ConfigurationBase config = mock.Object;
         
            //ACT
            string result = config.Name;

            //ASSERT
            StringAssert.Contains("Configuration",result);
        }
    }
}
