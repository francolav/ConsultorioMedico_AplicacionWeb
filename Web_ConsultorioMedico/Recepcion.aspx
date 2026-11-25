<%@ Page Title="Recepcion" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Recepcion.aspx.cs" Inherits="Web_ConsultorioMedico.Recepcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <div class="col-md-6" align="center">
        <h1>Recepción</h1>
    </div>


    <div class="container text-center">
        <div class="row align-items-start">
            <div class="col col-md-6">
                <div>
                    <h3>Medicos de turno</h3>
                </div>

                <asp:GridView class="table  table-bordered color-scheme-light" ID="GvMedicosTurno" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Médico" />
                        <asp:TemplateField  HeaderText="Especialidades">
                            <ItemTemplate>
                                <%#  Eval("IdEspecialidades") != null ? string.Join(", ", (System.Collections.Generic.List<int>)Eval("IdEspecialidades")) : "" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="matricula" HeaderText="Matrícula" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col">
                <h3>Turnos en Proceso</h3>
                <div align="center" style="width: 300px; height: 200px; background-color: blueviolet;">
                    <h1>FALTA GRIDVIEW</h1>
                </div>
                <asp:GridView ID="GvTurnosEnProceso" runat="server"></asp:GridView>
            </div>
        </div>

        <div style="margin-top: 50px;">
            <h2>Gestion de Turnos
            </h2>
        </div>
    </div>
    <div class="row align-items-start" style="margin-top: 30px;">
        <div class="col">
        </div>
        <div class="col" align="center">

            <asp:Button ID="BtnNuevoTurno" runat="server" Text="Nuevo Turno" CssClass="theme-primary btn-solid btn-styled" OnClick="BtnNuevoTurno_Click" />
        </div>
        <div class="col" align="center">
            <asp:Button ID="BtnConsultarTurno" runat="server" Text="Consultar Turno" CssClass="theme-primary btn-solid btn-styled" OnClick="BtnConsultarTurnos_Click" />
        </div>
        <div class="col">
        </div>

    </div>
    <div style="margin-top: 50px; margin-block-end: 100px;">

        <asp:Panel ID="PnConsultarTurno" runat="server" CssClass="form-group">
            <div class="row align-items-start">
                <div class="col" align="right">
                    <label for="Buscar Por" class="theme-primary ">Buscar Por:</label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="DdlBuscarPor" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Apellido">Apellido</asp:ListItem>
                        <asp:ListItem Value="DNI">DNI</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <asp:TextBox ID="TxtBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col">
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="theme-primary btn-solid btn-styled" />
                </div>
            </div>
            <div class="container-text-center">
                <asp:GridView ID="GvTurnos" runat="server" CssClass="table table-striped table-bordered"></asp:GridView>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
