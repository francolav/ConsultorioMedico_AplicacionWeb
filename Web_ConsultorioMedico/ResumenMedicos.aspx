<%@ Page Title="Resumen Médico" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ResumenMedicos.aspx.cs" Inherits="Web_ConsultorioMedico.ResumenMedicos" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <p>Turnos confirmados para hoy: <asp:Label ID="lblTurnosHoy" runat="server" Text="0" /></p>
        <p>Turnos proximos (7 dias): <asp:Label ID="lblTurnosProximos" runat="server" Text="0" /></p>
        <asp:Label ID="lblMensaje" runat="server" Visible="false" />
        <p><a href="Agenda.aspx" class="btn-primary">Ver agenda completa</a></p>
    </div>
</asp:Content>
