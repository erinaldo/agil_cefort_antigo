using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using ProtocoloAgil.Base.Models;
using MenorAprendizWeb.Base;

namespace ProtocoloAgil.pages
{
    public partial class CadastroMotivosAfastamento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "configuracoes";
            Page.Form.DefaultButton = btnpesquisa.UniqueID;
            if (!IsPostBack)
            {
                BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
                Session["tipoacesso"] = Criptografia.Decrypt(Request.QueryString["acs"], GetConfig.Key());
                MultiView1.ActiveViewIndex = 0;
            }

            if (Session["tipoacesso"] != null && Session["tipoacesso"].ToString().Equals("S"))
            {
                btn_novo.Enabled = false;
                BTaltera.Enabled = false;
            }
        }

        private void BindGridView(int type)
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {

                var query = (from i in db.CA_MotivosdeAfastamento
                             select i).OrderBy(i => i.Maf_Descricao);


                if (type == 2)
                {
                    query = query.Where(p => p.Maf_Descricao.ToLower().Contains(pesquisa.Text.Trim().ToLower())).OrderBy(p => p.Maf_Descricao);
                }


                HFRowCount.Value = query.Count().ToString();
                GridView1.DataSource = query;
                GridView1.DataBind();
            }
        }


        protected void btnpesquisa_Click(object sender, EventArgs e)
        {
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }



        protected void BTaltera_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["comando"].ToString() != "Alterar")
                {
                    using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
                    {
                        if (db.CA_MotivosdeAfastamento.Where(item => item.Maf_Codigo == TBcodigo.Text).Count() > 0) throw new ArgumentException("Código ja cadastrado.");
                    }

                }
                var cn = new Conexao();
                var sql = GeraSql();
                cn.Alterar(sql);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Ação Realizada com Sucesso.')", true);
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    $"alert('{ex.Message}')", true);
            }
            catch (SqlException ex)
            {
                Funcoes.TrataExcessao("000020", ex);
            }
        }

        private string GeraSql()
        {
            try
            {

                if (TBNome.Text.Equals(string.Empty)) throw new ArgumentException("Digite o local de formação.");

                string sqlupdate = "UPDATE CA_MotivosdeAfastamento SET Maf_Descricao = '" + TBNome.Text +
                                   "', Maf_Presenca = '" + ddAfastamento.SelectedValue + "' WHERE  Maf_Codigo = '" + Session["AlrteraCodigo_modelo"] + "' ";
                var sqlinsert = "INSERT INTO CA_MotivosdeAfastamento(Maf_Codigo, Maf_Descricao,Maf_Presenca  ) VALUES( '" +
                                TBcodigo.Text + "', '" + TBNome.Text + "', '" + ddAfastamento.SelectedValue + "' ) ";

                return Session["comando"].Equals("Alterar") ? sqlupdate : sqlinsert;
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    $"alert('{ex.Message}')", true);
            }
            catch (Exception e)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('Código já existe');", true);
                return "";
            }
            return "";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var bd = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var row = ((GridView)sender).SelectedRow;
                var dados = from i in bd.CA_MotivosdeAfastamento
                            where i.Maf_Codigo.Equals(WebUtility.HtmlDecode(row.Cells[0].Text))
                            select new { i.Maf_Codigo, i.Maf_Descricao, i.Maf_Presenca };
                if (dados.Count() > 0)
                {
                    var parentesco = dados.First();
                    TBcodigo.Text = parentesco.Maf_Codigo.ToString();
                    Session["AlrteraCodigo_modelo"] = parentesco.Maf_Codigo.ToString();

                    TBNome.Text = parentesco.Maf_Descricao;
                    ddAfastamento.SelectedValue = parentesco.Maf_Presenca;
                    Session["comando"] = "Alterar";
                    MultiView1.ActiveViewIndex = 1;

                    TBcodigo.Enabled = false;
                }
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
            LimpaCampos();
            //TBcodigo.Enabled = false;
            Session["comando"] = "Novo";
            voltar.Visible = false;
            MultiView1.ActiveViewIndex = 1;
            TBcodigo.Enabled = true;
        }

        private void LimpaCampos()
        {
            TBcodigo.Text = string.Empty;
            TBNome.Text = string.Empty;
        }

        protected void btn_listar_Click(object sender, EventArgs e)
        {
            voltar.Visible = false;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
            MultiView1.ActiveViewIndex = 0;
        }


        protected void btn_relatorio_Click(object sender, EventArgs e)
        {
            Session["id"] = 106;
            MultiView1.ActiveViewIndex = 2;
            voltar.Visible = true;
        }


        protected void IMBexcluir_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            var escola = button.CommandArgument.ToString();
            using (var repository = new Repository<CA_MotivosdeAfastamento>(new Context<CA_MotivosdeAfastamento>()))
            {
                if (Convert.ToBoolean(HFConfirma.Value))
                    repository.Remove(escola);
            }

            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView(pesquisa.Text.Equals(string.Empty) ? 1 : 2);
        }
    }
}