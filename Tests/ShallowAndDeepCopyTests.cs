using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Choker.ShallowAndDeepCopy
{
    [TestClass]
    public class ShallowAndDeepCopyTests
    {
        [TestMethod]
        public void SameInstanceTest()
        {
            var obj = CreateObject();

            var sameObj = obj; // both refer to same instance

            Assert.ReferenceEquals(obj, sameObj);

            sameObj.ValueType += 1;

            Assert.AreEqual(sameObj.ValueType, obj.ValueType); // value type changes for both references since they refer to same object
        }

        [TestMethod]
        public void ShallowCopyTest()
        {
            var obj = CreateObject();

            var shallowCopy = obj.ShallowCopy();

            Assert.AreNotSame(obj, shallowCopy); // ensure different instances

            shallowCopy.ValueType += 1; // change a value type field in the shallow copy

            Assert.AreNotEqual(obj.ValueType, shallowCopy.ValueType);// value type shouldn't change for obj since they refer to different instances

            Assert.AreSame(obj.RefType, shallowCopy.RefType); // here is the major difference with deep copy

            shallowCopy.RefType.InnerValueType += 1; // changes for both instances since for RefType in shallow copy the reference is copied but the referred object is not so it points to same instance in obj & shallowCopy

            Assert.AreEqual(obj.RefType.InnerValueType, shallowCopy.RefType.InnerValueType);

            shallowCopy.RefType = null; // changes for this instance only since it's now refering to a new null object

            Assert.IsNotNull(obj.RefType);
        }

        [TestMethod]
        public void DeepCopyTest()
        {
            var obj = CreateObject();

            var deepCopy = obj.DeepCopy();

            Assert.AreNotSame(obj, deepCopy); // ensure different instances

            deepCopy.ValueType += 1; // change a value type field in the deep copy

            Assert.AreNotEqual(obj.ValueType, deepCopy.ValueType);// value type shouldn't change for obj since they refer to different instances

            Assert.AreNotSame(obj.RefType,deepCopy.RefType); // here is the major difference with shallow copy

            deepCopy.RefType.InnerValueType += 1; // changes for deep copied object only since for RefType in deep copy a new instance is created so it also points to a different instance

            Assert.AreNotEqual(obj.RefType.InnerValueType, deepCopy.RefType.InnerValueType);

            deepCopy.RefType = null; // changes for this instance only

            Assert.IsNotNull(obj.RefType);
        }

        OuterClass CreateObject() => new OuterClass { ValueType = 10, RefType = new InnerClass { InnerValueType = 20 } };
    }
}
