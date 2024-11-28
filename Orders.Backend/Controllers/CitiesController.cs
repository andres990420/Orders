using Microsoft.AspNetCore.Mvc;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        private readonly ICitiesUnitOfWork _citiesUnitOfWork;

        public CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : base(unitOfWork)
        {
            _citiesUnitOfWork = citiesUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTOs pagination)
        {
            var action = await _citiesUnitOfWork.GetAsync(pagination);
            if (action.WasSucceess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTOs pagination)
        {
            var action = await _citiesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSucceess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}