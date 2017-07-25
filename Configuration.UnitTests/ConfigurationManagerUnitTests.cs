using System;
using System.IO;
using System.Threading;
using Moq;
using NUnit.Framework;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TIConfiguration.UnitTests
{
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
            if(Directory.Exists(ConfigurationManager.DefaultPath))
              Directory.Delete(ConfigurationManager.DefaultPath, true);
        }
        [Test]
        public void Constructor_InternalConfigurationDoesNotExist_WritesToFileSystem()
        {
          

            //ARRANGE
            const string expectedPath = ".\\configs\\_internals\\_MasterConfig.json";
            if (File.Exists(expectedPath))
                File.Delete(expectedPath);

            //ACT
            //because instance may have been set on another test, it will not recreate the master config
            ConfigurationManager.Reset();
            var instance = ConfigurationManager.Instance;

            

            //ASSERT
            Assert.IsTrue(File.Exists(expectedPath));
        }

        [Test]
        public void Get_MasterConfig_Default()
        {
            //ARRANGE
            var instance = ConfigurationManager.Instance;
            //ACT
            var result = instance.MasterConfig;
            //ASSERT
            Assert.IsNotNull(result);
        }
        [Test]
        public void Reset_GetsNewInstance_OfMasterConfig()
        {
            //ARRANGE
            var instance = ConfigurationManager.Instance;
            var old = instance.MasterConfig;
            //ACT
            ConfigurationManager.Reset();
            var result = instance.MasterConfig;
            //ASSERT
            Assert.AreNotSame(old,result);
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

        
    }
}