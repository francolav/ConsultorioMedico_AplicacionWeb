<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionMedicos.aspx.cs" Inherits="Web_ConsultorioMedico.GestionMedicos" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <h1 class="text-center">Gestión de Médicos</h1>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="GvMedicos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered"
                    AllowPaging="true" PageSize="10" DataKeyNames="Id"
                    OnSelectedIndexChanged="GvMedicos_SelectedIndexChanged"
                    OnRowDeleting="GvMedicos_RowDeleting"
                    OnPageIndexChanging="GvMedicos_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                        <asp:BoundField DataField="nombreCompleto" HeaderText="Nombre Completo" />
                        <asp:BoundField DataField="Dni" HeaderText="DNI" />
                        <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
                        <asp:TemplateField HeaderText="Especialidades">
                            <ItemTemplate>
                                <%#  Eval("IdEspecialidades") != null ? string.Join(", ", (System.Collections.Generic.List<int>)Eval("IdEspecialidades")) : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Select" Text="Modificar" CssClass="btn btn-primary btn-sm me-1" />
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" Text="Eliminar" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Está seguro de que desea eliminar este médico?');" />
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
