// SpotifyGPX by Simon Field

using Disposal;
using Logging;
using Logging.Broadcasting;
using System.IO;

namespace FileHandler.Input;

public abstract class FileInputBase : DisposableBase
{
    protected FileInputBase(string path, IBroadcaster<string> bcaster) : base(bcaster)
    {
        FileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        BCaster.Broadcast($"New file stream opened for reading '{path}'.", LogLevel.Debug);
        StreamReader = new StreamReader(FileStream);
    }

    /// <summary>
    /// The name of the file format.
    /// </summary>
    protected abstract string FormatName { get; }

    /// <summary>
    /// Serves as the reading stream for a file on the disk.
    /// </summary>
    protected FileStream FileStream { get; private set; }

    /// <summary>
    /// Serves as the stream reader for the file stream, <see cref="FileStream"/>.
    /// </summary>
    protected StreamReader StreamReader { get; private set; }

    /// <summary>
    /// Clears this file's original document contents from memory.
    /// </summary>
    protected abstract void DisposeDocument();

    protected override void DisposeClass()
    {
        StreamReader.Dispose();
        FileStream.Dispose();
        DisposeDocument();
    }
}
