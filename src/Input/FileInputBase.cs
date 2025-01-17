﻿// Techkid.FileHandler by Simon Field

using System;
using System.IO;

namespace Techkid.FileHandler.Input;

public abstract class FileInputBase : IDisposable
{
    protected FileInputBase(string path)
    {
        FileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
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

    protected bool Disposed { get; set; }

    public void Dispose()
    {
        StreamReader.Dispose();
        FileStream.Dispose();
        DisposeDocument();
        Disposed = true;
        GC.SuppressFinalize(this);
    }
}
