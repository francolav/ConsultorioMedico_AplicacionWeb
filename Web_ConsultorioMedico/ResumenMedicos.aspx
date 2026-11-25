<%@ Page Title="Resumen Médico" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResumenMedicos.aspx.cs" Inherits="Web_ConsultorioMedico.ResumenMedicos" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" TypeName="Web_ConsultorioMedico.MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Resumen de agenda</h2>
        <p>Turnos confirmados para hoy: <asp:Label ID="lblTurnosHoy" runat="server" Text="0" /></p>
        <p>Turnos próximos (7 días): <asp:Label ID="lblTurnosProximos" runat="server" Text="0" /></p>
        <asp:Label ID="lblMensaje" runat="server" Visible="false" />
        <p><a href="Agenda.aspx" class="btn-primary">Ver agenda completa</a></p>
    </div>
</asp:Content>
