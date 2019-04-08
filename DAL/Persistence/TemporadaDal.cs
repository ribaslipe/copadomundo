using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence
{
    public class TemporadaDal
    {
        private Model Con;
        public TemporadaDal()
        {
            Con = new Model();
        }

        public temporada ObterPorId(int id)
        {
            try
            {
                return Con.temporada.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public void Atualizar(temporada t)
        {
            try
            {
                var tantigo = ObterPorId(t.temporada_id);
                tantigo.atual = t.atual;
               
                Con.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public temporada ObterNovaTemporada(int idtemporadaAtual)
        {
            try
            {
                var t = Con.temporada.ToList();
                return t.SkipWhile(x => x.temporada_id != idtemporadaAtual).Skip(1).FirstOrDefault();
            }
            catch 
            {
                throw;
            }
        }

        public temporada RetornaTemporadaAtual()
        {
            try
            {
                return Con.temporada.Where(s => s.atual == true).SingleOrDefault();
            }
            catch 
            {

                throw;
            }
        }

    }
}
