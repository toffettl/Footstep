using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Preferences
{
    public interface IPreferenceWriteOnlyRepository
    {
        Task Add(Preference preference);
    }
}
