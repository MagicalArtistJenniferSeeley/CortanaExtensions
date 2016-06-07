using System;

namespace CortanaExtensions.Exceptions
{
	/// <summary>
	/// If a Voice Command has over 10 Listen Statements
	/// </summary>
	public class FieldOverflowException : Exception
    {
        public FieldOverflowException(object ErroredField, string message) : base(message)
        {
            this.ErroredField = ErroredField;
        }
        /// <summary>
        /// State of Listen Statements
        /// </summary>
        public object ErroredField { get; set; }
    }
}
