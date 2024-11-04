using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ProjetoBiblioteca
{
    class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        public int Isbn { get => isbn; set => isbn = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Editora { get => editora; set => editora = value; }
        public List<Exemplar> Exemplares { get => exemplares; set => exemplares = value; }

        public Livro()
        {
            exemplares = new List<Exemplar>();
        }

        public void adicionarExemplar (Exemplar exemplar)
        {
            Exemplares.Add(exemplar);
        }
        
        public int qtdeExemplares()
        {
           return Exemplares.Count; 
        }
        
        public int qtdeDisponiveis()
        {
            int i=0; 
            foreach(Exemplar exemplar in Exemplares)
            {
                if (exemplar.disponivel() == true)
                {
                    i++;
                }
            }
            return i; 
        }

        public int qtdeEmprestimo()
        {
            int i = 0;
            foreach (Exemplar exemplar in Exemplares)
            {
                if (!exemplar.disponivel())
                {
                    i++;
                }
            }
            return i;
        }

        public double percDisponibilidade()
        {
            int total = qtdeExemplares();
            int disponivel = qtdeDisponiveis();

            if(total == 0)
            {
                return 0;
            }
            else
            {
                return (double)disponivel / total * 100;
            }
        }
    }
}
