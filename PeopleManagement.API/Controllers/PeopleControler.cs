using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.API.Model;
using PeopleManagement.API.Services;

namespace PeopleManagement.API.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleControler : ControllerBase
    {
        private readonly IValidator<PersonModel> _validator;
        private readonly IPersonInfoRepository _personInfoRepository;
        private readonly ILogger<PeopleControler> _logger;
        private readonly IMapper _mapper;
        public PeopleControler(
            ILogger<PeopleControler> logger,
            IValidator<PersonModel> validator,
            IPersonInfoRepository personInfoRepository,
            IMapper mapper
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _personInfoRepository = personInfoRepository ?? throw new ArgumentNullException(nameof(personInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PersonWithoutRegistrationsModel>>> GetPeople()
        {
            try
            {
                var peopleEntities = await _personInfoRepository.GetPeopleAsync();
                
                return Ok(_mapper.Map<IEnumerable<PersonWithoutRegistrationsModel>>(peopleEntities));
                //empty list is not a 404, is a empty list -> ok status
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while Geting People.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("inTheBuilding")]
        public async Task<ActionResult<IEnumerable<PersonWithoutRegistrationsModel>>> GetPeopleInTheBuilding()
        {
            try
            {
                var peopleEntities = await _personInfoRepository.GetPeopleInTheBuildingAsync();

                return Ok(_mapper.Map<IEnumerable<PersonWithoutRegistrationsModel>>(peopleEntities));
                //empty list is not a 404, is a empty list -> ok status
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while Geting People in the Building.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        
        [HttpGet("{id}", Name = "GetPersonById")]
        public async Task<ActionResult<PersonWithoutRegistrationsModel>> GetPersonById(int id)
        {
            try
            {
                // find people
                var peopleEntities = await _personInfoRepository.GetPersonAsync(id, false);

                //end the person doesn't exist return 404
                if (peopleEntities == null)
                {
                    _logger.LogInformation($"Person with id {id} does not exist.");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<PersonWithoutRegistrationsModel>>(peopleEntities));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while Geting Person with the id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        
        //[HttpPost]
        //public async Task<ActionResult<PersonModel>> CreatePerson(PersonModel person)
        //{
        //    try
        //    {
        //        // validate data input using FluentValidation
        //        var validationResults = _validator.Validate(person);

        //        List<string> errors = new List<string>();
        //        if (!validationResults.IsValid)
        //        {
        //            foreach (ValidationFailure failure in validationResults.Errors)
        //            {
        //                errors.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
        //            }
        //            _logger.LogInformation($"The input data is not valid.");
        //            return BadRequest(errors);
        //        }

        //        // when the person already exist return Bad Request
        //        if (await _personInfoRepository.PersonExistsAsync(person.Id))
        //        {
        //            _logger.LogInformation($"Person with id {person.Id} already exists.");
        //            return BadRequest();
        //        }

        //        var finalPerson = _mapper.Map<Entities.Person>(person);

        //        await _personInfoRepository.AddPersonForPersonAsync(finalPerson);
        //        await _personInfoRepository.SaveChangesAsync();


        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"Exception while Creating Person.", ex);
        //        return StatusCode(500, "A problem happened while handling your request.");
        //    }
       
        //} 

    }
}
