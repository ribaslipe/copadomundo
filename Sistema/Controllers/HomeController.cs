using DAL.Entity;
using DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData["Jogos"] = BuscarJogosTodos();
            return View();
        }

        #region jogos

        private List<jogo> BuscarJogos()
        {
            try
            {
                return new JogoDal().ListarTodosAtual();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return null;
            }
        }

        private List<jogo> BuscarJogosTodos()
        {
            try
            {
                return new JogoDal().ListarTodos();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return null;
            }
        }

        public ActionResult SalvarTemporada(string lst)
        {
            try
            {
                JogoDal jDal = new JogoDal();
                TemporadaDal tDal = new TemporadaDal();
                int idtemporada = 0, idtemporadaNova = 0;
                string[] pars = lst.Split('°');
                for (int i = 0; i < pars.Length; i++)
                {
                    if (pars[i] != "")
                    {
                        string[] itens = pars[i].Split('~');
                        var timeid = Convert.ToInt32(itens[0]);
                        var numgol = Convert.ToInt32(itens[2]);
                        idtemporada = Convert.ToInt32(itens[1]);


                        var jogo1 = jDal.ObterPorTime1(timeid, idtemporada);
                        if (jogo1 != null)
                        {
                            jogo1.time1_gols = numgol;
                            jDal.Atualizar(jogo1);

                            if (i == 0)
                            {
                                var temp = tDal.ObterPorId(idtemporada);
                                temp.atual = false;
                                tDal.Atualizar(temp);
                                idtemporadaNova = tDal.ObterNovaTemporada(idtemporada).temporada_id;
                                var tempNext = tDal.ObterPorId(idtemporadaNova);
                                if (tempNext != null)
                                {
                                    tempNext.atual = true;
                                    tDal.Atualizar(temp);
                                }
                            }
                        }
                        var jogo2 = jDal.ObterPorTime2(timeid, idtemporada);
                        if (jogo2 != null)
                        {
                            jogo2.time2_gols = numgol;
                            jDal.Atualizar(jogo2);
                        }
                    }
                }
                CriaTemporadaNova(idtemporada, idtemporadaNova);

                return PartialView("_lstJogosMontada", BuscarJogosTodos());
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return PartialView("_lstJogosMontada", BuscarJogosTodos());
            }
        }

        public void CriaTemporadaNova(int idTemporada, int idTemporadaNova)
        {
            try
            {
                TimeDal tDal = new TimeDal();
                JogoDal jDal = new JogoDal();
                var timesNovaTemp = tDal.ListarTimesProximaFase(idTemporada);

                if (timesNovaTemp.Count == 1)
                {
                    jogo j = new jogo();
                    j.temporada_id = idTemporadaNova;
                    j.time1_id = timesNovaTemp[0].time_id;
                    j.time2_id = null;
                    jDal.Salvar(j);
                }
                else
                {
                    for (int i = 0; i < timesNovaTemp.Count; i++)
                    {
                        i = 0;
                        var item = timesNovaTemp[i];

                        jogo j = new jogo();
                        j.temporada_id = idTemporadaNova;
                        j.time1_id = item.time_id;
                        j.time2_id = timesNovaTemp[i + 1].time_id;
                        jDal.Salvar(j);

                        timesNovaTemp.Remove(timesNovaTemp[i]);
                        timesNovaTemp.Remove(timesNovaTemp[i]);
                    }
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult zerarCampeonato()
        {
            try
            {
                JogoDal jDal = new JogoDal();
                TemporadaDal tDal = new TemporadaDal();


                var lst1 = jDal.ListarTodos(1);
                var lst2 = jDal.ListarTodos(2);
                var lst3 = jDal.ListarTodos(3);
                var lst4 = jDal.ListarTodos(4);

                foreach (var item in lst1) { item.time1_gols = null; item.time2_gols = null; jDal.Atualizar(item); }
                foreach (var item in lst2) jDal.Excluir(item);
                foreach (var item in lst3) jDal.Excluir(item);
                foreach (var item in lst4) jDal.Excluir(item);

                var temp = tDal.ObterPorId(4);
                temp.atual = false;
                tDal.Atualizar(temp);
                
                var tempNext = tDal.ObterPorId(1);
                if (tempNext != null)
                {
                    tempNext.atual = true;
                    tDal.Atualizar(temp);
                }

                return PartialView("_lstJogosMontada", BuscarJogosTodos());
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return PartialView("_lstJogosMontada", BuscarJogosTodos());
            }
        }

        #endregion

    }
}