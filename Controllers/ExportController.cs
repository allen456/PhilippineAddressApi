using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhilippineAddressApi.Models;
using PhilippineAddressApi.Data;
using AutoMapper;

namespace PhilippineAddressApi.Controllers
{
    //api/export
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IAddressApiRepo _repo;
        private readonly IMapper _mapper;

        public ExportController(IAddressApiRepo repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        
        //GET api/export
        [HttpGet]
        public ActionResult <IEnumerable<Region>> ExportData()
        {
            return Ok(_repo.Export()); 
        }       
    }
}