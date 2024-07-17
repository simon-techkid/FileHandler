// FileHandler by Simon Field

using Hashing.Provisioning;
using Hashing.Provisioning.Xml;
using Logging.Broadcasting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FileHandler.Output.Xml;

/// <summary>
/// Provides instructions for serializing and transforming XML data.
/// </summary>
public abstract class XmlSaveable<TRecord>(Func<TRecord> doc, string? trackName, IBroadcaster<string> bcast) :
    SaveableAndTransformableBase<TRecord, XDocument, IEnumerable<XElement>>(doc, bcast, trackName)
{
    protected override IHashProvider<IEnumerable<XElement>> HashProvider => new XmlHashProvider(OutputEncoding);

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
