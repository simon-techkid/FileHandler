// Techkid.FileHandler by Simon Field

namespace Techkid.FileHandler.Output;

public partial class SaveableAndTransformableBase<TRecord, TDocument, THashed>
{
    private const bool EnableDebugXsltTransformations = false;
    private const bool EnableXsltDocumentFunction = true;
    private const bool EnableXsltScript = true;
}
