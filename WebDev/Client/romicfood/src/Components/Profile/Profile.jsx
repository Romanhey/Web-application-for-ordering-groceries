import React from "react";
import "./profile.css";
import {ENV} from "../../Share/share";
import {useNavigate} from "react-router-dom"; // Подключаем стили

export const Profile = ({user}) => {

    let history = useNavigate();
    let [orders, setOrders] = React.useState([]);

    if (user?.userId == null) {
        history("/auth");
    }

    React.useEffect( () => {
        if (user?.userId == null) {
            history("/auth");
        }
         getOrders();
    }, []);

    let getOrders = async () => {
        try {
            const response = await fetch(`${ENV.BASE_URL}/order/${user.userId}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });
            if (response.ok) {
                const data = await response.json();

                // Проверяем, что data является массивом. Если нет — преобразуем.
                if (Array.isArray(data)) {
                    setOrders(data);
                } else {
                    setOrders([data]); // Если это объект, оборачиваем его в массив
                }
            } else {
                console.log("Ошибка получения данных о заказах");
            }
        } catch (e) {
            console.log(e);
        }
    }

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
                    <span className="info-value">{user.fullName}</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Email:</span>
                    <span className="info-value">{user.email}</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Адрес:</span>
                    <span className="info-value">{user.address}</span>
                </div>
                <div className="info-row">
                    <span className="info-label">Логин:</span>
                    <span className="info-value">{user.login}</span>
                </div>
            </div>
            {/* Секция "Мои заказы" */}
            <div className="orders-section">
                <h2>Мои заказы</h2>
                <ul>
                    {orders != null && orders.map((order, index) => (
                        <li key={index}>
                            <div className="order-row">
                                <span className="order-label">Дата заказа:</span>
                                <span className="order-value">{
                                    new Date(order.orderDate).toLocaleDateString()
                                }</span>
                            </div>
                            <div className="order-row">
                                <span className="order-label">Статус:</span>
                                <span className="order-value">{order.status}</span>
                            </div>
                            <div className="order-row">
                                <span className="order-label">Сумма:</span>
                                <span className="order-value">{order.totalPrice} ₽</span>
                            </div>
                            <div className="order-row">
                                <span className="order-label">Товары:</span>
                                <span className="order-value">
                                    {order.orderProducts.map((product, index) => (
                                        <span key={index}>{product.productName}</span>
                                    ))}
                                </span>
                            </div>
                        </li>
                    )
                    )}
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
