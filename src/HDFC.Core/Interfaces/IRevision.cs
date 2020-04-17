namespace HDFC.Core.Interfaces
{
    public interface IRevision
    {
        long BaseId { get; }
        int RevisionNumber { get; }
        bool IsCurrent { get; }
    }
}
