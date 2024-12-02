import React, { useState } from 'react';
import './header.css';
import { NavLink } from "react-router-dom";
import { FiShoppingCart, FiTruck } from "react-icons/fi"; // Добавляем иконку для логотипа
import { TbTruckDelivery } from "react-icons/tb";

function Header({ isAuth }) {
    const [menuOpen, setMenuOpen] = useState(false);

    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
    };

    return (
        <header>
            <nav>
                <div className="nav_logo">
                    <TbTruckDelivery /> {/* Используем иконку FiTruck для логотипа */}
                </div>
                <div className="navigation">
                <div className="nav_search">
                    <input type="text" placeholder="Search"/>
                    <img src={`${process.env.PUBLIC_URL}/img/icons-reach.svg`} alt="search icon"/>
                </div>
                    <div className={`nav_buttons ${menuOpen ? 'open' : ''}`}>
                        <a href="">Сообщество</a>
                        <a href="">Ресурсы</a>
                        <a href="">Контакты</a>
                    </div>
                    <a href="" id="cart_button" className="cart-icon">
                        <FiShoppingCart/>
                    </a>
                    <div className={`nav_buttons 0{menuOpen ? 'open' : ''}`}>
                        <div className="nav_profile">
                            <NavLink
                                to="/profile"
                                id="profile_button"
                                className={isAuth ? "active" : ""}
                            >
                                Профиль
                            </NavLink>
                            <NavLink
                                to="/auth"
                                id="sign_in_button"
                                className={!isAuth ? "active" : ""}
                            >
                                Войти
                            </NavLink>
                        </div>
                    </div>
                </div>

                <div className="burger-menu" onClick={toggleMenu}>
                    <div className="burger-icon"></div>
                    <div className="burger-icon"></div>
                    <div className="burger-icon"></div>
                </div>
            </nav>
            <div className="header_content">
                <div className="header_content_text">
                    <h1>Абсолютно бесплатная еда</h1>
                    <h3>бесплатная еда каждый час</h3>
                    <a href="">Меню</a>
                </div>
            </div>
        </header>
    );
}

export default Header;
