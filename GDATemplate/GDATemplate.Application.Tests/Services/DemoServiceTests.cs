using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GDATemplate.Application.DTO.Entities;
using GDATemplate.Application.Mapping;
using GDATemplate.Application.Services;
using GDATemplate.Application.Services.Entities;
using GDATemplate.Data.Interfaces;
using GDATemplate.Domain.Entities;
using Moq;
using Xunit;

namespace GDATemplate.Application.Tests.Services
{
    public class DemoServiceTests
    {
        private DemoService _demoService;
        private AutoMapperSetup _autoMapper = new();
        private Mock<IDemoRepository> _demoRepository = new();
        private MapperConfiguration _configuration;
        private Mapper _mapper;
        private IEnumerable<Demo> _demoList;

        #region Mock
        public static IEnumerable<Demo> GetDemos()
            => new List<Demo>()
            {
                new Demo
                {
                    IdDemo = Guid.Parse("8c80e519-412b-4c43-9333-b1e010cefca1"),
                    Nome = "Usuario Teste 1",
                    Email = "usuarioteste@gmail.com",
                    DtInclusao = DateTime.Now
                },
                new Demo
                {
                    IdDemo = Guid.Parse("eea0698d-939a-410e-bcc9-67107281fa48"),
                    Nome = "Usuario Teste 2 ",
                    Email = "usuarioteste2@gmail.com",
                    DtInclusao = DateTime.Now
                },
                new Demo
                {
                    IdDemo = Guid.Parse("7a1f9e22-0c35-4576-8f1e-e17b22fc8153"),
                    Nome = "Usuario Teste 3",
                    Email = "usuarioteste3@gmail.com",
                    DtInclusao = DateTime.Now
                },
            };
        #endregion

        public DemoServiceTests()
        {
            _configuration = new(x => x.AddProfile(_autoMapper));
            _mapper = new(_configuration);
            _demoList = GetDemos();
            _demoRepository.Setup(x => x.GetAll()).Returns(_demoList);

            _demoService = new(_demoRepository.Object, _mapper);
        }



        #region ValidatingRequiredFields
        [Fact]
        public void Add_ValidObject()
        {
            var model = new DemoDTO
            {
                IdDemo = Guid.NewGuid(),
                Nome = "Usuario Teste",
                Email = "usuarioteste@gmail.com",
                DtInclusao = DateTime.Now
            };

            Assert.NotNull(_demoService.Add(model));

        }

        [Fact]
        public void Add_InvalidObject()
        {
            var model = new DemoDTO
            {
                IdDemo = Guid.NewGuid(),
                Nome = "Usuario Teste",
                DtInclusao = DateTime.Now
            };

            var exception = Assert.Throws<ValidationException>(() => _demoService.Add(model));
            Assert.Equal("The Email field is required.", exception.Message);
        }

        [Fact]
        public void GetAll_ValidadeCount()
        {
            var result = _demoService.GetAll();

            Assert.True(result.Any());
        }

        [Fact]
        public void GetAll_ValidateGuid()
        {

            var result = _demoService.GetAll().Where(x => x.IdDemo == Guid.Parse("7a1f9e22-0c35-4576-8f1e-e17b22fc8153")).First();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_ValidateEmail()
        {

            var result = _demoService.GetAll().Where(x => x.Email == "usuarioteste@gmail.com").First();

            Assert.NotNull(result);
        }



        #endregion
    }
}
