using MenorAprendizWeb.Base;
using ProtocoloAgil.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloAgil.pages.aprendiz.cadastro
{
    public partial class CalendarioAprendizPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int codAprendiz = int.Parse(Session["codAprendiz"].ToString());
            using (var db = new DC_ProtocoloAgilDataContext(GetConfig.Config()))
            {
                var user = (from i in db.CA_Aprendiz
                            join l in db.CA_ParceirosUnidades on i.Apr_UnidadeParceiro equals l.ParUniCodigo
                            where i.Apr_Codigo == codAprendiz
                            select new
                            {
                                i.Apr_Nome,
                                l.ParUniDescricao
                            }).Single();
                Session["Print_Aprendiz_Nome"] = user.Apr_Nome;
                Session["Print_Aprendiz_Parceiro"] = user.ParUniDescricao;
            }
            }
    }
}