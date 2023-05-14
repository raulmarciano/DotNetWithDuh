using Bogus;
using DotNetWithDuh.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWithDuh.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly Faker<BookEntity> _bookEntityFaker;

        private readonly IEnumerable<BookEntity> _bookEntities;

        public BookController()
        {
            _bookEntityFaker = new Faker<BookEntity>()
                .RuleFor(r => r.Id, f => f.Random.Int(min: 0))
                .RuleFor(r => r.Title, f => f.Company.CompanyName())
                .RuleFor(r => r.Author, f => f.Name.FindName());

            _bookEntities = _bookEntityFaker.Generate(30);
        }

        //Tudo mocado
        //TODO: Falta fazer um DTO básico, camada de infra, database e um worker (rabbitmq)

        [HttpGet()]
        public ActionResult<IEnumerable<BookEntity>> Get()
        {
            return Ok(_bookEntities);
        }

        [HttpGet("{id}")]
        public ActionResult<BookEntity> Get(int id)
        {
            var book = _bookEntities.FirstOrDefault();
            book.Id = id;

            return Ok(book);
        }

        [HttpGet("bookEntity")]
        public ActionResult Get([FromBody] BookEntity bookEntity)
        {
            return Ok(bookEntity);
        }

        [HttpPost]
        public ActionResult Create([FromBody] BookEntity bookEntity)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] BookEntity bookEntity)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] int id)
        {
            return Ok();
        }
    }
}