<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionTurno.aspx.cs" Inherits="Web_ConsultorioMedico.AltaTurno" %>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container ">
        <div class="row ">

            <div class="col">

                <div>

                    <asp:Label ID="LbDNI" runat="server" Text="DNI Paciente:"></asp:Label>
                    <asp:TextBox ID="TxDniPaciente" runat="server" CssClass="form-control color-scheme-light" OnTextChanged="TxDniPaciente_TextChange" AutoPostBack="true"></asp:TextBox>

                </div>

                <div>


                    <asp:Label ID="LbNombre" runat="server" Text="Nombre:"></asp:Label>

                    <asp:TextBox ID="TxNombre" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefono" runat="server" Text="Teléfono:"></asp:Label>

                    <asp:TextBox ID="TxTelefono" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>


                </div>
                <div>

                    <asp:Label ID="LbEmail" runat="server" Text="Email:"></asp:Label>

                    <asp:TextBox ID="TxEmail" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>


                </div>
                <asp:ScriptManager runat="server">
                </asp:ScriptManager>
                <!--  <asp:UpdatePanel runat="server">
                    <ContentTemplate> -->
                <div class="row g-3 align-items-center" style="margin-bottom: 20px;">

                    <div class="col">
                        <asp:Label ID="Label1" runat="server" Text="Especialidad:"></asp:Label>

                        <asp:DropDownList ID="DdlEspecialidad" runat="server" CssClass="form-control color-scheme-light"
                            OnSelectedIndexChanged="DdlEspecialidad_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Seleccione.....</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col">

                        <asp:Label ID="LbMedico" runat="server" Text="Médico:"></asp:Label>

                        <asp:DropDownList ID="DdlMedico" runat="server" CssClass="form-control color-scheme-light"
                            OnSelectedIndexChanged="DdlMedico_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="">Seleccione.....</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>

                <!--      </ContentTemplate>
                </asp:UpdatePanel> -->

            </div>

            <div class="col">
                <div>

                    <asp:Label ID="LbApellido" runat="server" Text="Apellido:"></asp:Label>

                    <asp:TextBox ID="TxApellido" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbFechaNacimiento" runat="server" Text="Fecha de Nacimiento:"></asp:Label>

                    <asp:TextBox ID="TxFechaNacimiento" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbTelefonoAlternativo" runat="server" Text="Teléfono Alternativo:"></asp:Label>

                    <asp:TextBox ID="TxTelefonoAlternativo" runat="server" CssClass="form-control color-scheme-light"></asp:TextBox>

                </div>

                <div>

                    <asp:Label ID="LbObraSocial" runat="server" Text="Obra Social:"></asp:Label>

                    <asp:DropDownList ID="DdlObraSocial" runat="server" CssClass="form-control color-scheme-light" AppendDataBoundItems="true">
                        <asp:ListItem Value="">Seleccione....</asp:ListItem>
                    </asp:DropDownList>

                </div>

                <div>

                    <asp:Label ID="LbFechaTurno" runat="server" Text="Fecha del Turno:"></asp:Label>
                    <asp:TextBox ID="TxSeleccionarFecha" placeholder="Elige una fecha" runat="server"
                        CssClass="form-control color-scheme-light"
                        autocomplete="off"></asp:TextBox>
                    <asp:HiddenField ID="hfDiasHabilitados" runat="server" />

                </div>

            </div>
        </div>

        <div class="container align-items-center">
            <div class="row g-3 align-items-center" style="margin-bottom: 20px;">
                <div class="col">

                    <asp:Button ID="BtnVolver" runat="server" Text="Volver" CssClass="theme-primary btn-solid btn-styled" OnClick="BtnVolver_Click" />

                    <asp:Button ID="BtnGuardarTurno" runat="server" Text="Guardar Turno" CssClass="theme-primary btn-solid btn-styled" />

                </div>
            </div>
        </div>

    </div>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr/dist/l10n/es.js"></script>

    <script>
function initCalendario() {
    var hf = document.getElementById('<%= hfDiasHabilitados.ClientID %>');
    var input = document.getElementById('<%= TxSeleccionarFecha.ClientID %>');
    var diasHabilitados = [];
    try { diasHabilitados = hf.value ? JSON.parse(hf.value) : []; } catch(e) { diasHabilitados = []; }

    if (input._flatpickr) input._flatpickr.destroy();

    if (!diasHabilitados || diasHabilitados.length === 0) {
        input.value = "";
        input.placeholder = "El médico no tiene agenda activa";
        input.disabled = true;
        return;
    }

    input.disabled = false;
    input.placeholder = "Elige una fecha";

    flatpickr(input, {
        locale: 'es',
        dateFormat: 'd/m/Y',
        minDate: 'today',
        disable: [ function(date) { return !diasHabilitados.includes(date.getDay()); } ]
    });
}
Sys.Application.add_load(function () { initCalendario(); });
</script>
</asp:Content>
