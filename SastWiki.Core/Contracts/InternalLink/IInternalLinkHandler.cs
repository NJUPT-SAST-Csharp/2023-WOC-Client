using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.InternalLink
{
    public interface IInternalLinkHandler
    {
        public void Trigger(Uri ilink);
    }
}
