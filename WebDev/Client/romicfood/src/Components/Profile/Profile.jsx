import React from "react";
import "./profile.css"; // Подключаем стили

export const Profile = () => {
    return (
        <div className="profile-page">
            {/* Заголовок страницы */}
            <div className="profile-header">
                <h1>Профиль</h1>
                <button className="edit-button">Редактировать</button>
            </div>

            {/* Основная информация о профиле */}
            <div className="profile-main-info">
                <div className="info-row">
                    <span className="info-label">Полное имя:</span>
                    <span className="info-value">Иван Иванов</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Email:</span>
                    <span className="info-value">ivan.ivanov@email.com</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Адрес:</span>
                    <span className="info-value">ул. Пушкина, д. Колотушкина</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Логин:</span>
                    <span className="info-value">ivan123</span>
                </div>
            </div>
            {/* Секция "Мои заказы" */}
            <div className="orders-section">
                <h2>Мои заказы</h2>
                <ul>
                    <li>Пицца "Маргарита" - 12.11.2024</li>
                    <li>Чизкейк - 10.11.2024</li>
                    <li>Капучино - 08.11.2024</li>
                </ul>
            </div>

            {/* Кнопка выхода */}
            <div className="logout-section">
                <button className="logout-button">
                    Выйти из аккаунта
                </button>
            </div>
        </div>
    );
};
