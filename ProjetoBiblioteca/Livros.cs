using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    class Livros
    {
        private List<Livro> acervo;
        public List<Livro> Acervo { get => acervo; }

        public Livros()
        {
            acervo = new List<Livro>();
        }
        public void adicionar (Livro Livro)
        {
            if(Livro != null && !Acervo.Contains(Livro))
            {
                acervo.Add(Livro);
            }
        }

        public Livro pesquisar (Livro livro)
        {
            return acervo.FirstOrDefault(l => l.Isbn ==livro.Isbn);
        }

    }
}
