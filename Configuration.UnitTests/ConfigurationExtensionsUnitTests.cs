using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NUnit.Framework;
using TI.Configuration.Logic;
using TI.Configuration.Logic.Abstracts;
using TI.Configuration.Logic.API;

namespace TIConfiguration.UnitTests
{
    [TestFixture]
    public class ConfigurationExtensionsUnitTests
    {
        [TearDown, ExcludeFromCodeCoverage]
        public void CleanUp()
        {
            if(Directory.Exists(ConfigurationManager.DefaultPath))
                Directory.Delete(ConfigurationManager.DefaultPath, true);
        }
        [ExcludeFromCodeCoverage]
        public class NotInternalCfg : ConfigurationBase
        {
            public string Value { get; set; } = "old";

            public override IConfiguration Default()
            {
                throw new NotImplementedException();
            }
        }

        [InternalConfiguration]
        public class InternalCfg : ConfigurationBase
        {
            public override IConfiguration Default()
            {
                throw new NotImplementedException();
            }
        }

        [InternalConfiguration("abc", "123")]
        public class InternalCustomCfg : ConfigurationBase
        {
            public override IConfiguration Default()
            {
                throw new NotImplementedException();
            }
        }

        public IConfiguration GetDefaultInstanceOf<T>() where T:ConfigurationBase, new()
        {
            return new T();
        }

        [Test] public void GetFilePath_NotInternalCfg_ReturnCorrectFolderandFilename()
        {

            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            string expected = $"\\debug\\{nameof(NotInternalCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected, result);
        }
        [Test] public void GetFilePath_InternalCfg_ReturnCorrectFolderandFilename()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            string expected = $"_internals\\_{nameof(InternalCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected, result);
        }
        [Test] public void GetFilePath_InternalCustomCfg_ReturnCorrectFolderandFilename()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            string expected = $"abc\\123{nameof(InternalCustomCfg)}.json";
            //ACT
            string result = cfg.GetFilePath();
            //ASSERT
            StringAssert.Contains(expected,result);
        }
        [Test] public void GetInternalConfig_NotDecorated_Null()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            //ACT
            InternalConfigurationAttribute result = cfg.GetInternalConfig();
            //ASSERT
            Assert.IsNull(result);
        }
        [Test] public void GetInternalConfigProperty_FilePrefix_HasDefaultValues()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.FilePrefix);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
        }
        [Test] public void GetInternalConfigProperty_FolderName_HasDefaultValues()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.Foldername);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
        }

        [Test] public void GetInternalConfigProperty_CustomCfgFolderName_HasCustomValue()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.Foldername);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
            Assert.AreEqual("abc",value);
        }
        [Test] public void GetInternalConfigProperty_CustomCfgFilePrefix_HasCustomValue()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCustomCfg>();
            //ACT
            string value = cfg.GetInternalConfigProperty(x => x.FilePrefix);
            //ASSERT
            Assert.IsNotNullOrEmpty(value);
            Assert.AreEqual("123", value);
        }
        [Test] public void GetInternalConfig_Decorated_NotNull()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            InternalConfigurationAttribute result = cfg.GetInternalConfig();
            //ASSERT
            Assert.IsNotNull(result);
        }
        [Test] public void IsInternalConfiguration_DecoratedClass_True()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<InternalCfg>();
            //ACT
            bool result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsTrue(result);
        }
        [Test] public void IsInternalConfiguration_NotDecoratedClass_False()
        {
            //ARRANGE
            IConfiguration cfg = GetDefaultInstanceOf<NotInternalCfg>();
            //ACT
            bool result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsFalse(result);
        }


        [Test]
        public void IsInternalConfiguration_OnConfiguration_ReturnFalse()
        {
            //ARRANGE
            NotInternalCfg cfg = new NotInternalCfg();
            //ACT
            var result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsFalse(result);
        }

        [Test]
        public void IsInternalConfiguration_OnInternalConfiguration_ReturnTrue()
        {
            //ARRANGE
            InternalCfg cfg = new InternalCfg();
            //ACT
            var result = cfg.IsInternalConfiguration();
            //ASSERT
            Assert.IsTrue(result);
        }

        [Test]
        public void GetInternalConfigProperty_OnNonInternalConfig_Throws()
        {
            //ARRANGE
            NotInternalCfg cfg = new NotInternalCfg();
            //ACT
            Assert.Throws<InvalidOperationException>(delegate
            {
                var result = cfg.GetInternalConfigProperty(x=>x.Foldername);
            });
            
            //ASSERT
        }
        [Test]
        public void GetInternalConfig_OnInternalConfiguration_ReturnInstance()
        {
            InternalCfg cfg = new InternalCfg();
            var result = cfg.GetInternalConfig();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetInternalConfig_OnInternalConfiguration_ReturnAttribute()
        {
            InternalCfg cfg = new InternalCfg();
            var result = cfg.GetInternalConfig();

            Assert.IsInstanceOf<InternalConfigurationAttribute>(result);
        }

        [Test]
        public void GetInternalConfig_OnRegularConfiguration_ReturnNull()
        {
            NotInternalCfg cfg = new NotInternalCfg();
            var result = cfg.GetInternalConfig();

            Assert.IsNull(result);
        }



    }
}