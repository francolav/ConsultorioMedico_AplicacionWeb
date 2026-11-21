<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaMedicos.aspx.cs" Inherits="Web_ConsultorioMedico.AltaMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container text-center ">
        <h1>Gestion de Médicos</h1>
    </div>

    <div class="container ">
        <div class="row ">

            <div class="col">

                <div>

                    <asp:Label ID="LbDNI" runat="server" Text="DNI Medico:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TxDniMedico" runat="server" CssClass="form-control" OnTextChanged="TxDniMedico_TextChange" AutoPostBack="true"></asp:TextBox>

                </div>

                <div>


                    <asp:Label ID="LbNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefono" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxTelefono" runat="server" CssClass="form-control"></asp:TextBox>


                </div>
                <div>

                    <asp:Label ID="LbEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxEmail" runat="server" CssClass="form-control"></asp:TextBox>


                </div>

            </div>

            <div class="col">
                <div>

                    <asp:Label ID="LbApellido" runat="server" Text="Apellido:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxApellido" runat="server" CssClass="form-control"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbMatricula" runat="server" Text="Matrícula:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxMatricula" runat="server" CssClass="form-control"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefonoAlternativo" runat="server" Text="Teléfono Alternativo:" CssClass="form-label"></asp:Label>

                    <asp:TextBox ID="TxTelefonoAlternativo" runat="server" CssClass="form-control"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbEspecialidad" runat="server" Text="Especialidad:" CssClass="form-label"></asp:Label>

                    <asp:DropDownList ID="DdlEspecialidad" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Seleccione....</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
        </div>
    </div>
    <div class="container align-items-center" style="margin-block: 30px;">
        <div class="row g-3 align-items-center" style="margin-bottom: 30px;">
            <div class="col">

                <asp:Button ID="BtnVolver" runat="server" Text="Volver" CssClass="theme-primary btn-solid btn-styled" OnClick="BtnVolver_Click" />

                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="theme-primary btn-solid btn-styled" OnClick="BtnGuardar_Click" />


            </div>

            <div class="col">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <div class="col">
                            <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar Paciente" CssClass="theme-primary btn-solid btn-styled" BackColor="DarkRed" OnClick="BtnEliminar_Click" />
                        </div>
                        <%if (ConfirmarEliminarMedico)
                            {  %>
                        <div class="col">
                            <asp:CheckBox ID="CheckBoxConfirmarEliminar" runat="server" Text="Confirmar Eliminacion" AutoPostBack="true" OnCheckedChanged="CheckBoxConfirmarEliminar_CheckedChanged" />


                            <asp:Button ID="BtnConfirmarEliminar" runat="server" Text="Confirma Eliminacion" CssClass="theme-primary btn-solid btn-styled" BackColor="DarkRed" OnClick="BtnConfirmarEliminar_Click" />
                        </div>
                       <%} %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
