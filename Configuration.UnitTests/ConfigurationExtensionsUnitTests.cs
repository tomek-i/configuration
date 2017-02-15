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


        [Test, Category("Integration Tests")]
        public void Write_Configuration_FileExists()
        {
            RegularConfig cfg = new RegularConfig();
            cfg.Write();
            Assert.IsTrue(File.Exists("./configs/debug/RegularConfig.json"));
        }
        [Test, Category("Integration Tests")]
        public void Write_Configuration_ReturnsTrue()
        {
            RegularConfig cfg = new RegularConfig();
            var result = cfg.Write();

            Assert.IsTrue(result);
        }

        [Test, Category("Integration Tests")]
        public void Reload_ExistingCfgFile_ReturnsNewInstance()
        {
           RegularConfig actual = new RegularConfig();
            actual.Value = "testvalue";
            actual.Write();

            var cfgUnderTest = actual.Reload<RegularConfig>();

            Assert.AreNotSame(actual,cfgUnderTest);
        }
        [Test,Category("Integration Tests")]
        public void Reload_ExistingCfgFile_InstanceReturnedHasCorrectValue()
        {
            new RegularConfig {Value = "testvalue"}.Write();

            var cfgUnderTest = new RegularConfig().Reload<RegularConfig>();

            Assert.AreEqual("testvalue",cfgUnderTest.Value);
        }
        [Test, Category("Integration Tests")]
        public void Reload_NonExistingCfgFile_ReturnsSameInstance()
        {
            var cfg = new RegularConfig { Value = "testvalue" };
            
            var cfgUnderTest = cfg.Reload<RegularConfig>();

            Assert.AreSame(cfg,cfgUnderTest);
        }

        [Test, Category("Integration Tests")]
        public void Update_NonExistingCfgFile_WritesCfg()
        {
            var cfg = new RegularConfig();

            cfg.Update();

            Assert.IsTrue(File.Exists("./configs/debug/RegularConfig.json"));
        }
        [Test, Category("Integration Tests")]
        public void Update_ExistingCfgFile_OverWritesCfgWithNewValue()
        {
            var cfg = new RegularConfig();

            cfg.Update<RegularConfig>(x=>x.Value = "NewValue");

            var cfgUnderTest = new RegularConfig().Reload<RegularConfig>();
            Assert.AreEqual("NewValue", cfgUnderTest.Value);
        }
        [Test, Category("Integration Tests")]
        public void Update_ExistingCfgFile_SetsPropertyToNewValue()
        {
            var cfg = new RegularConfig();

            cfg.Update<RegularConfig>(x => x.Value = "NewValue");

            Assert.AreEqual("NewValue", cfg.Value);
        }



    }
}