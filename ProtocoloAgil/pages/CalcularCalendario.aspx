<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalcularCalendario.aspx.cs"
    Inherits="ProtocoloAgil.pages.CalcularCalendario" %>

<%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Styles/cdl_bh.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style>
        .esquerda {
            text-align: left;
        }

        .table {
            border: solid 1px;
        }

            .table tr {
                border: solid 1px;
            }

            .table td {
                border: solid 1px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ScriptMode="Release">
        </asp:ScriptManager>

        <asp:HiddenField runat="server" ID="HFAlert" Value="false" />

        <table>
            <tr>
                <td>Código</td>
                <td>Nome</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox CssClass="fonteTexto" ID="txtCodigo" runat="server" Enabled="false"></asp:TextBox></td>
                <td>
                    <asp:TextBox CssClass="fonteTexto" ID="txtNome" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>
        </table>

        <table class="FundoPainel Table">
            <tr>
                <th colspan="7" class="cortitulo titulo corfonte" style="font-size: medium; font-weight: bold;">Calcular Calendario
                </th>
            </tr>
            <tr>
                <td class="tam05">Dias Teória
                </td>
                <td class="fonteTab tam05">Dias Prática
                </td>
                <td class="fonteTab Tam20">Data Inicio Contrato
                </td>
                <td class="fonteTab tam12">Jornada Diária
                </td>
                <td class="fonteTab tam12">Número Encontros Iniciais
                </td>
                <td class="fonteTab tam12">Dia de Simultaneidade
                </td>
                   <td class="fonteTab tam12">Encontros Simultaineidade
                </td>
            </tr>
            <tr>
                <td class="tam05">
                    <asp:TextBox CssClass="fonteTexto" ID="txtTotalEncontro" runat="server" Width="83%"></asp:TextBox>
                </td>
                <td class="fonteTab">
                    <asp:TextBox CssClass="fonteTexto" ID="txtTotalPratica" runat="server" Width="83%"></asp:TextBox>
                </td>
                <td class="fonteTab">
                    <asp:TextBox CssClass="fonteTexto" ID="txtDataInicioContrato" ToolTip="Informe a data de inicio do contrato" runat="server" Width="83%"></asp:TextBox>
                    <cc2:CalendarExtenderPlus Format="dd/MM/yyyy" ID="CalendarExtenderPlus2"
                        PopupPosition="BottomRight" runat="server" TargetControlID="txtDataInicioContrato">
                    </cc2:CalendarExtenderPlus>
                </td>
                <td class="fonteTab">
                    <asp:TextBox CssClass="fonteTexto" ID="txtHorasJornadas" runat="server" Width="83%"></asp:TextBox>
                </td>
                 <td class="tam05">
                    <asp:TextBox CssClass="fonteTexto" ID="txtNumEncInicial" runat="server" Width="83%"></asp:TextBox>
                </td>
                <td class="fonteTab">
                    <asp:DropDownList CssClass="fonteTexto" ID="ddDiaSimultaneidade" runat="server" Width="83%">
                        <asp:ListItem Value="0" Text="Não selecionado"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Segunda-Feira"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Terça-Feira"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Quarta-Feira"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Quinta-Feira"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Sexta-Feira"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td class="tam05">
                    <asp:TextBox CssClass="fonteTexto" ID="txtNumEncSimultaneidade" runat="server" Width="83%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCalcular" runat="server" OnClientClick="javascript:mostrarAlert()" CssClass="btn_novo" Text="Calcular" OnClick="BtnCalcular_Click" />
                </td>
                <td wdith="150" colspan="2">
                    <asp:HyperLink Target="_blank" ID="btnCalendario" NavigateUrl="~/pages/aprendiz/cadastro/CalendarioAprendizPrint.aspx?visul=calc" runat="server" CssClass="btn_novo" Text="Visualizar Calendário" />
                </td>
                <td  colspan="2">
                    <asp:HyperLink Target="_blank" ID="HyperLink1" NavigateUrl="~/pages/aprendiz/cadastro/CalendarioAprendizPrint.aspx?min=true&visul=calc" runat="server" CssClass="btn_novo" Text="Imprimir Calendário" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table class="table table-bordered table-hover">
            <tr>
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label4" runat="server" Text="Data de Termino de Contrato: "></asp:Label></th>
                <td width="80px">
                    <asp:Label CssClass="fonteTab" Font-Bold="true" ID="lblTermino" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label2" runat="server" Text="Data de inicio da Proximidade"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="tbtDataInicioAmb" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda" runat="server" visible="false">
                    <asp:Label CssClass="fonteTab" ID="Label16" runat="server" Text="Data de Termino da Proximidade: "></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblTerminoIntro" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label1" runat="server" Text="Número de Encontro Iniciais: "></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="txtNumeroIntro" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label5" runat="server" Text="Data de Inicio na Empresa: "></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="txtDataEmpresa" runat="server"></asp:Label></td>
            </tr>

            <tr runat="server" visible="false">
                <th class="esquerda" runat="server" visible="false">
                    <asp:Label CssClass="fonteTab" ID="Label6" runat="server" Text="Data de Inicio Prosperidade: "></asp:Label></th>
                <td  runat="server" visible="false">
                    <asp:Label CssClass="fonteTab" ID="lblInicioTrabalho" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda" runat="server" visible="false">
                    <asp:Label CssClass="fonteTab" ID="Label8" runat="server" Text="Numero de Encontros Prosperidade"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblNTrab" runat="server"></asp:Label></td>
            </tr>
            <tr  runat="server" visible="false">
                <th class="esquerda" runat="server" visible="false">
                    <asp:Label CssClass="fonteTab" ID="Label10" runat="server" Text="Data de Inicio do Aprender Fazendo: "></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblIniciovida" runat="server"></asp:Label></td>
            </tr>
            <tr >
                <th class="esquerda"  >
                    <asp:Label CssClass="fonteTab" ID="Label7" runat="server" Text="Numero de Encontros Simultaneidade"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblNVida" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label11" runat="server" Text="Numero de Encontros Simultaneidade"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblNComplementares" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false"> 
                <th class="esquerda" >
                    <asp:Label CssClass="fonteTab" ID="Label9" runat="server" Text="Numero de Engajamento Cidadão"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblEncontroMensais" runat="server"></asp:Label></td>
            </tr>
            <tr  runat="server" visible="false">
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label14" runat="server" Text="Data de Inicio Pl. Des. Pessoal: "></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblDataInicioFinalizacao" runat="server"></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label3" runat="server" Text="Número de Encontro de Confiança no futuro"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="txtNumeroFinalizacao" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr runat="server" visible="false">
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label12" runat="server" Text="Numero de Encontros não Agendados"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblEncontrosNAgendados" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label13" runat="server" Text="Numero de Dias Teoria"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblTotalTeoria" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th class="esquerda">
                    <asp:Label CssClass="fonteTab" ID="Label15" runat="server" Text="Numero de Dias Pratica"></asp:Label></th>
                <td>
                    <asp:Label CssClass="fonteTab" ID="lblDiasPratica" runat="server"></asp:Label></td>
            </tr>
        </table>


    </form>
</body>
</html>
