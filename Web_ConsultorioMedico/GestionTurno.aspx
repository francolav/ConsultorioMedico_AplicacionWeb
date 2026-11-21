<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="GestionTurno.aspx.cs" Inherits="Web_ConsultorioMedico.AltaTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container text-center ">
            <h1>Gestion de Turnos</h1>
        </div>
        <div class="container ">
            <div class="row ">

                <div class="col">

                    <div >

                        <asp:Label ID="LbDNI" runat="server" Text="DNI Paciente:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TxDniPaciente" runat="server" CssClass="form-control" OnTextChanged="TxDniPaciente_TextChange" AutoPostBack="true"></asp:TextBox>

                    </div>

                    <div >


                        <asp:Label ID="LbNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxNombre" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>

                    <div >

                        <asp:Label ID="LbTelefono" runat="server" Text="Teléfono:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxTelefono" runat="server" CssClass="form-control"></asp:TextBox>


                    </div>
                    <div >

                        <asp:Label ID="LbEmail" runat="server" Text="Email:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxEmail" runat="server" CssClass="form-control"></asp:TextBox>


                    </div>


                    <div class="row g-3 align-items-center" style="margin-bottom: 20px;">

                        <div class="col">
                            <asp:Label ID="Label1" runat="server" Text="Especialidad:" CssClass="form-label"></asp:Label>

                            <asp:DropDownList ID="DdlEspecialidad" runat="server" CssClass="form-control"
                                OnSelectedIndexChanged="DdlEspecialidad_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Value="">Seleccione.....</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col">

                            <asp:Label ID="LbMedico" runat="server" Text="Médico:" CssClass="form-label"></asp:Label>

                            <asp:DropDownList ID="DdlMedico" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>

                <div class="col">
                    <div ">

                        <asp:Label ID="LbApellido" runat="server" Text="Apellido:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxApellido" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>

                    <div >

                        <asp:Label ID="LbFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxFechaNacimiento" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>

                    <div >

                        <asp:Label ID="LbTelefonoAlternativo" runat="server" Text="Teléfono Alternativo:" CssClass="form-label"></asp:Label>

                        <asp:TextBox ID="TxTelefonoAlternativo" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>

                    <div >

                        <asp:Label ID="LbObraSocial" runat="server" Text="Obra Social:" CssClass="form-label"></asp:Label>

                        <asp:DropDownList ID="DdlObraSocial" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                            <asp:ListItem Value="">Seleccione....</asp:ListItem>
                        </asp:DropDownList>

                    </div>

                    <div >

                        <asp:Label ID="LbFechaTurno" runat="server" Text="Fecha del Turno:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TxSeleccionarFecha" Text="Elige una fecha" runat="server" CssClass="form-control w-12" data-bs-toggle="datepicker" autocomplete="off" placeholder="Choose date…"></asp:TextBox>

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
            <asp:Label ID="prueba" runat="server" Text="Label"></asp:Label>
        </div>
</asp:Content>
