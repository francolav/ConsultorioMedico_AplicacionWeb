<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="Interfaz_de_usuario.CambiarPassword" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Crear contraseña - SanaSana</title>
    <link href="Styles/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-layout">
            <section class="login-panel">
                <div class="login-card">
                    <div class="login-brand">
                        <div class="brand-logo-icon" aria-hidden="true">+</div>
                        <div class="brand-text">
                            <span class="brand-title">SanaSana</span>
                            <span class="brand-subtitle">GESTIÓN CLÍNICA</span>
                        </div>
                    </div>
                    <div class="login-heading">
                        <h1>Creá una nueva contraseña</h1>
                        <p>Por seguridad, reemplazá tu contraseña temporal para continuar.</p>
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="login-error" Visible="false" role="alert"></asp:Label>
                    <div class="input-group">
                        <asp:TextBox ID="txtNuevaPassword" runat="server" CssClass="input-control" TextMode="Password" placeholder="Nueva contraseña" autocomplete="new-password" />
                    </div>
                    <div class="input-group">
                        <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="input-control" TextMode="Password" placeholder="Confirmar contraseña" autocomplete="new-password" />
                    </div>
                    <p class="password-hint">Usá al menos 8 caracteres.</p>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar contraseña" CssClass="btn-login" OnClick="btnGuardar_Click" />
                </div>
            </section>
            <aside class="login-visual" aria-hidden="true">
                <img src="Images/login-bg.jpg" alt="" />
            </aside>
        </div>
    </form>
</body>
</html>
