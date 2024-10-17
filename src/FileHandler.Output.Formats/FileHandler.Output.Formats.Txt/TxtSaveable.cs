// FileHandler by Simon Field

using System;
using System.Linq;
using System.Xml.Linq;

namespace FileHandler.Output.Formats.Txt;

/// <summary>
/// Provides instructions for serializing and transforming TXT data.
/// </summary>
public abstract class TxtSaveable<TRecord>(Func<TRecord> doc, string? trackName) :
    SaveableAndTransformableBase<TRecord, string?[], string?[]>(doc, trackName)
{

    /// <summary>
    /// Default line ending for the TXT document.
    /// Default value: <see cref="Environment.NewLine"/>, override to change.
    /// </summary>
    protected virtual string LineEnding => Environment.NewLine;

    protected override byte[] ConvertToBytes()
    {
        string doc = string.Join(LineEnding, Document);
        return OutputEncoding.GetBytes(doc);
    }

    protected override XDocument TransformToXml()
    {
        XElement root = new("Root");

        Document.Select(line => new XElement("Line", line))
            .ToList()
            .ForEach(root.Add);

        return new XDocument(root);
    }

    protected override string[] ClearDocument()
    {
        return Array.Empty<string>();
    }
}
