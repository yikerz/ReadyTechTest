﻿using CoffeeMachineAPI.Models;
using CoffeeMachineAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineAPI.Controllers
{
    [ApiController]
    public class CoffeeMachineAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICoffeeMachine _coffeeMachine;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CoffeeMachineAPIController(ICoffeeMachine coffeeMachine, IDateTimeProvider dateTimeProvider)
        {
            _response = new();
            _coffeeMachine = coffeeMachine;
            _dateTimeProvider = dateTimeProvider;
        }
        // GET endpoint for brewing coffee
        [HttpGet]
        [Route("brew-coffee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(StatusCodes.Status418ImATeapot)]
        public ActionResult<APIResponse> Get()
        {
            DateTime now = _dateTimeProvider.Now();
            // Check if it's April 1st
            if (now.Month == 4 && now.Day == 1)
            {
                // Return 418 status code (I'm a teapot) if it's April 1st
                return StatusCode(418, new { });
            }
            // Check if coffee can be brewed
            var isBrewSuccess = _coffeeMachine.IsBrewSuccess();
            if (isBrewSuccess)
            {
                // Coffee brewed successfully
                _response.message = "Your piping hot coffee is ready";
                _response.prepared = DateTime.Now;
                return Ok(_response);
            }
            else
            {
                // Coffee cannot be brewed due to insufficient stock
                return StatusCode(503, new { });
            }
        }
    }
}
