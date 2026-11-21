<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResumenAdministración.aspx.cs" Inherits="Interfaz_de_usuario.ResumenAdministración" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <link href="Styles/ResumenAdministracion.css" rel="stylesheet" />

    <!--  Tarjetas KPI -->
    <section class="kpi-grid">
        <div class="kpi-card">
            <div class="kpi-top">
                <span class="kpi-label">Turnos del día</span>
                <span class="kpi-icon kpi-icon-green">
                     <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" height="24px" width="24px" fill="currentColor">
                     <path d="M5,22q-0.83,0-1.41-0.59T3,20V6Q3,5.18 3.59,4.59T5,4H6V2H8V4h8V2h2V4h1q0.83,0 1.41,0.59Q21,5.18 21,6V20q0,0.82-0.59,1.41T19,22H5Zm0-2H19V10H5V20ZM5,8H19V6H5V8ZM5,8V6V8Zm7,6q-0.42,0-0.71-0.29Q11,13.43 11,13t0.29-0.71T12,12t0.71,0.29T13,13t-0.29,0.71T12,14ZM7.29,13.71Q7,13.43 7,13t0.29-0.71T8,12t0.71,0.29T9,13T8.71,13.71T8,14q-0.42,0-0.71-0.29ZM16,14q-0.42,0-0.71-0.29Q15,13.43 15,13t0.29-0.71T16,12q0.43,0 0.71,0.29T17,13t-0.29,0.71T16,14Zm-4,4q-0.42,0-0.71-0.29Q11,17.43 11,17t0.29-0.71T12,16t0.71,0.29T13,17t-0.29,0.71T12,18ZM7.29,17.71Q7,17.43 7,17t0.29-0.71T8,16t0.71,0.29T9,17T8.71,17.71T8,18q-0.42,0-0.71-0.29ZM16,18q-0.42,0-0.71-0.29Q15,17.43 15,17t0.29-0.71T16,16q0.43,0 0.71,0.29T17,17t-0.29,0.71T16,18Z"/></svg>
                </span>
            </div>
            <div class="kpi-value">24</div> <!--Aca deberia ir el valor real -->
            <div class="kpi-meta">4 profesionales · 3 en atención / atendidos</div>
        </div>

        <div class="kpi-card">
            <div class="kpi-top">
                <span class="kpi-label">Confirmados por pacientes</span>
                <span class="kpi-icon kpi-icon-green">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" height="24px" width="24px" fill="currentColor">
                    <path d="M9.55,18l-5.7-5.7l1.43-1.42l4.28,4.28l9.18-9.18L20.15,7.4L9.55,18Z"/></svg>
                </span>
            </div>
            <div class="kpi-value">14</div><!--Aca deberia ir el valor real -->
            <div class="kpi-meta">Confirmación recibida vía WhatsApp</div>
        </div>

        <div class="kpi-card">
            <div class="kpi-top">
                <span class="kpi-label">Pendientes de confirmación</span>
                <span class="kpi-icon kpi-icon-amber">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" height="24px" width="24px" fill="currentColor">
                        <path d="M12,21q-1.88,0-3.51-0.71T5.64,18.36T3.71,15.51T3,12T3.71,8.49T5.64,5.64Q6.85,4.42 8.49,3.71T12,3q2.05,0 3.89,0.88T19,6.35V4h2v6H15V8h2.75q-1.02-1.4-2.52-2.2T12,5Q9.08,5 7.04,7.04T5,12t2.04,4.96T12,19q2.63,0 4.59-1.7T18.9,13h2.05q-0.38,3.43-2.94,5.71T12,21Zm2.8-4.8L11,12.4V7h2v4.6l3.2,3.2l-1.4,1.4Z"/></svg>
                </span>
            </div>
            <div class="kpi-value">05</div> <!--Aca deberia ir el valor real -->
            <div class="kpi-meta">Recordatorio: no son turnos libres</div>
        </div>

        <div class="kpi-card">
            <div class="kpi-top">
                <span class="kpi-label">Horarios disponibles hoy</span>
                <span class="kpi-icon kpi-icon-green">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" height="24px" width="24px" fill="currentColor">
                        <path d="M18,20V17H15V15h3V12h2v3h3v2H20v3H18ZM3,21q-0.82,0-1.41-0.59T1,19V5Q1,4.17 1.59,3.59T3,3H17q0.82,0 1.41,0.59T19,5v5H17V8H3V19H16v2H3ZM3,6H17V5H3V6ZM3,6V5V6Z"/></svg>
                </span>
            </div>
            <div class="kpi-value">08</div> <!--Aca deberia ir el valor real -->
            <div class="kpi-meta">2 liberados por cancelación · 3 profesionales</div>
        </div>
    </section>

    <!-- Barra de fecha y filtros -->
    <section class="toolbar-bar">
        <div class="date-indicator">
            <span>Hoy, lunes 16 de noviembre de 2026</span>
            <span class="time-pill">• 09:18</span>
        </div>

        <div class="filter-controls">
            <asp:DropDownList ID="ddlJornada" runat="server" CssClass="filter-select">
                <asp:ListItem Text="Filtrar jornada" Value="" />
                <asp:ListItem Text="Mañana" Value="Manana" />
                <asp:ListItem Text="Tarde" Value="Tarde" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlProfesional" runat="server" CssClass="filter-select">
                <asp:ListItem Text="Todos los profesionales" Value="" />
                <asp:ListItem Text="Dra. Ana Martínez" Value="Ana Martinez" />
                <asp:ListItem Text="Dr. Diego Ruiz" Value="Diego Ruiz" />
                <asp:ListItem Text="Dra. Sofía López" Value="Sofia Lopez" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="filter-select">
                <asp:ListItem Text="Todas las especialidades" Value="" />
                <asp:ListItem Text="Clínica Médica" Value="Clinica" />
                <asp:ListItem Text="Cardiología" Value="Cardiologia" />
                <asp:ListItem Text="Dermatología" Value="Dermatologia" />
            </asp:DropDownList>
        </div>
    </section>

    <!-- 4. Dos columnas: Turnos del día + Disponibilidad de hoy -->
    <section class="resumen-grid">
        <!-- Columna izquierda: Turnos del día -->
        <div class="panel-card">
            <div class="panel-header">
                <div>
                    <h2 class="panel-title">Turnos del día</h2>
                    <p class="panel-subtitle">Seguí cada atención y la respuesta de los pacientes.</p>
                </div>
                <asp:LinkButton ID="btnAbrirAgenda" runat="server" CssClass="panel-link">Abrir agenda ›</asp:LinkButton>
            </div>

            <div class="status-tabs">
                <button type="button" class="status-tab active">Todos <span class="tab-count">24</span></button>
                <button type="button" class="status-tab">Confirmados <span class="tab-count">14</span></button>
                <button type="button" class="status-tab">Pendientes <span class="tab-count">5</span></button>
                <button type="button" class="status-tab">Cancelados <span class="tab-count">1</span></button>
                <button type="button" class="status-tab">En atención / atendidos <span class="tab-count">3</span></button>
            </div>

            <div class="turnos-list">
                <div class="turno-row">
                    <div class="turno-time">
                        <strong>08:30</strong>
                        <span>30 min</span>
                    </div>
                    <div class="turno-patient">
                        <span class="mini-avatar">RH</span>
                        <div>
                            <div class="item-main">Roberto Hernández</div>
                            <div class="item-sub">35.482.901 · OSDE</div>
                        </div>
                    </div>
                    <div class="turno-doctor">
                        <div class="item-main">Dra. Ana Martínez</div>
                        <div class="item-sub">Clínica Médica</div>
                    </div>
                    <div class="turno-status">
                        <span class="badge badge-info">Atendido</span>
                        <span class="item-sub">Atención finalizada</span>
                    </div>
                </div>

                <div class="turno-row">
                    <div class="turno-time">
                        <strong>09:00</strong>
                        <span>30 min</span>
                    </div>
                    <div class="turno-patient">
                        <span class="mini-avatar">CM</span>
                        <div>
                            <div class="item-main">Carolina Morales</div>
                            <div class="item-sub">29.841.220 · Swiss Medical</div>
                        </div>
                    </div>
                    <div class="turno-doctor">
                        <div class="item-main">Dr. Diego Ruiz</div>
                        <div class="item-sub">Cardiología</div>
                    </div>
                    <div class="turno-status">
                        <span class="badge badge-success">En consultorio</span>
                        <span class="item-sub">Ingresó a las 09:00</span>
                    </div>
                </div>

                <div class="turno-row">
                    <div class="turno-time">
                        <strong>09:30</strong>
                        <span>30 min</span>
                    </div>
                    <div class="turno-patient">
                        <span class="mini-avatar">TR</span>
                        <div>
                            <div class="item-main">Tomás Rodríguez</div>
                            <div class="item-sub">38.712.434 · Galeno</div>
                        </div>
                    </div>
                    <div class="turno-doctor">
                        <div class="item-main">Dra. Sofía López</div>
                        <div class="item-sub">Dermatología</div>
                    </div>
                    <div class="turno-status">
                        <span class="badge badge-success">✓ Confirmado</span>
                        <span class="item-sub">Respondió WhatsApp 08:42</span>
                    </div>
                </div>

                <div class="turno-row">
                    <div class="turno-time">
                        <strong>09:30</strong>
                        <span>30 min</span>
                    </div>
                    <div class="turno-patient">
                        <span class="mini-avatar">FP</span>
                        <div>
                            <div class="item-main">Fiona Pérez</div>
                            <div class="item-sub">34.913.112 · Omint</div>
                        </div>
                    </div>
                    <div class="turno-doctor">
                        <div class="item-main">Dr. Diego Ruiz</div>
                        <div class="item-sub">Cardiología</div>
                    </div>
                    <div class="turno-status">
                        <span class="badge badge-warning">Pendiente</span>
                        <span class="item-sub">Sin respuesta</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Columna derecha: Disponibilidad de hoy -->
        <aside class="panel-card">
            <div class="panel-header">
                <div>
                    <h2 class="panel-title">Disponibilidad de hoy</h2>
                    <p class="panel-subtitle">Asigná un lugar a quien llega sin turno. Todos los horarios son posteriores a las 09:18.</p>
                </div>
                <span class="pill-count">8 libres</span>
            </div>

            <div class="notice-banner">
                2 cupos liberados por cancelación
            </div>

            <div class="slot-group">
                <div class="slot-doctor-header">
                    <span class="mini-avatar">AM</span>
                    <div>
                        <div class="item-main">Dra. Ana Martínez</div>
                        <div class="item-sub">Clínica Médica · Consultorio 1</div>
                    </div>
                    <span class="slot-free-tag">3 libres</span>
                </div>

                <div class="slot-item">
                    <div>
                        <strong>10:30 - 11:00</strong>
                    </div>
                    <asp:LinkButton ID="btnAsignar1" runat="server" CssClass="btn-assign">+ Asignar</asp:LinkButton>
                </div>

                <div class="slot-item slot-item-highlight">
                    <div>
                        <strong>12:00 - 12:30</strong>
                        <span class="slot-tag-green">Libre</span>
                        <div class="item-sub">Cancelado a las 08:54</div>
                    </div>
                    <asp:LinkButton ID="btnAsignar2" runat="server" CssClass="btn-assign btn-assign-solid">+ Asignar</asp:LinkButton>
                </div>
            </div>

            <div class="slot-group">
                <div class="slot-doctor-header">
                    <span class="mini-avatar">DR</span>
                    <div>
                        <div class="item-main">Dr. Diego Ruiz</div>
                        <div class="item-sub">Cardiología · Consultorio 2</div>
                    </div>
                    <span class="slot-free-tag">2 libres</span>
                </div>

                <div class="slot-item slot-item-highlight">
                    <div>
                        <strong>11:00 - 11:30</strong>
                        <span class="slot-tag-green">Libre</span>
                        <div class="item-sub">Fabián Benítez · Canceló 09:05</div>
                    </div>
                    <asp:LinkButton ID="btnAsignar3" runat="server" CssClass="btn-assign btn-assign-solid">+ Asignar</asp:LinkButton>
                </div>
            </div>
        </aside>
    </section>
</asp:Content>
