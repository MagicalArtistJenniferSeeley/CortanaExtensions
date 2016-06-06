using CortanaExtensions.PhraseGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaExtensions.Exceptions
{
    public class PhraseListIemOverflowException : Exception
    {
        public PhraseListIemOverflowException(IList<PhraseList> PhraseLists) : base("Can't have over 2000 items in PhraseLists")
        {
            this.PhraseLists = PhraseLists;
        }
        public IList<PhraseList> PhraseLists { get; set; }
    }
}
