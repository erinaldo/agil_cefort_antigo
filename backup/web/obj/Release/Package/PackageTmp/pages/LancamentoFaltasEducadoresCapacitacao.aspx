﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MaEducador.Master" AutoEventWireup="true" CodeBehind="LancamentoFaltasEducadoresCapacitacao.aspx.cs" Inherits="ProtocoloAgil.pages.LancamentoFaltasEducadoresCapacitacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register TagPrefix="cc2" Namespace="AjaxControlToolkitPlus" Assembly="CalendarExtenderPlus" %>
    <style type="text/css">
        .auto-style1 {
            width: 67%;
        }

        .auto-style2 {
            height: 18px;
        }
    </style>

    <%--<script type="text/javascript">
function FormataFaltaJustificado(campo, evt) {
    var tecla = getKeyCode(evt);
    xPos = PosicaoCursor(campo);
    if (!teclaValida(tecla))
        return;
    if (tecla != 70 && tecla != 74 && tecla != 190 && tecla != 76 && tecla != 83 && tecla != 68  && tecla != 112) {
        campo.value = "";
        MovimentaCursor(campo, xPos);
    }
}
</script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadcrumb">
        <p style="text-align: left;">
            Lançamento de Faltas > <span>Lançamento de Faltas Capacitação</span>
        </p>
    </div>
    <asp:MultiView runat="server" ID="multi">
        <asp:View ID="viewLancarFaltas" runat="server">


            <div class="List_results">
                <br />
                <table align="center" class="auto-style1">
                    
                    <tr>
                        <td align="right" class="auto-style2">Turma:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        <td align="left" class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Data:</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:DropDownList ID="DD_TURMA" runat="server" Style="margin-left: 0px" DataTextField="TurNome" DataValueField="TurCodigo" OnSelectedIndexChanged="DD_TURMA_SelectedIndexChanged" AutoPostBack="True" Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td align="left">&nbsp;<asp:DropDownList ID="DD_Data" runat="server" DataTextField="AcpDataAula" DataValueField="AcpDataAula" DataTextFormatString="{0:dd/MM/yyyy}" AutoPostBack="True" OnSelectedIndexChanged="DD_Data_SelectedIndexChanged">
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="left">
                            <asp:Button ID="btSalvar" runat="server" CssClass="btn_novo" Text="Salvar" Width="122px" OnClick="btSalvar_Click"/>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <label id="Label2" runat="server" class="fonteTab"
                                text="" />
                            <span class="fonteTab">Legenda.: </span><span class="fonteTexto"><strong>F</strong> - Falta. &nbsp; <strong>J</strong> - Falta Justificada. &nbsp; <strong>L</strong> -Licença Maternidade. &nbsp; <strong>S</strong> - Serviço Militar. &nbsp; <strong>D</strong> - Desligado. &nbsp; </span></td>
                        <caption>
                            &gt;
                        </caption>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <span class="fonteTexto">
                                <strong>P</strong> - Presença.
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <div class="centralizar">
                                    <asp:GridView ID="gvAlunos" runat="server" Width="580px" CssClass="list_results" AutoGenerateColumns="False" DataKeyNames="CodAprendiz,CodTurma,Data" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Nome" HeaderText="Aluno">
                                                <HeaderStyle Width="85%" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodAprendiz" HeaderText="CodAprendiz" Visible="False" />
                                            <asp:BoundField DataField="CodTurma" HeaderText="CodTurma" Visible="False" />
                                            <asp:BoundField DataField="Data" HeaderText="Data" Visible="False" />
                                            <asp:TemplateField HeaderText="Presença">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TBPresenca" OnTextChanged="TBPresenca_TextChanged" AutoPostBack="true" runat="server" Height="16px" Text='<%# Bind("Presenca") %>' Width="20px" onkeyup="FormataFaltaJustificado(this,event);" MaxLength="1"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="15%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                            <ItemTemplate>

                                                <asp:Label ID="lblSalvo" Visible="false" runat="server" MaxLength="1" Text="Alteração Realizada" Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                        </asp:TemplateField>
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
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>

        </asp:View>

       
    </asp:MultiView>
</asp:Content>
