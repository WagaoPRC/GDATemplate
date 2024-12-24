using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GDATemplate.Application.DTO.Entities;
using GDATemplate.Application.Interfaces;
using GDATemplate.Data.Interfaces;
using GDATemplate.Domain.Entities;

namespace GDATemplate.Application.Services.Entities
{
    public class DemoService : BaseService<DemoDTO, Demo>, IBaseService<DemoDTO, Demo>
    {
        private readonly IDemoRepository _demoRepository;

        public DemoService(IDemoRepository demoRepository, IMapper mapper) : base(demoRepository, mapper)
        {
            _demoRepository = demoRepository;
        }
    }
}
