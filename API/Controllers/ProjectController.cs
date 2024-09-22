using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository repository)
        {
            _projectRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> Get()
        {
            return Ok(await _projectRepository.GetProjectsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(string id)
        {
            return Ok(await _projectRepository.GetProjectById(id));
        }
    }
}