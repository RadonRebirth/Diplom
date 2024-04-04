<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TestASPProject.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>2FactorAuth
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="https://www.youtube.com/watch?v=dQw4w9WgXcQ&amp;ab_channel=RickAstley">🤣</asp:HyperLink>
        </h1>
        <div style="text-align:center;">
            <h2>Добро пожаловать на наш сайт для учета сотрудников!</h2>
            <br />
            <span style="font-size:16px">Наше веб-приложение обеспечивает безопасность и удобство при учете сотрудников с двухфакторной аутентификацией и генерацией бейджиков.</span>
            <br />
        </div>

        <div style="margin-top:40px; display:flex">
            <div style="
                width:fit-content;
                height:250px;
                background: linear-gradient(0deg, #eeaeca 0%, #94bbe9 100%);
                border-radius: 10px;
                border: 1px white solid;
                ">
                <div style="float:left; width:75%; color:white;">
                    <h3 style="margin-left:10px;">Наши особенности:</h3>
                    <ul style="font-size:15px">
                        <li><strong>Безопасность:</strong> Мы используем двухфакторную аутентификацию для защиты данных.</li>
                        <li><strong>Простота использования:</strong> Наш интерфейс удобен и интуитивно понятен.</li>
                        <li><strong>Генерация бейджиков:</strong> Мы автоматически генерируем бейджики для всех сотрудников.</li>
                        <li><strong>Отчетность и аналитика:</strong> Мы предоставляем детальные отчеты о входах пользователей.</li>
                    </ul>
                </div>
            </div>

            <!-- Изображение с эффектом анимации -->
            <div style="margin-left: 150px; margin-top:30px">
                <img id="animatedImage" src="https://cdn.icon-icons.com/icons2/1622/PNG/512/3741738-assurance-bussiness-ecommerce-marketplace-onlinestore-store_108896.png" alt="Security Badge" style="max-width:100%; height:180px; animation: bounce 5s infinite;">
            </div>
        </div>
    </div>

    <style>
        @keyframes bounce {
            0% { transform: translateY(-20px); }
            50% { transform: translateY(20px); }
            100% { transform: translateY(-20px); }
        }
    </style>
</asp:Content>
