import React from 'react';
import './header.css';
import {NavLink} from "react-router-dom";

function Header({isAuth}) {

    console.log(isAuth);

    return (
        <header>
            <nav>
                <div className="nav_logo">
                    <img src={`${process.env.PUBLIC_URL}/img/Group 5.svg`} alt="logo"/>
                </div>
                <div className="nav_search">
                    <input type="text" placeholder="Search"/>
                    <img src={`${process.env.PUBLIC_URL}/img/icons-reach.svg`} alt="search icon"/>
                </div>
                <div className="nav_buttons">
                    <a href="">Сообщество</a>
                    <a href="">Ресурсы</a>
                    <a href="">Контакты</a>
                    <a href="" id="cart_button">
                        <img src={`${process.env.PUBLIC_URL}/img/icons-cart.png`} alt="asd"/>
                    </a>
                    <div className="nav_profile">
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
            </nav>
            <div
                className="header_content"
            >
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