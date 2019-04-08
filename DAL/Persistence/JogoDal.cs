using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence
{
    public class JogoDal
    {
        private Model Con;
        public JogoDal()
        {
            Con = new Model();
        }

        public List<jogo> ListarTodos()
        {
            try
            {
                return Con.jogo.ToList();
            }
            catch 
            {
                throw;
            }
        }

        public List<jogo> ListarTodos(int idTemporada)
        {
            try
            {
                return Con.jogo.Where(s => s.temporada_id == idTemporada).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<jogo> ListarTodosAtual()
        {
            try
            {
                return Con.jogo.Where(s => s.temporada.atual == true).ToList();
            }
            catch
            {
                throw;
            }
        }

        public jogo ObterPorTime1(int id, int idtemporada)
        {
            try
            {
                return Con.jogo.Where(s => s.time1_id == id && s.temporada_id == idtemporada).SingleOrDefault();
            }
            catch 
            {

                throw;
            }
        }
        public jogo ObterPorTime2(int id, int idtemporada)
        {
            try
            {
                return Con.jogo.Where(s => s.time2_id == id && s.temporada_id == idtemporada).SingleOrDefault();
            }
            catch
            {

                throw;
            }
        }

        public jogo ObterPorId(int id)
        {
            try
            {
                return Con.jogo.Find(id);
            }
            catch 
            {
                throw;
            }
        }

        public void Atualizar(jogo j)
        {
            try
            {
                var jantigo = ObterPorId(j.jogo_id);
                jantigo.time1_gols = j.time1_gols;
                jantigo.time2_gols = j.time2_gols;
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }        

        public void Salvar(jogo j)
        {
            try
            {
                Con.jogo.Add(j);
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Excluir(jogo j)
        {
            try
            {
                Con.jogo.Remove(j);
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

    }
}
