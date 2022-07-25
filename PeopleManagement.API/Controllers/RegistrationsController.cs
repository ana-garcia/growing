using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.API.Model;
using PeopleManagement.API.Services;

namespace PeopleManagement.API.Controllers
{
    [Route("api/people/{personId}/registrations")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IValidator<RegistrationForCreationModel> _validator;

        private readonly ILogger<RegistrationsController> _logger;

        private readonly IPersonInfoRepository _personInfoRepository;

        private readonly IMapper _mapper;

        public RegistrationsController(
            ILogger<RegistrationsController> logger,
            IValidator<RegistrationForCreationModel> validator,
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationModel>>> GetRegistrations(int personId)
        {
            try
            {
                // when the person doesn't exist return 404
                if (!await _personInfoRepository.PersonExistsAsync(personId))
                {
                    _logger.LogInformation($"Person with id {personId} does not exist.");
                    return NotFound();
                }
                var registrationsEntities =  await _personInfoRepository.GetRegistrationsForPersonAsync(personId);
                return Ok(_mapper.Map<IEnumerable<RegistrationModel>>(registrationsEntities));
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while Geting Resgistrations", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{registrationId}", Name = "GetRegistrationById")]
        public async Task<ActionResult<IEnumerable<RegistrationModel>>> GetRegistrationById(int personId, int registrationId)
        {
            try
            {
                // when the person doesn't exist return 404
                if (!await _personInfoRepository.PersonExistsAsync(personId))
                {
                    _logger.LogInformation($"Person with id {personId} does not exist.");
                    return NotFound();
                }

                // when registration doesn't exit return 404
                var registrationsEntities = await _personInfoRepository.GetResistrationForPersonAsync(personId, registrationId);
                if (registrationsEntities == null)
                {
                    _logger.LogInformation($"Registration with id {registrationId} does not exist.");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<RegistrationModel>>(registrationsEntities));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while Geting Resgistration with the id {registrationId}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }


        
        [HttpPost]
        public async Task<ActionResult<RegistrationModel>> CreateRegistration(
            int personId,
            RegistrationForCreationModel registration)
        {
            try
            {
                // validate data input using FluentValidation
                var validationResults = _validator.Validate(registration);

                List<string> errors = new List<string>();
                if (!validationResults.IsValid)
                {
                    foreach (ValidationFailure failure in validationResults.Errors)
                    {
                        errors.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
                    }
                    _logger.LogInformation($"Input data not valid.");
                    return BadRequest(errors);
                }

                // when the person doesn't exist return 404
                if (!await _personInfoRepository.PersonExistsAsync(personId))
                {
                    _logger.LogInformation($"Person with id {personId} does not exist.");
                    return NotFound();
                }
                // when the person is already in the building return a BadRequest
                if (!await _personInfoRepository.PersonHasActiveRegistretionAsync(personId))
                {
                    _logger.LogInformation($"Person with id {personId} already has an active registration.");
                    return BadRequest();
                }
                var finalRegistration = _mapper.Map<Entities.Registration>(registration);

                await _personInfoRepository.AddRegistrationForPersonAsync(personId, finalRegistration);
                await _personInfoRepository.SaveChangesAsync();

                var createdRegistrationToReturn = _mapper.Map<Model.RegistrationModel>(finalRegistration);
                return CreatedAtRoute("GetRegistrationById",
                    new
                    {
                        personId = personId,
                        registrationId = createdRegistrationToReturn.Id,
                    },
                    createdRegistrationToReturn
                    );
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while creating Resgistration to the personId {personId}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }
        /*
        [HttpPut("updateExit")]
        public ActionResult UpdateExit(int personId)
        {
            try
            {
                // when the person doesn't exist return 404
                if (!await _personInfoRepository.PersonExistsAsync(personId))
                {
                    _logger.LogInformation($"Person with id {personId} does not exist.");
                    return NotFound();
                }
                // return badRequest in case the person is not in the building

                var registration = person.Registrations.Where(r => r.IsActive == true).FirstOrDefault();
                if (registration == null)
                {
                    _logger.LogInformation($"Person with id {personId} does not have an active registration.");
                    return BadRequest();
                }
                registration.Exit = DateTime.Now;
                registration.IsActive = false;

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while updating Exit with the personId {personId}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        */
        private Boolean HasActiveRegistration(PersonModel person)
        {
            return person.Registrations.Any(r => r.IsActive);
        }
    }
}
