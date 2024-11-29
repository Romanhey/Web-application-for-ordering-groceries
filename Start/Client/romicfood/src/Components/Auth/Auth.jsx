import React from 'react';
import './auth.css';
function Auth(props) {

    const [isRegister, setIsRegister] = React.useState(false);

    return (
        <div className="auth_page active">
            <div className="login_Form active">
                <h1>Вход</h1>
                <div className="login_form_inputs">
                    <label htmlFor="email">Почта</label>
                    <input type="text" id="email" placeholder="example@email.com"/>
                    <label htmlFor="password">Пароль</label>
                    <input type="password" id="password" placeholder="Password"/>
                </div>
                <div className="login_form_buttons">
                    <button id="login_submit">Войти</button>
                    <p>У вас нет аккаунта? <a id="register_sw_button">Зарегистрироваться</a></p>
                </div>
            </div>
            <div className="register_Form">
                <h1>Введите адрес электронной почты</h1>
                <div className="register_form_inputs inp1">
                    <label htmlFor="email">Почта</label>
                    <input type="text" id="register_email" placeholder="name@email.com"/>


                    <div className="inp2">
                        <label htmlFor="register_login">Логин</label>
                        <input type="text" id="register_login" placeholder="логин"/>
                        <label htmlFor="register_name">Имя</label>
                        <input type="text" id="register_name" placeholder="имя"/>
                        <label htmlFor="register_addres">Адрес</label>
                        <input type="text" id="register_addres" placeholder="адрес"/>
                        <label htmlFor="register_password">Пароль</label>
                        <input type="password" id="register_password" placeholder="пароль"/>
                        <label htmlFor="register_repeatpassword">Подтверждение пароля</label>
                        <input type="password" id="register_repeatpassword" placeholder="пароль"/>
                    </div>
                </div>
                <div className="register_form_buttons">
                    <button id="register_next">Далее</button>
                    <button id="register_button">Зарегистрироваться</button>
                    <p>Уже зарегистрированы? <a id="login_sw_button">Войти</a></p>
                </div>
            </div>

        </div>
    );
}

export default Auth;