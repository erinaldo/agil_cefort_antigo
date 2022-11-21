using System;
using System.Linq;
using System.Web.UI;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections;

namespace ProtocoloAgil.pages
{
    public class CalendarDay
    {
        public string CssClass { get; set; }
        public DateTime Date { get; set; }
        public CalendarType CalendarType { get; set; }
        public string Task { get; set; }
    }
    public class Simultaneidade
    {
        public DayOfWeek DiaDaSemana { get; set; }
        public int? Quantidade { get; set; }
        public int Id { get; set; }
        public CalendarType CalendarType { get; internal set; }
    }
    public enum CalendarType
    {
        Feriado = 1,
        FinalDeSemana = 2,
        Inicializacao = 3,
        SimultaneidadeVida = 4,
        simultaneidadeMundoTrab = 9,
        SimultaneidadeTrabalho = 8,
        Trabalho = 5,
        Finalizacao = 6,
        Mensal = 7
    }

    public partial class CalcularCalendario : System.Web.UI.Page
    {
        int j;

        int AulasGeral = 0;
        public List<CalendarDay> Calendario { get; set; }

        int codAprendiz = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["codAprendizAcompanhamento"] != null)
            {
                codAprendiz = int.Parse(Session["codAprendizAcompanhamento"].ToString());
            }
            else if (!string.IsNullOrWhiteSpace(Request.QueryString["codAprendiz"]))
            {
                codAprendiz = int.Parse(Request.QueryString["codAprendiz"]);
            }
            if (!IsPostBack)
            {
                txtTotalEncontro.Text = "0";
                //txtHorasJornadas.Text = "6";
                txtTotalPratica.Text = "0";
                //multiview1.ActiveViewIndex = 0;
                CarregarTurma();
            }

        }

        public void CarregarTurma()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from i in db.CA_Turmas
                             join A in db.CA_Aprendiz on i.TurUnidade equals A.Apr_Unidade
                             where A.Apr_Codigo == int.Parse(Session["codAprendizAcompanhamento"].ToString())
                             select new
                             {
                                 i.TurCurso,
                                 i.TurNome,
                                 i.TurNumeroMeses,
                                 i.TurStatus,
                                 i.TurCodigo,
                                 i.TurUnidade,
                                 A.Apr_HorasDiarias,
                                 A.Apr_DataContrato,
                                 A.Apr_Nome,
                                 A.Apr_InicioAprendizagem
                             }).OrderBy(p => p.TurNome);

                txtCodigo.Text = Session["codAprendizAcompanhamento"].ToString();
                txtNome.Text = query.FirstOrDefault().Apr_Nome;


                txtHorasJornadas.Text = query.FirstOrDefault().Apr_HorasDiarias.ToString() ?? "";
                txtDataInicioContrato.Text = string.Format("{0: dd/MM/yyyy}", query.FirstOrDefault().Apr_InicioAprendizagem);

            }
        }
        public DayOfWeek DayOfWeekSemana(string dia)
        {
            switch (dia)
            {
                case "1":
                    return DayOfWeek.Sunday;
                case "2":
                    return DayOfWeek.Monday;
                case "3":
                    return DayOfWeek.Tuesday;
                case "4":
                    return DayOfWeek.Wednesday;
                case "5":
                    return DayOfWeek.Thursday;
                case "6":
                    return DayOfWeek.Friday;
                case "7":
                    return DayOfWeek.Saturday;
                default:
                    return DayOfWeek.Sunday;
            }
        }
        public HashSet<DateTime> CarregaFeriados()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = from F in db.CA_Feriados
                                 where F.FerUnidade == 1 || F.FerUnidade == 99 || F.FerUnidade == int.Parse(Session["unidade"].ToString())
                                 select F.FerData;

                return new HashSet<DateTime>(datasource);
            }
        }




        protected void BtnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtTotalEncontro.Text) <= 0) throw new ArgumentException("Total de encontros deve ser positivo");
                if (txtTotalEncontro.Text.Equals(string.Empty)) throw new ArgumentException("Informe o total de encontros");

                if (txtHorasJornadas.Text.Length < 0) throw new ArgumentException("Números de horas da Jornada Diária deve ser positivo");
                if (txtHorasJornadas.Text.Equals(string.Empty)) throw new ArgumentException("Informe o números de horas da Jornada Diária");

                if (txtDataInicioContrato.Text.Equals(string.Empty)) throw new ArgumentException("Informe a data de encontro");


                var sqlDel = "DELETE CA_CalendarioJovem WHERE CLJCodigo = " + codAprendiz;
                var connDel = new Conexao();
                connDel.Consultar(sqlDel);

                btnCalcular.Enabled = false;
                // CarregarIntroducao();
                List<CalendarDay> calendario = CalcularDiasCalendario();
                Session["Calendario_User"] = calendario;
                preencherLabels(calendario);
                CalendarioCarrega();
                btnCalcular.Enabled = true;
                // btnGerarEnturmacao.Visible = true;
                //btnFimContrato.Visible = true;
                throw new ArgumentException("Calculos Realizados. ");
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('" + ex.Message + "')", true);
            }
        }

        private void preencherLabels(List<CalendarDay> calendario)
        {
            lblTermino.Text = calendario.Max(c => c.Date).ToShortDateString();
            tbtDataInicioAmb.Text = calendario.Where(c => c.CalendarType == CalendarType.Inicializacao).Min(c => c.Date).ToShortDateString();
            lblTerminoIntro.Text = calendario.Where(c => c.CalendarType == CalendarType.Inicializacao).Max(c => c.Date).ToShortDateString();
            txtNumeroIntro.Text = calendario.Where(c => c.CalendarType == CalendarType.Inicializacao).Count().ToString();
            txtDataEmpresa.Text = calendario.Where(c => c.CalendarType == CalendarType.Trabalho).Min(c => c.Date).ToShortDateString();

            if (calendario.Where(c => c.CalendarType == CalendarType.Mensal).Count() > 0)
            {
                Session["dataInicioMensal"] = calendario.Where(c => c.CalendarType == CalendarType.Mensal).Min(c => c.Date).ToShortDateString();
                Session["dataTerminoMensal"] = calendario.Where(c => c.CalendarType == CalendarType.Mensal).Max(c => c.Date).ToShortDateString();
            }
            //lblNMundoTrab.Text = calendario.Where(c => c.CalendarType == CalendarType.simultaneidadeMundoTrab).Count().ToString();
            //lblInicioMundTrab.Text = calendario.Where(c => c.CalendarType == CalendarType.simultaneidadeMundoTrab).Min(c => c.Date).ToShortDateString();

            if (calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeVida).Count() > 0)
                lblIniciovida.Text = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeVida).Min(c => c.Date).ToShortDateString();

            if (calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeVida).Count() > 0)
                Session["dataTerminoVidaTrab"] = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeVida).Max(c => c.Date).ToShortDateString();


            lblNVida.Text = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeVida).Count().ToString();

            if (calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeTrabalho).Count() > 0)
                lblInicioTrabalho.Text = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeTrabalho).Min(c => c.Date).ToShortDateString();

            if (calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeTrabalho).Count() > 0)
                Session["dataTerminoTrabalho"] = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeTrabalho).Max(c => c.Date).ToShortDateString();


            lblNTrab.Text = calendario.Where(c => c.CalendarType == CalendarType.SimultaneidadeTrabalho).Count().ToString();

            if (calendario.Where(c => c.CalendarType == CalendarType.Finalizacao).Count() > 0)
                lblDataInicioFinalizacao.Text = calendario.Where(c => c.CalendarType == CalendarType.Finalizacao).Min(c => c.Date).ToShortDateString();

            if (calendario.Where(c => c.CalendarType == CalendarType.Finalizacao).Count() > 0)
                Session["dataTerminoFinalizacao"] = calendario.Where(c => c.CalendarType == CalendarType.Finalizacao).Max(c => c.Date).ToShortDateString();

            txtNumeroFinalizacao.Text = calendario.Where(c => c.CalendarType == CalendarType.Finalizacao).Count().ToString();
            lblNComplementares.Text = "";
            lblEncontroMensais.Text = calendario.Where(c => c.CalendarType == CalendarType.Mensal).Count().ToString();
            lblEncontrosNAgendados.Text = "";
            lblTotalTeoria.Text = calendario.Where(c =>
            c.CalendarType != CalendarType.Trabalho
            && c.CalendarType != CalendarType.FinalDeSemana
            && c.CalendarType != CalendarType.Feriado).Count().ToString();
            lblDiasPratica.Text = calendario.Where(c => c.CalendarType == CalendarType.Trabalho).Count().ToString();
        }
        public List<CalendarDay> CalcularDiasCalendario()
        {
            List<CalendarDay> calendar = new List<CalendarDay>();
            List<CA_CalendarioJovem> calendario_dias = new List<CA_CalendarioJovem>();
            using (var dbVida = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                DateTime date = Convert.ToDateTime(txtDataInicioContrato.Text);
                int diasTeoria = int.Parse(txtTotalEncontro.Text);
                int diasIntroducao = int.Parse(txtNumEncInicial.Text);
                int diasPratica = int.Parse(txtTotalPratica.Text);
                // mundo do trabalho
                int diasSimultaneidade = int.Parse(txtNumEncSimultaneidade.Text);
                var aprendiz = dbVida.CA_Aprendiz.Where(a => a.Apr_Codigo == codAprendiz).FirstOrDefault();

                Session["areaAtuacao"] = aprendiz.Apr_AreaAtuacao;


                Session["unidade"] = aprendiz.Apr_Unidade;
                HashSet<DateTime> feriados = CarregaFeriados();

                //int totalMeses = int.Parse(Session["numeroMeses"].ToString());




                for (; diasTeoria > 0 || diasPratica > 0; date = date.AddDays(1))
                {
                    if (feriados.Contains(date))
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.Feriado,
                            CssClass = "feriadoV",
                            Date = date,
                            Task = "F"
                        });
                        continue;
                    }

                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.FinalDeSemana,
                            CssClass = "finaldesemana",
                            Date = date,
                            Task = ""
                        });
                        continue;
                    }

                    if (diasIntroducao > 0 && diasTeoria > 0)
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.Inicializacao,
                            CssClass = "teoriaN",
                            Date = date,
                            Task = "A"
                        });
                        var calendarAmb = new CA_CalendarioJovem
                        { CLJCodigo = codAprendiz, CLJDataEncontro = date, CLJTurma = 0, CLJTipo = (int)CalendarType.Inicializacao };
                        calendario_dias.Add(calendarAmb);
                        diasTeoria--;
                        diasIntroducao--;
                        continue;
                    }

                    if (diasSimultaneidade> 0 && date.DayOfWeek == DayOfWeekSemana(ddDiaSimultaneidade.SelectedValue))
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.SimultaneidadeVida,
                            CssClass = "teoriaN",
                            Date = date,
                            Task = "S"
                        });
                        var calendarAmb = new CA_CalendarioJovem
                        { CLJCodigo = codAprendiz, CLJDataEncontro = date, CLJTurma = 0, CLJTipo = (int)CalendarType.SimultaneidadeVida };

                        calendario_dias.Add(calendarAmb);
                        diasTeoria--;
                        diasSimultaneidade--;
                        continue;
                    }


                    if (diasPratica > 0)
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.Trabalho,
                            CssClass = "trabalho",
                            Date = date,
                            Task = "P"
                        });
                        diasPratica--;
                        var calendarJovem = new CA_CalendarioJovem();
                        calendarJovem.CLJCodigo = codAprendiz;
                        calendarJovem.CLJDataEncontro = date;
                        calendarJovem.CLJTurma = 0;
                        calendarJovem.CLJTipo = 5;

                        dbVida.CA_CalendarioJovem.InsertOnSubmit(calendarJovem);
                        dbVida.SubmitChanges();
                        continue;
                    }

                    if ( diasPratica == 0)
                    {
                        calendar.Add(new CalendarDay()
                        {
                            CalendarType = CalendarType.Finalizacao,
                            CssClass = "teoriaN",
                            Date = date,
                            Task = "C"
                        });
                        var calendarAmb = new CA_CalendarioJovem
                        { CLJCodigo = codAprendiz, CLJDataEncontro = date, CLJTurma = 0, CLJTipo = (int)CalendarType.Finalizacao };
                        calendario_dias.Add(calendarAmb);
                        diasTeoria--;
                        continue;
                    }

                }
                dbVida.CA_CalendarioJovem.InsertAllOnSubmit(calendario_dias);
                dbVida.SubmitChanges();
            }
            return calendar;
        }
        protected void CalendarioCarrega()
        {
            try
            {

                string diaSemana;

                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                {
                    diaSemana = Funcoes.DiaSemana(int.Parse(ddDiaSimultaneidade.SelectedValue));
                    Session["cursoAprendiz"] = db.CA_AreaAtuacaos.Where(a => a.AreaCodigo == int.Parse(Session["areaAtuacao"].ToString())).FirstOrDefault().AreaDescricao;
                }

                Session["CargaHoraria"] = txtHorasJornadas.Text;
                Session["formacaoTeorica"] = diaSemana;
                Session["inicioIntro"] = txtDataInicioContrato.Text;
                Session["TerminoIntro"] = lblTerminoIntro.Text;

                Session["inicioContrato"] = txtDataInicioContrato.Text;
                Session["TerminoContrato"] = lblTermino.Text;

                Session["InicioAprendiz"] = txtDataInicioContrato.Text;
                Session["TerminoAprendiz"] = lblTermino.Text;
                //Session["diaSimultaneidade"] = ddDiaSemanaVida.SelectedValue;
                //Session["diaEncontro"] = ddEncontroMensal.SelectedValue;
                Session["JornadaTeorioa"] = lblTotalTeoria.Text;
                Session["JornadaTeorioaHoras"] = int.Parse(txtHorasJornadas.Text) * int.Parse(lblTotalTeoria.Text);
                Session["DiasPratica"] = lblDiasPratica.Text;
                Session["PraticaHoras"] = int.Parse(txtHorasJornadas.Text) * int.Parse(lblDiasPratica.Text);

                Session["codAprendiz"] = codAprendiz;
                Session["cargaHoraria"] = txtHorasJornadas.Text + "H";
                //Session["formacaoVida"] = ddDiaSemanaVida.SelectedItem;
                //Session["formacaoTrab"] = ddDiaSemanaTrab.SelectedItem;
                //Session["formacaoEnc"] = ddEncontroMensal.SelectedItem;
                Session["ambientacao"] = txtDataInicioContrato.Text + " a " + lblTerminoIntro.Text;
                // Session["dataContrato"] = txtDataInicioContrato.Text + " a " + txtDataFinalContrato.Text;
                Session["dataFinalFinalizacao"] = lblDataInicioFinalizacao.Text;
                // Session["dataTerminoContrato"] = txtDataFinalContrato.Text;
                Session["dataInicioAmb"] = txtDataInicioContrato.Text;
                Session["dataTerminoAmb"] = lblTerminoIntro.Text;
                Session["dataInicioEmp"] = txtDataEmpresa.Text;
                Session["dataInicioVida"] = lblIniciovida.Text;
                Session["dataInicioTrab"] = lblInicioTrabalho.Text;
                Session["nEncAmb"] = txtNumeroIntro.Text;
                // Session["nEncFinal"] = txtNFinalizacao.Text;
                Session["nEncVida"] = lblNVida.Text;
                Session["nEncTrab"] = lblNTrab.Text;
                Session["nEncCompNecessario"] = lblNComplementares.Text;
                Session["nEncMensaisPossiveis"] = lblEncontroMensais.Text;
                Session["nEncNAgendados"] = lblEncontrosNAgendados.Text;
                Session["nDiasTeoria"] = lblTotalTeoria.Text;
            }

            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                   "alert('" + ex.Message + "')", true);
            }
        }

        //protected void BtnImprimir_Click(object sender, EventArgs e)
        //{
        //    Session["codAprendiz"] = codAprendiz;
        //    Session["cargaHoraria"] = txtHorasJornadas.Text + "H";
        //    Session["formacaoVida"] = ddDiaSemanaVida.SelectedItem;
        //    Session["formacaoTrab"] = ddDiaSemanaTrab.SelectedItem;
        //    Session["formacaoEnc"] = ddEncontroMensal.SelectedItem;
        //    Session["ambientacao"] = txtDataInicioContrato.Text + " a " + lblTerminoIntro.Text;
        //    // Session["dataContrato"] = txtDataInicioContrato.Text + " a " + txtDataFinalContrato.Text;
        //    Session["dataFinalFinalizacao"] = lblDataInicioFinalizacao.Text;
        //    // Session["dataTerminoContrato"] = txtDataFinalContrato.Text;
        //    Session["dataInicioAmb"] = txtDataInicioContrato.Text;
        //    Session["dataTerminoAmb"] = lblTerminoIntro.Text;
        //    Session["dataInicioEmp"] = txtDataEmpresa.Text;
        //    Session["dataInicioVida"] = lblIniciovida.Text;
        //    Session["dataInicioTrab"] = lblInicioTrabalho.Text;
        //    Session["nEncAmb"] = txtNumeroIntro.Text;
        //    // Session["nEncFinal"] = txtNFinalizacao.Text;
        //    Session["nEncVida"] = lblNVida.Text;
        //    Session["nEncTrab"] = lblNTrab.Text;
        //    Session["nEncCompNecessario"] = lblNComplementares.Text;
        //    Session["nEncMensaisPossiveis"] = lblEncontroMensais.Text;
        //    Session["nEncNAgendados"] = lblEncontrosNAgendados.Text;
        //    Session["nDiasTeoria"] = lblTotalTeoria.Text;
        //    Session["nDiasPratica"] = lblDiasPratica.Text;

        //    Session["id"] = "115";

        //    multiview1.ActiveViewIndex = 1;
        //}

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            //multiview1.ActiveViewIndex = 0;
        }
        //protected DataTable TabelaInsercaoIntrodutorio()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Clear();
        //    dt.Columns.Add("TurCodigo", typeof(int));
        //    dt.Columns.Add("DisCodigo", typeof(int));
        //    dt.Columns.Add("PlcCargaHoraria", typeof(int));
        //    dt.Columns.Add("PlcOrdemDisciplina", typeof(int));
        //    dt.Columns.Add("PlcNumeroAulas", typeof(int));
        //    dt.Columns.Add("EnsNumeroPeriodos", typeof(string));
        //    dt.Columns.Add("TurDiaSemana", typeof(string));
        //    dt.Columns.Add("EducCodigo", typeof(int));
        //    dt.Columns.Add("TurnumeroMeses", typeof(int));

        //    Session["TotalAulasPlano"] = 0;

        //    var db = new DC_ProtocoloAgilDataContext(GetConfig.Config());

        //    int Contaaulaplano = 0;

        //    string turcodigo = Session["turma"].ToString();
        //    string codAprendiz = this.codAprendiz.ToString();

        //    var sql = (from T in db.CA_Turmas
        //               join P in db.CA_PlanoCurriculars on T.TurPlanoCurricular equals P.PlcCodigoPlano
        //               where T.TurCodigo == int.Parse(turcodigo)
        //               orderby P.PlcOrdemDisciplina ascending
        //               select new
        //               {
        //                   T.TurCodigo,
        //                   P.PlcDisciplina,
        //                   P.PlcCargaHoraria,
        //                   P.PlcOrdemDisciplina,
        //                   P.PlcNumeroAulas,
        //                   T.TurDiaSemana,
        //                   P.EducCodigo,
        //                   T.TurNumeroMeses,
        //                   T.TurUnidade,
        //                   T.TurEducador
        //               });

        //    Session["TurUnidade"] = sql.FirstOrDefault().TurUnidade;
        //    Session["TurEducador"] = sql.FirstOrDefault().TurEducador;
        //    foreach (var row in sql)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[0] = row.TurCodigo;
        //        dr[1] = row.PlcDisciplina;
        //        dr[2] = row.PlcCargaHoraria;
        //        dr[3] = row.PlcOrdemDisciplina;
        //        dr[4] = row.PlcNumeroAulas;
        //        dr[5] = 1;
        //        dr[6] = row.TurDiaSemana;
        //        dr[7] = row.TurEducador;
        //        dr[8] = row.TurNumeroMeses;
        //        Contaaulaplano += row.PlcNumeroAulas.Value;
        //        dt.Rows.Add(dr);
        //    }

        //    Session["TotalAulasPlano"] = Contaaulaplano;
        //    return dt;
        //}
        //protected void btnGererEnturmacao_Click(object sender, EventArgs e)
        //{

        //    int QueryTotal;
        //    using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //    {
        //        var query = (from i in db.CA_Aprendiz
        //                     where i.Apr_Codigo == codAprendiz && (i.Apr_HorasDiarias != 0 &&
        //                     i.Apr_UnidadeParceiro != 0 && i.Apr_Turma != 0 && i.Apr_Tutor != string.Empty &&
        //                     i.Apr_TipoContratacao != string.Empty && i.AprInicioExpediente != null && i.AprTerminoExpediente != null)
        //                     select i);
        //        QueryTotal = query.Count();
        //    }
        //    if (QueryTotal > 0)
        //    {
        //        try
        //        {
        //            var conn = new Conexao();

        //            string sqlAlocacoes = "INSERT INTO CA_AlocacaoAprendiz ( ALAAprendiz, ALADataInicio, " +
        //                                  "ALADataPrevTermino, ALATurma, ALAUnidadeParceiro, ALAStatus, ALATutor, " +
        //                                  "ALAInicioExpediente, ALATerminoExpediente, ALAValorBolsa, ALAValorTaxa, " +
        //                                  "ALApagto, ALAValorMatricula )" +
        //                                  "SELECT CA_CalendarioJovem.CLJCodigo, Min(CA_CalendarioJovem.CLJDataEncontro) " +
        //                                  "AS MínDeCLJDataEncontro, Max(CA_CalendarioJovem.CLJDataEncontro) AS MáxDeCLJDataEncontro, " +
        //                                  "CA_CalendarioJovem.CLJTurma, CA_Aprendiz.Apr_UnidadeParceiro, 'A' AS Expr1, " +
        //                                  "CA_Aprendiz.APR_TUTOR, CA_Aprendiz.AprInicioExpediente, CA_Aprendiz.APRTERMINOEXPEDIENTE, " +
        //                                  "0 AS Expr2, 0 AS Expr3, CA_Aprendiz.Apr_TipoContratacao, 0 AS Expr4 " +
        //                                  "FROM CA_Aprendiz INNER JOIN(CA_CalendarioJovem INNER JOIN CA_Turmas ON " +
        //                                  "CA_CalendarioJovem.CLJTurma = CA_Turmas.TurCodigo) ON " +
        //                                  "CA_Aprendiz.Apr_Codigo = CA_CalendarioJovem.CLJCodigo " +
        //                                  "GROUP BY CA_CalendarioJovem.CLJCodigo, CA_CalendarioJovem.CLJTurma, " +
        //                                  "CA_Aprendiz.Apr_UnidadeParceiro, CA_Aprendiz.APR_TUTOR, CA_Aprendiz.AprInicioExpediente, " +
        //                                  "CA_Aprendiz.APRTERMINOEXPEDIENTE, CA_Aprendiz.Apr_TipoContratacao " +
        //                                  "HAVING(((CA_CalendarioJovem.CLJCodigo) = " + codAprendiz + "))";

        //            string sqlAulasDisc = "INSERT INTO CA_AulasDisciplinasAprendiz ( AdiCodAprendiz, AdiDataAula, AdiTurma, AdiEducador, " +
        //                                  "AdiCargaHoraria, AdiPresenca, AdiDisciplina ) " +
        //                                  "SELECT CA_CalendarioJovem.CLJCodigo, CA_CalendarioJovem.CLJDataEncontro, CA_CalendarioJovem.CLJTurma, " +
        //                                  "CA_Turmas.TurEducador, CA_Aprendiz.Apr_HorasDiarias, '.' AS Expr5, CA_PlanoCurricular.PlcDisciplina " +
        //                                  "FROM(CA_Aprendiz INNER JOIN(CA_CalendarioJovem INNER JOIN CA_Turmas ON CA_CalendarioJovem.CLJTurma = " +
        //                                  "CA_Turmas.TurCodigo) ON CA_Aprendiz.Apr_Codigo = CA_CalendarioJovem.CLJCodigo) INNER JOIN CA_PlanoCurricular " +
        //                                  "ON CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano " +
        //                                  "WHERE(((CA_CalendarioJovem.CLJCodigo) = " + codAprendiz + "))";

        //            string sqlTerminoContrato = "SELECT CA_CalendarioJovem.CLJCodigo, Max(CA_CalendarioJovem.CLJDataEncontro) AS DataTermino, " +
        //                                     "Min(CA_CalendarioJovem.CLJDataEncontro) AS DataInicio " +
        //                                     "FROM CA_Aprendiz INNER JOIN CA_CalendarioJovem ON CA_Aprendiz.Apr_Codigo = CA_CalendarioJovem.CLJCodigo " +
        //                                     "GROUP BY CA_CalendarioJovem.CLJCodigo " +
        //                                     "HAVING(((CA_CalendarioJovem.CLJCodigo) = " + codAprendiz + "))";

        //            conn.Alterar(sqlAlocacoes);
        //            conn.Alterar(sqlAulasDisc);

        //            var result = conn.Consultar(sqlTerminoContrato);
        //            while (result.Read())
        //            {
        //                using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
        //                {
        //                    var query = from a in db.CA_Aprendiz
        //                                where a.Apr_Codigo == codAprendiz
        //                                select a;

        //                    var aprendiz = query.First();
        //                    aprendiz.Apr_Situacao = 6;
        //                    aprendiz.Apr_DataContrato = DateTime.Parse(result["DataInicio"].ToString());
        //                    aprendiz.Apr_InicioAprendizagem = DateTime.Parse(result["DataInicio"].ToString());
        //                    aprendiz.Apr_DataInicioEmpresa = DateTime.Parse(result["DataInicio"].ToString());
        //                    aprendiz.Apr_PrevFimAprendizagem = DateTime.Parse(result["DataTermino"].ToString());

        //                    db.SubmitChanges();
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //                "alert('Enturmação já foi gerada')", true);
        //            return;
        //        }
        //        btnGerarEnturmacao.Visible = false;
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //                "alert('Enturmação gerada com sucesso')", true);
        //    }
        //}

        //protected void InsereCronogramaEncontro(int diaSemana, int semanaEncontro, int nEncontro)
        //{

        //    var con = new Conexao();




        //    var sql = "Select top (" + nEncontro + ") * from CA_DatasEncontros where DteData >= '" + Session["dataInicio"].ToString() +
        //              "' and DteTipoEncontro = " + semanaEncontro + " and DATEPART(DW,[DteData]) =" + diaSemana;
        //    var result = con.Consultar(sql);
        //    var dataFinal = "";
        //    while (result.Read())
        //    {

        //        sql = "insert into CA_AulasDisciplinasAprendiz values (" + Session["codAprendiz"] + " , " + Session["codTurma"] + ", 767 , " +
        //              Session["TurEducador"].ToString() + ", '" + result["DteData"] + "', 6, '.', '')";
        //        con = new Conexao();
        //        con.Alterar(sql);

        //        dataFinal = result["DteData"].ToString();
        //    }

        //    var sqlUpdate = "Update CA_AlocacaoAprendiz set ALADataPrevTermino = '" + dataFinal +
        //             "'  where ALAAprendiz = '" + Session["CodAprendiz"].ToString() + "' and ALATurma = '" +
        //             Session["codTurma"] + "'";

        //    con.Alterar(sqlUpdate);


        //    return;

        //}


        //protected void gerarCronograma(int numeroDias, string codTurma, DateTime dataInicio, DateTime dataFinal)
        //{

        //    Session["TurCodigo"] = codTurma;
        //    Session["turma"] = codTurma;
        //    DateTime data = dataInicio;

        //    Session["codAprendiz"] = codAprendiz;
        //    Session["codTurma"] = codTurma;
        //    Session["dataInicio"] = dataInicio;
        //    Session["dataFinal"] = dataFinal;

        //    Session["codAprendiz"] = codAprendiz;
        //    Session["codTurma"] = codTurma;
        //    Session["dataInicio"] = dataInicio;

        //    int diaSemana = 0;
        //    int diaSemana2 = 0;
        //    var turCurso = "";
        //    int educturma = 0;
        //    var semanaEncontro = 0;
        //    int nEncontro = 0;
        //    var sql3 = "Select Turcurso,TurEducador,TurDiaSemana,TurDiaSemana02,TurNumeroMeses,TurSemanaEncontroMensal from Ca_Turmas where TurCodigo = '" +
        //               codTurma + "'";
        //    var con3 = new Conexao();
        //    var result3 = con3.Consultar(sql3);

        //    while (result3.Read())
        //    {
        //        try
        //        {
        //            turCurso = result3["Turcurso"].ToString();
        //            educturma = int.Parse(result3["TurEducador"].ToString());
        //            diaSemana = int.Parse(string.IsNullOrWhiteSpace(result3["TurDiaSemana"].ToString()) ? "0" : result3["TurDiaSemana"].ToString());
        //            diaSemana2 = int.Parse(string.IsNullOrWhiteSpace(result3["TurDiaSemana02"].ToString()) ? "0" : result3["TurDiaSemana02"].ToString());
        //            semanaEncontro = int.Parse(string.IsNullOrWhiteSpace(result3["TurSemanaEncontroMensal"].ToString()) ? "0" : result3["TurSemanaEncontroMensal"].ToString());
        //            nEncontro = int.Parse(result3["TurNumeroMeses"].ToString());
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }
        //    }
        //    con3.Fechar();
        //    Session["TurSemanaEncontroMensal"] = semanaEncontro;
        //    Session["TurCurso"] = turCurso;
        //    Session["TurEducador"] = educturma;

        //    if (turCurso.Equals("010"))
        //    {
        //        InsereCronogramaEncontro(diaSemana, semanaEncontro, nEncontro);
        //        return;
        //    }

        //    //else if (!turCurso.Equals("001"))
        //    //{
        //    //    var sql4 =
        //    //        "SELECT CA_AlocacaoAprendiz.ALAAprendiz, CA_Turmas.TurCurso, CA_AlocacaoAprendiz.ALAAprendiz, CA_AulasDisciplinasAprendiz.AdiDataAula FROM CA_AlocacaoAprendiz INNER JOIN CA_AulasDisciplinasAprendiz ON CA_AlocacaoAprendiz.ALATurma = CA_AulasDisciplinasAprendiz.AdiTurma AND CA_AlocacaoAprendiz.ALAAprendiz = CA_AulasDisciplinasAprendiz.AdiCodAprendiz INNER JOIN CA_Turmas ON CA_AlocacaoAprendiz.ALATurma = CA_Turmas.TurCodigo WHERE CA_AlocacaoAprendiz.ALAAprendiz='" +
        //    //        codAprendiz + "' AND (CA_Turmas.TurCurso='001' Or CA_Turmas.TurCurso='007')";
        //    //    var con4 = new Conexao();
        //    //    var result4 = con4.Consultar(sql4);
        //    //    var turma = "";
        //    //    int k = 0;

        //    //    while (result4.Read())
        //    //    {
        //    //        k++;
        //    //    }
        //    //    con3.Fechar();
        //    //    if (k == 0)
        //    //    {
        //    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
        //    //            "alert('É necessário gerar cronograma de Ambientação primeiro');", true);
        //    //        //       return;
        //    //    }
        //    //}


        //    //var sql = "Select TurDiaSemana from CA_Turmas where TurCodigo = " + codTurma + "";
        //    //var con = new Conexao();
        //    //var result = con.Consultar(sql);

        //    //int diaSemana = 0;

        //    //while (result.Read())
        //    //{
        //    //    diaSemana = int.Parse(result["TurDiaSemana"].ToString());
        //    //    break;
        //    //}


        //    var sql2 = "Select count(*) as qtd from CA_AulasDisciplinasAprendiz where AdiTurma = " + codTurma +
        //               " and AdiCodAprendiz = " + codAprendiz + "";
        //    var con2 = new Conexao();
        //    var result2 = con2.Consultar(sql2);
        //    int i = 0;
        //    while (result2.Read())
        //    {
        //        if (int.Parse(result2["qtd"].ToString()) > 0)
        //        {
        //            i++;
        //        }

        //        break;
        //    }
        //    con2.Fechar();

        //    if (i > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
        //            "alert('Já existe cronogramdo gerado para esse aprendiz nesta turma.');", true);
        //        return;
        //    }

        //    InsereCronogramaOutrasdisciplinas(diaSemana, diaSemana2);

        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
        //        "alert('Cronograma inserido com sucesso.');", true);
        //    //if (diaSemana == 1)
        //    //{
        //    //    InsereCronogramaOutrasdisciplinas(1);
        //    //}
        //    //else
        //    //{
        //    //    InsereCronogramaOutrasdisciplinas(7);
        //    //}

        //}
        public DayOfWeek dayOfWeek(int dSemana)
        {
            DayOfWeek diaSemana = new DayOfWeek();

            switch (dSemana)
            {
                case 2:
                    diaSemana = DayOfWeek.Monday;
                    break;

                case 3:
                    diaSemana = DayOfWeek.Tuesday;
                    break;
                case 4:
                    diaSemana = DayOfWeek.Wednesday;
                    break;
                case 5:
                    diaSemana = DayOfWeek.Thursday;
                    break;
                case 6:
                    diaSemana = DayOfWeek.Friday;
                    break;
            }

            return diaSemana;
        }

        //protected void InsereCronogramaOutrasdisciplinas(int diaSemana1, int diaSemana2)
        //{
        //    // thassio

        //    bool verificaUltima = false;
        //    string a = Session["TurCodigo"].ToString(); //simulada DataKeys[1]
        //    DateTime data = Convert.ToDateTime(Session["dataInicio"].ToString());
        //    DateTime dataFinal = Convert.ToDateTime(Session["dataFinal"].ToString());

        //    DataTable dt = TabelaInsercaoIntrodutorio();
        //    int DiscilpinaInicial = 0;
        //    int prinDisc = 0;
        //    int numAulas = 0;
        //    int contador = 0; // DiscilpinaInicial;
        //    int contaulas = 0;
        //    int cont02 = 0;

        //    // verificar se foi escolhida a disciplina correta
        //    //var sqlverifica = "SELECT TOP(1) CA_AulasDisciplinasAprendiz.AdiDataAula, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM CA_AulasDisciplinasAprendiz WHERE AdiTurma = '" + Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date + "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula ";
        //    //var converifica = new Conexao();
        //    //var resultado = converifica.Consultar(sqlverifica);

        //    //while (resultado.Read())
        //    //{
        //    //}
        //    AulasGeral = dt.Rows.Count;
        //    if (AulasGeral == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //            "alert('Não existem disciplinas definidas para esta turma/plano curricular, o cronograma não será gerado, favor consultar o administrador')", true);
        //        return;
        //    }
        //    for (; cont02 < AulasGeral && data <= dataFinal; contador++)
        //    {
        //        if (prinDisc.Equals(0))
        //        {
        //            prinDisc = 1;

        //            if (contador < dt.Rows.Count)
        //            {
        //                numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //            }
        //            else
        //            {
        //                break;
        //            }

        //            var sqlPrimeiraDisciplina =
        //                "SELECT Top(1) CA_AulasDisciplinasAprendiz.AdiTurma, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date +
        //                "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //            var conexaoPrimeiroDisciplina = new Conexao();
        //            var resultadoPrimeiraDisciplinas = conexaoPrimeiroDisciplina.Consultar(sqlPrimeiraDisciplina);
        //            while (resultadoPrimeiraDisciplinas.Read())
        //            {
        //                prinDisc = int.Parse(resultadoPrimeiraDisciplinas["PlcOrdemDisciplina"].ToString());
        //                contador = prinDisc - 1;
        //                break;
        //            }
        //            conexaoPrimeiroDisciplina.Fechar();
        //        }
        //        else
        //        {
        //            if (contador < dt.Rows.Count)
        //            {
        //                numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }


        //        if ((numAulas > 1) && (contaulas == 0))
        //        {
        //            var sqlPrimeiraDisciplina01 =
        //                "SELECT Top(1) CA_AulasDisciplinasAprendiz.AdiTurma, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_PlanoCurricular.PlcDisciplina,CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date +
        //                "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //            var conexaoPrimeiroDisciplina01 = new Conexao();
        //            var resultadoPrimeiraDisciplinas01 = conexaoPrimeiroDisciplina01.Consultar(sqlPrimeiraDisciplina01);
        //            while (resultadoPrimeiraDisciplinas01.Read())
        //            {
        //                var prinDisc01 = int.Parse(resultadoPrimeiraDisciplinas01["PlcDisciplina"].ToString());
        //                var sqlPrimeiraDisciplina02 =
        //                    "SELECT Top(5) CA_AulasDisciplinasAprendiz.AdiTurma,TurUnidade,TurUnidade, TurNumeroMeses, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                    Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date + "'  and AdiDisciplina =  '" +
        //                    prinDisc01 + "' ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //                var conexaoPrimeiroDisciplina02 = new Conexao();
        //                //                      List<string> resultalinhas = new List<string>(); 

        //                var resultadoPrimeiraDisciplinas02 =
        //                    conexaoPrimeiroDisciplina02.Consultar(sqlPrimeiraDisciplina02);
        //                //                       resultalinhas = conexaoPrimeiroDisciplina02.Consultar(sqlPrimeiraDisciplina02);

        //                // Session["TurUnidade"] = resultadoPrimeiraDisciplinas02["TurUnidade"];

        //                //Session["TurUnidade"] = resultadoPrimeiraDisciplinas02["TurUnidade"];

        //                numAulas = 0;
        //                contaulas = 1;
        //                while (resultadoPrimeiraDisciplinas02.Read())
        //                {
        //                    prinDisc = int.Parse(resultadoPrimeiraDisciplinas02["PlcOrdemDisciplina"].ToString());
        //                    AulasGeral = int.Parse(dt.Rows[0][8].ToString()); ;
        //                    if (contador < dt.Rows.Count)
        //                    {
        //                        if (contador < dt.Rows.Count)
        //                        {
        //                            numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //                        }
        //                    }
        //                }
        //                conexaoPrimeiroDisciplina02.Fechar();
        //                contador = prinDisc - 1;
        //                break;
        //            }
        //            conexaoPrimeiroDisciplina01.Fechar();
        //        }

        //        for (int i = 0; i < numAulas && data <= dataFinal;) //and data menor que data final...
        //        {
        //            verificaUltima = false;

        //            if (data.DayOfWeek.Equals(DayOfWeek.Saturday)) //sabado
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (VerificaFeriado(data))
        //            {
        //                data = data.AddDays(1);
        //                verificaUltima = true;
        //            }
        //            else
        //            {
        //                if (diaSemana1 == 1 || data.DayOfWeek == dayOfWeek(diaSemana1) || data.DayOfWeek == dayOfWeek(diaSemana2))
        //                {
        //                    using (var repository =
        //                        new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
        //                    {
        //                        var ap = new AulasDisciplinasAprendiz();
        //                        ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
        //                        ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
        //                        ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
        //                        ap.AdiEducador = Convert.ToInt32(Session["TurEducador"].ToString()); // codigo educador
        //                        ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString());
        //                        ; //codigo turma
        //                        ap.AdiPresenca = ".";
        //                        ap.AdiDataAula = data;


        //                        repository.Add(ap);
        //                        cont02++;
        //                        i++;


        //                        if (i == numAulas - 1)
        //                        {
        //                            j = j + 1;
        //                        }
        //                    }

        //                    if (verificaUltima == false)
        //                    {
        //                        data = data.AddDays(1);
        //                    }

        //                }
        //                else
        //                {
        //                    data = data.AddDays(1);
        //                }

        //            }
        //        }
        //    }


        //    //          DiscilpinaInicial = contador;

        //    if (cont02 != AulasGeral)
        //    {
        //        InsereCronogramaOutras(dt, data, prinDisc, dataFinal, diaSemana1, diaSemana2);
        //    }


        //    //            inserir aqui o update data data de fim da aloacação
        //    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data Final do aprendiz atualizada para a turma com sucesso.')", true);
        //}


        //protected void InsereCronogramaOutras(DataTable dt, DateTime data, int DiscilpinaInicial, DateTime dataFinal, int diaSemana1, int diaSemana2)
        //{
        //    if (VerificaFeriado(data).Equals(true))
        //    {
        //        data = data.AddDays(1);
        //    }

        //    for (int contador = 0; contador < DiscilpinaInicial - 1 && data < dataFinal;)
        //    {
        //        int numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //        for (int i = 0;
        //            i < numAulas && j < AulasGeral && data < dataFinal;
        //            i++) //and data menor que data final...
        //        {
        //            if (data.DayOfWeek.Equals(DayOfWeek.Saturday)) //sabado
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (VerificaFeriado(data.Date).Equals(true))
        //            {
        //                data = data.AddDays(1);
        //                i = i - 1;
        //            }
        //            else
        //            {
        //                if (diaSemana1 == 1 || data.DayOfWeek == dayOfWeek(diaSemana1) || data.DayOfWeek == dayOfWeek(diaSemana2))
        //                {
        //                    using (var repository =
        //                                       new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
        //                    {
        //                        var ap = new AulasDisciplinasAprendiz();
        //                        ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
        //                        ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
        //                        ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
        //                        ap.AdiEducador = Convert.ToInt32(Session["TurEducador"].ToString());
        //                        //Convert.ToInt32(dt.Rows[contador][7].ToString()); // codigo educador
        //                        ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString());
        //                        ; //codigo turma
        //                        ap.AdiPresenca = ".";
        //                        ap.AdiDataAula = data;
        //                        repository.Add(ap);
        //                        if (i == numAulas - 1)
        //                        {
        //                            j = j + 1;
        //                        }
        //                    }

        //                    data = data.AddDays(1);
        //                    contador++;
        //                }
        //                else
        //                {
        //                    data = data.AddDays(1);
        //                }
        //            }
        //        }
        //    }

        //}

        //protected void InsereCronogramaOutrasdisciplinas(int numeroDias)
        //{
        //    // thassio

        //    bool verificaUltima = false;
        //    string a = Session["TurCodigo"].ToString(); //simulada DataKeys[1]
        //    DateTime data = Convert.ToDateTime(Session["dataInicio"].ToString());
        //    DateTime dataFinal = Convert.ToDateTime(Session["dataFinal"].ToString());

        //    DataTable dt = TabelaInsercaoIntrodutorio();
        //    int DiscilpinaInicial = 0;
        //    int prinDisc = 0;
        //    int numAulas = 0;
        //    int contador = 0; // DiscilpinaInicial;
        //    int contaulas = 0;

        //    // verificar se foi escolhida a disciplina correta
        //    //var sqlverifica = "SELECT TOP(1) CA_AulasDisciplinasAprendiz.AdiDataAula, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM CA_AulasDisciplinasAprendiz WHERE AdiTurma = '" + Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date + "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula ";
        //    //var converifica = new Conexao();
        //    //var resultado = converifica.Consultar(sqlverifica);

        //    //while (resultado.Read())
        //    //{
        //    //}
        //    AulasGeral = dt.Rows.Count;
        //    if (AulasGeral == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
        //            "alert('Não existem disciplinas definidas para esta turma/plano curricular, o cronograma não será gerado, favor consultar o administrador')", true);
        //        return;
        //    }
        //    for (; contador < AulasGeral && data <= dataFinal; contador++)
        //    {
        //        if (prinDisc.Equals(0))
        //        {
        //            prinDisc = 1;
        //            numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());

        //            var sqlPrimeiraDisciplina =
        //                "SELECT Top(1) CA_AulasDisciplinasAprendiz.AdiTurma, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date +
        //                "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //            var conexaoPrimeiroDisciplina = new Conexao();
        //            var resultadoPrimeiraDisciplinas = conexaoPrimeiroDisciplina.Consultar(sqlPrimeiraDisciplina);
        //            while (resultadoPrimeiraDisciplinas.Read())
        //            {
        //                prinDisc = int.Parse(resultadoPrimeiraDisciplinas["PlcOrdemDisciplina"].ToString());
        //                contador = prinDisc - 1;
        //                break;
        //            }
        //            conexaoPrimeiroDisciplina.Fechar();
        //        }
        //        else
        //            numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());

        //        if ((numAulas > 1) && (contaulas == 0))
        //        {
        //            var sqlPrimeiraDisciplina01 =
        //                "SELECT Top(1) CA_AulasDisciplinasAprendiz.AdiTurma, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_PlanoCurricular.PlcDisciplina,CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date +
        //                "'  ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //            var conexaoPrimeiroDisciplina01 = new Conexao();
        //            var resultadoPrimeiraDisciplinas01 = conexaoPrimeiroDisciplina01.Consultar(sqlPrimeiraDisciplina01);
        //            while (resultadoPrimeiraDisciplinas01.Read())
        //            {
        //                var prinDisc01 = int.Parse(resultadoPrimeiraDisciplinas01["PlcDisciplina"].ToString());
        //                var sqlPrimeiraDisciplina02 =
        //                    "SELECT Top(5) CA_AulasDisciplinasAprendiz.AdiTurma,TurUnidade,TurUnidade, TurNumeroMeses, CA_AulasDisciplinasAprendiz.AdiDataAula, CA_PlanoCurricular.PlcOrdemDisciplina, CA_AulasDisciplinasAprendiz.AdiDisciplina FROM (CA_AulasDisciplinasAprendiz INNER JOIN CA_Turmas ON CA_AulasDisciplinasAprendiz.AdiTurma = CA_Turmas.TurCodigo) INNER JOIN CA_PlanoCurricular ON (CA_AulasDisciplinasAprendiz.AdiDisciplina = CA_PlanoCurricular.PlcDisciplina) AND (CA_Turmas.TurPlanoCurricular = CA_PlanoCurricular.PlcCodigoPlano) WHERE AdiTurma = '" +
        //                    Session["TurCodigo"] + "' and AdiDataAula >= '" + data.Date + "'  and AdiDisciplina =  '" +
        //                    prinDisc01 + "' ORDER BY CA_AulasDisciplinasAprendiz.AdiDataAula";
        //                var conexaoPrimeiroDisciplina02 = new Conexao();
        //                //                      List<string> resultalinhas = new List<string>(); 

        //                var resultadoPrimeiraDisciplinas02 =
        //                    conexaoPrimeiroDisciplina02.Consultar(sqlPrimeiraDisciplina02);
        //                //                       resultalinhas = conexaoPrimeiroDisciplina02.Consultar(sqlPrimeiraDisciplina02);

        //                // Session["TurUnidade"] = resultadoPrimeiraDisciplinas02["TurUnidade"];

        //                //Session["TurUnidade"] = resultadoPrimeiraDisciplinas02["TurUnidade"];

        //                numAulas = 0;
        //                contaulas = 1;
        //                while (resultadoPrimeiraDisciplinas02.Read())
        //                {
        //                    prinDisc = int.Parse(resultadoPrimeiraDisciplinas02["PlcOrdemDisciplina"].ToString());
        //                    AulasGeral = dt.Rows.Count;
        //                    numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //                }
        //                conexaoPrimeiroDisciplina02.Fechar();
        //                contador = prinDisc - 1;
        //                break;
        //            }
        //            conexaoPrimeiroDisciplina01.Fechar();
        //        }

        //        for (int i = 0; i < numAulas && data <= dataFinal; i++) //and data menor que data final...
        //        {
        //            verificaUltima = false;

        //            if (data.DayOfWeek.Equals(DayOfWeek.Saturday)) //sabado
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (VerificaFeriado(data))
        //            {
        //                data = data.AddDays(numeroDias);
        //                verificaUltima = true;
        //                i = i - 1;
        //            }
        //            else
        //            {
        //                using (var repository =
        //                    new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
        //                {
        //                    var ap = new AulasDisciplinasAprendiz();
        //                    ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
        //                    ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
        //                    ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
        //                    ap.AdiEducador = Convert.ToInt32(Session["TurEducador"].ToString()); // codigo educador
        //                    ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString());
        //                    ; //codigo turma
        //                    ap.AdiPresenca = ".";
        //                    ap.AdiDataAula = data;
        //                    if (i == numAulas - 1)
        //                    {
        //                        j = j + 1;
        //                    }
        //                    repository.Add(ap);
        //                }

        //                if (verificaUltima == false)
        //                {
        //                    data = data.AddDays(numeroDias);
        //                }
        //            }
        //        }
        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
        //        //"alert('Cronograma inserido com sucesso.');", true);

        //    }

        //    //          DiscilpinaInicial = contador;

        //    j = 0;
        //    InsereCronogramaOutras(dt, data, prinDisc, dataFinal, numeroDias);


        //    //            inserir aqui o update data data de fim da aloacação
        //    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Data Final do aprendiz atualizada para a turma com sucesso.')", true);
        //}
        //protected void InsereCronogramaOutras(DataTable dt, DateTime data, int DiscilpinaInicial, DateTime dataFinal,
        //   int numeroDias)
        //{
        //    if (VerificaFeriado(data))
        //    {
        //        data = data.AddDays(numeroDias);
        //    }

        //    var k = 0;

        //    for (int contador = 0; contador <= DiscilpinaInicial - 1; contador++)
        //    {
        //        int numAulas = Convert.ToInt32(dt.Rows[contador][4].ToString());
        //        for (int i = 0;
        //            i < numAulas && k <= AulasGeral && data <= dataFinal;
        //            i++) //and data menor que data final...
        //        {
        //            if (data.DayOfWeek.Equals(DayOfWeek.Saturday)) //sabado
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (data.DayOfWeek.Equals(DayOfWeek.Sunday)) //domingo
        //            {
        //                data = data.AddDays(1);
        //            }

        //            if (VerificaFeriado(data.Date))
        //            {
        //                data = data.AddDays(numeroDias);
        //                i = i - 1;
        //            }
        //            else
        //            {
        //                using (var repository =
        //                    new Repository<AulasDisciplinasAprendiz>(new Context<AulasDisciplinasAprendiz>()))
        //                {
        //                    var ap = new AulasDisciplinasAprendiz();
        //                    ap.AdiCargaHoraria = Convert.ToInt32(dt.Rows[contador][2].ToString()); //carga horaria
        //                    ap.AdiCodAprendiz = Convert.ToInt32(Session["CodAprendiz"].ToString()); //codigo aprendiz
        //                    ap.AdiDisciplina = Convert.ToInt32(dt.Rows[contador][1].ToString()); // codigo Disciplina
        //                    ap.AdiEducador = Convert.ToInt32(Session["TurEducador"].ToString());
        //                    //Convert.ToInt32(dt.Rows[contador][7].ToString()); // codigo educador
        //                    ap.AdiTurma = Convert.ToInt32(dt.Rows[contador][0].ToString());
        //                    ; //codigo turma
        //                    ap.AdiPresenca = ".";
        //                    ap.AdiDataAula = data;
        //                    repository.Add(ap);
        //                    if (i == numAulas - 1)
        //                    {
        //                        k = k + 1;
        //                    }
        //                }

        //                data = data.AddDays(numeroDias);
        //            }
        //        }
        //    }

        //    var sql = "Update CA_AlocacaoAprendiz set ALADataPrevTermino = '" + data.AddDays(-numeroDias) +
        //              "'  where ALAAprendiz = '" + Session["CodAprendiz"].ToString() + "' and ALATurma = '" +
        //              Session["TurCodigo"] + "'";
        //    var con = new Conexao();
        //    con.Alterar(sql);
        //}


        protected bool VerificaFeriado(DateTime feriado)
        {

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var datasource = from F in db.CA_Feriados
                                 where F.FerData.Date.Equals(feriado) && (F.FerUnidade == int.Parse(Session["TurUnidade"].ToString()) || F.FerUnidade == 99)
                                 select new { F.FerData };
                if (datasource.Any())
                    return true;
                else
                    return false;
            }

        }


        //protected void btnSim_Click(object sender, EventArgs e)
        //{
        //    gerarEnturmacao();
        //    multiview1.SetActiveView(view1);
        //}

        //protected void btnNão_Click(object sender, EventArgs e)
        //{
        //    multiview1.SetActiveView(view1);
        //}
    }
}