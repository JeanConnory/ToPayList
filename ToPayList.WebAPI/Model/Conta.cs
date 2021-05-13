using System;
using System.ComponentModel.DataAnnotations;

namespace ToPayList.WebAPI.Model
{
    public class Conta
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Descrição só pode ter no máximo 255 caracteres")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataInclusao { get; set; }
    }
}