﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Backend.UnitsOfWork.Implementations;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class StatesController : GenericController<State>
    {
        private readonly IStatesUnitOfWork _statesUnitOfWork;

        public StatesController(IGenericUnitOfWork<State> unitOfWork, IStatesUnitOfWork statesUnitOfWork) : base(unitOfWork)
        {
            _statesUnitOfWork = statesUnitOfWork;
        }

        [HttpGet("combo/{countryId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetComboAsync(int countryId)
        {
            return Ok(await _statesUnitOfWork.GetComboAsync(countryId));
        }

        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _statesUnitOfWork.GetAsync();
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _statesUnitOfWork.GetAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _statesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _statesUnitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
