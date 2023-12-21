using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3.Entidades
{
    public class Cliente
    {
        public int      ID         { get; set; }
        public string   NOME       { get; set; }
        public string   EMAIL      { get; set; }
        public DateTime NASCIMENTO { get; set; }
        public string   CPF        { get; set; }
        public string   ENDERECO   { get; set; }

        public override string ToString()
        {
            return $"Id: {this.ID}, Nome: {this.NOME}, E-mail: {this.EMAIL}, " +
                   $"Nascimento: {this.NASCIMENTO.ToString("dd/MM/yyyy")}, " +
                   $"CPF: {this.CPF}, Endereço: {this.ENDERECO}";
        }
    }
}
