using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public int Tombo { get => tombo; set => tombo = value; }
        public List<Emprestimo> Emprestimos { get => emprestimos; }

        public Exemplar()
        {
            emprestimos = new List<Emprestimo>();
        }

        public bool emprestar()
        {
            if (disponivel())
            {
                emprestimos.Add(new Emprestimo { DtEmprestimo = DateTime.Now });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool devolver()
        {
            var ultimoEmprestimo = emprestimos.LastOrDefault();
            if (ultimoEmprestimo != null && ultimoEmprestimo.DtDevolucao == DateTime.MinValue) 
            {
                ultimoEmprestimo.DtDevolucao = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool disponivel()
        {
            var ultimoEmprestimo = emprestimos.LastOrDefault();
            if (ultimoEmprestimo == null)
            {
                return true;
            }
            if (ultimoEmprestimo.DtDevolucao != DateTime.MinValue)
            {
                return true;
            }
            return false;
        }

        public int qtdeEmprestimos()
        {
            return emprestimos.Count;
        }
    }
}
