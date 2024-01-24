using Microsoft.AspNetCore.Mvc;
using Orange_Portfolio_BackEnd.Models;
using Orange_Portfolio_BackEnd.Models.Interfaces;

namespace Orange_Portfolio_BackEnd.Controllers
{
    [Route("api/v1/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listTags = _tagRepository.GetAll();
            return Ok(listTags);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tag = _tagRepository.GetById(id);
            return tag == null ? NotFound("Tag not found!") : Ok(tag);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Tag tag)
        {
            _tagRepository.Add(tag);
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Tag tag, int id)
        {
            _tagRepository.Update(tag);
            return Ok(tag);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tagRepository.Delete(id);
            return Ok();
        }
    }
}
