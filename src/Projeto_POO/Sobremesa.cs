using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    internal class Sobremesa
    {
        private string _descricao;
        private double _preco;
        
        public Sobremesa(string descricao, double preco)
        {
            _descricao = descricao;
            _preco = preco;
        }
        public double CalcularValorFinal()
        {
            return _preco;
        }
        public override string ToString()
        {
            return $"{_descricao} - {_preco:C2}";
        }
    }
}
