// FileHandler by Simon Field

using System;

namespace FileHandler.Output.Formats.Byte;

/// <summary>
/// Provides instructions for serializing an array of bytes.
/// </summary>
public abstract class ByteSaveable<TRecord>(Func<TRecord> doc, string? trackName) : SaveableBase<TRecord, byte[], byte[]>(doc, trackName)
{
    protected override byte[] ConvertToBytes()
    {
        return Document;
    }

    protected override byte[] ClearDocument()
    {
        return Array.Empty<byte>();
    }
}
