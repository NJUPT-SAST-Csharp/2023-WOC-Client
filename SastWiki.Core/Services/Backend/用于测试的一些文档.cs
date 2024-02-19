using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend
{
    public class 用于测试的一些文档 : IEntryProvider
    {
        public Task<int> AddEntryAsync(EntryDto entry)
        {
            throw new NotImplementedException();
        }

        public async Task<EntryDto> GetEntryByIdAsync(int id) =>
            id switch
            {
                114514
                    => new EntryDto()
                    {
                        Id = 114514,
                        Title = "Markdown Renderer Test",
                        Content =
                            @"
# Markdown Renderer Test

This is a **paragraph** !

---

## This is a heading 2

### This is a heading 3

#### This is a heading 4

##### This is a heading 5

This is a paragraph with a [external link](https://www.google.com) in it.

This is a paragraph with a [internal link](wiki://sast-wiki/Entry?id=1919810) in it.

This is a List:
- Item 1

- Item 2

    - Sub Item

- Item 3

This is a numbered list:

1. Item 1

2. Item 2

3. Item 3
"
                    },
                1919810
                    => new EntryDto()
                    {
                        Id = 1919810,
                        Title = "This is a test entry.",
                        Content = @"# This is a test entry."
                    },
                _
                    => new EntryDto()
                    {
                        Id = 0,
                        Title = "Not Found",
                        Content = "Entry not found."
                    },
            };

        public Task<List<int>> GetEntryIDListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<EntryDto>> GetEntryMetadataList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEntryExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntryAsync(EntryDto entry)
        {
            throw new NotImplementedException();
        }
    }
}
