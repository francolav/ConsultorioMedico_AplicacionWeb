<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PacientesLista.aspx.cs" Inherits="Web_ConsultorioMedico.PacientesLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" >
        <asp:GridView class="table table-striped table-bordered color-scheme-light " ID="gvListadoPaciente" runat="server" 
            AllowPaging="true" PageSize="5" DataKeyNames="Id" AutoGenerateColumns="false"
            OnPageIndexChanging="gvListadoPaciente_PageIndexChaning">
            <Columns>
                <asp:BoundField DataField="NombreCompleto" HeaderText="Pacientes" HeaderStyle-CssClass ="text-center" ItemStyle-CssClass="text-center align-middle"/>
                <asp:BoundField DataField="Dni" HeaderText="DNI" HeaderStyle-CssClass ="text-center" ItemStyle-CssClass="text-center align-middle"/>
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" HeaderStyle-CssClass ="text-center" ItemStyle-CssClass="text-center align-middle"/>
                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-CssClass ="text-center"
                        ItemStyle-CssClass="text-center align-middle"/>
                <asp:BoundField DataField="ObraSocial.Nombre" HeaderText="ObraSocial" ItemStyle-CssClass="text-center align-middle"
                        HeaderStyle-CssClass ="text-center"/>
                <asp:CommandField HeaderText="" ShowSelectButton="true" SelectText="Modificar" HeaderStyle-CssClass ="text-center" 
                        ItemStyle-CssClass="text-center align-middle" ControlStyle-CssClass="btn btn-ouline-danger btn-sm fw-bold"/>
                <asp:CommandField  HeaderText="Eliminar" ShowSelectButton="true" SelectText="X" HeaderStyle-CssClass ="text-center"
                        ItemStyle-CssClass="text-center align-middle" ControlStyle-CssClass="btn btn-ouline-danger btn-sm fw-bold"  />
                
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
