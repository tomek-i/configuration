using System;
using System.IO;
using Moq;
using NUnit.Framework;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;
using TI.Configuration.Logic._internals.Configs;

namespace TIConfiguration.UnitTests
{



    [TestFixture]
    public class ConfigurationExtensionsUnitTests
    {
        [TearDown]
        public void CleanUp()
        {
            if(Directory.Exists(ConfigurationManager.DefaultPath))
                Directory.Delete(ConfigurationManager.DefaultPath, true);
        }

        public class NotInternalCfg : ConfigurationBase
        {
            
        }
        [InternalConfiguration]
        public class InternalCfg : ConfigurationBase
        {

        }
        [InternalConfiguration("abc","123")]
        public class InternalCustomCfg : ConfigurationBase
        {

        }


        public IConfiguration GetDefaultInstanceOf<T>() where T:ConfigurationBase, new()
        {
            return new T();
        }
        [Test]
        public void GetFilePath_NotInternalCfg_ReturnCorrectFolderandFilename()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            string expected = $"\\debug\\{nameof(NotInternalCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected, result);
        }
        [Test]
        public void GetFilePath_InternalCfg_ReturnCorrectFolderandFilename()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            string expected = $"_internals\\_{nameof(InternalCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected, result);
        }
        [Test]
        public void GetFilePath_InternalCustomCfg_ReturnCorrectFolderandFilename()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            string expected = $"abc\\123{nameof(InternalCustomCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected,result);
        }

        [Test]
        public void GetInternalConfig_NotDecorated_Null()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            //ACT
            InternalConfigurationAttribute result = cfg.GetInternalConfig();
            //ASSERT
            Assert.IsNull(result);
        }

        [Test]
        public void GetInternalConfigProperty_FilePrefix_HasDefaultValues()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.FilePrefix);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
        }
        [Test]
        public void GetInternalConfigProperty_FolderName_HasDefaultValues()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.Foldername);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
        }

        [Test]
        public void GetInternalConfigProperty_CustomCfgFolderName_HasCustomValue()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.Foldername);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
            Assert.AreEqual("abc",value);
        }
        [Test]
        public void GetInternalConfigProperty_CustomCfgFilePrefix_HasCustomValue()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.FilePrefix);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
            Assert.AreEqual("123", value);
        }
        [Test]
        public void GetInternalConfig_Decorated_NotNull()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            InternalConfigurationAttribute result = cfg.GetInternalConfig();
            //ASSERT
            Assert.IsNotNull(result);
        }

        [Test]
        public void IsInternalConfiguration_DecoratedClass_True()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            bool result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsTrue(result);
        }
        [Test]
        public void IsInternalConfiguration_NotDecoratedClass_False()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            //ACT
            bool result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsFalse(result);
        }

    }

    [TestFixture]
    public class ConfigurationManagerUnitTests
    {
        //INFO: Those are integration tests
        private class ConfigStub : ConfigurationBase
        {
            public string MyCustomStetting { get; set; }
        }
        private ConfigStub CreateConfig(string configValue)
        {
            return new ConfigStub() { MyCustomStetting  = configValue};
        }

        [TearDown]
        public void CleanUp()
        {
            Directory.Delete(ConfigurationManager.DefaultPath, true);
        }

        [Test]
        public void Update_UpdateSingleProperty_ShouldWriteIt()
        {
            //ARRANGE
            var expected = CreateConfig("Test");
            ConfigurationManager.Instance.Write(expected);
            //ACT
            var ret = ConfigurationManager.Instance.Update<ConfigStub>(x => x.MyCustomStetting = "New Value");
            var fromHd = ConfigurationManager.Instance.Read<ConfigStub>();
            //ASSERT
            Assert.AreEqual("New Value", fromHd.MyCustomStetting);
            Assert.AreEqual("New Value", ret.MyCustomStetting);
        }

       
        [Test]
        public void Write_AnyConfiguration_ConfigurationIsPersistent()
        {
            //ARRANGE
            ConfigStub myCfg = CreateConfig(It.IsAny<string>());

            //ACT
            var result = ConfigurationManager.Instance.Write(myCfg);
            var fileExist = File.Exists(myCfg.GetFilePath());
            //ASSERT
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, fileExist);
        }

        [Test]
        public void Read_ExistingConfig_UpdatesTheConfirguration()
        {
            //ARRANGE
            var expected = CreateConfig("Test");
            ConfigurationManager.Instance.Write(expected);

            //ACT
            var cfgHd = ConfigurationManager.Instance.Read<ConfigStub>();
            
            //ASSERT
            Assert.AreEqual(expected.MyCustomStetting, cfgHd.MyCustomStetting);
        }

        [Test]
        public void Constructor_InternalConfigurationDoesNotExist_WritesToFileSystem()
        {
            //ARRANGE
            const string expectedPath = ".\\configs\\_internals\\_MasterConfig.json";
            if (File.Exists(expectedPath))
                File.Delete(expectedPath);
            
            //ACT
            var instance = ConfigurationManager.Instance;
            
            //ASSERT
            Assert.IsTrue(File.Exists(expectedPath));
        }
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
