<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarAprendiz.aspx.cs" Inherits="ProtocoloAgil.pages.CalendarAprendiz" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendario</title>
    <link href="Styles/cdl_bh.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
    <style type="text/css">
        .calendar {
            float: left;
            padding: 10px;
            z-index: 1;
        }

        .fontecal01 {
            font-family: Tahoma,Calibri;
            font-size: 12px;
            color: #00008B;
            margin-left: 20px;
            top: 0px;
            left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 70px">
            <table style="width: 990px">
                <tr>
                    <td>
                        <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/images/logofundacao.png" />
                    </td>
                    <td style="width: 600px">
                        <div class="col-md-12 fontecal01">
                            <br />
                            <br />
                            <asp:Label runat="server" CssClass="" ID="lblEmpresa"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" ID="lblNome"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label runat="server" CssClass="" ID="lblTurma"></asp:Label><br />
                            <asp:Label runat="server" Visible="false" CssClass="" ID="lblQuantidade"></asp:Label>
                            <asp:Label runat="server" CssClass="" ID="lblInicio"></asp:Label>
                            <asp:Label runat="server" CssClass="" ID="lblTermino"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" ID="lblParceiro"></asp:Label><br />
                            <br />
                            <asp:Label runat="server" CssClass="" ID="Label1"> Legenda: Dias em destaque - Atividade Teórica</asp:Label>
                        </div>
                    </td>
                    <td style="width: 50px">
                        <asp:Image ID="ImgFundacao" runat="server" Visible="false" ImageUrl="~/images/logofundacao.png" />
                    </td>
                </tr>
            </table>
            <div class="calendar">
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>

            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar3" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>

            </div>


            <div class=" calendar">
                <asp:Calendar ID="Calendar4" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar5" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar6" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar7" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar8" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar9" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar10" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar11" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar12" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar13" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar14" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>
            <% carregarDias();
                if (Convert.ToInt32(hfDias.Value) > 445)
                { %>
            <br />

            <table runat="server" visible="false" style="width: 790px">
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logofundacao.png" />
                    </td>

                    <td style="width: 600px">
                        <div class="col-md-12 fontecal01">

                            <asp:Label runat="server" CssClass="" ID="lblEmpresa2"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" ID="lblNome2"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" CssClass="" ID="lblTurma2"></asp:Label><br />
                            <asp:Label runat="server" Visible="false" CssClass="" ID="lblQuantidade2"></asp:Label>
            <asp:Label runat="server" CssClass="" ID="lblInicio2"></asp:Label>
                            <asp:Label runat="server" CssClass="" ID="lblTermino2"></asp:Label><br />
                            <asp:Label runat="server" CssClass="" ID="lblParceiro2"></asp:Label><br />
                            <br />
                            <asp:Label runat="server" CssClass="" ID="Label10"> Legenda: Dias em destaque - Atividade Teórica</asp:Label>
                        </div>
                    </td>
                    <td style="width: 50px">
                        <asp:Image ID="Image2" runat="server" Visible="false" ImageUrl="~/images/logofundacao.png" />
                    </td>
                </tr>

            </table>
            <% }   %>
   |       
            <div class=" calendar">
                <asp:Calendar ID="Calendar15" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
                <asp:HiddenField ID="hfDias" runat="server" />
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar16" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar17" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar18" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar19" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>

            <div class=" calendar">
                <asp:Calendar ID="Calendar20" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" CellSpacing="3"
                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="270px"
                    Width="250px" EnableTheming="True" SelectionMode="None" ShowGridLines="True" Visible="False" ShowNextPrevMonth="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Height="8pt" BackColor="#333399" />
                    <DayStyle BackColor="White" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="White" Font-Size="0pt" BackColor="White" />
                    <SelectedDayStyle BackColor="Gray" ForeColor="White" BorderColor="#CCCCCC" Font-Size="12pt" BorderStyle="Solid" BorderWidth="2pt" Font-Bold="True" Font-Italic="True" />
                    <TitleStyle BackColor="White" BorderStyle="Solid" Font-Bold="True" Font-Size="9pt" ForeColor="#333399" Height="12pt" />
                    <WeekendDayStyle BackColor="White" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" />
                </asp:Calendar>
            </div>
            <div style="clear: both; width: 100%">
                <table>
                    <tr>

                        <td>
                            <asp:Label Text="<br> <br> ______________________________________________________ <br>Aprendiz" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Text="<br> <br> ______________________________________________________ <br>Empresa" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Text="<br> <br> ______________________________________________________ <br>CEFORT" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label2" runat="server" Visible="false" CssClass="fontecal01" Text="Label"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
