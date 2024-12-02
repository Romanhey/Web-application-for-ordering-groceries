import React from 'react';
import './auth.css';
import {ENV} from "../../Share/share";
function Auth(props) {

    const [isRegister, setIsRegister] = React.useState(false);
    const [isSecondRegisterPage, setIsSecondRegisterPage] = React.useState(false);

    // Login
    const [login, setLogin] = React.useState("");
    const [password, setPassword] = React.useState("");

    // Register

    const [registerEmail, setRegisterEmail] = React.useState("");
    const [registerLogin, setRegisterLogin] = React.useState("");
    const [registerName, setRegisterName] = React.useState("");
    const [registerAddres, setRegisterAddres] = React.useState("");
    const [registerPassword, setRegisterPassword] = React.useState("");
    const [registerRepeatPassword, setRegisterRepeatPassword] = React.useState("");


    let Login = async () => {


        try {
            await fetch(`${ENV.BASE_URL}/user/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    login: login,
                    password: password
                })
            }).then(res => res.json())
                .then(data => {
                    console.log(data);
                    if (data.error) {
                        alert(data.error);
                    }
                });
        } catch (e) {
            alert(e);
        }
    }


    let Register = async () => {

        if (registerPassword !== registerRepeatPassword) {
            alert("Пароли не совпадают");
            return
        }

        try {
            await fetch(`${ENV.BASE_URL}/auth/register`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    name: registerName,
                    email: registerEmail,
                    addres: registerAddres,
                    login: registerLogin,
                    password: registerPassword,
                })
            }).then(res => res.json())
                .then(data => {
                    if (data.error) {
                        alert(data.error);
                    } else {
                        alert("Успешно");
                    }
                });
        } catch (e) {
            alert(e);
        }
    }

    return (
        <div className="auth_page active">
            <div className={[`login_Form`, !isRegister ? "active" : ""].join(" ")}>
                <h1>Вход</h1>
                <div className="login_form_inputs">
                    <label htmlFor="email">Логин</label>
                    <input type="text" id="email" placeholder="логин"/>
                    <label htmlFor="password">Пароль</label>
                    <input type="password" id="password" placeholder="пароль"/>
                </div>
                <div className="login_form_buttons">
                    <button
                        id="login_submit"
                        onClick={() => Login()}
                    >Войти</button>
                    <p>У вас нет аккаунта? <a id="register_sw_button"
                        onClick={() => setIsRegister(true)}
                    >Зарегистрироваться</a></p>
                </div>
            </div>
            <div className={["register_Form", isRegister ? " active " : "", isSecondRegisterPage ? " next " : ""].join(" ")}>
                <h1>Введите адрес электронной почты</h1>
                <div className="register_form_inputs inp1" >
                    <label htmlFor="email">Почта</label>
                    <input
                        type="text"
                        id="register_email"
                        placeholder="name@email.com"
                        disabled={isSecondRegisterPage}
                    />


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
                    <button
                        id="register_next"
                        onClick={() => setIsSecondRegisterPage(true)}
                        >Далее</button>
                    <button
                        id="register_button"
                        onClick={() => Register()}
                    >Зарегистрироваться</button>
                    <p>Уже зарегистрированы? <a id="login_sw_button">Войти</a></p>
                </div>
            </div>

        </div>
    );
}

export default Auth;