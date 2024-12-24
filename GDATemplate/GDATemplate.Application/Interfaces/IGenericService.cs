using System.Collections.Generic;
using System.Threading.Tasks;
using GDATemplate.Application.DTO.Entities;

namespace GDATemplate.Application.Interfaces
{
    public interface IGenericService
    {
        void Example1();
        Task<IEnumerable<DemoDTO>> Example2();
    }
}