using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsDelivery.Core.Shared.Dto.Erro
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }

        public ErrorResponse(string id)
        {
            Id = id;
            Data = DateTime.Now;
            Mensagem = "Erro inesperado.";
        }
    }
}
