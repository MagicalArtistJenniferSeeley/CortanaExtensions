using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaExtensions.Exceptions
{
    public class ListenStatementOverflowException : Exception
    {
        public ListenStatementOverflowException(List<ListenStatement> ListenStatements) : base("VoiceCommands can only have 10 Listen Statements")
        {
            this.ListenStatements = ListenStatements;
        }
        public List<ListenStatement> ListenStatements { get; set; }
    }
}
