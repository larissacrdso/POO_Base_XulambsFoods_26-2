using System.Text;

namespace XulambsFoods {
    public abstract class Pedido {
        private static int s_ultimoPedido=0;
        private int _idPedido;
        private DateOnly _data;
        protected LinkedList<Pizza> _pizzas;
        protected LinkedList<Bebida> _bebidas;
        protected LinkedList<Sobremesa> _sobremesas;
        private bool _aberto;

        public Pedido() {
            _pizzas = new LinkedList<Pizza>();
            _bebidas= new LinkedList<Bebida>();
            _sobremesas = new LinkedList<Sobremesa>();
            _aberto = true;
            _data =  DateOnly.FromDateTime(DateTime.Now);
            s_ultimoPedido++;
            _idPedido = _data.Day*100 + s_ultimoPedido;
        }

        protected virtual void PodeAdicionar() {
            if (!_aberto)
                throw new PedidoFechadoException();
        } //virtual: autorização para ser sobrescrito.

        public int Adicionar(Pizza pizza) {
            if (pizza == null)
                throw new ArgumentNullException("Pizza não foi criada");
            
            PodeAdicionar();
            _pizzas.AddLast(pizza);
            return _pizzas.Count;
        }
        public int Adicionar(Bebida bebida)
        {
            if (bebida == null)
                throw new ArgumentNullException("Bebida não foi criada");

            PodeAdicionar();
            _bebidas.AddLast(bebida);
            return _bebidas.Count;
        }
        public int Adicionar(Sobremesa sobremesa)
        {
            if (sobremesa == null)
                throw new ArgumentNullException("Sobremesa não foi criada");

            PodeAdicionar();
            _sobremesas.AddLast(sobremesa);
            return _sobremesas.Count;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public abstract double PrecoAPagar();
        
        protected double ValorItens(){
            double preco = 0d;
            foreach (Pizza pizza in _pizzas) {
                preco += pizza.CalcularValorFinal();
            }
            foreach (Bebida bebida in _bebidas) {
                preco += bebida.CalcularValorFinal();
            }
            foreach (Sobremesa sobremesa in _sobremesas) {
                preco += sobremesa.CalcularValorFinal();
            }
            return preco;
        }

        protected string CabecalhoPedido()
        {
            StringBuilder relat = new StringBuilder($"Pedido nº{_idPedido} - {_data}\n");
            string estado = "fechado";
            if (_aberto)
                estado = "aberto";
            relat.AppendLine($"Pedido {estado}.");
            int i = 0;
            foreach (Pizza pizza in _pizzas)
            {
                relat.AppendLine($"{++i:D2} {pizza.ToString()}");
            }
            return relat.ToString();
        }

        protected virtual string RodapePedido()
        {
            return $"\nTOTAL DO PEDIDO: {PrecoAPagar():C2}";
        }

        
        public override string ToString() {
            return $"{CabecalhoPedido()}\n{RodapePedido()}";
        }

        public override bool Equals(object? obj) {
            Pedido outro = obj as Pedido;
            return this._idPedido == outro._idPedido;
        }

        public override int GetHashCode() {
            return _idPedido;
        }
    }
}
