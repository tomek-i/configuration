using System;

namespace TI.Configuration.Logic.Properties
{
    /// <summary>
    /// Indicates how method invocation affects content of the collection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CollectionAccessAttribute : Attribute
    {
        public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
        {
            CollectionAccessType = collectionAccessType;
        }

        public CollectionAccessType CollectionAccessType { get; private set; }
    }
}