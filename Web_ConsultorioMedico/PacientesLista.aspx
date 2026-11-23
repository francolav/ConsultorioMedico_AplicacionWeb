<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacientesLista.aspx.cs" Inherits="Web_ConsultorioMedico.PacientesLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" >
       <asp:GridView class="table table-striped table-bordered" ID="gvListadoPaciente" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="NombreCompleto" HeaderText="Pacientes" />
        <asp:BoundField DataField="Dni" HeaderText="DNI" />
        <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="ObraSocial.Nombre" HeaderText="ObraSocial" />
    </Columns>
</asp:GridView>
    </div>
    
</asp:Content>
