using System.ComponentModel.DataAnnotations;

namespace Projeto3.API.Models
{
    public class ClienteViewModel
    {
        public int      ID         { get; set; }

        [Required (ErrorMessage = "O nome do cliente é obrigatório")]
        public string   NOME       { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório")]
        public string   EMAIL      { get; set; }

        [Required(ErrorMessage = "A data de nascimento do cliente é obrigatório")]
        public string   NASCIMENTO { get; set; }

        [Required(ErrorMessage = "O CPF do cliente é obrigatório")]
        public string   CPF        { get; set; }

        [Required(ErrorMessage = "O endereço do cliente é obrigatório")]
        public string   ENDERECO   { get; set; }
    }
}
