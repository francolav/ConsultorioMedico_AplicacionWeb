<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Interfaz_de_usuario.Login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar Sesión - SanaSana</title>

    <link href="Styles/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfRolSeleccionado" runat="server" Value="Administracion" />

        <div class="login-layout">
            <!-- Columna izquierda: Formulario -->
            <section class="login-panel">
                <div class="login-card">
                    <!-- Logo SanaSana -->
                    <div class="login-brand">
                        <div class="brand-logo-icon">
                            <svg viewBox="0 0 24 24" width="20" height="20" fill="none" stroke="currentColor" stroke-width="2.2">
                                <line x1="12" y1="5" x2="12" y2="19"></line>
                                <line x1="5" y1="12" x2="19" y2="12"></line>
                            </svg>
                        </div>
                        <div class="brand-text">
                            <span class="brand-title">SanaSana</span>
                            <span class="brand-subtitle">GESTIÓN CLÍNICA</span>
                        </div>
                    </div>

                    <!-- Encabezado de bienvenida -->
                    <div class="login-heading">
                        <h1>Bienvenido, por favor identifíquese</h1>
                        <p>Seleccione su categoría</p>
                    </div>

                    <!-- Selector de categoría (Administración / Médico) -->
                    <div class="role-selector" role="group" aria-label="Categoría de usuario">
                        <button type="button" class="role-btn active" data-role="Administracion">
                            Administración
                        </button>
                        <button type="button" class="role-btn" data-role="Medico">
                            Médico
                        </button>
                    </div>

                    <!-- Campo Usuario -->
                    <div class="input-group">
                        <span class="input-icon" aria-hidden="true">
                            <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" stroke-width="2">
                                <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path>
                                <circle cx="12" cy="7" r="4"></circle>
                            </svg>
                        </span>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-control" placeholder="Usuario"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblError" runat="server" CssClass="login-error" Visible="false" role="alert"></asp:Label>

                    <!-- Campo Contraseña -->
                    <div class="input-group">
                        <span class="input-icon" aria-hidden="true">
                            <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" stroke-width="2">
                                <rect x="3" y="11" width="18" height="11" rx="2" ry="2"></rect>
                                <path d="M7 11V7a5 5 0 0 1 10 0v4"></path>
                            </svg>
                        </span>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                    </div>

                    <!-- Botón principal Login -->
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn-login" OnClick="btnLogin_Click" />
                    <asp:LinkButton ID="btnMostrarPrimerAcceso" runat="server" CssClass="first-access-link" Text="¿Inicias sesión por primera vez?" CausesValidation="false" OnClick="btnMostrarPrimerAcceso_Click" />
                    <asp:LinkButton ID="btnMostrarRecuperarClave" runat="server" CssClass="first-access-link" Text="¿Olvidaste tu clave?" CausesValidation="false" OnClick="btnMostrarRecuperarClave_Click" />

                    <asp:Panel ID="pnlRecuperarClave" runat="server" CssClass="first-access-panel" Visible="false">
                        <h2>Restablecer contraseña</h2>
                        <p>Por seguridad, solicitá a Administración que restablezca tu cuenta desde la pantalla protegida de Usuarios. La clave temporal será <strong>1234</strong> y deberás cambiarla al ingresar.</p>
                    </asp:Panel>

                    <asp:Panel ID="pnlPrimerAcceso" runat="server" CssClass="first-access-panel" Visible="false">
                        <h2>Activá tu cuenta</h2>
                        <p>Ingresá tu usuario y contraseña temporal <strong>1234</strong> para crear una nueva contraseña.</p>
                        <asp:Label ID="lblPrimerAccesoMensaje" runat="server" CssClass="login-error" Visible="false" role="alert"></asp:Label>
                        <div class="input-group">
                            <asp:TextBox ID="txtPrimerAccesoUsuario" runat="server" CssClass="input-control" placeholder="Usuario" autocomplete="username" />
                        </div>
                        <div class="input-group">
                            <asp:TextBox ID="txtClaveTemporal" runat="server" CssClass="input-control" TextMode="Password" placeholder="Contraseña temporal" autocomplete="current-password" />
                        </div>
                        <div class="input-group">
                            <asp:TextBox ID="txtPrimerAccesoPassword" runat="server" CssClass="input-control" TextMode="Password" placeholder="Nueva contraseña" autocomplete="new-password" />
                        </div>
                        <div class="input-group">
                            <asp:TextBox ID="txtPrimerAccesoConfirmacion" runat="server" CssClass="input-control" TextMode="Password" placeholder="Confirmar nueva contraseña" autocomplete="new-password" />
                        </div>
                        <p class="password-hint">La nueva contraseña debe tener al menos 8 caracteres.</p>
                        <asp:Button ID="btnCompletarPrimerAcceso" runat="server" Text="Guardar nueva contraseña" CssClass="btn-login" OnClick="btnCompletarPrimerAcceso_Click" />
                    </asp:Panel>
                </div>
            </section>

            <!-- Columna derecha: Imagen lateral -->
            <aside class="login-visual" aria-hidden="true">
                <img src="Images/login-bg.jpg" alt="bg de login" />
            </aside>
        </div>
    </form>

    <script>
        (function () {
            const buttons = document.querySelectorAll('.role-btn');
            const hiddenRole = document.getElementById('<%= hfRolSeleccionado.ClientID %>');

            buttons.forEach(function (btn) {
                btn.addEventListener('click', function () {
                    buttons.forEach(function (b) { b.classList.remove('active'); });
                    btn.classList.add('active');
                    if (hiddenRole) {
                        hiddenRole.value = btn.getAttribute('data-role');
                    }
                });
            });
        })();
    </script>
</body>
</html>