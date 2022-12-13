// MIT License
//
// Copyright (c) 2022 Kamil Ercan Turkarslan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Reflection;
using hvc.DataStructures.Node;
using hvc.Extensions;

namespace hvc.Parser;

public abstract class Parser
{
    protected Parser(Filename filename)
    {
        Filename = filename;
    }

    public Filename Filename { get; }

    public static Parser? Get(String filename, Boolean throwIfNotFound = false)
    {
        if (String.IsNullOrWhiteSpace(filename))
            throw new ArgumentNullException(nameof(filename));

        var fullFilename = new Filename(filename);
        
        if (String.IsNullOrWhiteSpace(fullFilename.Extension))
            throw new ArgumentException(nameof(fullFilename.Full));

        var parserClasses = AppDomain.CurrentDomain.GetClassesByAttribute(typeof(ParserExtensionAttribute)).ToArray();

        Type? parserType = null;
        if (parserClasses.Any())
        {
            var attr =
                parserClasses.First().GetCustomAttribute(typeof(ParserExtensionAttribute)) as
                    ParserExtensionAttribute;

            if ((attr?.Extension).IsEqualTo(fullFilename.Extension))
            {
                parserType = parserClasses.First();

                if (parserType == typeof(Parser))
                    throw new InvalidOperationException("Invalid use of 'ParserExtension' attribute");
            }
        }

        if (parserType == null && throwIfNotFound)
            throw new NotImplementedException($"{fullFilename.Extension} parser was not found for file '{fullFilename.Full}'");

        // ReSharper disable once AssignNullToNotNullAttribute
        return (parserType == null) ? null : (Parser?) Activator.CreateInstance(parserType, fullFilename);
    }

    public abstract void Parse(Stack<ModelNodeBase> modelNodes);
}