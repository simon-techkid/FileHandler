// FileHandler by Simon Field

using Hashing.Provisioning;
using Logging.Broadcasting;
using System;

namespace FileHandler.Output;

/// <summary>
/// Provides instructions for serializing an array of bytes.
/// </summary>
public abstract class ByteSaveable<TRecord>(Func<TRecord> doc, string? trackName, IBroadcaster<string> bcast) : SaveableBase<TRecord, byte[], byte[]>(doc, bcast, trackName)
{
    protected override IHashProvider<byte[]> HashProvider => new ByteHashProvider();

    protected override byte[] ConvertToBytes()
    {
        return Document;
    }

    protected override byte[] ClearDocument()
    {
        return Array.Empty<byte>();
    }
}
