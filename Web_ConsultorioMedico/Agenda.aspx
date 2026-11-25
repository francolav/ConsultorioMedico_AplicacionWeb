<%@ Page Title="Agenda" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="Web_ConsultorioMedico.Agenda" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" TypeName="Web_ConsultorioMedico.MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Label ID="lblMensaje" runat="server" Visible="false" />
        <asp:GridView ID="gvAgenda" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" EmptyDataText="No hay turnos registrados.">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Hora">
                    <ItemTemplate><%# ((TimeSpan)Eval("Hora")).ToString(@"hh\:mm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PacienteNombre" HeaderText="Paciente" />
                <asp:BoundField DataField="EspecialidadNombre" HeaderText="Especialidad" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
