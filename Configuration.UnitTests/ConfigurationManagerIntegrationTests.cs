using System.IO;
using NUnit.Framework;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TIConfiguration.UnitTests
{
    [TestFixture, Category("Integration Tests")]
    public class ConfigurationManagerIntegrationTests
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

            public override IConfiguration Default()
            {
                throw new System.NotImplementedException();
            }
        }

        internal class RegularConfig : ConfigurationBase
        {
            public string Value { get; set; } = "old";

            public override IConfiguration Default()
            {
                throw new System.NotImplementedException();
            }
        }

       

        

        [Test, Category("Integration Tests")]
        public void Update_Property_UpdatePersistsInFile()
        {
            //Arrange
            Cleanup();
            ConfigurationManager.Instance.Write(new RegularConfig() {Value = "old"});

            //ACT
            ConfigurationManager.Instance.Update<RegularConfig>(x => x.Value = "new");

            //Assert
            Assert.IsTrue(ConfigurationManager.Instance.Read<RegularConfig>().Value == "new");
        }

        [Test, Category("Integration Tests")]
        public void Update_Property_ReturnsConfigWithUpdatedProperty()
        {
            //Arrange
            ConfigurationManager.Instance.Write(new RegularConfig() {Value = "old"});

            //ACT
            var updated = ConfigurationManager.Instance.Update<RegularConfig>(x => x.Value = "new");

            //Assert
            Assert.IsTrue(updated.Value == "new");
        }

        
        [Test, Category("Integration Tests")]
        public void Write_InternalConfgiguration_ShouldreturnTrueIfSuccessful()
        {
            //ARRANGE
            InternalConfig cfg = new InternalConfig();
            //ACT
            var result = ConfigurationManager.Instance.Write(cfg);
            //ASSERT
            Assert.IsTrue(result);
        }

        [Test, Category("Integration Tests")]
        public void Write_InternalConfgiguration_ShouldBeInInternalFolder()
        {
            //ARRANGE
            InternalConfigurationAttribute att = new InternalConfigurationAttribute();
            string expectedFilename = $"./configs/{att.Foldername}/{att.FilePrefix}{nameof(InternalConfig)}.json";

            InternalConfig cfg = new InternalConfig();
            //ACT
            if (!ConfigurationManager.Instance.Write(cfg))
            {
                Assert.Fail("Probably couldn't write to hdd");
            }

            //ASSERT
            Assert.IsTrue(File.Exists(expectedFilename));
        }

        [Test, Category("Integration Tests")]
        public void Write_Confgiguration_ShouldBeInConfigModeFolder()
        {
            //ARRANGE
            // ReSharper disable once JoinDeclarationAndInitializer
            string mode;
#if DEBUG
            mode = "debug";
#elif !DEBUG
            mode = "release";
#endif
            string expectedFilename = $"./configs/{mode}/{nameof(RegularConfig)}.json";

            RegularConfig cfg = new RegularConfig();
            //ACT
            if (!ConfigurationManager.Instance.Write(cfg))
            {
                Assert.Fail("Probably couldn't write to hdd");
            }

            //ASSERT
            Assert.IsTrue(File.Exists(expectedFilename));
        }


        [Test, Category("Integration Tests")]
        public void Read_Configuration_IfNotExistsCreate()
        {
            //ARRANGE
            Cleanup();
            var att = new InternalConfigurationAttribute();
            string expectedFilename = $"./configs/{att.Foldername}/{att.FilePrefix}{nameof(InternalConfig)}.json";

            //ACT
            ConfigurationManager.Instance.Read<InternalConfig>();

            //ASSERT
            Assert.IsTrue(File.Exists(expectedFilename));
        }

        [Test, Category("Integration Tests")]
        public void Read_WhenFileNotExist_ReturnDefault()
        {
            //ARRANGE
            Cleanup();
            
            InternalConfig defaultConfig = new InternalConfig();
            //ACT
            var result = ConfigurationManager.Instance.Read<InternalConfig>();

            //ASSERT
            Assert.AreEqual(defaultConfig.TestValue, result.TestValue);
            Assert.AreEqual(123, result.TestValue);
        }
    }
}