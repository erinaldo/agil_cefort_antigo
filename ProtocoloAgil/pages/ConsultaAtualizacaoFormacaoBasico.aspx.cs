using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{



    public partial class ConsultaAtualizacaoFormacaoBasico : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            var scriptManager = ScriptManager.GetCurrent(Page);
           
            if (!IsPostBack)
            {
                BuscaTurma();

                BindGridView();
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }
            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }


        private void BuscaDisciplina()
        {
            var sql = "Select * from CA_disciplinas order by DisDescricao";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDDisciplina.DataSource = datasource;
            DDDisciplina.DataBind();
        }

        public void BuscaProfessores()
        {


            var sql = "select EducCodigo, EducNome from CA_Educadores";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDProfessor.DataSource = datasource;
            DDProfessor.DataBind();
        }

        public void BuscaTurma()
        {


            var sql = "select TurCodigo, TurNome from CA_Turmas order by TurNome";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            DDTurma.DataSource = datasource;
            DDTurma.DataBind();

            DDTurmaPesquisa.DataSource = datasource;
            DDTurmaPesquisa.DataBind();
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtData.Text.Equals(string.Empty)) throw new ArgumentException("Informe uma data");
                if (DDTurma.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe uma turma");
                if (DDDisciplina.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe uma disciplina");
                if (DDProfessor.SelectedValue.Equals(string.Empty)) throw new ArgumentException("Informe um professor");
                if (txtSequencia.Text.Equals(string.Empty)) throw new ArgumentException("Informe uma sequência");
                

                var sqlinsert = "Insert into CA_AulasDisciplinasTurmaProf values ('"+DDTurma.SelectedValue+"', '"+DDProfessor.SelectedValue+"', '"+DDDisciplina.SelectedValue+"', '"+txtData.Text+"', '"+txtSequencia.Text+"', '', '','','','"+Session["codigoUsuarioLogado"]+"','"+DateTime.Today+"')";
                var sqlupdate = @"update CA_AulasDisciplinasTurmaProf set ADPDisciplina = '"+DDDisciplina.SelectedValue+"', ADPProfessor = '"+DDProfessor.SelectedValue+"' " +
                "where ADPTurma = '" + Session["turma"] + "' and ADPProfessor = '" + Session["professor"] + "' and ADPDisciplina = '" + Session["disciplina"] + "' and ADPDataAula = '" + Session["data"] + "' and ADPOrdemAula = '" + Session["seq"] + "'";

                var con = new Conexao();
                con.Alterar(Session["comando"].Equals("Inserir") ? sqlinsert : sqlupdate);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                           "alert('" + ex.Message + "')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000025", ex);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var row = ((GridView)sender).SelectedRow;

            //var bt = (ImageButton)row.Cells[3].FindControl("IMBexcluir");

            var turma = GridView1.DataKeys[row.RowIndex]["ADPTurma"].ToString();
            var professor = GridView1.DataKeys[row.RowIndex]["ADPprofessor"].ToString();
            var disciplina = GridView1.DataKeys[row.RowIndex]["ADPDisciplina"].ToString();
            var data = GridView1.DataKeys[row.RowIndex]["ADPDataAula"].ToString();
            var seq = GridView1.DataKeys[row.RowIndex]["ADPOrdemAula"].ToString();


            Session["turma"] = turma;
            Session["professor"] = professor;
            Session["disciplina"] = disciplina;
            Session["data"] = data;
            Session["seq"] = seq;

            //Session["AlrteraUnidade"] = bt.CommandArgument;
            Session["comando"] = "Alterar";
            PreencheCampos(turma, professor, disciplina, data, seq);
            BuscaTurma();
            BuscaProfessores();
            BuscaDisciplina();
            MultiView1.ActiveViewIndex = 1;
        }

        private void PreencheCampos(string turma, string professor, string disciplina, string data, string seq)
        {
            LimpaCampos();

            var where = "";


            var sql = @"Select A.ADPOrdemAula, A.ADPDataAula, A.ADPProfessor, ADPConteudoLecionado, A.ADPTurma, A.ADPDisciplina, T.TurNome, D.DisDescricao, E.EducNome from CA_AulasDisciplinasTurmaProf A join CA_Turmas T on A.ADPTurma = T.TurCodigo join CA_Disciplinas D on A.ADPDisciplina = D.DisCodigo join CA_Educadores E on A.ADPprofessor = E.EducCodigo where 1 = 1 
            and ADPTurma = '" +turma+"' and ADPProfessor = '"+professor+"' and ADPDisciplina = '"+disciplina+"' and ADPDataAula = '"+data+"' and ADPOrdemAula = '"+seq+"' ";
            var con = new Conexao();
            var result = con.Consultar(sql);

            while (result.Read())
            {
                txtData.Text = string.Format("{0:dd/MM/yyyy}", (DateTime)result["ADPDataAula"]);
                DDTurma.SelectedValue = result["ADPTurma"].ToString();
                DDDisciplina.SelectedValue = result["ADPDisciplina"].ToString();
                DDProfessor.SelectedValue = result["ADPProfessor"].ToString();
                txtSequencia.Text = result["ADPOrdemAula"].ToString();

                txtData.Enabled = false;
                DDTurma.Enabled = false;
                txtSequencia.Enabled = false;
            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btn_novo_Click(object sender, EventArgs e)
        {
            BuscaDisciplina();
            BuscaProfessores();
            BuscaTurma();
            LimpaCampos();
            Session["comando"] = "Inserir";
            MultiView1.ActiveViewIndex = 1;
        }

        private void LimpaCampos()
        {
            txtData.Text = string.Empty;
            DDTurma.SelectedValue = string.Empty;
            DDProfessor.SelectedValue = string.Empty;
            DDDisciplina.SelectedValue = string.Empty;
            txtSequencia.Text =  string.Empty;
        }


      
        protected void btn_listar_Click(object sender, EventArgs e)
        {
            BindGridView();
            MultiView1.ActiveViewIndex = 0;
        }


        public void BindGridView() {

            var where = "";

            if (!DDTurmaPesquisa.SelectedValue.Equals(string.Empty))
            {

                where += " and ADPTurma = '" + DDTurmaPesquisa.SelectedValue + "'";
            }

            if (!txtDataInicio.Text.Equals(string.Empty))
            {
                where += " and ADPDataAula >= '" + txtDataInicio.Text + "'";
            }


            if (!txtDataInicio.Text.Equals(string.Empty))
            {
                where += " and ADPDataAula <= '" + txtDataTermino.Text + "'";
            }


            var sql = "Select A.ADPOrdemAula, A.ADPDataAula, ADPConteudoLecionado, A.ADPTurma, A.ADPDisciplina, A.ADPprofessor, T.TurNome, D.DisDescricao, E.EducNome from CA_AulasDisciplinasTurmaProf A join CA_Turmas T on A.ADPTurma = T.TurCodigo join CA_Disciplinas D on A.ADPDisciplina = D.DisCodigo join CA_Educadores E on A.ADPprofessor = E.EducCodigo where 1 = 1 ";

            sql += where + " order by ADPDataAula ";

            SqlDataSource datasource = new SqlDataSource { ID = "SDSParceiroUnidade", SelectCommand = sql, ConnectionString = GetConfig.Config() };

            GridView1.DataSource = datasource;
            GridView1.DataBind();
        }

        


        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var row = (GridViewRow)button.Parent.Parent;
            using (var repository = new Repository<Feriados>(new Context<Feriados>()))
            {
                var feriado = repository.All().Where(p => p.FerData == DateTime.Parse(row.Cells[0].Text)
                    && p.FerOrdem == int.Parse(button.CommandArgument)).First();
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(feriado);
            }
            BindGridView();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Session["TurmaPesquisa"] = " ";
            Session["dataInicio"] = " ";
            Session["dataTermino"] = " ";
            Session["turmaImpressao"] = " ";

            if (!DDTurmaPesquisa.SelectedValue.Equals(string.Empty))
            {

                Session["TurmaPesquisa"] = DDTurmaPesquisa.SelectedValue;
                Session["turmaImpressao"] = DDTurmaPesquisa.SelectedItem;

            }

            if (!txtDataInicio.Text.Equals(string.Empty))
            {
                Session["dataInicio"] =  txtDataInicio.Text;
            }


            if (!txtDataInicio.Text.Equals(string.Empty))
            {
               Session["dataTermino"] = txtDataTermino.Text ;
            }


            Session["id"] = 119;
            MultiView1.ActiveViewIndex = 2;
        }
    }
}