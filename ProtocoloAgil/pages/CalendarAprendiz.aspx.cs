using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace ProtocoloAgil.pages
{
    public partial class CalendarAprendiz : System.Web.UI.Page
    {
        string codAprendiz;
        protected void Page_Load(object sender, EventArgs e)
        {
             codAprendiz = Request.QueryString["id"];
            hfDias.Value = codAprendiz;
            visualizarCalendario(Convert.ToInt32(codAprendiz));

        }

        public void carregarDias()
        {
            var sql = "select DATEDIFF(day, Apr_InicioAprendizagem , "+
            "Apr_PrevFimAprendizagem) as dias from Ca_Aprendiz "+
            "where Apr_Codigo =" + codAprendiz;

            var conn = new Conexao();
            var result = conn.Consultar(sql);

            while(result.Read()){
                hfDias.Value = result["dias"].ToString();
            }
        }
        public void visualizarCalendario(int codAprendiz)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados = (from i in bd.CA_AulasDisciplinasAprendiz
                             where i.AdiCodAprendiz == codAprendiz
                             select new

                             {
                                 i.AdiDataAula
                             }).OrderBy(x => x.AdiDataAula);

                // criar array para guardar os dados do mes e ano

                int[] mes = new int[dados.Count()];
                int cont = 0;
                int[] ano = new int[dados.Count()];


                foreach (var linha in dados)
                {
                    // verifica se a o mes é igual para não aprecer dois calendario com o mesmo mes 
                    if (cont == 0 || Convert.ToInt32(linha.AdiDataAula.Month) != mes[cont - 1])
                    {
                        ano[cont] = Convert.ToInt32(linha.AdiDataAula.ToString().Substring(6, 4));
                        mes[cont] = Convert.ToInt32(linha.AdiDataAula.ToString().Substring(3, 2));

                        cont++;
                    }
                }

                // vai adicionando o mes inicial nos calendarios e colocando visivel
                try
                {
                    Calendar1.VisibleDate = new DateTime(ano[0], mes[0],1);
                    Calendar1.Visible = true;
                    Calendar2.VisibleDate = new DateTime(ano[1], mes[1],1);
                    Calendar2.Visible = true;
                    Calendar3.VisibleDate = new DateTime(ano[2], mes[2],1);
                    Calendar3.Visible = true;
                    Calendar4.VisibleDate = new DateTime(ano[3], mes[3],1);
                    Calendar4.Visible = true;
                    Calendar5.VisibleDate = new DateTime(ano[4], mes[4],1);
                    Calendar5.Visible = true;
                    Calendar6.VisibleDate = new DateTime(ano[5], mes[5],1);
                    Calendar6.Visible = true;
                    Calendar7.VisibleDate = new DateTime(ano[6], mes[6],1);
                    Calendar7.Visible = true;
                    Calendar8.VisibleDate = new DateTime(ano[7], mes[7],1);
                    Calendar8.Visible = true;
                    Calendar9.VisibleDate = new DateTime(ano[8], mes[8],1);
                    Calendar9.Visible = true;
                    Calendar10.VisibleDate = new DateTime(ano[9], mes[9],1);
                    Calendar10.Visible = true;
                    Calendar11.VisibleDate = new DateTime(ano[10], mes[10],1);
                    Calendar11.Visible = true;
                    Calendar12.VisibleDate = new DateTime(ano[11], mes[11],1);
                    Calendar12.Visible = true;
                    Calendar13.VisibleDate = new DateTime(ano[12], mes[12],1);
                    Calendar13.Visible = true;
                    Calendar14.VisibleDate = new DateTime(ano[13], mes[13],1);
                    Calendar14.Visible = true;
                    Calendar15.VisibleDate = new DateTime(ano[14], mes[14],1);
                    Calendar15.Visible = true;
                    Calendar16.VisibleDate = new DateTime(ano[15], mes[15],1);
                    Calendar16.Visible = true;
                    Calendar17.VisibleDate = new DateTime(ano[16], mes[16],1);
                    Calendar17.Visible = true;
                    Calendar18.VisibleDate = new DateTime(ano[17], mes[17],1);
                    Calendar18.Visible = true;
                    Calendar19.VisibleDate = new DateTime(ano[18], mes[18],1);
                    Calendar19.Visible = true;
                    Calendar20.VisibleDate = new DateTime(ano[19], mes[19],1);
                    Calendar20.Visible = true;


                }
                catch (Exception ex)
                {
                   // Alert.Show(ex.Message);
                }
                finally
                {

                    carregarCalendario(mes, codAprendiz);
                }
            }
        }
        public void carregarCalendario(int[] mesVisivel, int codAprendiz)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var dados = (from i in bd.CA_Aprendiz
                             join j in bd.CA_AulasDisciplinasAprendiz on i.Apr_Codigo equals j.AdiCodAprendiz
                             join k in bd.CA_Turmas on i.Apr_Turma equals k.TurCodigo
                             join l in bd.CA_ParceirosUnidades on i.Apr_UnidadeParceiro equals l.ParUniCodigo

                             where j.AdiCodAprendiz == codAprendiz
                             select new

                             {
                                 j.AdiDataAula,
                                 j.AdiCodAprendiz,
                                 i.Apr_Nome,
                                 i.Apr_InicioAprendizagem,
                                 i.Apr_PrevFimAprendizagem,
                                 k.TurNome,
                                 k.TurObservacoes,
                                 l.ParUniCNPJ,
                                 l.ParUniDescricao
                             }).OrderBy(x => x.AdiDataAula);

                var aprendiz = dados.First();

                carregarFeriado(Convert.ToDateTime(aprendiz.Apr_InicioAprendizagem), Convert.ToDateTime(aprendiz.Apr_PrevFimAprendizagem));

                SelectedDatesCollection diasEstudado1 = Calendar1.SelectedDates;
                SelectedDatesCollection diasEstudado2 = Calendar2.SelectedDates;
                SelectedDatesCollection diasEstudado3 = Calendar3.SelectedDates;
                SelectedDatesCollection diasEstudado4 = Calendar4.SelectedDates;
                SelectedDatesCollection diasEstudado5 = Calendar5.SelectedDates;
                SelectedDatesCollection diasEstudado6 = Calendar6.SelectedDates;
                SelectedDatesCollection diasEstudado7 = Calendar7.SelectedDates;
                SelectedDatesCollection diasEstudado8 = Calendar8.SelectedDates;
                SelectedDatesCollection diasEstudado9 = Calendar9.SelectedDates;
                SelectedDatesCollection diasEstudado10 = Calendar10.SelectedDates;
                SelectedDatesCollection diasEstudado11 = Calendar11.SelectedDates;
                SelectedDatesCollection diasEstudado12 = Calendar12.SelectedDates;
                SelectedDatesCollection diasEstudado13 = Calendar13.SelectedDates;
                SelectedDatesCollection diasEstudado14 = Calendar14.SelectedDates;
                SelectedDatesCollection diasEstudado15 = Calendar15.SelectedDates;
                SelectedDatesCollection diasEstudado16 = Calendar16.SelectedDates;
                SelectedDatesCollection diasEstudado17 = Calendar17.SelectedDates;
                SelectedDatesCollection diasEstudado18 = Calendar18.SelectedDates;
                SelectedDatesCollection diasEstudado19 = Calendar19.SelectedDates;
                SelectedDatesCollection diasEstudado20 = Calendar20.SelectedDates;
                diasEstudado1.Clear();

                int dia = 0;
                int ano = 0;
                int mes = 0;

                foreach (var linha in dados)
                {
                    // faz a separação de sem dia e ano 
                    dia = Convert.ToInt32(linha.AdiDataAula.ToString().Substring(0, 2));
                    mes = Convert.ToInt32(linha.AdiDataAula.ToString().Substring(3, 2));
                    ano = Convert.ToInt32(linha.AdiDataAula.ToString().Substring(6, 4));

                    try
                    {

                        // verifica se o mes adicionado é o mesmo que o mes do calendario
                        if (mesVisivel[0] == mes)
                        {
                            diasEstudado1.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[1] == mes)
                        {
                            diasEstudado2.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[2] == mes)
                        {
                            diasEstudado3.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[3] == mes)
                        {
                            diasEstudado4.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[4] == mes)
                        {
                            diasEstudado5.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[5] == mes)
                        {
                            diasEstudado6.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[6] == mes)
                        {
                            diasEstudado7.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[7] == mes)
                        {
                            diasEstudado8.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[8] == mes)
                        {
                            diasEstudado9.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[9] == mes)
                        {
                            diasEstudado10.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[10] == mes)
                        {
                            diasEstudado11.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[11] == mes)
                        {
                            diasEstudado12.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[12] == mes)
                        {
                            diasEstudado13.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[13] == mes)
                        {
                            diasEstudado14.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[14] == mes)
                        {
                            diasEstudado15.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[15] == mes)
                        {
                            diasEstudado16.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[16] == mes)
                        {
                            diasEstudado17.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[17] == mes)
                        {
                            diasEstudado18.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[18] == mes)
                        {
                            diasEstudado19.Add(new DateTime(ano, mes, dia));
                        }
                        if (mesVisivel[19] == mes)
                        {
                            diasEstudado20.Add(new DateTime(ano, mes, dia));
                        }


                    }
                    catch
                    {

                    }
                    finally
                    {
                        lblEmpresa.Text = "CALENDÁRIO";
                        lblNome.Text = "Aprendiz: " + aprendiz.Apr_Nome;
                        lblTurma.Text = "Turma: " + aprendiz.TurNome;
                        lblQuantidade.Text = "QTDE Aula: " + dados.Count().ToString();
                        lblInicio.Text = " Periodo de Aprendizagem de: " + Calendar1.SelectedDate.ToShortDateString();
                        lblTermino.Text = "a " + new DateTime(ano, mes, dia).ToShortDateString();
                        lblParceiro.Text = "Parceiro: " + aprendiz.ParUniDescricao + " CNPJ: " + aprendiz.ParUniCNPJ;


                       lblEmpresa2.Text = "CALENDÁRIO";
                       lblNome2.Text = "Aprendiz: " + aprendiz.Apr_Nome;
                       lblTurma2.Text = "Turma: " + aprendiz.TurNome;
                       lblQuantidade2.Text = "QTDE Aula: " + dados.Count().ToString();
                       lblInicio2.Text = " Periodo de Aprendizagem de: " + Calendar1.SelectedDate.ToShortDateString();
                       lblTermino2.Text = "a " + new DateTime(ano, mes, dia).ToShortDateString();
                       lblParceiro2.Text = "Parceiro: " + aprendiz.ParUniDescricao + " CNPJ: " + aprendiz.ParUniCNPJ;

                    }

                }
            }
        }

        public void carregarFeriado(DateTime inicio, DateTime termino)
        {
            string texto = " As datas a seguir são feriados ou recessos : ";


            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var dados2 = (from j in bd.CA_Feriados
                              where j.FerData >= inicio && j.FerData <= termino
                              select new
                              {
                                  j.FerData

                              }).OrderBy(x => x.FerData);
                if (dados2.Count() > 0)
                {
                    var dataFeriado = dados2.First();
                    var contador = 0;
                    foreach (var linha in dados2)
                    {
                        if (contador == 0)
                        {
                            texto += string.Format("{0:dd/MM/yyyy}", linha.FerData);
                        }
                        else if (contador < dados2.Count() - 1)
                        {
                            texto += ", " + string.Format("{0: dd/MM/yyyy}", linha.FerData);
                        }
                        else
                        {
                            texto += " e " + string.Format("{0: dd/MM/yyyy}", linha.FerData + ".");

                        }
                        contador++;
                            
                   }
                    Label2.Text = texto;
                }
            }
            
        }
    }
}