<h1> Требования:</h1>
<ol>
    <li>Создайте новый проект ASP.NET Core Web API с использованием языка C# и .NET Core.</li>
    <li>Создайте модель данных для пользователя (User) с атрибутами Id, Name, Age, Email и связанной сущностью "Роль" (Role).</li>
    <li>
     Создайте контроллер (UserController) с методами для выполнения следующих операций:
        <ul>
            <li>Получение списка всех пользователей(обязательно реализовать пагинацию, сортировку и фильтрацию по каждому атрибуту в модели User и в модели Role).</li>
            <li>Получение пользователя по Id и всех его ролей.</li>
            <li>Добавление новой роли пользователю.</li>
            <li>Создание нового пользователя.</li>
            <li>Обновление информации о пользователе по Id.</li>
            <li>Удаление пользователя по Id.</li>
        </ul>
    </li>
    <li>
       Добавьте в контроллер бизнес-логику для валидации данных:
        <ul>
            <li>Проверка наличия обязательных полей (Имя, Возраст, Email).</li>
            <li>Проверка уникальности Email.</li>
            <li>Проверка возраста на положительное число.</li>
        </ul>
    </li>
    <li>Используйте Entity Framework Core (или любой другой ORM по вашему выбору) для доступа к данным и сохранения пользователей и ролей в базе данных.</li>
    <li>Создайте миграцию использую ORM для создания необходимой таблицы в базе данных. </li>
    <li>Добавьте обработку ошибок и возврат соответствующих статусных кодов HTTP (например, 404 при отсутствии пользователя).</li>
    <li>Документируйте ваш API с использованием инструментов Swagger или подобных.</li>
    <li>
       Дополнительные задания (по желанию):
        <ul>
            <li>Настройте логирование действий в API (например, с использованием Serilog).</li>
        </ul>
    </li>
</ol>
