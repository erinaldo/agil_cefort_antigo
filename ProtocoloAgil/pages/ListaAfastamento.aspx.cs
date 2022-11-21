using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloAgil.Base;
using MenorAprendizWeb.Base;
using System.Data.Linq.SqlClient;

namespace ProtocoloAgil.pages
{
    public partial class ListaAfastamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CurrentPage"] = "aprendiz";
            if (!IsPostBack)
            {
                multiview.ActiveViewIndex = 0;
                carregaParceiro();
                CarregarMotivoAfastamento();
            }
        }

        public void carregaParceiro()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from i in db.CA_Parceiros
                            select new
                            {
                                i.ParDescricao,
                                i.ParCodigo
                            }).OrderBy(i => i.ParDescricao);

                DDParceiro.DataSource = query;
                DDParceiro.DataBind();
            }
        }

        public void CarregarMotivoAfastamento()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = from i in db.CA_MotivosdeAfastamento
                            select new
                            {
                                i.Maf_Codigo,
                                i.Maf_Descricao
                            };

                ddTipoAfastamento.DataSource = query;
                ddTipoAfastamento.DataBind();

            }
        }

        protected void IndiceZero(object sender, EventArgs e)
        {
            var indice0 = new ListItem("Selecione", "");
            var objDropDownList = (DropDownList)sender; //Cast no sender para DropDownList
            objDropDownList.Items.Insert(0, indice0); //Adiciona um novo Item
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            carregarGrid();
        }

        public void carregarGrid()
        {
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = from i in db.View_CA_ListaAfastamento
                            select i;

                if (ddStatus.SelectedValue.Equals("A"))
                    query = query.Where(i => i.Afa_DataInicio <= DateTime.Today && i.Afa_DataTermino >= DateTime.Today);

                if (txtNome.Text != string.Empty)
                    query = query.Where(i => SqlMethods.Like(i.Apr_Nome, $"%{txtNome.Text}%"));


                if (DDMes.SelectedValue != string.Empty)
                {
                    if (ddTipoData.SelectedValue.Equals("I"))
                    {
                        query = query.Where(i => int.Parse(DDMes.SelectedValue) == i.Afa_DataInicio.Month);
                    }
                    else
                    {
                        query = query.Where(i => int.Parse(DDMes.SelectedValue) == i.Afa_DataTermino.Month);
                    }
                }

                if (txtAno.Text != string.Empty)
                {
                    if (ddTipoData.SelectedValue.Equals("I"))
                    {
                        query = query.Where(i => int.Parse(txtAno.Text) == i.Afa_DataInicio.Year);
                    }
                    else
                    {
                        query = query.Where(i => int.Parse(txtAno.Text) == i.Afa_DataTermino.Year);
                    }
                }

                if (ddTipoAfastamento.SelectedValue != string.Empty)
                {
                    query = query.Where(i => ddTipoAfastamento.SelectedValue.Equals(i.Afa_Motivo));
                }

                if (DDParceiro.SelectedValue != string.Empty)
                {
                    query = query.Where(i => int.Parse(DDParceiro.SelectedValue) == i.ParCodigo);
                }

                if (DDUnidadeParceiro.SelectedValue != string.Empty)
                {
                    query = query.Where(i => int.Parse(DDUnidadeParceiro.SelectedValue) == i.ParUniCodigo);
                }

                gridAfastamento.DataSource = query;
                gridAfastamento.DataBind();
            }
        }



        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            multiview.ActiveViewIndex = 0;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Session["id"] = "107";
            Session["where"] = ddStatus.SelectedValue.Equals("A") ? " and Afa_DataInicio <= GetDate() and Afa_DataTermino >= GetDate() " : "";
            multiview.SetActiveView(View02);
        }

        protected void gridAfastamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAfastamento.PageIndex = e.NewPageIndex;
            carregarGrid();
        }

        protected void DDParceiro_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDParceiro.SelectedValue.Equals(string.Empty)) return;

            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var query = (from i in db.CA_ParceirosUnidades
                            where i.ParUniCodigoParceiro == int.Parse(DDParceiro.SelectedValue)
                            select new
                            {
                                i.ParUniDescricao,
                                i.ParUniCodigo
                            }).OrderBy(i => i.ParUniDescricao);

                DDUnidadeParceiro.DataSource = query;
                DDUnidadeParceiro.DataBind();
            }
        }
    }
}