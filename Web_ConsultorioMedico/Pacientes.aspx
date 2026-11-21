<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="Web_ConsultorioMedico.Pacientes" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    

    <div class="container ">
        <div class="row ">

            <div class="col">

                <div>

                    <asp:Label ID="LbDNI" runat="server" Text="DNI Paciente:" ></asp:Label>
                    <asp:TextBox ID="TxDniPaciente" runat="server" CssClass="form-control color-scheme-light" OnTextChanged="TxDniPaciente_TextChange" AutoPostBack="true"></asp:TextBox>

                </div>

                <div>


                    <asp:Label ID="LbNombre" runat="server" Text="Nombre:" ></asp:Label>

                    <asp:TextBox ID="TxNombre" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefono" runat="server" Text="Teléfono:"></asp:Label>

                    <asp:TextBox ID="TxTelefono" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>


                </div>
                <div>

                    <asp:Label ID="LbEmail" runat="server" Text="Email:" ></asp:Label>

                    <asp:TextBox ID="TxEmail" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>


                </div>

            </div>

            <div class="col">
                <div>

                    <asp:Label ID="LbApellido" runat="server" Text="Apellido:" ></asp:Label>

                    <asp:TextBox ID="TxApellido" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" ></asp:Label>

                    <asp:TextBox ID="TxFechaNacimiento" runat="server" CssClass="form-control color-scheme-light" data-bs-toggle="datepicker " autocomplete="off"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefonoAlternativo" runat="server" Text="Teléfono Alternativo:" ></asp:Label>

                    <asp:TextBox ID="TxTelefonoAlternativo" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbObraSocial" runat="server" Text="Obra Social:" ></asp:Label>

                    <asp:DropDownList ID="DdlObraSocial" runat="server" CssClass="form-control color-scheme-light" AppendDataBoundItems="true">
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
                        <%if (ConfirmarEliminar)
                                { %>
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
