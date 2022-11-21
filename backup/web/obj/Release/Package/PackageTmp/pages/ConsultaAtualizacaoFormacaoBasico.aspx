<%@ Page Title="Mestre Agil WEB - Soluções Acadêmicas e Financeiras" Language="C#" AutoEventWireup="true" MasterPageFile="~/MPusers.Master" Inherits="ProtocoloAgil.pages.ConsultaAtualizacaoFormacaoBasico"
    CodeBehind="ConsultaAtualizacaoFormacaoBasico.aspx.cs" %>

<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc1" %>
<%@ Register Assembly="CalendarExtenderPlus" Namespace="AjaxControlToolkitPlus" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = document.getElementById("<%# HFConfirma.ClientID %>");
            if (confirm("Deseja realmente excluir este recesso/feriado?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass=" Table centralizar">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Pedagógico > <span>Consulta e Atualização Aulas</span>
            </p>
        </div>
        <div class="controls">

            <div style="float: left;">
                <asp:Button ID="btn_listar" runat="server" CssClass="btn_controls" Text="Listar" OnClick="btn_listar_Click" />
                <asp:Button ID="btn_novo" runat="server" CssClass="btn_controls" Text="Novo" OnClick="btn_novo_Click" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btn_controls" Text="Imprimir" OnClick="btnImprimir_Click" />

            </div>
        </div>
    </asp:Panel>
    <div class="formatoTela_02">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">

                <table>
                    <tr>
                        <td class="fonteTab">Turma</td>
                        <td class="fonteTab">Data Início</td>
                        <td class="fonteTab">Data Término</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList OnDataBound="IndiceZero" runat="server" Width="150px" ID="DDTurmaPesquisa" DataValueField="TurCodigo" DataTextField="TurNome" CssClass="fonteTexto"></asp:DropDownList>
                        </td>
                        <td class="fonteTab">
                            <asp:TextBox ID="txtDataInicio" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10"
                                onkeyup="formataData(this,event);" Width="95%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus1" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataInicio">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td class="fonteTab">
                            <asp:TextBox ID="txtDataTermino" runat="server" CssClass="fonteTexto" Height="18px"
                                MaxLength="10"
                                onkeyup="formataData(this,event);" Width="95%"></asp:TextBox>
                            <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus3" runat="server"
                                Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtDataTermino">
                            </cc2:CalendarExtenderPlus>
                        </td>
                        <td>
                            <asp:Button ID="btnPesquisar" runat="server" CssClass="btn_controls" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="list_results"
                            AutoGenerateColumns="False" OnDataBound="GridView_DataBound"
                            Style="width: 80%; margin: 0 auto" HorizontalAlign="Center" DataKeyNames="ADPTurma, ADPprofessor, ADPDisciplina, ADPDataAula, ADPOrdemAula"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ADPDataAula" HeaderText="Data"
                                    InsertVisible="False" ReadOnly="True" SortExpression="Data"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DisDescricao" HeaderText="Disciplina"
                                    SortExpression="Nome">
                                    <HeaderStyle Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EducNome" HeaderText="Professor"
                                    SortExpression="Unidade">
                                    <HeaderStyle Width="40%" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ADPOrdemAula" HeaderText="Seq"
                                    SortExpression="Unidade">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ADPConteudoLecionado" HeaderText="Conteúdo"
                                    SortExpression="Unidade">
                                    <HeaderStyle Width="40%" />
                                </asp:BoundField>

                                <asp:CommandField ButtonType="Image" HeaderText="Alterar"
                                    SelectImageUrl="~/Images/icon_edit.png" ShowSelectButton="True">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField Visible="false" HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IMBexcluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ADPOrdemAula")%>'
                                            OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="List_results" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle Height="25px" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" BackColor="#999999" />
                            <FooterStyle Width="30px" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Height="20px" BorderStyle="Groove" BorderWidth="1px" />
                            <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                                PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                            <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:Panel ID="Panel4" runat="server" CssClass="centralizar" Height="400px" Width="700px">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="titulo cortitulo corfonte" style="font-size: large;">
                                Cadastro de Aulas de Formação
                            </div>
                            <br />
                            <table class="Table FundoPainel">

                                <tr>
                                    <td class="fonteTab" style="width: 25px">Data</td>
                                    <td class="fonteTab" style="width: 155px">Disciplina</td>
                                    <td class="fonteTab" style="width: 155px">Turma</td>
                                </tr>
                                <tr>
                                    <td class="fonteTab">
                                        <asp:TextBox ID="txtData" runat="server" CssClass="fonteTexto" Height="18px"
                                            MaxLength="10"
                                            onkeyup="formataData(this,event);" Width="65%"></asp:TextBox>
                                        <cc2:CalendarExtenderPlus ID="CalendarExtenderPlus2" runat="server"
                                            Format="dd/MM/yyyy" PopupPosition="BottomRight" TargetControlID="txtData">
                                        </cc2:CalendarExtenderPlus>
                                    </td>
                                    <td class="fonteTab">
                                        <asp:DropDownList runat="server" Width="95%" ID="DDDisciplina" OnDataBound="IndiceZero" CssClass="fonteTexto" DataTextField="DisDescricao" DataValueField="DisCodigo"></asp:DropDownList>
                                    </td>
                                    <td class="fonteTab">
                                        <asp:DropDownList runat="server" ID="DDTurma" Width="95%" OnDataBound="IndiceZero" DataValueField="TurCodigo" DataTextField="TurNome" CssClass="fonteTexto"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fonteTab">Professor</td>
                                    <td class="fonteTab">Sequência</td>
                                </tr>
                                <td class="fonteTab">
                                    <asp:DropDownList Width="160px" runat="server" OnDataBound="IndiceZero" ID="DDProfessor" CssClass="fonteTexto" DataValueField="EducCodigo" DataTextField="EducNome"></asp:DropDownList>
                                </td>
                                <td class="fonteTab">
                                    <asp:TextBox ID="txtSequencia" runat="server" CssClass="fonteTexto" Height="18px"
                                        MaxLength="2" onkeyup="formataInteiro(this, event);" Width="10%"></asp:TextBox>
                                </td>

                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="controls" style="margin: 0 auto; text-align: center;">
                        <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" />
                        &nbsp;
                         <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar"
                             OnClick="btn_listar_Click" />

                    </div>
                </asp:Panel>
                <asp:SqlDataSource ID="SDS_Unidades" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ProtocoloAgilConnectionString %>"
                    SelectCommand="SELECT UniCodigo, UniNome FROM CA_Unidades WHERE UniCodigo <>99 order by UniNome"></asp:SqlDataSource>
            </asp:View>

            <asp:View ID="View10" runat="server">
                <div class="centralizar">
                    <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
