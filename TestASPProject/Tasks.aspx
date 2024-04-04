<%@ Page Title="Tasks Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TestASPProject.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <script>
            $(document).on("click", "[data-target='#updateTaskModal']", function () {
                var taskId = $(this).data("task-id");
                $("#<%= hdnTaskId.ClientID %>").val(taskId);
            });
            $(document).on("click", "[data-target='#selectEmplTodoById']", function () {
                var taskId = $(this).data("task-id");
                $("#<%= hdnTaskTodoId.ClientID %>").val(taskId);
            });
            $(document).on("click", "[data-target='#delEmplTask']", function () {
                var taskId = $(this).data("task-id");
                $("#<%= hdnDelEmplTaskId.ClientID %>").val(taskId);
             });

        </script>

    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>
        <div style="margin-left:470px">
            <p>Ваши задачи</p>
        </div>
        <div style="margin-top: 20px; margin-bottom: 20px">
            <button style="margin-left: 450px; width: 200px" type="button" class="btn btn-success" data-toggle="modal" data-target="#addTaskModal">Добавить Задачу</button>
        </div>
        <div>
            <button style="margin-left: 450px; width: 200px" type="button" class="btn btn-danger" data-toggle="modal" data-target="#deleteTaskModal">Удалить Задачу</button>
        </div>

        
        <div id="Div" runat="server" style="display: flex; flex-wrap: wrap;"> </div>


    </div>
    
        <!-- Модальное окно добавления задачи -->

        <div id="addTaskModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Создание задачи</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -400px; width: fit-content; display: flex">
                            <div>
                                <label for="txtFullName">Название задачи:</label>
                                <asp:TextBox ID="txtTaskName" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtTaskAbout">Описание:</label>
                                <div style="margin-left: 420px;margin-bottom:10px">
                                    <asp:TextBox ID="txtTaskAbout" runat="server" TextMode="MultiLine" CssClass="form-control" Width="500px" MaxLength="50" Style="resize: none;"></asp:TextBox>
                                </div>

                                <div style="margin-left: 420px">
                                    <asp:Button ID="CreateTaskButton" runat="server" Text="Создать задачу" CssClass="btn btn-primary" OnClick="btnGenerateTask_Click"></asp:Button>
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

        <!-- Модальное окно удаления задачи -->
        <div id="deleteTaskModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Удаление задачи</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -250px; width: fit-content; display: flex">
                            <div>
                                <div style="margin-left: 55px">
                                    <label for="txtIdTask">ID задачи:</label>
                                </div>
                                <asp:TextBox ID="txtIdTask" runat="server" CssClass="form-control"></asp:TextBox>
                                <div style="margin-top: 10px; margin-left: 450px; margin-bottom: 20px">
                                    <asp:Button ID="DeleteTask" runat="server" Text="Удалить задачу" CssClass="btn btn-danger" OnClick="DeleteTask_Click"></asp:Button>
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

        <!-- Модальное окно изменения задачи -->

        <div id="updateTaskModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Изменение Задачи</h3>
                    </div>
                    <div class="modal-body">
                        <div style="margin-left: -400px; width: fit-content; display: flex">
                            <div>
                                <input type="hidden" id="hdnTaskId" runat="server"/>
                                <label for="txtUpdFullName">Название задачи:</label>
                                <asp:TextBox ID="txtNameTaskUpd" runat="server" CssClass="form-control"></asp:TextBox>
                                <label for="txtUpdAbout">Описание:</label>
                                <div style="margin-left: 420px;margin-bottom:10px">
                                    <asp:TextBox ID="txtAboutTaskUpd" runat="server" TextMode="MultiLine" CssClass="form-control" Width="500px" MaxLength="50" Style="resize: none;"></asp:TextBox>
                                </div>
                                <div style="margin-left: 420px">
                                    <asp:Button ID="ButtonUpd" runat="server" Text="Обновить данные" CssClass="btn btn-primary" OnClick="btnUpdateTask_Click"></asp:Button>
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
            <!-- Модальное окно назначения сотрудника на задачу -->

        <div id="selectEmplTodoById" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h3>Назначение сотрудника на задачу</h3>
            </div>
            <div class="modal-body">
                <div style="margin-left: -250px; width: fit-content; display: flex">
                    <div>
                        <div style="margin-left: -70px">
                            <input type="hidden" id="hdnTaskTodoId" runat="server"/>
                            <label for="txtIdEmpl">Укажите ID сотрудника, которого хотите назначить:</label>
                        </div>
                        <asp:TextBox ID="txtIdEmpl" runat="server" CssClass="form-control"></asp:TextBox>
                        <div style="margin-top: 10px; margin-left: 450px; margin-bottom: 20px">
                            <asp:Button ID="btnSelectTodo" runat="server" Text="Назначить" CssClass="btn btn-success" OnClick="btnSelEmplTodoTask_Click"></asp:Button>
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

            <div id="delEmplTask" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h3>Потдверждаете снятие сотрудника с задачи?</h3>
            </div>
            <div class="modal-body">
                <div style="margin-left: -250px; width: fit-content; display: flex">
                    <div>
                        <div style="margin-left: -70px">
                            <input type="hidden" id="hdnDelEmplTaskId" runat="server"/>
                        </div>
                        <div style="margin-top: 10px; margin-left: 450px; margin-bottom: 20px">
                            <asp:Button ID="btnDelEmplTask" runat="server" Text="Снять с задачи" CssClass="btn btn-danger" OnClick="DeleteAssignedEmplId_Click"></asp:Button>
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
    <div class="row">
    </div>

</asp:Content>
