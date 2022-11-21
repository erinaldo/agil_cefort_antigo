<%@ Page Title="" Language="C#" MasterPageFile="~/MPusers.Master" AutoEventWireup="true" CodeBehind="ListaAfastamento.aspx.cs" Inherits="ProtocoloAgil.pages.ListaAfastamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel4" runat="server" CssClass=" Table centralizar" Height="10px" Width="973px">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Aprendiz > <span>Lista Afastamento</span>
            </p>
        </div>
    </asp:Panel>
    <asp:MultiView ID="multiview" runat="server">
        <asp:View runat="server" ID="View1">
            <div class="controls">
                <div style="float: left;">
                    <table class="FundoPainel centralizar" style="width: 85%; border: 1px #7f7f7f solid">
                        <tr>
                            <td class="titulo cortitulo corfonte" colspan="7" style="font-size: large;">Lista de Afastamento</td>
                        </tr>
                        <tr>

                            <td class="fonteTab">Nome </td>
                            <td class="fonteTab">Mês de Referência </td>
                            <td class="fonteTab">Ano Referência </td>
                            <td class="fonteTab">Pesquisar por</td>


                        </tr>
                        <tr>

                            <td class="fonteTab">
                                <asp:TextBox ID="txtNome" runat="server" CssClass="fonteTexto" Height="20px" Width="250px"></asp:TextBox>
                            </td>


                            <td class="fonteTab">
                                <asp:DropDownList ID="DDMes" Width="150px" runat="server" CssClass="fonteTexto">
                                    <asp:ListItem Value="">Selecione</asp:ListItem>
                                    <asp:ListItem Value="01">Janeiro </asp:ListItem>
                                    <asp:ListItem Value="02">Fevereiro</asp:ListItem>
                                    <asp:ListItem Value="03">Março</asp:ListItem>
                                    <asp:ListItem Value="04">Abril</asp:ListItem>
                                    <asp:ListItem Value="05">Maio</asp:ListItem>
                                    <asp:ListItem Value="06">Junho</asp:ListItem>
                                    <asp:ListItem Value="07">Julho</asp:ListItem>
                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                    <asp:ListItem Value="09">Setembro</asp:ListItem>
                                    <asp:ListItem Value="10">Outubro</asp:ListItem>
                                    <asp:ListItem Value="11">Novembro</asp:ListItem>
                                    <asp:ListItem Value="12">Dezembro</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab">
                                <asp:TextBox Width="80px" runat="server" ID="txtAno" onkeyup="formataInteiro(this, event);" MaxLength="4"></asp:TextBox>
                            </td>
                            <td class="fonteTab">
                                <asp:DropDownList ID="ddTipoData" Width="250px" runat="server" CssClass="fonteTexto"
                                    Height="18px" OnDataBound="IndiceZero" AutoPostBack="true">
                                    <asp:ListItem Value="I" Text="Data Inicio"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Data Termino"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="fonteTab">Parceiro </td>
                            <td class="fonteTab">Unidade </td>
                            <td class="fonteTab">Situação </td>
                            <td class="fonteTab">Tipo de afastamento </td>
                        </tr>
                        <tr>
                            <td class="fonteTab">
                                <asp:DropDownList ID="DDParceiro" Width="250px" runat="server" AutoPostBack="true" CssClass="fonteTexto"
                                    DataTextField="ParDescricao" DataValueField="ParCodigo" Height="18px" OnDataBound="IndiceZero"
                                    OnSelectedIndexChanged="DDParceiro_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab">
                                <asp:DropDownList ID="DDUnidadeParceiro" Width="200px" runat="server" CssClass="fonteTexto" DataTextField="ParUniDescricao"
                                    DataValueField="ParUniCodigo" Height="18px" OnDataBound="IndiceZero" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab">
                                <asp:DropDownList runat="server" ID="ddStatus" CssClass="fonteTexto" Height="20px" Width="150px">
                                    <asp:ListItem Value="A">Ativo</asp:ListItem>
                                    <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fonteTab">
                                <asp:DropDownList ID="ddTipoAfastamento" Width="250px" runat="server" CssClass="fonteTexto" DataTextField="Maf_Descricao"
                                    DataValueField="Maf_Codigo" Height="18px" OnDataBound="IndiceZero" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="espaco" colspan="7">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="espaco" colspan="7"></td>
                        </tr>
                    </table>
                    <asp:Button runat="server" ID="btnPesquisar" CssClass="btn_novo" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    <asp:Button runat="server" ID="btnImprimir" CssClass="btn_novo" OnClick="btnImprimir_Click" Text="Imprimir" />
                </div>
            </div>
            <br />

            <br />
            <div class="table-responsive2">
                <asp:GridView runat="server" CssClass="grid_Aluno" ID="gridAfastamento" AutoGenerateColumns="false"
                    AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="15" OnPageIndexChanging="gridAfastamento_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Apr_Codigo" HeaderText="Código" />
                        <asp:BoundField DataField="Apr_Nome" HeaderText="Nome" />
                        <asp:BoundField DataField="Afa_DataInicio" DataFormatString="{0:d}" HeaderText="Data Início" />
                        <asp:BoundField DataField="Afa_DataTermino" DataFormatString="{0:d}" HeaderText="Data Termino" />
                        <asp:BoundField DataField="Afa_Motivo" HeaderText="Motivo" />
                        <asp:BoundField DataField="maf_Descricao" HeaderText="Descrição" />
                        <asp:BoundField DataField="ParUniDescricao" HeaderText="Unidade" />
                        <asp:BoundField DataField="ParUniCNPJ" HeaderText="CNPJ" />
                        <asp:BoundField DataField="Apr_DataDeNascimento" HeaderText="Data Nascimento" />
                        <asp:BoundField DataField="Apr_Sexo" HeaderText="Sexo" />
                        <%--     <asp:BoundField HeaderText="Endereço" DataField="Apr_Endereco" />
                        <asp:BoundField HeaderText="Numero" DataField="Apr_NumeroEndereco" />
                        <asp:BoundField HeaderText="Complemento" DataField="Apr_Complemento" />
                        <asp:BoundField HeaderText="Bairro" DataField="Apr_Bairro" />
                        <asp:BoundField HeaderText="Cidade" DataField="Apr_Cidade" />
                        <asp:BoundField HeaderText="UF" DataField="Apr_UF" />
                        <asp:BoundField HeaderText="Cep" DataField="Apr_CEP" />--%>
                        <asp:BoundField DataField="Apr_Telefone" HeaderText="Telefone" />
                        <asp:BoundField DataField="Apr_Celular" HeaderText="Celular" />
                        <asp:BoundField DataField="Apr_Email" HeaderText="Email" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </asp:View>
        <asp:View runat="server" ID="View02">
            <div class="controls">
                <div>
                    <asp:Button runat="server" ID="btnVoltar" OnClick="btnVoltar_Click" CssClass="btn_novo" Text="Voltar" />
                </div>
            </div>
            <div class="centralizar">
                <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
