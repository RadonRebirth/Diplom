<%@ Page Title="Profil Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="TestASPProject.Profil" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>
        <div style="margin-left:470px">
            <p >Ваш профиль</p>
        </div>
    <div style='margin-left: 217px;width: 700px;background-color: #DCDCDC;border-radius: 10px;padding: 10px 10px 10px 10px;align-items: center;'>
        <div style="display:flex; align-items:flex-end; justify-content:center">
            <div>
                 <asp:Image ID="imgProfile" runat="server" CssClass="img2" Style="width: 150px; height: 150px; border-radius: 100%;" />
            </div>
            <div>
                <button style="border-radius:100%" type="button"  data-toggle="modal" data-target="#updateImageeModal"><img style="width:15px;" src="/Images/upd.svg" /></button>
            </div>
        </div>
        <div style="display:flex; align-items:flex-end; justify-content:center">
            <div>
               <h2><asp:Label ID="lblFullName" runat="server" /></h2>
            </div>
               <div>
                   <button style="border-radius:100%" type="button" data-toggle="modal" data-target="#updateNameModal"><img style="width:15px;" src="/Images/upd.svg" /></button>
               </div>
        </div>
        <div style="border-top: 1px solid #C0C0C0;">
                    <div style="margin-left:250px; ">
            <h3 style="margin-left: 30px; ">Обо мне:</h3>
        </div>
        </div>

        <div style="display:flex; justify-content:center">
           
            <div style="margin-right:10px; ">
                 <span><asp:Label ID="lblAbout" runat="server" /></span>
            </div>
               <div>
                   <button style="border-radius:100%" type="button"  data-toggle="modal" data-target="#updateAboutModal"><img style="width:15px;" src="/Images/upd.svg" /></button>
               </div>
        </div>
        </div>
    </div>

    <!-- Модальное окно изменения изображения -->

    <div id="updateImageeModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3>Изменение Изображения</h3>
                </div>
                <div class="modal-body">
                    <div style="margin-left: -400px; width: fit-content; display: flex">
                        <div>
                            <label for="fileUpdUpload">Изображение:</label>
                            <div style="margin-bottom: 20px">
                                <asp:FileUpload ID="fileUpdUpload" runat="server" CssClass="form-control-file"></asp:FileUpload>
                            </div>
                            <div style="margin-left: 420px">
                                <asp:Button ID="ButtonUpdImage" runat="server" Text="Обновить изображение" CssClass="btn btn-primary" OnClick="btnUpdateImage_Click"></asp:Button>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>

        <!-- Модальное окно изменения имени -->

    <div id="updateNameModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3>Изменение Имени</h3>
                </div>
                <div class="modal-body">
                    <div style="margin-left: -400px; width: fit-content; display: flex">
                        <div>
                            <label for="txtFullName">Полное имя:</label>
                            <div style="margin-bottom: 10px">
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div style="margin-left: 420px">
                                <asp:Button ID="ButtonUpdName" runat="server" Text="Обновить имя" CssClass="btn btn-primary" OnClick="btnUpdateName_Click"></asp:Button>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>

        <!-- Модальное окно изменения описания о себе -->

    <div id="updateAboutModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3>Изменение описания о себе</h3>
                </div>
                <div class="modal-body">
                    <div style="margin-left: -400px; width: fit-content; display: flex">
                        <div>
                            <label for="txtAbout">Напишите о себе:</label>
                            <div style="margin-left: 420px; margin-bottom:10px">
                                <asp:TextBox ID="txtAbout" runat="server" TextMode="MultiLine" CssClass="form-control" Width="500px" MaxLength="50" Style="resize: none;"></asp:TextBox>
                            </div>
                            <div style="margin-left: 420px">
                                <asp:Button ID="ButtonUpdAbout" runat="server" Text="Обновить описание" CssClass="btn btn-primary" OnClick="btnUpdateAbout_Click"></asp:Button>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
    </div>

</asp:Content>