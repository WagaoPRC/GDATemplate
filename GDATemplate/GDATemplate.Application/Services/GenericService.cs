using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDATemplate.Application.DTO.Entities;
using GDATemplate.Application.Interfaces;
using GDATemplate.Application.Services.Entities;
using GDATemplate.Data;
using GDATemplate.Data.Interfaces;
using GDATemplate.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GDATemplate.Application.Services
{
    public class GenericService : IGenericService
    {
        private readonly DemoService _demoService;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(DemoService demoService, IUnitOfWork unitOfWork)
        {
            _demoService = demoService;
            _unitOfWork = unitOfWork;
        }

        public void Example1()
        {
            try
            {
                var model = new DemoDTO
                {
                    IdDemo = Guid.Parse("6763db6d-ff18-419f-9b77-3c7cf446db2c"),
                    Nome = "User Test",
                    Detalhes = "Test",
                    DtInclusao = DateTime.Now,
                    Email = "Test@gmail.com"
                };

                _demoService.Add(model);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }
        }

        public async Task<IEnumerable<DemoDTO>> Example2()
        {
            return await _demoService.GetAllAsync();
        }
    }
}
