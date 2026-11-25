<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Web_ConsultorioMedico.Usuarios" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <p>Al restablecer una cuenta activa, su contraseña temporal será <strong>1234</strong> y deberá cambiarla en el próximo inicio de sesión.</p>
        <asp:Label ID="lblResultado" runat="server" Visible="false" />
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" DataKeyNames="IdUsuario"
            CssClass="table table-striped table-bordered" EmptyDataText="No hay usuarios registrados."
            OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Usuario" />
                <asp:CheckBoxField DataField="Activo" HeaderText="Activo" ReadOnly="true" />
                <asp:CheckBoxField DataField="requiereCambioPassword" HeaderText="Debe cambiar clave" ReadOnly="true" />
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer contraseña" CssClass="btn btn-warning btn-sm"
                            CommandName="Restablecer" CommandArgument='<%# Eval("IdUsuario") %>'
                            Enabled='<%# Eval("Activo") %>'
                            OnClientClick="return confirm('¿Restablecer la contraseña de este usuario a 1234? Deberá cambiarla al iniciar sesión.');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
