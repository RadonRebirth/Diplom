<%@ Page Title="Emploeers Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Baidge.aspx.cs" Inherits="TestASPProject.Baidge" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        $(document).on("click", "[data-target='#updateEmployeeModal']", function () {
            var employeeId = $(this).data("employee-id");
            $("#<%= hdnEmployeeId.ClientID %>").val(employeeId);
        });

    </script>

    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>
        <div style="margin-left:470px">
          <p>Ваши сотрудники</p>
      </div>
        <div style="margin-top: 20px; margin-bottom: 20px">
            <button style="margin-left: 450px; width: 200px" type="button" class="btn btn-success" data-toggle="modal" data-target="#addEmployeeModal">Добавить сотрудника</button>
        </div>
        <div>
            <button style="margin-left: 450px; width: 200px" type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteEmployeeModal">Удалить сотрудника</button>
        </div>

        <!-- Модальное окно добавления сотрудника -->

        <div id="addEmployeeModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Создание сотрудника</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -400px; width: fit-content; display: flex">
                            <div>
                                <label for="txtFullName">Полное имя:</label>
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtAbout">Описание:</label>
                                <div style="margin-left: 420px">
                                    <asp:TextBox ID="txtAbout" runat="server" TextMode="MultiLine" CssClass="form-control" Width="500px" MaxLength="50" Style="resize: none;"></asp:TextBox>
                                </div>

                                <label for="fileUpload">Изображение:</label>
                                <div style="margin-bottom: 20px">
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file"></asp:FileUpload>
                                </div>
                                <div style="margin-left: 420px">
                                    <asp:Button ID="CreateEmploeeButton" runat="server" Text="Создать сотрудника" CssClass="btn btn-primary" OnClick="btnGenerateEmploee_Click"></asp:Button>
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

        <!-- Модальное окно удаления сотрудника -->
        <div id="deleteEmployeeModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Удаление сотрудника</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -250px; width: fit-content; display: flex">
                            <div>
                                <div style="margin-left: 55px">
                                    <label for="txtFullName">ID сотрудника:</label>
                                </div>

                                <asp:TextBox ID="txtIDemployee" runat="server" CssClass="form-control"></asp:TextBox>
                                <div style="margin-top: 10px; margin-left: 450px; margin-bottom: 20px">
                                    <asp:Button ID="DeleteEmploeeButton" runat="server" Text="Удалить сотрудника" CssClass="btn btn-danger" OnClick="DeleteEmploee_Click"></asp:Button>
                                </div>

                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Закрыть</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!-- Модальное окно изменения сотрудника -->

        <div id="updateEmployeeModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Изменение сотрудника</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -400px; width: fit-content; display: flex">
                            <div>
                                <input type="hidden" id="hdnEmployeeId" runat="server"/>
                                <label for="txtUpdFullName">Полное имя:</label>
                                <asp:TextBox ID="TextBoxUpd" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtUpdAbout">Описание:</label>
                                <div style="margin-left: 420px">
                                    <asp:TextBox ID="TextBoxUpd2" runat="server" TextMode="MultiLine" CssClass="form-control" Width="500px" MaxLength="50" Style="resize: none;"></asp:TextBox>
                                </div>

                                <label for="fileUpdUpload">Изображение:</label>
                                <div style="margin-bottom: 20px">
                                    <asp:FileUpload ID="fileUpdUpload" runat="server" CssClass="form-control-file"></asp:FileUpload>
                                </div>
                                <div style="margin-left: 420px">
                                    <asp:Button ID="ButtonUpd" runat="server" Text="Обновить данные" CssClass="btn btn-primary" OnClick="btnUpdateEmploee_Click"></asp:Button>
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


        <div id="Div" runat="server" style="display: flex; flex-wrap: wrap;"> </div>

    </div>
    <div class="row"></div>

</asp:Content>
