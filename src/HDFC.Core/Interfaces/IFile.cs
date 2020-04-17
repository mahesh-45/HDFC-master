namespace HDFC.Core.Interfaces
{
    public interface IFile
    {
        string Name { get; }
        string Path { get; }
        decimal Size { get; }
    }
}
