using AutoMapper;
using HousingAPI.Dtos;
using HousingAPI.Interfaces;
using HousingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public  async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await unitOfWork.PropertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypeDTO = mapper.Map<IEnumerable<PropertyTypeDto>>(propertyTypes);
            return Ok(propertyTypeDTO);
        }
    }
}
