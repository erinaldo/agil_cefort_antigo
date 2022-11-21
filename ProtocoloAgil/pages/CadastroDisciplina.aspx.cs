﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;


namespace ProtocoloAgil.pages
{
    public partial class CadastroDisciplina : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "pedagogico";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            var scriptManager = ScriptManager.GetCurrent(Page);
            if (scriptManager != null) scriptManager.RegisterPostBackControl(texto);
            if ( Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                Novo.Enabled = false;
                BTinsert.Enabled = false;
            }

            if (IsPostBack) return;
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
            MultiView1.ActiveViewIndex = 0;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBtituloAlt.Text = "Alteração de Curso";
            BTinsert.Text = "Alterar";
            Session["comando"] = "Alterar";
            Session["Alteracodigo"] = GridView1.SelectedRow.Cells[0].Text;
            TBCodigo_curso.Visible = true;
            PreencheCampos();
            MultiView1.ActiveViewIndex = 1;
        }

        private void BindGridView(int tipo)
        {
            using (var repository = new Repository<Disciplina>(new Context<Disciplina>()))
            {
                var datasource = new List<Disciplina>();
                switch (tipo)
                {
                    case 1: datasource.AddRange(repository.All().OrderBy(p => p.DisDescricao)); break;
                    case 2: datasource.AddRange(repository.All().Where(p => p.DisDescricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.DisDescricao)); break;
                }
                GridView1.DataSource = datasource;
                HFRowCount.Value = datasource.Count.ToString();
                GridView1.DataBind();
            }
        }

        private void PreencheCampos()
        {
            var bd = new  DC_ProtocoloAgilDataContext(GetConfig.Config());
            var curso = (from i in bd.CA_Disciplinas where i.DisCodigo.Equals(Session["Alteracodigo"]) select i).First();
            TBCodigo_curso.Text = curso.DisCodigo.ToString();
            TBNome.Text = curso.DisDescricao;
            TB_Abreviatura.Text = curso.DisAbreviatura;
            TxtCor.Text = curso.DisCor;

        }

        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Session["comando"].Equals("Inserir") && TBCodigo_curso.Text.Equals(string.Empty)) throw new ArgumentException("Informe o código da disciplina.");
                if (TB_Abreviatura.Text.Equals(string.Empty)) throw new ArgumentException("Informe a abreviatura da disciplina.");
                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o nome da disciplina.");

                using (var repository = new Repository<Disciplina>(new Context<Disciplina>()))
                {
                    var disciplina = (Session["comando"].Equals("Inserir")) ? new Disciplina() : repository.Find(Convert.ToInt16(Session["Alteracodigo"].ToString()));
                    disciplina.DisCodigo = (short) ((Session["comando"].Equals("Inserir")) ? 0 : Convert.ToInt16(TBCodigo_curso.Text));
                    disciplina.DisDescricao = TBNome.Text;
                    disciplina.DisAbreviatura = TB_Abreviatura.Text;
                    disciplina.DisCor = TxtCor.Text.Replace("#","");
                    if (Session["comando"].Equals("Inserir")) repository.Add(disciplina);
                    else repository.Edit(disciplina);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                             "alert('Ação realizada com sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                            "alert('"+ ex.Message+"')", true);
                return;
            }
            catch (Exception ex)
            {
                Funcoes.TrataExcessao("000115", ex);
            }
        }

        protected void BTlimpar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void LimpaCampos()
        {
            TBCodigo_curso.Text = string.Empty;
            TBNome.Text = string.Empty;
            TB_Abreviatura.Text = string.Empty;
            TxtCor.Text = string.Empty;
        }

        protected void listar_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Novo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
            LBtituloAlt.Text = "Cadastro de Disciplinas";
            BTinsert.Text = "Salvar";
            Session["comando"] = "Inserir";
            LimpaCampos();
            TBCodigo_curso.Visible = false;
            MultiView1.ActiveViewIndex = 1;
        }

        protected void relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = "5";
            MultiView1.ActiveViewIndex = 2;
        }

        protected void texto_Click(object sender, EventArgs e)
        {
            var filePath = Server.MapPath("/files");
            // Deleta o arquivo existente e cria outro.
            File.Delete(filePath + @"/temp.txt");
            var write = new FileManager(filePath + @"/temp.txt");
            try
            {
                using (var repository = new Repository<Disciplina>(new Context<Disciplina>()))
                {
                    var dados = repository.All();
                    foreach (var item in dados)
                    {
                        var linha = item.DisCodigo + "; " + item.DisDescricao + "; " + item.DisAbreviatura;
                        write.Escreve(linha);
                    }
                    string fileName = filePath + @"/temp.txt";
                    Funcoes.Download(fileName, "Lista de Disciplinas.txt");
                }
            }
            catch (IOException ex)
            {
                Funcoes.TrataExcessao("000116", ex);
            }
        }

        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
           BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            Funcoes.SetFooterRow((GridView)sender, HFRowCount.Value);
        }

        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var curso = Convert.ToInt16(button.CommandArgument);
            var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config());
            if(bd.CA_DisciplinasTurmaProfs.Where(p => p.DPDisciplina == curso).Count() > 0 )
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                                         "alert('ERRO - A disciplina está associada à uma aula. impossível excluir.')", true);
                return;
            }


            using (var repository = new Repository<Disciplina>(new Context<Disciplina>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(curso);
            }
            BindGridView(pesquisa.Text.Equals(string.Empty)? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

        }
    }
}