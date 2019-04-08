using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence
{
    public class TimeDal
    {

        private Model Con;
        public TimeDal()
        {
            Con = new Model();
        }

        public time ObterPorId(int id)
        {
            try
            {
                return Con.time.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public List<time> ListarTimesProximaFase(int idTemporada)
        {
            try
            {
                List<time> lst = new List<time>();
                var lstJogosFaseAnterior = new JogoDal().ListarTodos(idTemporada);
                foreach (var item in lstJogosFaseAnterior)
                {
                    if (item.time1_gols > item.time2_gols)
                        lst.Add(ObterPorId((int)item.time1_id));
                    else
                        lst.Add(ObterPorId((int)item.time2_id));
                }
                return lst;
            }
            catch
            {

                throw;
            }
        }

    }
}
