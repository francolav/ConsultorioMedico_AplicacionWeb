<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionObraSocial.aspx.cs" Inherits="Web_ConsultorioMedico.GestionObraSocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container text-center">
        <div class="row">
            <div class="col-md-12">

                <h2>Gestión de Obras Sociales</h2>
                <asp:GridView ID="GvObrasSociales" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered color-scheme-light">
                    <Columns>

                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClick="BtnEliminar_Click" CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
            <div class="col-md-12 mt-3 " style="margin-block:20px">
                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar Nueva Obra Social" OnClick="BtnAgregar_Click" CssClass=" btn-primary color-scheme-auto"  />
            </div>

            <%if (agregar)
                {  %>

            <div class="container">
                <div class="col">
                    <asp:Label ID="LblMensaje" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="BtnConfirmar" runat="server" Text="Confirmar" OnClick="BtnConfirmar_Click" CssClass="btn btn-primary mt-2" />
                    <asp:CheckBox ID="ChkConfirmacion" runat="server" Text="Confirma" CssClass="form-check-input mt-2" />
                </div>
            </div>

            <%} %>
        </div>
    </div>
</asp:Content>
