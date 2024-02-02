using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Backend
{
    public interface IEntryProvider
    {
        public string GetEntry(int id);
    }
}
