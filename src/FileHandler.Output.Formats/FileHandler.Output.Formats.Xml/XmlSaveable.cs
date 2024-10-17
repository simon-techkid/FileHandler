// FileHandler by Simon Field

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FileHandler.Output.Formats.Xml;

/// <summary>
/// Provides instructions for serializing and transforming XML data.
/// </summary>
public abstract class XmlSaveable<TRecord>(Func<TRecord> doc, string? trackName) :
    SaveableAndTransformableBase<TRecord, XDocument, IEnumerable<XElement>>(doc, trackName)
{
    protected override Encoding OutputEncoding => XmlSettings.Encoding;

    /// <summary>
    /// The XML namespace for the document type.
    /// </summary>
    protected virtual XNamespace Namespace => XNamespace.Xmlns;

    protected override byte[] ConvertToBytes()
    {
        string xmlString = TransformToXmlToString();
        return OutputEncoding.GetBytes(xmlString);
    }

    protected override XDocument TransformToXml()
    {
        return Document;
    }

    protected override XDocument ClearDocument()
    {
        return new XDocument();
    }
}
