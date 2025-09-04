using Footstep.Domain.Entities;

namespace Footstep.Domain.Repositories.Preferences
{
    public interface IPreferenceReadOnlyRepository
    {
        Task<List<Preference>> GetAll();
    }
}
