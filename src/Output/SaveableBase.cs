// FileHandler by Simon Field

using Disposal;
using Hashing.Provisioning.Providers;
using Logging;
using Logging.Broadcasting;
using System;
using System.IO;

namespace FileHandler.Output;

/// <summary>
/// Provides instructions for serializing <typeparamref name="TDocument"/> data to <see langword="byte"/>[] and saving it in the target format.
/// </summary>
/// <typeparam name="TDocument">The source format type.</typeparam>
/// <typeparam name="THashed">The format type of the hashable portion of this document.</typeparam>
public abstract class SaveableBase<TRecord, TDocument, THashed> : DisposableBase, IFileOutput
{
    protected SaveableBase(Func<TRecord> pairs, IBroadcaster<string> bcast, string? trackName = null) : base(bcast)
    {
        DataProvider = pairs;
        Document = GetDocument(trackName);
    }

    public abstract string FormatName { get; }

    /// <summary>
    /// The document in format <typeparamref name="TDocument"/> that will be serialized and saved to the disk.
    /// </summary>
    protected TDocument Document { get; private set; }
    public abstract int Count { get; }

    /// <summary>
    /// Provides access to the collection of <typeparamref name="TRecord"/> objects to be saved to the document in format <typeparamref name="TDocument"/>.
    /// </summary>
    protected Func<TRecord> DataProvider { get; }

    /// <summary>
    /// Provides access to the document in format <typeparamref name="TDocument"/> that will be serialized and saved to the disk.
    /// </summary>
    protected abstract TDocument GetDocument(string? trackName);

    public void Save(string path)
    {
        byte[] doc = ConvertToBytes();
        Save(path, doc);
    }

    protected virtual void Save(string path, byte[] bytes)
    {
        using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write);
        fileStream.Write(bytes, 0, bytes.Length);
        BCaster.Broadcast($"New file stream opened for writing {bytes.Length} bytes to {path}.", LogLevel.Debug);
    }

    /// <summary>
    /// Converts <typeparamref name="TDocument"/> to <see langword="byte"/>[].
    /// </summary>
    /// <returns>This <see cref="Document"/>, as <see langword="byte"/>[].</returns>
    protected abstract byte[] ConvertToBytes();

    /// <summary>
    /// The <see cref="IHashProvider{T}"/> for generating a hash of the document of type <typeparamref name="THashed"/>
    /// </summary>
    protected abstract IHashingProvider<THashed> HashProvider { get; }

    /// <summary>
    /// Clears the contents of the <see cref="Document"/> in preparation for disposal.
    /// </summary>
    /// <returns>A <typeparamref name="TDocument"/> that has been cleared.</returns>
    protected abstract TDocument ClearDocument();

    protected override void DisposeClass()
    {
        Document = ClearDocument();
    }
}
