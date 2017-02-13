using System.IO;
using NUnit.Framework;
using TIConfiguration.Logic.API;
using TIConfiguration.Logic._internals.Abstracts;

namespace TIConfiguration.UnitTests
{
    [TestFixture]
    public class ConfigurationExtensionsUnitTests
    {
        [TearDown]
        public void Cleanup()
        {
            // Thread.Sleep(300);
            if (Directory.Exists("./configs"))
                Directory.Delete("./configs", true);
        }

        [InternalConfiguration]
        internal class InternalConfig : ConfigurationBase
        {
            public int TestValue { get; set; } = 123;
        }

        internal class RegularConfig : ConfigurationBase
        {
            public string Value { get; set; } = "old";
        }


        [Test]
        public void IsInternalConfiguration_OnConfiguration_ReturnFalse()
        {
            //ARRANGE
            RegularConfig cfg = new RegularConfig();
            //ACT
            var result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInternalConfiguration_OnInternalConfiguration_ReturnTrue()
        {
            //ARRANGE
            InternalConfig cfg = new InternalConfig();
            //ACT
            var result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsTrue(result);
        }

        [Test]
        public void GetInternalConfig_OnInternalConfiguration_ReturnInstance()
        {
            InternalConfig cfg = new InternalConfig();
            var result = cfg.GetInternalConfig();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetInternalConfig_OnInternalConfiguration_ReturnAttribute()
        {
            InternalConfig cfg = new InternalConfig();
            var result = cfg.GetInternalConfig();

            Assert.IsInstanceOf<InternalConfigurationAttribute>(result);
        }

        [Test]
        public void GetInternalConfig_OnRegularConfiguration_ReturnNull()
        {
            RegularConfig cfg = new RegularConfig();
            var result = cfg.GetInternalConfig();

            Assert.IsNull(result);
        }


        [Test]
        public void WriteTest()
        {
            Assert.Fail();
        }

        [Test]
        public void RefreshTest()
        {
            Assert.Fail();
        }

        [Test]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}