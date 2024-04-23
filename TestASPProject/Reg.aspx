<%@ Page Title="TestASP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="TestASPProject.Reg" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;pp=ygUO0YDQtdC60YDQvtC70Ls%3D" ToolTip="НЕ НАЖИМАЙ!!!!!!!!!!">🤣</asp:HyperLink>
        </h1>
        <div>
            <p class="lead">Введите логин</p>
            <asp:TextBox ID="txtUsername" runat="server" ToolTip="Введите свой логин" Width="175px" CssClass="form-control" Height="25px" Wrap="False"></asp:TextBox>
        </div>
        <div>
            <p class="lead">Введите пароль</p>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="177px" CssClass="form-control" Height="25px" ToolTip="Введите пароль"></asp:TextBox>
        </div>
        <div>
            <p class="lead">Подтвердите пароль</p>
            <asp:TextBox ID="txtPasswordVF" runat="server" TextMode="Password" Width="177px" CssClass="form-control" Height="25px" ToolTip="Повторно введите пароль"></asp:TextBox>
        </div>
        <div style="margin-top:20px; margin-left: 430px">
         <asp:Button ID="Button1" runat="server"  Text="Зарегистрироваться" CssClass="btn btn-success" OnClick="ButtonReg_Click" />
        </div>
        <div style="margin-left:430px; margin-top:20px; display:grid">
            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl="~/Auth.aspx">Уже зарегистрированы?</asp:HyperLink>
            <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>

    <div class="row">
    </div>

</asp:Content>
