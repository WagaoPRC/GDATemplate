using System.Collections.Generic;

namespace GDATemplate.Domain.Model.Retorno
{
    public class PageModel<T>
    {
        public int PaginaAtual { get; set; }

        public int TotalPaginas { get; set; }

        public int TotalItens { get; set; }

        public int ItensPorPagina { get; set; }

        public IEnumerable<T> Tabela { get; set; }

    }
}
