namespace Bookify.Services
{
    public class TransientService : ITransientService
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
