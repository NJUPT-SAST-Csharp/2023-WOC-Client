﻿using System.Collections.Specialized;
using SastWiki.Core.Contracts.InternalLink;

namespace SastWiki.Core.Services.InternalLink;

public class InternalLinkService : IInternalLinkService
{
    public Dictionary<string, EventHandler<NameValueCollection>> Paths { get; private set; }

    public InternalLinkService() => Paths = [];

    public bool Register(string path, EventHandler<NameValueCollection> handler) =>
        Paths.TryAdd(path, handler);

    public bool ContainsPath(string path) => Paths.ContainsKey(path);
}
