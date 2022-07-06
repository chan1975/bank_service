using System.Collections.Generic;
using System.Threading.Tasks;

namespace client.application.Features.Person
{
    public interface IPersonService
    {
        Task<core.Person> GetPerson(int id);
        Task<IEnumerable<core.Person>> GetPeople();
        Task<core.Person> CreatePerson(core.Person person);
        Task<core.Person> UpdatePerson(core.Person person);
        Task<core.Person> DeletePerson(int id);
    }
}