<%@ Page Title="Auth Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Auth.aspx.cs" Inherits="TestASPProject.Auth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#emailModal').on('hidden.bs.modal', function () {
                $('#myModal').modal('show');
            });

            $('#qrModal').on('hidden.bs.modal', function () {
                $('#myModal').modal('show');
            });
        });

        function openModal() {
            $('#myModal').modal('show'); // используем jQuery для открытия модального окна
        }

        function hideModal() {
            $('#myModal').modal('hide'); // используем jQuery для скрытия модального окна
        }

    </script>



    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>

        <p class="lead">Введите логин</p>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Height="25px" Width="175px"></asp:TextBox>

        <p class="lead">Введите пароль</p>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Height="25px" TextMode="Password" Width="175px"></asp:TextBox>


        <div style="margin-top: 10px; margin-left: 435px">
            <button type="button" class="btn btn-success" data-toggle="modal" data-target="#myModal">Войти в аккаунт</button>
        </div>
        <div style="margin-left:370px; margin-top:10px">
            <asp:Label ID="errorTxt" runat="server"></asp:Label>
        </div>
       

    </div>

    <div class="row">
    </div>

    <!-- Основное модальное окно -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Выберите способ аутентификации</h4>
                </div>
                <div class="modal-body">
                   <div style="margin-left: -40%; margin-bottom: 20px">
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#emailModal" style="width: 200px; margin-left:420px">Email авторизация</button>
                </div>
                    <div style="margin-left: -40%">
                        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#qrModal" style="width: 200px; margin-left:420px">QR авторизация</button>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Модальное окно для QR авторизации -->
    <div id="qrModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">QR авторизация</h4>
                </div>
                <div class="modal-body">
                    <div style="margin-left: 32%; margin-bottom: 30px;">
                        <asp:Image ID="QRImage" runat="server" Height="234px" ImageAlign="Middle" Width="246px" ImageUrl="https://cdn.icon-icons.com/icons2/2248/PNG/512/qrcode_scan_icon_136286.png" />
                    </div>
                    <div style="margin-left:210px; margin-bottom:10px">
                        <asp:Button ID="Button1" runat="server" Text="Сгенерировать QR код" CssClass="btn btn-success" OnClick="BtnGenerQR_Click" Width="200px" type="button" UseSubmitBehavior="false" />

                    </div>
                    <div><p class="lead">Введите код</p></div>
                    <div style="margin-left: -35%; margin-bottom: 10px">
                        <asp:TextBox ID="txtSecretCode" runat="server" CssClass="form-control" Height="25px" Width="175px"></asp:TextBox>
                    </div>
                    <div style="margin-top: 10px; margin-left: 210px">
                        <asp:Button ID="BtnLoginFromModal" runat="server" Text="Войти в аккаунт" CssClass="btn btn-success" OnClick="BtnLogin_Click" Width="200px" type="button" UseSubmitBehavior="false" />

                    </div>

                    <div style="text-align: center; margin-top: 10px">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>

     <!-- Модальное окно для Email авторизации -->
 <div id="emailModal" class="modal fade" role="dialog">
     <div class="modal-dialog">
         <div class="modal-content">
             <div class="modal-header">
                 <button type="button" class="close" data-dismiss="modal">&times;</button>
                 <h4 class="modal-title">Email авторизация</h4>
             </div>
             <div class="modal-body">
                 <p class="lead">Введите почту</p>
                 <div style="margin-left: -35%; margin-bottom: 10px">
                     <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Height="25px" Width="175px"></asp:TextBox>
                 </div>
                 <div style="margin-top: 10px; margin-left: 210px">
                     <asp:Button ID="Button2" runat="server" Text="Отправить код" CssClass="btn btn-success" OnClick="BtnSendCode_Click" Width="200px" type="button" UseSubmitBehavior="false" />

                 </div>
                 <p class="lead">Введите код</p>
                 <div style="margin-left: -35%; margin-bottom: 10px">
                     <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Height="25px" Width="175px"></asp:TextBox>
                 </div>
                 <div style="margin-top: 10px; margin-left: 210px">
                     <asp:Button ID="Button3" runat="server" Text="Войти в аккаунт" CssClass="btn btn-success" OnClick="BtnLogin_Click" Width="200px" type="button" UseSubmitBehavior="false" />

                 </div>

                 <div style="text-align: center; margin-top: 10px">
                     <asp:Label ID="Label2" runat="server"></asp:Label>
                 </div>
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
             </div>
         </div>
     </div>
 </div>

</asp:Content>
