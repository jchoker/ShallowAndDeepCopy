namespace Choker.ShallowAndDeepCopy
{
    /// <summary>
    /// The purpose of this small project is to demonstrate the difference between shallow copy and deep copy in .Net.
    /// Often times it can be confusing to tell the difference and why would you need to deep copy an object instead of using a shallow copy.
    /// </summary>
    /// <author>J. Choker</author>
    public class OuterClass
    {
        public int ValueType; // value type field

        public InnerClass RefType; // reference type field

        /// <summary>
        /// Returns a new instance (shallow copy).
        /// https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=netcore-3.1
        /// </summary>
        public OuterClass ShallowCopy() => (OuterClass)this.MemberwiseClone();

        /// <summary>
        /// Returns a new instance (deep copied).
        /// </summary>
        public OuterClass DeepCopy()
        {
            // creates a shallow copy from current object and copies the nonstatic fields of the current object to the new object.
            // if a field is a value type, then nothing else needs to be done since a bit-by-bit copy of the field is performed by MemberwiseClone.
            var copy = (OuterClass)this.MemberwiseClone();

            // if a field is a reference type, the reference is copied but the referred object is not; therefore, the original object and its clone refer to the same object.
            // in this case similarly we create a deep copy of all contained fields.
            copy.RefType = this.RefType.DeepCopy();

            return copy;
        }
    }

    public class InnerClass
    {
        public int InnerValueType; // value type field

        // for simplicity no reference fields

        public InnerClass DeepCopy()
        {
            var copy = (InnerClass)this.MemberwiseClone();
            // no further action is required since the class doesn't contain any reference type fields, in this case shallow and deep copy functions are identical for this object
            return copy;
        }
    }
}
