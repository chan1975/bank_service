using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using client.application.Contracts.Persistence;
using client.application.Execptions;
using Microsoft.Extensions.Logging;

namespace client.application.Features.Person
{
    public class PersonService: IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IUnitOfWork unitOfWork, ILogger<PersonService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<core.Person> GetPerson(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            if(person is null) throw new NotFoundExepction(nameof(core.Person), id);
            return person;
        }

        public async Task<IEnumerable<core.Person>> GetPeople()
        {
            var people = await _unitOfWork.PersonRepository.GetAllAsync();
            return people;
        }

        public async Task<core.Person> CreatePerson(core.Person person)
        {
            var newPerson = await _unitOfWork.PersonRepository.AddAsync(person);
            await _unitOfWork.Complete();
            return newPerson;
        }

        public async Task<core.Person> UpdatePerson(core.Person person)
        {
            var personToUpdate = await _unitOfWork.PersonRepository.GetByIdAsync(person.Id);
            if(personToUpdate is null) throw new NotFoundExepction(nameof(core.Person), person.Id);
            var updatedPerson = await _unitOfWork.PersonRepository.UpdateAsync(person);
            await _unitOfWork.Complete();
            return updatedPerson;
        }

        public async Task<core.Person> DeletePerson(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            if(person is null) throw new NotFoundExepction(nameof(core.Person), id);
            await _unitOfWork.PersonRepository.DeleteAsync(person);
            await _unitOfWork.Complete();
            return person;
        }
    }
}