<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarioAprendizPrint.aspx.cs" Inherits="ProtocoloAgil.pages.aprendiz.cadastro.CalendarioAprendizPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ANEXO - CALENDÁRIO DA APRENDIZAGEM</title>
    <style>
        body {
            height: 100%;
            padding: 1rem;
            background-color: #FEFEFE;
            -webkit-font-smoothing: antialiased;
            text-rendering: optimizeLegibility;
            font-family: -apple-system, BlinkMacSystemFont, system-ui, "Segoe UI", Roboto, Oxygen, Ubuntu, "Helvetica Neue", sans-serif;
        }



        @media screen and (min-width: 55em) {
            body {
                height: 100vh;
                margin: 0;
            }
        }

        h1, h2, h3, h4, h5, h6 {
            -webkit-font-smoothing: antialiased;
        }

        .calendar-contain {
            border-radius: 0;
            width: 100%;
            overflow: hidden;
            max-width: 250px;
            margin: 0.6rem 0.4rem;
            background-color: #fBfBfB;
            padding: 8px;
            box-shadow: 2px 2px 5px rgba(30, 46, 50, 0.3);
            color: #040605;
            float: left;
        }

        .jornada {
            border-collapse: collapse;
            margin: auto;
            width: 27rem;
            display: block;
            margin-bottom: 10rem;
            border: none;
        }

            .jornada td, .jornada th {
                border: 1px solid black;
                text-align: right;
            }

        .calendar__heading-highlight {
            color: #2d444a;
            font-weight: 900;
        }

        .calendar__days {
            align-items: stretch;
            width: 100%;
            float: none;
            font-size: 12px;
            padding: 0.8rem 0 1rem 1rem;
            background: #fDfDfD;
        }

        .top-bar__days {
            width: 100%;
            padding: 0 4px;
            color: #2d4338;
            font-weight: 100;
            -webkit-font-smoothing: subpixel-antialiased;
            font-size: 1rem;
        }

        .calendar__day {
            width: 100%;
            padding: 0.5rem 0.1rem 0.1rem 0;
        }

        .calendar__date {
            /*color: #040605;*/
            font-size: 1.4rem;
            font-weight: 600;
            line-height: 0.7;
        }

        .inactive .calendar__date,
        .finaldesemana .calendar__date,
        .feriado .calendar__date {
            color: #BBB;
        }

        .trabalho .calendar__date {
            color: #090;
        }

        .simultaneidade .calendar__date,
        .mensal .calendar__date {
            color: #228;
        }

        .feriadoV .calendar__date {
            color: red;
        }

        .introducao .calendar__date {
            color: #990;
        }


        .date {
            text-align: center
        }

        table {
            page-break-inside: avoid
        }

        .quad {
            display: block;
            width: 15px;
            height: 15px;
            display: inline-block
        }

        .teoria {
            background-color: blue;
        }

        .pratica {
            background-color: #090;
        }

        .feriado {
            background-color: red;
        }


        .teoriaN {
            color: blue !important;
        }

        .praticaN {
            color: #090 !important;
        }

        .feriadoN {
            color: red !important;
        }

        @media screen {
            .container {
                max-width: 1024px;
                margin: auto;
            }
        }
    </style>

  
    <style>
        .calendar__task {
            color: #fff !important;
            display: flex;
            font-size: 0.7rem;
            margin-top: -5px;
        }

        .calendar__task {
            display: flex;
            font-size: 0.7rem;
            margin-top: -5px;
        }
    </style>


    <% if (!string.IsNullOrWhiteSpace(this.Request.QueryString["min"]))
        {%>
    <style>
        @page {
            size: auto; /* auto is the initial value */
            /* this affects the margin in the printer settings */
            margin: 20mm 20mm 20mm 20mm;
        }

        .lastpage {
            page-break-inside: avoid;
        }

            .lastpage h1 {
                font-size: 1.6rem;
                margin-bottom: 1rem;
                clear: right;
            }

        .cabecalho {
            margin: auto;
            font-size: 0.9rem;
            margin-bottom: 2rem;
        }

        .feriadoV .calendar__date {
            color: #040605
        }

        .calendar__date {
            color: #040605;
            font-size: 1.4rem;
            font-weight: 600;
            line-height: 0.7;
        }

        * {
            transition: none !important
        }

        .calendar__day {
            border: 1px #fBfBfB solid;
        }

        .trabalho {
            border: 1px black solid;
        }

            .trabalho .calendar__date {
                color: #050;
            }

        .simultaneidade,
        .mensal,
        .introducao,
        .finalizacao {
            border: 1px black dashed;
        }

            .introducao .calendar__date {
                color: #770;
            }

        .inactive .calendar__date,
        .finaldesemana .calendar__date {
            color: #FFEFFF;
        }

        .feriado .calendar__date {
            color: #CCC;
        }

        .calendar-contain {
            margin-left: 15px !important;
        }

        table {
            border: 1px black solid;
        }

        .calendar__date {
            font-size: 1rem;
        }

        .calendar__day {
            padding-top: 3px
        }
    </style>

    <style>
        .calendar__task {
            margin-top: -6px;
            font-size: 0.6rem;
        }

        .top-bar__days {
            padding: 0 1px;
            color: #2d4338;
            font-weight: 100;
            -webkit-font-smoothing: subpixel-antialiased;
            font-size: 0.7rem;
        }

        .calendar__date {
            font-size: 0.7rem;
        }

        .calendar__day {
            font-size: 0.8rem;
            padding-top: 0px;
        }

        .calendar-contain {
            margin-left: 5px !important;
        }

        .calendar-contain {
            border-radius: 0;
            width: 158px;
            margin: 0.1rem 0.2rem;
            background-color: #fBfBfB;
            padding: 0px;
            box-shadow: 1px 1px 3px rgba(30, 46, 50, 0.3);
            color: #040605;
            float: left;
        }
    </style>
    <%}%>
</head>
<%
    if (!string.IsNullOrWhiteSpace(this.Request.QueryString["min"]))
    { %>
<body onload="window.print();">
    <%
        }
        else
        {
    %>
    <body>
        <% } %>

        <form id="form1" class="container" runat="server">
            <% 
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");
                List<ProtocoloAgil.pages.CalendarDay> days = (List<ProtocoloAgil.pages.CalendarDay>)Session["Calendario_User"];
                ProtocoloAgil.pages.CalendarDay firstDay = days.OrderBy(d => d.Date).First();
                ProtocoloAgil.pages.CalendarDay lastDay = days.OrderByDescending(d => d.Date).First();
                int firstMonth = firstDay.Date.Month;
                int firstYear = firstDay.Date.Year;
                int lastMonth = lastDay.Date.Month;
                int lastYear = lastDay.Date.Year;
                int monthsDifference = ((lastYear - firstYear) * 12) + lastMonth - firstMonth;
                DateTime currentDate = new DateTime(firstDay.Date.Year, firstDay.Date.Month, 1);
            %>

            <section>

                <h1>
                    <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/images/logofundacao.png" Style="width: 120px" />
                    ANEXO - CALENDÁRIO DA APRENDIZAGEM</h1>
                <table class="cabecalho">
                    <tr>
                        <td colspan="2">Nome do Aprendiz: <%= Session["Print_Aprendiz_Nome"] %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Curso: <%= Session["cursoAprendiz"] %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Unidade Concedente da Aprendizagem: <%= Session["Print_Aprendiz_Parceiro"] %>
                        </td>
                    </tr>
                    <tr>
                        <td>Carga Horária Diária (Teorica / Pratica): <%= Session["CargaHoraria"] %> - <%= Session["cargaHorariaPratica"] %></td>
                        <td>Dia Encontro Semanal: <%= Session["formacaoTeorica"] %></td>
                    </tr>
                    <tr>
                        <td>Ambientação: <%= days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.Inicializacao).Min(d => d.Date).ToShortDateString() + " a " + days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.Inicializacao).Max(d => d.Date).ToShortDateString()  %>
                        </td>
                        <td>Duração do contrato: <%=  days.Min(d => d.Date).ToShortDateString() + " a " + days.Max(d => d.Date).ToShortDateString() %>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>Simultaneidade: <%= days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.SimultaneidadeVida).Min(d => d.Date).ToShortDateString() + " a " + days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.SimultaneidadeVida).Max(d => d.Date).ToShortDateString()  %></td>
                        <td>Sequencial: <%= days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.Finalizacao).Any() ? days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.Finalizacao).Min(d => d.Date).ToShortDateString() + " a " + days.Where(d => d.CalendarType == ProtocoloAgil.pages.CalendarType.Finalizacao).Max(d => d.Date).ToShortDateString() : ""  %></td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                      

                        <td colspan="2">Legenda : <b class="quad teoria"></b>: Teorica   <b class="quad pratica"></b>: Prática  <b class="quad feriado"></b>: Feriado/Recesso
                        </td>
                       
                    </tr>
                </table>
            </section>
            <%
                for (int i = 0; i <= monthsDifference; i++)
                {
                    var monthStarted = false;
                    var monthNumber = currentDate.Month;
            %>
            <table class="calendar-contain">
                <caption><%= currentDate.ToString("MMMM yyyy").ToUpper() %></caption>
                <thead class="calendar__days">
                    <tr class="calendar__top-bar">
                        <th class="top-bar__days">Dom</th>
                        <th class="top-bar__days">Seg</th>
                        <th class="top-bar__days">Ter</th>
                        <th class="top-bar__days">Qua</th>
                        <th class="top-bar__days">Qui</th>
                        <th class="top-bar__days">Sex</th>
                        <th class="top-bar__days">Sab</th>
                    </tr>
                </thead>
                <tbody class="calendar__body">
                    <%
                        int weekCount = 0;
                        while (currentDate.Month == monthNumber)
                        {
                    %>
                    <tr class="calendar__week">
                        <% for (int j = 0; j < 7; j++)
                            {
                                ProtocoloAgil.pages.CalendarDay day = days.SingleOrDefault(d => d.Date == currentDate);
                                if (day == null)
                                {
                                    day = new ProtocoloAgil.pages.CalendarDay()
                                    {
                                        CssClass = "inactive",
                                        Task = "&#8203;"
                                    };
                                }
                                if (currentDate.Month != monthNumber)
                                {
                        %>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <%
                                continue;
                            }
                            if (!monthStarted && currentDate.DayOfWeek == (DayOfWeek)j)
                            {
                                monthStarted = true;
                            }
                            if (monthStarted)
                            {
                        %>
                        <td class="calendar__day date <%= day.CssClass %>">
                            <span class="calendar__date"><%= currentDate.Day %></span>
                            <span class="calendar__task"><%= string.IsNullOrWhiteSpace(day.Task) ? "&#8203;" : day.Task %></span>
                        </td>
                        <% 
                                currentDate = currentDate.AddDays(1);
                            }
                            else
                            {
                        %>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <%  
                                }
                            }
                            weekCount++;
                        %>
                    </tr>

                    <%

                        }
                        while (weekCount < 6)
                        {
                    %>
                    <tr class="calendar__week">
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                        <td class="calendar__day">
                            <span class="calendar__date">&#8203;</span>
                            <span class="calendar__task">&#8203;</span>
                        </td>
                    </tr>
                    <%
                            weekCount++;
                        }
                    %>
                </tbody>
            </table>
            <%
                }
            %>
            <div class="lastpage">
                <h2 style="clear: both; margin-top: 2rem">Resumo Geral:
                </h2>
                <table class="jornada">
                    <tr>
                        <td></td>
                        <th>Encontros</th>
                        <th>Horas</th>
                        <th>Porcentagem</th>
                        <th>Inicio formação</th>
                    </tr>
                    <tr>
                        <th>Teoria</th>
                        <td>
                            <%= Session["JornadaTeorioa"] %></td>
                        <td>
                            <%= Session["JornadaTeorioaHoras"] %></td>
                        <td>
                            <%= (int.Parse(Session["JornadaTeorioaHoras"].ToString())/(1.0 * int.Parse(Session["JornadaTeorioaHoras"].ToString()) + int.Parse(Session["PraticaHoras"].ToString()))).ToString("p2")%></td>
                        <td>
                            <asp:Label runat="server" ID="lblInicioAprendizagem"><%= Session["inicioContrato"] %></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>Prática</th>
                        <td>
                            <%= Session["DiasPratica"] %></td>
                        <td>
                            <%=  Session["PraticaHoras"] %></td>
                        <td>
                            <%= (int.Parse(Session["PraticaHoras"].ToString())/(1.0 * int.Parse(Session["JornadaTeorioaHoras"].ToString()) + int.Parse(Session["PraticaHoras"].ToString()))).ToString("p2")%></td>

                        <td>
                            <%--     <%= days.Where(c => c.CalendarType == ProtocoloAgil.pages.CalendarType.SimultaneidadeVida).Min(c => c.Date).ToShortDateString() %>--%>
                        </td>
                    </tr>
                    <tr>
                        <th>Total</th>
                        <td>
                            <%= int.Parse(Session["JornadaTeorioa"].ToString()) + int.Parse(Session["DiasPratica"].ToString()) %></td>
                        <td>
                            <%= int.Parse(Session["JornadaTeorioaHoras"].ToString()) + int.Parse(Session["PraticaHoras"].ToString()) %> </td>
                    </tr>
                </table>

                <%if (!string.IsNullOrWhiteSpace(this.Request.QueryString["min"]))
                    { %>

                <table>
                    <tr style="text-align: center">
                        <td colspan="2">
                            <asp:Label runat="server"><br />_______, de _______________________, ________</asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td style="width: 300px">
                            <asp:Label Text="<br /> <br /> _____________________________________________________ <br />Entidade Qualificadora" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Text="<br /> <br /> ______________________________________________________ <br />Empregador (Concedente da Aprendizagem Prática)" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label Text="<br> <br> ______________________________________________________ <br>Aprendiz " runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label Text="<br> <br> ______________________________________________________ <br>Rensponsável Legal (somente para menores)" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>

                <%
                }%>

            </div>
                <table align="center">
                    <tr>

                        <td style="width: 300px">
                            <asp:Label CssClass="" Text="  _____________________________________________________ <br />  " runat="server"></asp:Label><%= Session["Print_Aprendiz_Nome"] %>
                        </td>
                        <td>
                            <asp:Label Text=" ______________________________________________________ <br /> Responsável Legal do Aprendiz" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td style="width: 300px">
                    <br />
                    <br />
                            <asp:Label CssClass="" runat="server" Text="  _____________________________________________________ <br />  Entidade Formadora"></asp:Label>
                        </td>
                        <td>
                            
                    <br />
                    <br />
                            <asp:Label Text=" ______________________________________________________ <br /> " runat="server"></asp:Label><%= Session["Print_Aprendiz_Parceiro"] %>
                        </td>
                    </tr>
                </table>
            <br />
        </form>
    </body>
</html>
