## Open Source Attributions

For domain specific language parsing, **hvc.Parser** uses **[Antlr4](https://www.antlr.org/)**. Antrl4 is a BSD licensed open source parser generator started by **[Professor Terence J. Parr](https://en.wikipedia.org/wiki/Terence_Parr)**

## hvc.Parser
**hvc.Parser** is a .NET 6 class library used by various hvc code generators such as [hvc.DynamoDBReport](https://github.com/keturk/hvc.DynamoDBReport).

Simply, **hvc.Parser** provides common functionality for parsing code written with a domain specific language. 

Since this library is used by multiple **hvc** code generators, it is published as a separate NuGet package.

**hvc.Parser** uses following **hvc** libraries.
[hvc.Extensions](https://github.com/keturk/hvc.Extensions) nuget package.
[hvc.DataStructures](https://github.com/keturk/hvc.DataStructures) nuget package.