using System.IO;
using NUnit.Framework;
using TIConfiguration.Logic;
using TIConfiguration.Logic.API;
using TIConfiguration.Logic._internals.Abstracts;

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
        }

        internal class RegularConfig : ConfigurationBase
        {
            public string Value { get; set; } = "old";
        }


        [Test, Category("Integration Tests")]
        public void Update_Property_UpdatePersistsInFile()
        {
            //Arrange
            Cleanup();
            ConfigurationManager.Write(new RegularConfig() {Value = "old"});

            //ACT
            ConfigurationManager.Update<RegularConfig>(x => x.Value = "new");

            //Assert
            Assert.IsTrue(ConfigurationManager.Read<RegularConfig>().Value == "new");
        }

        [Test, Category("Integration Tests")]
        public void Update_Property_ReturnsConfigWithUpdatedProperty()
        {
            //Arrange
            ConfigurationManager.Write(new RegularConfig() {Value = "old"});

            //ACT
            var updated = ConfigurationManager.Update<RegularConfig>(x => x.Value = "new");

            //Assert
            Assert.IsTrue(updated.Value == "new");
        }

        [Test, Category("Integration Tests")]
        public void Write_InternalConfgiguration_ShouldreturnTrueIfSuccessful()
        {
            //ARRANGE
            InternalConfig cfg = new InternalConfig();
            //ACT
            var result = ConfigurationManager.Write(cfg);
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
            if (!ConfigurationManager.Write(cfg))
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
            if (!ConfigurationManager.Write(cfg))
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
            ConfigurationManager.Read<InternalConfig>();

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
            var result = ConfigurationManager.Read<InternalConfig>();

            //ASSERT
            Assert.AreEqual(defaultConfig.TestValue, result.TestValue);
            Assert.AreEqual(123, result.TestValue);
        }
    }
}