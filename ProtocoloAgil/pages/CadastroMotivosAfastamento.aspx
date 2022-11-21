<%@ Page Title="Gestão Jovem Aprendiz - Agil Sistemas" Language="C#"
    EnableEventValidation="true" AutoEventWireup="true" MasterPageFile="~/MPusers.Master"
    Inherits="ProtocoloAgil.pages.CadastroMotivosAfastamento" CodeBehind="CadastroMotivosAfastamento.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function GetConfirm() {
            var hf = <%= HFConfirma.ClientID %>;
            if (confirm("Deseja realmente excluir atribuição?") == true)
                hf.value = "true";
            else
                hf.value = "false";
        }
    </script>
    <asp:Panel ID="Panel4" runat="server" CssClass=" Table centralizar" Height="103px" Width="973px">
        <div class="breadcrumb">
            <p style="text-align: left;">
                Configurações > <span>Cadastro Afastamento</span>
            </p>
        </div>
        <div class="controls">
            <div style="float: left;">
                <div class="btn-group">
                    <%--<asp:Button ID="btn_listar" runat="server" CssClass="btn btn-green mb-xs mt-xs mr-xs" Text="Listar" OnClick="btn_listar_Click" />--%>
                    <asp:Button ID="btn_novo" runat="server" CssClass="btn_novo" Text="Novo" OnClick="btn_novo_Click" />
                    <asp:Button ID="btn_relatorio" runat="server" CssClass="btn_novo"
                        Text="Relatorio" OnClick="btn_relatorio_Click" />
                    <asp:Button ID="voltar" runat="server" Visible="false" CssClass="btn_novo" Text="Voltar" OnClick="btn_listar_Click" />
                </div>
                <div>
                    <asp:TextBox ID="pesquisa" runat="server" CssClass="search_controls" Visible="false" />
                    <asp:Button ID="btnpesquisa" runat="server" CssClass="btn_search" Text="Pesquisar" Visible="false"
                        OnClick="btnpesquisa_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <asp:Panel ID="Panel3" runat="server">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <section class="panel">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" CssClass="grid_Aluno"
                                        AutoGenerateColumns="False" HorizontalAlign="Center" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="Maf_Codigo" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                                SortExpression="Maf_Codigo">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Maf_Descricao" HeaderText="Descrição" SortExpression="Maf_Descricao">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:CommandField ButtonType="Image" HeaderText="Alterar" SelectImageUrl="~/Images/icon_edit.png"
                                                ShowSelectButton="True">
                                                <HeaderStyle />

                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Excluir" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="IMBexcluir" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Maf_Codigo") %>'
                                                        OnClientClick="javascript:GetConfirm();" OnClick="IMBexcluir_Click" ImageUrl="~/images/icon_remove.png"
                                                        runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="Grid_Aluno" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />
                                        <PagerStyle CssClass="nav_results" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="Próximo"
                                            PreviousPageText="Anterior" FirstPageImageUrl="~/images/seta_primeiro.jpg" LastPageImageUrl="~/images/seta_ultimo.jpg" />


                                    </asp:GridView>
                                </div>
                            </div>
                        </section>
                        <asp:HiddenField runat="server" ID="HFConfirma" />
                        <asp:HiddenField ID="HFRowCount" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnpesquisa" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="text_titulo">
                <h1>Cadastro de motivos de afastamento</h1>
            </div>
            <br />


            <section class="panel">


                <table>
                    <tr>
                        <td><span class="fonteTab"><strong>Código:</strong></span></td>
                        <td><span class="fonteTab"><strong>Descricao:</strong></span></td>
                        <td><span class="fonteTab"><strong>correspondente na Presença:</strong></span></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TBcodigo" runat="server"  CssClass="fonteTexto" Height="20px" Width="50px"
                                MaxLength="1">
                            </asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="TBNome" runat="server" CssClass="fonteTexto" Height="20px" Width="350px" MaxLength="80">
                            </asp:TextBox></td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddAfastamento" CssClass="fonteTexto" Height="20px" Width="170px">
                                <asp:ListItem Value="." Text="Selecione"></asp:ListItem>
                                <asp:ListItem Value="O" Text="Afastamento covid-19"></asp:ListItem>
                                <asp:ListItem Value="C" Text="Férias"></asp:ListItem>
                                <asp:ListItem Value="J" Text="Atestado"></asp:ListItem>
                                <asp:ListItem Value="U" Text="Suspensão Contrato"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <footer class="panel-footer">
                    <asp:Button ID="BTaltera" runat="server" OnClick="BTaltera_Click" CssClass="btn_novo" Text="Confirmar" />
                    <asp:Button ID="btn_voltar" runat="server" CssClass="btn_novo" Text="Voltar" OnClick="btn_listar_Click" />
                </footer>
            </section>

        </asp:View>
        <asp:View ID="View3" runat="server">
            <div class="centralizar">
                <iframe id="IFrame4" class="VisualFrame" name="IFrame1" src="Visualizador.aspx"></iframe>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
