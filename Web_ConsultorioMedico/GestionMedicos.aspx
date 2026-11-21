<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionMedicos.aspx.cs" Inherits="Web_ConsultorioMedico.GestionMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <h1 class="text-center">Gestión de Médicos</h1>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="GvMedicos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                        <asp:TemplateField HeaderText="Especialidades">
                            <ItemTemplate>
                                <%#  Eval("IdEspecialidades") != null ? string.Join(", ", (System.Collections.Generic.List<int>)Eval("IdEspecialidades")) : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' Text="Editar" CssClass="btn btn-primary btn-sm" />
                                <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Está seguro de que desea eliminar este médico?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-12 text-right">
                
                <asp:Button ID="btnAgregarMedico" runat="server" Text="Agregar Médico" CssClass="btn btn-success" OnClick="btnAgregarMedico_Click" />
            </div>
        </div>
    </div>
</asp:Content>
