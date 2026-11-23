<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacientesLista.aspx.cs" Inherits="Web_ConsultorioMedico.PacientesLista" %>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView CssClass="table table-striped table-bordered color-scheme-light " ID="gvListadoPaciente" runat="server"
        AllowPaging="true" PageSize="5" DataKeyNames="Id" AutoGenerateColumns="false"
        OnSelectedIndexChanged="gvListadoPaciente_SelectedIndexChanged"
        OnPageIndexChanging="gvListadoPaciente_PageIndexChanging"
        OnRowDeleting="gvListadoPaciente_RowDeleting">
        <Columns>
            <asp:BoundField DataField="NombreCompleto" HeaderText="Pacientes" HeaderStyle-CssClass="text-center"
                ItemStyle-CssClass="text-center align-middle" />
            <asp:BoundField DataField="Dni" HeaderText="DNI" HeaderStyle-CssClass="text-center"
                ItemStyle-CssClass="text-center align-middle" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" HeaderStyle-CssClass="text-center"
                ItemStyle-CssClass="text-center align-middle" />
            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center"
                ItemStyle-CssClass="text-center align-middle" />
            <asp:BoundField DataField="ObraSocial.Nombre" HeaderText="ObraSocial"
                ItemStyle-CssClass="text-center align-middle" HeaderStyle-CssClass="text-center" />
            <asp:CommandField HeaderText="" ShowSelectButton="true" SelectText="Modificar"
                HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center align-middle"
                ControlStyle-CssClass="btn btn-ouline-danger btn-sm fw-bold" />
            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-CssClass="text-center"
                ItemStyle-CssClass="text-center align-middle">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminar" runat="server"
                        CommandName="Delete"
                        Text="X"
                        CssClass="btn btn-outline-danger btn-sm fw-bold"
                        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este paciente?');" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <div class="row">
        <div class="col-12" style="margin-block-start: 20px">
            <a href="Pacientes.aspx" class="btn-primary" style="background-color:darkblue">Agregar</a>
        </div>
    </div>

    <div style="margin-block:20px">
        <asp:Button runat="server" ID="btnMostrar" Text="Ver Inactivos" CssClass="btn-primary" BackColor="Blue" OnClick="btnMostrar_Click"/>
    </div>
    
    <div>
        <asp:GridView CssClass="table table-striped table-bordered color-scheme-light " ID="gvPacientesInactivos" runat="server"
            AllowPaging="true" PageSize="5" DataKeyNames="Id" AutoGenerateColumns="false"
            OnSelectedIndexChanged="gvPacientesInactivos_SelectedIndexChanged"
            OnPageIndexChanging="gvPacientesInactivos_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="NombreCompleto" HeaderText="Pacientes" HeaderStyle-CssClass="text-center"
                    ItemStyle-CssClass="text-center align-middle" />
                <asp:BoundField DataField="Dni" HeaderText="DNI" HeaderStyle-CssClass="text-center"
                    ItemStyle-CssClass="text-center align-middle" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" HeaderStyle-CssClass="text-center"
                    ItemStyle-CssClass="text-center align-middle" />
                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass="text-center"
                    ItemStyle-CssClass="text-center align-middle" />
                <asp:BoundField DataField="ObraSocial.Nombre" HeaderText="ObraSocial"
                    ItemStyle-CssClass="text-center align-middle" HeaderStyle-CssClass="text-center" />
                <asp:CommandField HeaderText="" ShowSelectButton="true" SelectText="Reactivar" 
                    HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center align-middle"
                    ControlStyle-CssClass="btn btn-ouline-danger btn-sm fw-bold" />

            </Columns>
        </asp:GridView>
    </div>


</asp:Content>
