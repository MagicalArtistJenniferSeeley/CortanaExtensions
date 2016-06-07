using System;
/// <summary>
/// From StackOverflow http://stackoverflow.com/a/18606291/5001796 User: Xyroid
/// </summary>
namespace CortanaExtensions.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            var other = obj as DescriptionAttribute;
            return other != null && other.Description == Description;
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
    }
}