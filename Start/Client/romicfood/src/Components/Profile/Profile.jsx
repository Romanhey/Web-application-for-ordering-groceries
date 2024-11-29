import React from "react";
import "./profile.css";

export const Profile = () => {
    return (
        <div className="profile-page">
            <div className="profile-header">
                <h1>Профиль</h1>
                <button className="edit-button">Редактировать</button>
            </div>

            <div className="profile-content">
                <div className="profile-photo">
                    <img
                        src="https://via.placeholder.com/150"
                        alt="Профиль"
                        className="profile-img"
                    />
                </div>

                <div className="profile-info">
                    <p>
                        <strong>Полное имя:</strong> Иван Иванов
                    </p>
                    <p>
                        <strong>Email:</strong> ivan.ivanov@email.com
                    </p>
                    <p>
                        <strong>Адрес:</strong> ул. Пушкина, д. Колотушкина
                    </p>
                    <p>
                        <strong>Логин:</strong> ivan123
                    </p>
                </div>
            </div>
        </div>
    );
};
